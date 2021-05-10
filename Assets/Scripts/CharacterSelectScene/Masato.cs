using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Masato : CharacterAbstract
{
    public override void Skill(){
        //自分の手札の属性を変える
        GameObject.Find ("SEManager").GetComponent<SEManager>().ChangeElementSE();
        GameObject[] Cards = GameObject.FindGameObjectsWithTag("Player");
        var rand = new System.Random();
        foreach (GameObject card in Cards) {
            //属性を除いたカード情報の取得
            int cardPow = card.GetComponent<CardModel>().cardIndex % 12;
            int element = rand.Next(0, 3);
            int index = cardPow + (element * 12);
            card.GetComponent<CardModel>().ChangeElement(index);
        }

         if(!PhotonNetwork.OfflineMode){
             cardInfo = GameObject.Find("Master").GetComponent<CardInfo>();
            int[] data = new int[3];
            for(int i = 0; i < data.Length; i++){
                data[i] = cardInfo.myHands[i+1].GetComponent<CardModel>().cardIndex;
            }
            SMNew.UseSkill(data);
        }
    }

    public override void SkillSync(int[] data){
        cardInfo = GameObject.Find("Master").GetComponent<CardInfo>();
        GameObject.Find ("SEManager").GetComponent<SEManager>().ChangeElementSE();
        for(int i = 0; i < data.Length; i++){
            cardInfo.enemyHands[i+1].GetComponent<CardModel>().ChangeElement(data[i]);
        }
    }

    public Masato() : base(){
        this.Name = "Masato";
        this.Picture = "pictures/Enemy";
        this.NameText = "マサト";
        this.ExplainText = "スキル:エレメントチェンジ\r\n自分の手札にある全てのカードの属性を変える。";
        this.StandPicture = "pictures/MasatoStand";
    }
}
