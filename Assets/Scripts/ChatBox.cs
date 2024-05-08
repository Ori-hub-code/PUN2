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
        transform.SetParent(uiManager.textBoxParent); // �θ� ����

        if(pv.IsMine)
        {
<<<<<<< HEAD
            verticalLayoutGroup.childAlignment = TextAnchor.UpperRight;
            chatBackImage.color = Color.yellow;
            chatBackImage.sprite = uiManager.rightChat;
=======
            verticalLayoutGroup.childAlignment = TextAnchor.UpperRight; // ���� ä���� �����ʿ� ��ġ
            chatBackImage.color = Color.yellow; // ���� ä���� �����
>>>>>>> 5f81e5cb2a60cb40cf28ea8f596a89605fcdb527

            pv.RPC("SetText", RpcTarget.AllBuffered, uiManager.textInput.text); // ���� �����ͷ� �ؽ�Ʈ ����

            uiManager.textInput.text = ""; // input field �ʱ�ȭ
        } 
        else
        {
<<<<<<< HEAD
            verticalLayoutGroup.childAlignment = TextAnchor.UpperLeft;
            chatBackImage.color = Color.gray;
            chatBackImage.sprite = uiManager.leftChat;
=======
            verticalLayoutGroup.childAlignment = TextAnchor.UpperLeft; // ��� ä���� ���ʿ� ��ġ
            chatBackImage.color = Color.gray; // ��� ä�� ����� ȸ��
>>>>>>> 5f81e5cb2a60cb40cf28ea8f596a89605fcdb527
        }

        this.gameObject.transform.localScale = new Vector3(1, 1, 1);
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
