using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // ����
    private readonly string version = "1.0f";

    public UIManager uiManager;
    PhotonView pv;

    [Header("# Prefab")]
    public GameObject playerPrefab;
    public GameObject chatBoxPrefab;

    [Header("# Player")]
    public string userName;
    public List<GameObject> playerList;

    private void Awake()
    {
        uiManager = UIManager.instance;
        pv = GetComponent<PhotonView>();
        playerList = new List<GameObject>();
    }

    // ���� ������ ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}"); // �κ� ���� ���� true , false ��� -> ���� ���̶� false
        // $"" => string.Format() �� ���ΰ� -> ""�ȿ� �ִ� ������ ���ڿ��� ��ȯ��. 

        PhotonNetwork.JoinLobby(); // �κ� ����
    }

    // �κ� ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}"); // �κ� ���� ���� true , false ��� -> ���� �Ķ� true

        //PhotonNetwork.JoinRandomRoom(); // ���� ��ġ����ŷ ��� ����

        // ���� �Ӽ� ����
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;              // �ִ� ������ �� : 20��
        roomOptions.IsOpen = true;                // ���� ���� ����
        roomOptions.IsVisible = true;             // �κ񿡼� �� ��Ͽ� ���� ��ų�� ����
        roomOptions.CleanupCacheOnLeave = false;  // ���� ������ �����ص� ������ ������ ������Ʈ�� �ڵ����� ���� ����.

        PhotonNetwork.JoinOrCreateRoom("2001565", roomOptions, null);
    }

    // ������ �� ������ �������� ��� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Failed {returnCode}:{message}");

        // ���� �Ӽ� ����
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;              // �ִ� ������ �� : 20��
        roomOptions.IsOpen = true;                // ���� ���� ����
        roomOptions.IsVisible = true;             // �κ񿡼� �� ��Ͽ� ���� ��ų�� ����
        roomOptions.CleanupCacheOnLeave = false;  // ���� ������ �����ص� ������ ������ ������Ʈ�� �ڵ����� ���� ����.

        // �� ����
        PhotonNetwork.CreateRoom("My Room", roomOptions);
    }

    // �� ������ �Ϸ�� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    // �뿡 ������ �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}");
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        // �뿡 ������ ����� ���� Ȯ��
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName},{player.Value.ActorNumber}"); // �̸��� ������ ���
        }

        // �÷��̾� �̸� ���� ����
        PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity, 0);
    }

    // �÷��̾ �������� �� ����Ǵ� �Լ�
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName} ���� �����ϼ̽��ϴ�.");
    }

    // �÷��̾ �������� �� ����Ǵ� �Լ�
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName} ���� �����ϼ̽��ϴ�.");

        pv.RPC("DeleteMyData", RpcTarget.AllBuffered, otherPlayer.ActorNumber);
    }




    // # �ΰ��� ��ư

    // ä�� ���� ��ư
    public void JoinChatButton()
    {
        if (uiManager.userIDInput.text.IsNullOrEmpty())
        {
            Debug.Log("���̵� �Է����ּ���");
        }
        else
        {
            // ȭ�� ��ȯ
            uiManager.loginCanvas.SetActive(false);
            uiManager.chattingCanvas.SetActive(true);

            // ���� ���� �����鿡�� �ڵ����� ���� �ε�
            PhotonNetwork.AutomaticallySyncScene = true;

            // ���� ������ �������� ���� ���
            PhotonNetwork.GameVersion = version;

            // ���� ���̵� �Ҵ�
            userName = uiManager.userIDInput.text;
            uiManager.userIDInput.text = ""; // �ʱ�ȭ
            PhotonNetwork.NickName = userName;

            // ���� ������ ��� Ƚ�� ���� -> �⺻���� �ʴ� 30ȸ
            Debug.Log(PhotonNetwork.SendRate);

            // ���� ����
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // �޼��� ������
    public void SendMyText()
    {
        if(!uiManager.textInput.text.IsNullOrEmpty())
        {
            PhotonNetwork.Instantiate(chatBoxPrefab.name, transform.position, Quaternion.identity, 0);

            uiManager.scrollbar.value = 0; // ä�� ���� �� scroll bar �� content �� �Ʒ��� ��ġ
        } 
        else
        {
            Debug.Log("�ؽ�Ʈ�� �Է����ּ���.");
        }
    }


    // ���� �� ���� ������ ����
    // RoomOtion ���� �����Ͱ� �ڵ� �����Ǵ� �� �������Ƿ�, PlayerNameBox �� ���� ����
    [PunRPC]
    void DeleteMyData(int actorNum)
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            PhotonView playerPV = playerList[i].GetComponent<PhotonView>();

            if (playerPV.ViewID / 1000 == actorNum)
            {
                Destroy(playerList[i].gameObject);
                playerList.Remove(playerList[i].gameObject);
            }
        }
    }
}
