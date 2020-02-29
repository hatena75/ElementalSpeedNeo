using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine;

// MonoBehaviourではなくMonoBehaviourPunCallbacksを継承して、Photonのコールバックを受け取れるようにする
public class Connection : MonoBehaviourPunCallbacks
{
    private StatusFeedBack sfb;

    private void Connect(string gameVersion)
    {
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    private void JoinLobby()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    //ランダムな部屋に入室する
    public void JoinRandomRoom()
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    private void TryLoadGameScene(){
        if(PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            Debug.Log("GameStart!");
            sfb.StatusMatch();

            PhotonNetwork.IsMessageQueueRunning = false;
            SceneManager.LoadScene("CardPvP");
        }
    }

    private void Start() {
        // PhotonServerSettingsに設定した内容を使ってマスターサーバーへ接続する
        Connect("1.0"); //バージョン指定
    }

    // Photonに接続した時
    public override void OnConnected()
    {
        Debug.Log("OnConnected");

        sfb = GameObject.Find("Status").GetComponent<StatusFeedBack>();
        sfb.StatusConnected();
 
        // ニックネームを付ける
        //SetMyNickName("Knohhoso");
    }

    // Photonから切断された時
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
    }
 
 
    // マスターサーバーに接続した時
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
 
        // ロビーに入る
        JoinLobby();
    }
 
 
    // ロビーに入った時
    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");

        JoinRandomRoom();
    }
 
 
    // ロビーから出た時
    public override void OnLeftLobby()
    {
        Debug.Log("OnLeftLobby");
    }
 
 
    // 部屋を作成した時
    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }
 
 
    // 部屋の作成に失敗した時
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed");
    }
 
 
    // 部屋に入室した時
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        sfb.StatusWaiting();

        TryLoadGameScene();
    }
 
    // 部屋から退室した時
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }
 
 
    // 他のプレイヤーが入室してきた時
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom");

        TryLoadGameScene();
    }
 
 
    // 他のプレイヤーが退室した時
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom");
    }

    // ランダムな部屋への入室に失敗した時
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed");
        //名前適当な2人部屋を新たに作成
        PhotonNetwork.CreateRoom(null ,new RoomOptions{MaxPlayers = 2} ,null);
    }
}
