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
    public TextMeshProUGUI chatText; // 텍스트
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
        transform.SetParent(uiManager.textBoxParent); // 부모 설정

        if(pv.IsMine)
        {
<<<<<<< HEAD
            verticalLayoutGroup.childAlignment = TextAnchor.UpperRight;
            chatBackImage.color = Color.yellow;
            chatBackImage.sprite = uiManager.rightChat;
=======
            verticalLayoutGroup.childAlignment = TextAnchor.UpperRight; // 본인 채팅은 오른쪽에 배치
            chatBackImage.color = Color.yellow; // 본인 채팅은 노란색
>>>>>>> 5f81e5cb2a60cb40cf28ea8f596a89605fcdb527

            pv.RPC("SetText", RpcTarget.AllBuffered, uiManager.textInput.text); // 본인 데이터로 텍스트 적용

            uiManager.textInput.text = ""; // input field 초기화
        } 
        else
        {
<<<<<<< HEAD
            verticalLayoutGroup.childAlignment = TextAnchor.UpperLeft;
            chatBackImage.color = Color.gray;
            chatBackImage.sprite = uiManager.leftChat;
=======
            verticalLayoutGroup.childAlignment = TextAnchor.UpperLeft; // 상대 채팅은 왼쪽에 배치
            chatBackImage.color = Color.gray; // 상대 채팅 배경은 회색
>>>>>>> 5f81e5cb2a60cb40cf28ea8f596a89605fcdb527
        }

        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    [PunRPC]
    void SetText(string text)
    {
        this.chatText.text = ""; // 초기화

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
