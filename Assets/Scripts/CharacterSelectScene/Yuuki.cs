using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yuuki : CharacterAbstract
{
    public override void Skill(){
        //フィールドのカードをシャッフルする
        GameObject[] Fields = GameObject.FindGameObjectsWithTag("Field");
        foreach (GameObject field in Fields) {
            field.GetComponent<CardModel>().RandomFace();
        }
    }

    public Yuuki() : base(){
        this.Name = "Yuuki";
        this.Picture = "pictures/Player";
    }
}
