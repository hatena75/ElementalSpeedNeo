﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class HandResetButton : MonoBehaviour
{
    private bool usable;
    private Button btn;

    public void Activate(){
        usable = true;
        btn.interactable = usable;
    }

    public void DeActivate(){
        usable = false;
        btn.interactable = usable;
    }

    public void OnClick() {
        if(usable){
            if(PhotonNetwork.IsMasterClient){
                GameObject[] playerHands = GameObject.FindGameObjectsWithTag("Player");
                foreach (GameObject playerHand in playerHands) {
                    playerHand.GetComponent<CardModel>().RandomFace();
                }
            }
            else{
                GameObject[] player2Hands = GameObject.FindGameObjectsWithTag("Player2");
                foreach (GameObject player2Hand in player2Hands) {
                    player2Hand.GetComponent<CardModel>().RandomFace();
                }
            }

            DeActivate();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        DeActivate();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
