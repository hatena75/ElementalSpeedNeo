using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haruka : CharacterAbstract
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
