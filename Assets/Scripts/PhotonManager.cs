using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    // 버전
    private readonly string version = "1.0f";

    public UIManager uiManager;

    [Header("# Prefab")]
    public GameObject playerPrefab;
    PhotonView pv;

    [Header("# Player")]
    public string userID;
    public PhotonView myPlayer;

    private void Awake()
    {
        uiManager = UIManager.instance;
    }

    // 포톤 서버에 접속 후 호출되는 콜백 함수
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}"); // 로비 접속 여부 true , false 출력 -> 접속 전이라서 false
        // $"" => string.Format() 를 줄인것 -> ""안에 있는 내용을 문자열로 반환함. 

        PhotonNetwork.JoinLobby(); // 로비 입장
    }

    // 로비에 접속 후 호출되는 콜백 함수
    public override void OnJoinedLobby()
    {
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}"); // 로비 접속 여부 true , false 출력 -> 접속 후라서 true

        PhotonNetwork.JoinRandomRoom(); // 랜덤 매치메이킹 기능 제공
    }

    // 랜덤한 룸 입장이 실패했을 경우 호출되는 콜백 함수
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log($"JoinRandom Failed {returnCode}:{message}");

        // 룸의 속성 정의
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;              // 최대 접속자 수 : 20명
        roomOptions.IsOpen = true;                // 룸의 오픈 여부
        roomOptions.IsVisible = true;             // 로비에서 룸 목록에 노출 시킬지 여부

        // 룸 생성
        PhotonNetwork.CreateRoom("My Room", roomOptions);
    }

    // 룸 생성이 완료된 후 호출되는 콜백 함수
    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room");
        Debug.Log($"Room Name = {PhotonNetwork.CurrentRoom.Name}");
    }

    // 룸에 입장한 후 호출되는 콜백 함수
    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}");
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        // 룸에 접속한 사용자 정보 확인
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName},{player.Value.ActorNumber}"); // 이름과 고유값 출력
        }

        // 캐릭터 생성
        //PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint, Quaternion.identity, 0);
    }

    // 플레이어가 입장했을 때 실행되는 함수
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"{newPlayer.NickName} 님이 입장하셨습니다.");
    }

    // 플레이어가 퇴장했을 때 실행되는 함수
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"{otherPlayer.NickName} 님이 퇴장하셨습니다.");
    }




    // # 인게임 버튼

    // 채팅 참여 버튼
    public void JoinChatButton()
    {
        if(uiManager.userIDInput.text.IsNullOrEmpty())
        {
            Debug.Log("아이디를 입력해주세요");
        } 
        else
        {
            // 같은 룸의 유저들에게 자동으로 씬을 로딩
            PhotonNetwork.AutomaticallySyncScene = true;

            // 같은 버전의 유저끼리 접속 허용
            PhotonNetwork.GameVersion = version;

            // 유저 아이디 할당
            userID = uiManager.userIDInput.text;
            uiManager.userIDInput.text = "";
            PhotonNetwork.NickName = userID;

            // 포톤 서버와 통신 횟수 설정 -> 기본값은 초당 30회
            Debug.Log(PhotonNetwork.SendRate);

            // 서버 접속
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}
