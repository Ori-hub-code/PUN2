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
    public GameObject loginCanvas;
    public GameObject chattingCanvas;
    public TMP_InputField userIDInput;
    public TMP_InputField textInput;
    public Transform textBoxParent;
    public Transform playerNameParent;
    public GameObject infoBox;

    // 싱글톤
    public static UIManager instance;

    private void Awake()
    {
        instance = this;

        loginCanvas.SetActive(true);
        chattingCanvas.SetActive(false);
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

        Vector2 openAnchorMin = new Vector2(0.7f, 0);
        Vector2 openAnchorMax = new Vector2(1, 1);
        Vector2 closeAnchorMin = new Vector2(1, 0);
        Vector2 closeAnchorMax = new Vector2(1.3f, 1);

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
        }



        obj.anchorMin = nextMin;
        obj.anchorMax = nextMax;

        yield return null;
    }
}
