using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Haruka : CharacterAbstract
{
    public override void Skill(){
        cardInfo = GameObject.Find("Master").GetComponent<CardInfo>();
        GameObject.Find ("SEManager").GetComponent<SEManager>().DownSE();
        GameObject[] Cards = GameObject.FindGameObjectsWithTag("Player2");

        int num = 0;

        foreach (GameObject card in Cards) {
            int cardindex = card.GetComponent<CardModel>().cardIndex;
            //攻撃力が20なら10に下げる
            if(cardindex % 12 >= 6){
                card.GetComponent<CardModel>().ChangeFace(cardindex - 6);
                card.GetComponent<CardModel>().WeakingEffect();
                num = cardInfo.GetKey(cardInfo.enemyHands, card);
                //ただし、下げられるのは1枚のみ
                break;
            }
        }
        
        //攻撃力20が無かった(スキル不発)場合は通信しない
        if(!PhotonNetwork.OfflineMode && num != 0){
            int[] data = {num, cardInfo.enemyHands[num].GetComponent<CardModel>().cardIndex};
            SMNew.UseSkill(data);
        }
    }

    public override void SkillSync(int[] data){
        cardInfo = GameObject.Find("Master").GetComponent<CardInfo>();
        GameObject.Find ("SEManager").GetComponent<SEManager>().DownSE();
        cardInfo.myHands[data[0]].GetComponent<CardModel>().ChangeFace(data[1]);
        cardInfo.myHands[data[0]].GetComponent<CardModel>().ChangeElementEffect();
    }

    public Haruka() : base(){
        this.Name = "Haruka";
        this.Picture = "pictures/Haruka";
        this.NameText = "ハルカ";
        this.ExplainText = "スキル：フォースダウン\r\n相手の攻撃力20のカード1枚の攻撃力を10にする。";
        this.StandPicture = "pictures/HarukaStand";
    }
}
