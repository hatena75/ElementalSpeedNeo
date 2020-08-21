using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Masato : CharacterAbstract
{
    private void EffectCard(string target){
        GameObject[] Cards = GameObject.FindGameObjectsWithTag(target);
        var rand = new System.Random();
        foreach (GameObject card in Cards) {
            //属性を除いたカード情報の取得
            int cardInfo = card.GetComponent<CardModel>().cardIndex % 12;
            int element = rand.Next(0, 3);
            int index = cardInfo + (element * 12);
            card.GetComponent<CardModel>().ChangeFace(index);
        }
    }

    public override void Skill(){
        //自分の手札の属性を変える
        if(PhotonNetwork.IsConnected){
            if(PhotonNetwork.IsMasterClient){
                EffectCard("Player");
            }
            else{
                EffectCard("Player2");
            }
        }
        else
        {
            EffectCard("Player");
        }
    }

    public Masato() : base(){
        this.Name = "Masato";
        this.Picture = "pictures/Enemy";
    }
}
