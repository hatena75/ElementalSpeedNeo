using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine;
using ExitGames.Client.Photon;
using System;
using System.Reflection;

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

            SendMyCharacter();

            //PhotonNetwork.IsMessageQueueRunning = false;
            //SceneManager.LoadScene("CardPvP");
        }
    }

    private void Start() {
        // PhotonServerSettingsに設定した内容を使ってマスターサーバーへ接続する
        PhotonNetwork.OfflineMode = !SceneManagerTitle.IsVs;
        Connect("1.0"); //バージョン指定
    }

    // Photonに接続した時
    public override void OnConnected()
    {
        Debug.Log("OnConnected");

        sfb = GameObject.Find("Status").GetComponent<StatusFeedBack>();
        sfb.StatusConnected();

        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
 
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

    ///////////////////////以下、RaiseEvent///////////////////
    /*
    public void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public void OnDisable()
    {
        //PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }
    */
    

    private enum EEventType : byte
    {
        SendMyCharacter = 1
    }

    public void OnEvent(EventData photonEvent)
    {
        var eventCode = (EEventType)photonEvent.Code;

        switch( eventCode )
        {
            case EEventType.SendMyCharacter:
                //CustomDataから送られたデータを取り出し
                string data = (string)photonEvent.CustomData;
                //dataの文字列の型(キャラ)のインスタンスをnew
                //リフレクションを用いて汎用的にする
                Type t = Type.GetType(data);
                //昔の引数有りのAddComponentと区別するため、引数の数を指定してメソッドを得る
                MethodInfo mi = typeof(GameObject).GetMethod(
                    "AddComponent",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                    null,
                    new Type[0], // ここに引数の型を配列で指定
                    null
                    );
                MethodInfo bound = mi.MakeGenericMethod(t);
                Debug.Log(bound);
                SceneManagerCharacterSelect.EnemyCharacter = (CharacterAbstract)bound.Invoke(gameObject, null);
                
                PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
                
                PhotonNetwork.IsMessageQueueRunning = false;
                SceneManager.LoadScene("CardPvP");
                break;
            default:
                break;
        }
    }

    public void SendMyCharacter()
    {
        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent( (byte)EEventType.SendMyCharacter, SceneManagerCharacterSelect.UsingCharacter.Name, raiseEventOptions, SendOptions.SendReliable);
        Debug.Log("sendcharacter");
    }

}
