using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentPlay : MonoBehaviour
{
    private Judgement jm;
    private SEManager se;
    private CardInfo cardInfo;

    //イベントの識別
    private Queue<int> raiseEvents = new Queue<int>();
    //イベントごとのコンテンツ
    //カードプレイ情報
    private Queue<object[]> playInfos = new Queue<object[]>();
    //スキル、リロード情報
    private Queue<int[]> SpecialInfos = new Queue<int[]>();

    public void PlayCard_Enqueue(int pos_hand, int pos_field, int new_index_hand){
        object[] tmp = new object[3];
        tmp[0] = cardInfo.enemyHands[pos_hand]; //GameObject型
        tmp[1] = cardInfo.fields[pos_field]; //GameObject型
        tmp[2] = new_index_hand; //int型

        Debug.Log(tmp[0]);
        playInfos.Enqueue(tmp);

        raiseEvents.Enqueue(3); //PlayCard
    }

    // Start is called before the first frame update
    void Start()
    {
        jm = GameObject.Find ("Master").GetComponent<Judgement>();
        se = GameObject.Find ("SEManager").GetComponent<SEManager>();
        cardInfo = GameObject.Find("Master").GetComponent<CardInfo>();
    }

    private bool MovingCard(object[] tmp)
    {
        GameObject hand = (GameObject)tmp[0];
        GameObject field = (GameObject)tmp[1];
        int next_index = (int)tmp[2];


        Vector3 prePos = hand.transform.position;
        hand.transform.position = Vector3.Lerp(hand.transform.position, field.transform.position, 0.1f);
        
        //十分近づいたら置く処理
        if(Vector3.Distance(prePos, hand.transform.position) < 0.05f)
        {
            jm.PutFeedBack(hand, field, next_index);
            se.CardPlaySE();
            return false;
        }
        else
        {
            return true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //キュー確認、先頭を参照、終わったらpop、state遷移は0,0,0
        if(raiseEvents.Count > 0)
        {
            if(!MovingCard(playInfos.Peek()))
            {
                playInfos.Dequeue();
                raiseEvents.Dequeue();
            }
        }
    }
}
