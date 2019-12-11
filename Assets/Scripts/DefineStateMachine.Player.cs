using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class DefineStateMachine : MonoBehaviour
{
    private class MyPlayState : ImtStateMachine<DefineStateMachine>.State
    {
        //private float timeOut;
        //private float timeElapsed;
        GameObject[] myHands = GameObject.FindGameObjectsWithTag("Player");
        private TimerController timer = GameObject.Find("TimeCount").GetComponent<TimerController>();

        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {
            //タイマーセット 5秒
            //timeOut = 5.0f;
            //timeElapsed = 0.0f;
            timer.Set(5.0f);

            //自分のCardを動かせるようにする
            foreach (GameObject myHand in myHands) {
                myHand.GetComponent<Mouse>().enabled = true;
                myHand.GetComponent<CardModel>().MovableColor();
                //Debug.Log(activeSelf);
            }

            Debug.Log("自分のプレイターン");
        }

        // 状態の更新はこのUpdateで行う
        protected internal override void Update()
        {
            /*
            //タイマー実行
            timeElapsed += Time.deltaTime;

            if(timeElapsed >= timeOut)
            {
                timeElapsed = 0.0f;

                stateMachine.SendEvent((int)StateEventId.MyPlayEnd);

            }
            //時間切れになったらMyAttackStateへ
            */

            if(!timer.IsActive()){
                stateMachine.SendEvent((int)StateEventId.MyPlayEnd);
            }
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
            //散らかったカードを戻し、自分のCardを動かせないようにする
            foreach (GameObject myHand in myHands) {
                myHand.GetComponent<CardModel>().ResetPos();
                myHand.GetComponent<CardModel>().UnMovableColor();
                myHand.GetComponent<Mouse>().enabled = false;
            }

            Debug.Log("自分のプレイターン終了");

        }
    }

    private class MyAttackState : ImtStateMachine<DefineStateMachine>.State
    {
        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {
            //ダメージ+エフェクト用コルーチン？
            GameObject.Find ("Enemy").GetComponent<EnemyStatus>().DamageCal();
            //処理が終わったらEnemyPlayStateへ

            Debug.Log("自分の攻撃開始");
        }

        // 状態の更新はこのUpdateで行う
        protected internal override void Update()
        {
            //ダメージ+エフェクトの処理？
            stateMachine.SendEvent((int)StateEventId.MyTurnEnd);
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
            //相手のHPが0ならWinシーンへ
             Debug.Log("自分の攻撃終了");
        }
    }
}
