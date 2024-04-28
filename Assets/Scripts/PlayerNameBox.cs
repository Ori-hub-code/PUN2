using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameBox : MonoBehaviour
{
    UIManager uiManager;
    public PhotonView pv;

    TextMeshProUGUI playerNameText;

    private void Awake()
    {
        uiManager = UIManager.instance;
        pv = GetComponent<PhotonView>();
        playerNameText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        transform.SetParent(uiManager.playerNameParent);

        if(pv.IsMine)
        {
            pv.RPC("SetPlayerBox", RpcTarget.AllBuffered, uiManager.photonManager.userName);
        }
    }

    [PunRPC]
    void SetPlayerBox(string name)
    {
        playerNameText.text = name;
        uiManager.photonManager.playerList.Add(this.gameObject);
    }
}
