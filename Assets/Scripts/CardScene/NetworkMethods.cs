using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkMethods : MonoBehaviourPun
{
    //カードの初期配置。自分と相手でそれぞれ手札を生成。
    //マスター側はフィールドも生成する。
    private Vector3 scale = new Vector3(0.817f, 0.817f, 0.817f);
    private Vector3 eh = new Vector3(2.34f, 0.1f, 0.817f);
    private Vector3 ph = new Vector3(1.88f, -2.53f, -155.34f);
    private Vector3 fi = new Vector3(0.81f, 0.51f, -155.3491f);


    public void InitialPlacement(){
        if(PhotonNetwork.IsMasterClient){
            PhotonNetwork.Instantiate("Card", ph + new Vector3(-8.57f, -1.4f, 155f), Quaternion.identity, 0);
            PhotonNetwork.Instantiate("Card", ph + new Vector3(-3.61f, -1.4f, 155f), Quaternion.identity, 0);        
            PhotonNetwork.Instantiate("Card", ph + new Vector3(1.26f, -1.4f, 155f), Quaternion.identity, 0);

            PhotonNetwork.Instantiate("Card2", eh + new Vector3(-9.18f, 9f, 155f), Quaternion.identity, 0);
            PhotonNetwork.Instantiate("Card2", eh + new Vector3(-4.08f, 9f, 155f), Quaternion.identity, 0);
            PhotonNetwork.Instantiate("Card2", eh + new Vector3(0.9f, 9f, 155f), Quaternion.identity, 0);

            PhotonNetwork.Instantiate("Field", fi + new Vector3(-6.4f, 2.15f, 155f), Quaternion.identity, 0);
            PhotonNetwork.Instantiate("Field", fi + new Vector3(1.03f, 2.15f, 155f), Quaternion.identity, 0);

            //ここでRPCでもう一人のプレイヤーにCard2の所有権を付与
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
