using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatBox : MonoBehaviour
{
    UIManager uiManager;
    public PhotonView pv;
    Image img; // 배경 이미지
    public TextMeshProUGUI chatText; // 텍스트

    private void Awake()
    {
        uiManager = UIManager.instance;
        pv = GetComponent<PhotonView>();
        img = GetComponent<Image>();
    }

    private void OnEnable()
    {
        transform.SetParent(uiManager.textBoxParent);

        if (pv.IsMine)
        {
            pv.RPC("SetText", RpcTarget.AllBuffered, uiManager.textInput.text);
            uiManager.textInput.text = ""; // input field 초기화
            img.color = Color.yellow;
        }
    }

    [PunRPC]
    void SetText(string text)
    {
        chatText.text = ""; // 초기화

        // 줄바꿈
        char[] textWords = text.ToCharArray();

        for(int i = 0; i < textWords.Length; i++)
        {
            if(i % 20 == 0 && i != 0)
            {
                chatText.text += System.Environment.NewLine + textWords[i];
            } 
            else
            {
                chatText.text += textWords[i];
            }
        }
    }
}
