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

    // My Text Box
    public GameObject myTextBox;
    public TextMeshProUGUI MyChatText;


    // Other Text Box
    public GameObject otherTextBox;
    public TextMeshProUGUI OtherChatText;

    private void Awake()
    {
        uiManager = UIManager.instance;
        pv = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        transform.SetParent(uiManager.textBoxParent);

        if (pv.IsMine)
        {
            pv.RPC("SetText", RpcTarget.AllBuffered, uiManager.textInput.text);
            uiManager.textInput.text = ""; // input field 초기화
        } 
    }

    [PunRPC]
    void SetText(string text)
    {
        MyChatText.text = ""; // 초기화

        // 줄바꿈
        char[] textWords = text.ToCharArray();

        for(int i = 0; i < textWords.Length; i++)
        {
            if(i % 20 == 0 && i != 0)
            {
                MyChatText.text += System.Environment.NewLine + textWords[i];
            } 
            else
            {
                MyChatText.text += textWords[i];
            }
        }
    }
}
