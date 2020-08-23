using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //added
using Photon.Pun;

public class EnemyStatus : CommonPlayerStatus
{
    void Start()
    {
        base.Start();
        if(PhotonNetwork.IsConnected){
            if(!PhotonNetwork.IsMasterClient){
                textHP = GameObject.Find ("PlayerHP").GetComponent<Text>();
                barHP = GameObject.Find("PlayerBar").GetComponent<Slider>();
                face = GameObject.Find ("PlayerCharactor");
            }
            else
            {
                textHP = GameObject.Find ("EnemyHP").GetComponent<Text>();
                barHP = GameObject.Find("EnemyBar").GetComponent<Slider>();
                face = GameObject.Find ("EnemyCharactor");
            }
        }
        else
        {
            textHP = GameObject.Find ("EnemyHP").GetComponent<Text>();
            barHP = GameObject.Find("EnemyBar").GetComponent<Slider>();
            face = GameObject.Find ("EnemyCharactor");
        }
    }

    void Update()
    {
        base.Update();
    }
}
