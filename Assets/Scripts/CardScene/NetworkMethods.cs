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
    private Vector3 eh = new Vector3(2.34f, 0.1f, -155.3491f); //0.817f
    private Vector3 ph = new Vector3(1.88f, -2.53f, -155.34f);
    private Vector3 fi = new Vector3(0.81f, 0.51f, -155.3491f);

    public Camera camera;

    public GameObject Card;
    public GameObject Field;
    public GameObject Card2;

    private CardInfo ci;

    private void InstantiateAddList(int n, GameObject obj, Dictionary<int, GameObject> dic){
        dic.Add(n, obj);
    }

    private void PlayerCardInstantiate(){
        InstantiateAddList(1, Instantiate(Card, ph + new Vector3(-6.77f, -4.4f, 155f), Quaternion.identity), ci.myHands);
        InstantiateAddList(2, Instantiate(Card, ph + new Vector3(-1.81f, -4.4f, 155f), Quaternion.identity), ci.myHands);
        InstantiateAddList(3, Instantiate(Card, ph + new Vector3(3.06f, -4.4f, 155f), Quaternion.identity), ci.myHands);
    }

    private void FieldCardInstantiate(){
        InstantiateAddList(1, Instantiate(Field, fi + new Vector3(-4.6f, -0.85f, 155f), Quaternion.identity), ci.fields);
        InstantiateAddList(2, Instantiate(Field, fi + new Vector3(2.83f, -0.85f, 155f), Quaternion.identity), ci.fields);
    }

    private void Player2CardInstantiate(){
        InstantiateAddList(1, Instantiate(Card2, eh + new Vector3(-7.38f, 6f, 155f), Quaternion.identity), ci.enemyHands);
        InstantiateAddList(2, Instantiate(Card2, eh + new Vector3(-2.28f, 6f, 155f), Quaternion.identity), ci.enemyHands);
        //PhotonNetwork.Instantiate("Card2", eh + new Vector3(0.9f, 9f, 155f), Quaternion.identity, 0);
        InstantiateAddList(3, Instantiate(Card2, eh + new Vector3(2.7f, 6f, 155f), Quaternion.identity), ci.enemyHands);
    }


    public void InitialPlacement(){
        PlayerCardInstantiate();
        FieldCardInstantiate();
        Player2CardInstantiate();
    }

    public void JoinOfflineRoom(){
        // MonoBehaviourPunCallbacksが必要なためここで定義
        //ここで入るのはofflineという特別なルーム
        PhotonNetwork.JoinRandomRoom();
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
        ci = GameObject.Find ("Master").GetComponent<CardInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
