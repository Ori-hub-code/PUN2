using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PhotonManager photonManager;

    [Header ("# Object")]
    public TMP_InputField userIDInput;
    public TMP_InputField textInput;

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
