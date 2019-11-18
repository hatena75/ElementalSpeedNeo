using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum Elements : int
{
    Fire, Wind, Ground, Water
}

public class Judgement : MonoBehaviour
{
    private Dictionary<Elements, Elements> ElementChart;
    // Start is called before the first frame update
    void Start()
    {
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

        if(playerNum + 1 == fieldNum || playerNum - 1 == fieldNum || System.Math.Abs(playerNum - fieldNum) == 5)
        {
            //ダメージの処理。
            DamageCalculator(hand, field);
            //フィールド側の処理
            field.GetComponent<CardModel>().ChangeFace(playerIndex);
            //使用カードリセット
            Instantiate(hand, hand.GetComponent<CardModel>().firstPos, Quaternion.identity);
            //使用カード消去
            Destroy (hand);

        }
        else
        {
            hand.GetComponent<CardModel>().ResetPos(); //適切でない場合元の位置に戻す。
        }

    }

    private int ElementCalculator(Elements playerEle, Elements fieldEle)
    {
        int times = 1;

        if(ElementChart[playerEle] == fieldEle){
            times *= 2;
        }

        return times;
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
        int times = ElementCalculator(playerEle, fieldEle);
        string attacker = hand.transform.tag;

        if(attacker == "Player")
        {
            GameObject.Find ("Enemy").GetComponent<EnemyStatus>().Damage(damage * times);
        }
        else
        {
            GameObject.Find ("Player").GetComponent<PlayerStatus>().Damage(damage * times);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
