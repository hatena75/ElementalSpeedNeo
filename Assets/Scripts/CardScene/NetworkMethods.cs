using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkMethods : MonoBehaviourPunCallbacks
{
    //カードの初期配置。自分と相手でそれぞれ手札を生成。
    //マスター側はフィールドも生成する。
    private Vector3 scale = new Vector3(0.817f, 0.817f, 0.817f);
    private Vector3 eh = new Vector3(2.34f, 0.1f, 0.817f);
    private Vector3 ph = new Vector3(1.88f, -2.53f, -155.34f);
    private Vector3 fi = new Vector3(0.81f, 0.51f, -155.3491f);

    public Camera camera;


    public void InitialPlacement(){
        //オフラインなら全て自分が生成
        if(!PhotonNetwork.OfflineMode){
            if(PhotonNetwork.IsMasterClient){
                PhotonNetwork.Instantiate("Card", ph + new Vector3(-6.77f, -4.4f, 155f), Quaternion.identity, 0);
                PhotonNetwork.Instantiate("Card", ph + new Vector3(-1.81f, -4.4f, 155f), Quaternion.identity, 0);
                PhotonNetwork.Instantiate("Card", ph + new Vector3(3.06f, -4.4f, 155f), Quaternion.identity, 0);
                                                                

                PhotonNetwork.Instantiate("Field", fi + new Vector3(-4.6f, -0.85f, 155f), Quaternion.identity, 0);
                PhotonNetwork.Instantiate("Field", fi + new Vector3(2.83f, -0.85f, 155f), Quaternion.identity, 0);
            }
            else
            {
                PhotonNetwork.Instantiate("Card2", eh + new Vector3(-7.38f, 6f, 155f), Quaternion.identity, 0);
                PhotonNetwork.Instantiate("Card2", eh + new Vector3(-2.28f, 6f, 155f), Quaternion.identity, 0);
                //PhotonNetwork.Instantiate("Card2", eh + new Vector3(0.9f, 9f, 155f), Quaternion.identity, 0);
                PhotonNetwork.Instantiate("Card2", eh + new Vector3(2.7f, 6f, 155f), Quaternion.identity, 0);

                Debug.Log("enter");
                camera = Camera.main;
                camera.transform.rotation = Quaternion.Euler(0, 0, 180);
            }
        }
        else
        {
            PhotonNetwork.Instantiate("Card", ph + new Vector3(-6.77f, -4.4f, 155f), Quaternion.identity, 0);
            PhotonNetwork.Instantiate("Card", ph + new Vector3(-1.81f, -4.4f, 155f), Quaternion.identity, 0);
            PhotonNetwork.Instantiate("Card", ph + new Vector3(3.06f, -4.4f, 155f), Quaternion.identity, 0);
                                                            

            PhotonNetwork.Instantiate("Field", fi + new Vector3(-4.6f, -0.85f, 155f), Quaternion.identity, 0);
            PhotonNetwork.Instantiate("Field", fi + new Vector3(2.83f, -0.85f, 155f), Quaternion.identity, 0);

            PhotonNetwork.Instantiate("Card2", eh + new Vector3(-7.38f, 6f, 155f), Quaternion.identity, 0);
            PhotonNetwork.Instantiate("Card2", eh + new Vector3(-2.28f, 6f, 155f), Quaternion.identity, 0);
            //PhotonNetwork.Instantiate("Card2", eh + new Vector3(0.9f, 9f, 155f), Quaternion.identity, 0);
            PhotonNetwork.Instantiate("Card2", eh + new Vector3(2.7f, 6f, 155f), Quaternion.identity, 0);
        }
    }

    public void JoinOfflineRoom(){
        // MonoBehaviourPunCallbacksが必要なためここで定義
        //ここで入るのはofflineという特別なルーム
        PhotonNetwork.JoinRandomRoom();
    }

    public bool MyIsMasterClient(){
        return PhotonNetwork.IsMasterClient;
    }

    // Photonから切断された時
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
    }

    // 部屋から退室した時
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    // 他のプレイヤーが退室した時
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom");
        GameObject.Find ("Master").GetComponent<SceneManagerMain>().Win();
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartGame();
        //camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
