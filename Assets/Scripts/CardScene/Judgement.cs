﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

enum Elements : int
{
    Fire, Wind, Ground, Water
}

public class Judgement : MonoBehaviour
{
    private PhotonView photonView;

    private Dictionary<Elements, Elements> ElementChart;

    private static CardInfo cardInfo;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();

        cardInfo = GameObject.Find("Master").GetComponent<CardInfo>();

        //属性相性の定義
        ElementChart = new Dictionary<Elements, Elements>();
        ElementChart.Add(Elements.Fire, Elements.Wind);
        ElementChart.Add(Elements.Wind, Elements.Ground);
        ElementChart.Add(Elements.Ground, Elements.Water);
        ElementChart.Add(Elements.Water, Elements.Fire);
        
    }

    //置けるかどうか確認
    public bool PutAble(GameObject hand, GameObject field)
    {
        int playerIndex = hand.GetComponent<CardModel>().cardIndex;
        int fieldIndex = field.GetComponent<CardModel>().cardIndex;
        int playerNum = playerIndex % 6;
        int fieldNum = fieldIndex % 6;

        return playerNum + 1 == fieldNum || playerNum - 1 == fieldNum || System.Math.Abs(playerNum - fieldNum) == 5;
    }

    //置けるかどうか確認+置く
    public void Put(GameObject hand, GameObject field)
    {
        int playerIndex = hand.GetComponent<CardModel>().cardIndex;
        int fieldIndex = field.GetComponent<CardModel>().cardIndex;
        int playerNum = playerIndex % 6;
        int fieldNum = fieldIndex % 6;

        Debug.Log("Put");
        if(playerNum + 1 == fieldNum || playerNum - 1 == fieldNum || System.Math.Abs(playerNum - fieldNum) == 5)
        {
            //ダメージの処理。
            DamageCalculator(hand, field);
            //フィールド側の処理
            field.GetComponent<CardModel>().ChangeFace(playerIndex);
            //使用カードリセット
            hand.GetComponent<CardModel>().Reset();

            if(!PhotonNetwork.OfflineMode){
                //同期処理
                SMNew.PlayCard(cardInfo.GetKey(cardInfo.myHands, hand), cardInfo.GetKey(cardInfo.fields, field), hand.GetComponent<CardModel>().cardIndex);
            }
        }
        else
        {
            hand.GetComponent<CardModel>().ResetPos(); //適切でない場合元の位置に戻す。
        }
    }

    //カードプレイ同期の処理
    public void PutFeedBack(GameObject hand, GameObject field, int next_index){
        int playerIndex = hand.GetComponent<CardModel>().cardIndex;
        int fieldIndex = field.GetComponent<CardModel>().cardIndex;
        int playerNum = playerIndex % 6;
        int fieldNum = fieldIndex % 6;

        Debug.Log("PutFeedBack");
        if(playerNum + 1 == fieldNum || playerNum - 1 == fieldNum || System.Math.Abs(playerNum - fieldNum) == 5)
        {
            //ダメージの処理。
            DamageCalculator(hand, field);
            //フィールド側の処理
            field.GetComponent<CardModel>().ChangeFace(playerIndex);
            //使用カードリセット
            hand.GetComponent<CardModel>().ResetPos();
            hand.GetComponent<CardModel>().ChangeFace(next_index);
        }
        else
        {
            hand.GetComponent<CardModel>().ResetPos(); //適切でない場合元の位置に戻す。
        }
    }

    private bool ElementCalculator(Elements playerEle, Elements fieldEle)
    {
        return ElementChart[playerEle] == fieldEle;
    }

    private void DamageCalculator(GameObject hand, GameObject field)
    {
        int playerIndex = hand.GetComponent<CardModel>().cardIndex;
        int fieldIndex = field.GetComponent<CardModel>().cardIndex;
        Elements playerEle = (Elements)(playerIndex / 12);
        Elements fieldEle = (Elements)(fieldIndex / 12);
        //ダメージが10か20かをインデックスから計算
        int damage = (((playerIndex % 12) / 6) + 1) * 10;
        //属性相性計算
        bool effective = ElementCalculator(playerEle, fieldEle);
        string attacker = hand.transform.tag;

        //属性有利の計算
        damage *= effective ? 2 : 1;

        if(attacker == "Player")
        {
            GameObject.Find ("Enemy").GetComponent<EnemyStatus>().DamagePlus(damage, effective);
        }
        else
        {
            GameObject.Find ("Player").GetComponent<PlayerStatus>().DamagePlus(damage, effective);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
