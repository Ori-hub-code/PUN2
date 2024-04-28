using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBox : MonoBehaviour
{
    UIManager uiManager;
    public PhotonView pv;
    public TextMeshProUGUI chatText;

    private void Awake()
    {
        uiManager = UIManager.instance;
        pv = GetComponent<PhotonView>();
    }

    private void OnEnable()
    {
        if(pv.IsMine)
        {
            transform.SetParent(uiManager.textBoxParent);
        }
    }
}
