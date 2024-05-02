using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChatBox : MonoBehaviour
{
    UIManager uiManager;
    public PhotonView pv;
    public GameObject realChatBox;
    public TextMeshProUGUI chatText; // �ؽ�Ʈ
    VerticalLayoutGroup verticalLayoutGroup;
    Image chatBackImage;

    private void Awake()
    {
        uiManager = UIManager.instance;
        pv = GetComponent<PhotonView>();
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();

        chatBackImage = realChatBox.GetComponent<Image>();
    }

    private void OnEnable()
    {
        transform.SetParent(uiManager.textBoxParent);

        if(pv.IsMine)
        {
            verticalLayoutGroup.childAlignment = TextAnchor.UpperRight;
            chatBackImage.color = Color.yellow;

            pv.RPC("SetText", RpcTarget.AllBuffered, uiManager.textInput.text);
        } 
        else
        {
            verticalLayoutGroup.childAlignment = TextAnchor.UpperLeft;
            chatBackImage.color = Color.gray;
        }

        //pv.RPC("SetText", RpcTarget.AllBuffered, uiManager.textInput.text);
        uiManager.textInput.text = ""; // input field �ʱ�ȭ
    }

    [PunRPC]
    void SetText(string text)
    {
        this.chatText.text = ""; // �ʱ�ȭ

        // �ٹٲ�
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
