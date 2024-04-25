using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // ����
    private readonly string version = "1.0f";

    [Header("# Prefab")]
    public GameObject playerPrefab;
    PhotonView pv;

    [Header("# Player")]
    public string masterName = "Test"; // ����� �̸�
    public PhotonView myPlayer;

    private void Awake()
    {
        // ���� ���� �����鿡�� �ڵ����� ���� �ε�
        PhotonNetwork.AutomaticallySyncScene = true;

        // ���� ������ �������� ���� ���
        PhotonNetwork.GameVersion = version;

        // ���� ���̵� �Ҵ�
        PhotonNetwork.NickName = masterName;

        // ���� ������ ��� Ƚ�� ���� -> �⺻���� �ʴ� 30ȸ
        Debug.Log(PhotonNetwork.SendRate);

        // ���� ����
        PhotonNetwork.ConnectUsingSettings();
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

        PhotonNetwork.JoinRandomRoom(); // ���� ��ġ����ŷ ��� ����
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

        // ĳ���� ����
        //PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint, Quaternion.identity, 0);
    }

    // ���� Lobby ���� �� ����Ǵ� �ݹ��Լ�
    public override void OnLeftLobby()
    {
        pv.RPC("LeftPhoton", RpcTarget.All, myPlayer.ViewID);
    }
}
