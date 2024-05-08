using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameBox : MonoBehaviour
{
    UIManager uiManager;
    public PhotonView pv;

    public TextMeshProUGUI playerNameText;
    Image backGroundImg;

    private void Awake()
    {
        uiManager = UIManager.instance;
        pv = GetComponent<PhotonView>();

        backGroundImg = GetComponent<Image>();
    }

    private void OnEnable()
    {
        transform.SetParent(uiManager.playerNameParent);

        if(pv.IsMine)
        {
            pv.RPC("SetPlayerBox", RpcTarget.AllBuffered, uiManager.photonManager.userName); // 본인 데이터로 텍스트 설정
            backGroundImg.color = Color.yellow;
        }
        else
        {
            backGroundImg.color = Color.gray;
        }

        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    [PunRPC]
    void SetPlayerBox(string name)
    {
        uiManager.photonManager.playerList.Add(this.gameObject);

        // 줄바꿈
        char[] textWords = name.ToCharArray();

        for (int i = 0; i < textWords.Length; i++)
        {
            if (i % 15 == 0 && i != 0)
            {
                playerNameText.text += System.Environment.NewLine + textWords[i];
            }
            else
            {
                playerNameText.text += textWords[i];
            }
        }
    }
}
