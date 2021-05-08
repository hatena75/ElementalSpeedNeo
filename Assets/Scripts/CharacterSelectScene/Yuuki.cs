using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Yuuki : CharacterAbstract
{
    public override void Skill(){
        //フィールドのカードをシャッフルする
        GameObject.Find ("SEManager").GetComponent<SEManager>().ReloadSE();
        GameObject[] Fields = GameObject.FindGameObjectsWithTag("Field");
        foreach (GameObject field in Fields) {
            field.GetComponent<CardModel>().RandomFace();
        }

        if(!PhotonNetwork.OfflineMode){
            cardInfo = GameObject.Find("Master").GetComponent<CardInfo>();
            int[] data = {cardInfo.fields[1].GetComponent<CardModel>().cardIndex, cardInfo.fields[2].GetComponent<CardModel>().cardIndex};
            SMNew.UseSkill(data);
        }
    }

    public override void SkillSync(int[] data){
        cardInfo = GameObject.Find("Master").GetComponent<CardInfo>();
        for(int i = 0; i < data.Length; i++){
            cardInfo.fields[i+1].GetComponent<CardModel>().ReloadSync(data[i]);
        }
    }

    public Yuuki() : base(){
        this.Name = "Yuuki";
        this.Picture = "pictures/Player";
        this.NameText = "ユウキ";
        this.ExplainText = "スキル：フィールドリロード\r\nフィールドのカードをリロードする。";
        this.StandPicture = "pictures/YuukiStand";
    }
}
