using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OpponentPlay : MonoBehaviour
{
    private Judgement jm;
    private SEManager se;
    private CardInfo cardInfo;
    private HandResetButton hrb;
    private CharacterAbstract opponent;

    //イベントの識別
    private Queue<FeedBackType> raiseEvents = new Queue<FeedBackType>();
    //カードプレイ情報
    private Queue<object[]> playInfos = new Queue<object[]>();
    //スキル、リロード情報
    private Queue<int[]> SpecialInfos = new Queue<int[]>();

    private Action sendState;

    private enum FeedBackType
    {
        PlayCard,
        UseReload,
        UseSkill,
        PlayEnd,
    }

    public void PlayCard_Enqueue(int pos_hand, int pos_field, int new_index_hand){
        object[] tmp = new object[3];
        tmp[0] = cardInfo.enemyHands[pos_hand]; //GameObject型
        tmp[1] = cardInfo.fields[pos_field]; //GameObject型
        tmp[2] = new_index_hand; //int型

        playInfos.Enqueue(tmp);
        raiseEvents.Enqueue(FeedBackType.PlayCard);
    }

    public void UseReload_Enqueue(int[] indexes){
        SpecialInfos.Enqueue(indexes);
        raiseEvents.Enqueue(FeedBackType.UseReload);
    }

    public void UseSkill_Enqueue(int[] indexes){
        SpecialInfos.Enqueue(indexes);
        raiseEvents.Enqueue(FeedBackType.UseSkill);
    }

    public void PlayEnd_Enqueue(Action a){
        sendState = a;
        raiseEvents.Enqueue(FeedBackType.PlayEnd);
    }

    // Start is called before the first frame update
    void Start()
    {
        jm = GameObject.Find ("Master").GetComponent<Judgement>();
        se = GameObject.Find ("SEManager").GetComponent<SEManager>();
        cardInfo = GameObject.Find("Master").GetComponent<CardInfo>();
        hrb = GameObject.Find("Button").GetComponent<HandResetButton>();
        opponent = SceneManagerCharacterSelect.EnemyCharacter;
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
        //キュー確認、先頭を参照、終わったらpop
        if(raiseEvents.Count > 0)
        {
            switch(raiseEvents.Peek()){
                case FeedBackType.PlayCard:
                    if(!MovingCard(playInfos.Peek()))
                    {
                        playInfos.Dequeue();
                        raiseEvents.Dequeue();
                    }
                    break;
                case FeedBackType.UseReload:
                    int[] reloadTmp = SpecialInfos.Dequeue();
                    hrb.OpponentReload(reloadTmp);
                    raiseEvents.Dequeue();
                    break;
                case FeedBackType.UseSkill:
                    opponent.SkillSync(SpecialInfos.Peek());
                    Debug.Log("skill done");
                    SpecialInfos.Dequeue();
                    raiseEvents.Dequeue();
                    break;
                case FeedBackType.PlayEnd:
                    sendState();
                    raiseEvents.Dequeue();
                    break;
                default:
                    break;
            }
        }
    }
}
