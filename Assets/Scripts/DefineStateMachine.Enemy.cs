using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class DefineStateMachine : MonoBehaviour
{
    private class EnemyPlayState : ImtStateMachine<DefineStateMachine>.State
    {
        private float timeOut;
        private float timeElapsed;
        GameObject[] enemyHands = GameObject.FindGameObjectsWithTag("Enemy");

        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {
            //タイマーセット 5秒
            timeOut = 5.0f;
            timeElapsed = 0.0f;

            Debug.Log("相手のプレイターン");
            //EnemyがEnemyCardを動かすことを許可
        }

        // 状態の更新はこのUpdateで行う
        protected internal override void Update()
        {
            //タイマー実行
            timeElapsed += Time.deltaTime;

            if(timeElapsed >= timeOut)
            {
                timeElapsed = 0.0f;

                //時間切れでEnemyAttackStateへ
                stateMachine.SendEvent((int)StateEventId.EnemyPlayEnd);

            }

            
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
            //カードの位置を初期位置にする
            timeElapsed = 0.0f;
            //散らかったカードを戻し、自分のCardを動かせないようにする
            foreach (GameObject enemyHand in enemyHands) {
                enemyHand.GetComponent<CardModel>().ResetPos();
            }
            //EnemyがEnemyCardを動かせないようにする

            Debug.Log("相手のプレイターン終了");
        }
    }

    private class EnemyAttackState : ImtStateMachine<DefineStateMachine>.State
    {
        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {
            //ダメージ+エフェクト用コルーチン？
            //処理が終わったらMyPlayStateへ
        }

        // 状態の更新はこのUpdateで行う
        protected internal override void Update()
        {
            stateMachine.SendEvent((int)StateEventId.EnemyTurnEnd);
            //ダメージ+エフェクト処理？
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
            Debug.Log("相手のターン終了");
            //プレイヤーのHPが0以下ならLoseシーンへ
        }
    }
}
