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
            GameObject.Find ("Enemy").GetComponent<EnemyPlay>().enabled = true;
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
            //EnemyがEnemyCardを動かせないようにする
            GameObject.Find ("Enemy").GetComponent<EnemyPlay>().enabled = false;

            timeElapsed = 0.0f;
            //散らかったカードを戻す
            foreach (GameObject enemyHand in enemyHands) {
                enemyHand.GetComponent<CardModel>().ResetPos();
            }

            Debug.Log("相手のプレイターン終了");
        }
    }

    private class EnemyAttackState : ImtStateMachine<DefineStateMachine>.State
    {
        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {
            //ダメージ+エフェクト用コルーチン？
            GameObject.Find ("Player").GetComponent<PlayerStatus>().DamageCal();
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
