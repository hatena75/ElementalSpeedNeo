using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Haruka : CharacterAbstract
{
    private void EffectCard(string target){
        GameObject[] Cards = GameObject.FindGameObjectsWithTag(target);
        foreach (GameObject card in Cards) {
            int cardInfo = card.GetComponent<CardModel>().cardIndex;
            //攻撃力が20なら10に下げる
            if(cardInfo % 12 >= 6){
                card.GetComponent<CardModel>().ChangeFace(cardInfo - 6);
            }
        }
    }
    public override void Skill(){
        //相手の全てのカードの攻撃力を10にする。
        if(PhotonNetwork.IsConnected){
            if(PhotonNetwork.IsMasterClient){
                EffectCard("Player2");
            }
            else{
                EffectCard("Player");
            }
        }
        else
        {
            EffectCard("Enemy");
        }
    }

    public Haruka() : base(){
        this.Name = "Haruka";
        this.Picture = "pictures/Haruka";
    }
}
