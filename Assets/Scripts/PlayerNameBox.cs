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
            pv.RPC("SetPlayerBox", RpcTarget.AllBuffered, uiManager.photonManager.userName);
            backGroundImg.color = Color.yellow;
        }
        else
        {
            backGroundImg.color = Color.gray;
        }
    }

    [PunRPC]
    void SetPlayerBox(string name)
    {
        playerNameText.text = name;
        uiManager.photonManager.playerList.Add(this.gameObject);
    }
}
