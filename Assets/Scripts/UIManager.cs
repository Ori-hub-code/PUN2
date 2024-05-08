using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using System.Drawing;

public class UIManager : MonoBehaviour
{
    public PhotonManager photonManager;

    [Header("# Object")]
<<<<<<< HEAD
    public GameObject loginCanvas;
    public GameObject chattingCanvas;
    public TMP_InputField userIDInput;
    public TMP_InputField textInput;
    public Transform textBoxParent;
    public Transform playerNameParent;
    public GameObject infoBox;
    public GameObject infoBackGround;
    public Scrollbar scrollbar;
=======
    public GameObject loginCanvas; // 로그인 창
    public GameObject chattingCanvas; // 채팅 창
    public TMP_InputField userIDInput; // 이름 입력 inputField
    public TMP_InputField textInput; // 채팅 입력 inputField
    public Transform textBoxParent; // 생성된 채팅의 부모(Content)
    public Transform playerNameParent; // 생성된 PlayerName 의 부모(Content)
    public GameObject infoBox; // 오른쪽 정보창
    public Scrollbar scrollbar; // 채팅창 스크롤 바
>>>>>>> 5f81e5cb2a60cb40cf28ea8f596a89605fcdb527

    public Sprite leftChat;
    public Sprite rightChat;

    // 싱글톤
    public static UIManager instance;

    private void Awake()
    {
        instance = this;

        loginCanvas.SetActive(true);
        chattingCanvas.SetActive(false);
        infoBackGround.SetActive(false);
    }

    // 게임 종료 버튼
    public void ExitGameButton()
    {
        Application.Quit();
    }

    // 채팅 정보 열람 버튼
    public void InfoButton()
    {
        RectTransform rectTransform = infoBox.GetComponent<RectTransform>();

<<<<<<< HEAD
        Vector2 openAnchorMin = new Vector2(0.3f, 0);
=======
        // Anchor 초기화
        Vector2 openAnchorMin = new Vector2(0.7f, 0);
>>>>>>> 5f81e5cb2a60cb40cf28ea8f596a89605fcdb527
        Vector2 openAnchorMax = new Vector2(1, 1);
        Vector2 closeAnchorMin = new Vector2(1, 0);
        Vector2 closeAnchorMax = new Vector2(1.7f, 1);

        // 닫혀있을 때
        if(rectTransform.anchorMin == closeAnchorMin && rectTransform.anchorMax == closeAnchorMax)
        {
            StartCoroutine(SmoothCoroutine(rectTransform, closeAnchorMin, closeAnchorMax, openAnchorMin, openAnchorMax, 0.25f));
        } 
        else // 열려있을 때
        {
            StartCoroutine(SmoothCoroutine(rectTransform, openAnchorMin, openAnchorMax, closeAnchorMin, closeAnchorMax, 0.25f));
        }
    }

    IEnumerator SmoothCoroutine(RectTransform obj, Vector2 currentMin, Vector2 currentMax, Vector2 nextMin, Vector2 nextMax, float time)
    {
        Vector3 velocity = Vector3.zero;

        obj.anchorMin = currentMin;
        obj.anchorMax = currentMax;

        float offset = 0.01f;

        if(nextMin.x < currentMin.x)
        {
            while (nextMin.x + offset <= obj.anchorMin.x && nextMax.x + offset <= obj.anchorMax.x)
            {
                obj.anchorMin
                    = Vector3.SmoothDamp(obj.anchorMin, nextMin, ref velocity, time);

                obj.anchorMax
                    = Vector3.SmoothDamp(obj.anchorMax, nextMax, ref velocity, time);

                yield return null;
            }
            infoBackGround.SetActive(true);
        }
        else
        {
            while (nextMin.x - offset >= obj.anchorMin.x && nextMax.x - offset >= obj.anchorMax.x)
            {
                obj.anchorMin
                    = Vector3.SmoothDamp(obj.anchorMin, nextMin, ref velocity, time);

                obj.anchorMax
                    = Vector3.SmoothDamp(obj.anchorMax, nextMax, ref velocity, time);

                yield return null;
            }
            infoBackGround.SetActive(false);
        }

        obj.anchorMin = nextMin;
        obj.anchorMax = nextMax;

        yield return null;
    }
}
