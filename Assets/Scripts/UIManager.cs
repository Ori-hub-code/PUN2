using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    // ΩÃ±€≈Ê
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
