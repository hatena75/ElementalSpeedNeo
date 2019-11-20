using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class DefineStateMachine : MonoBehaviour
{
    private class MyPlayState : ImtStateMachine<DefineStateMachine>.State
    {
        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {
            //タイマーセット 5秒
            //自分のCardを動かせるようにする
        }

        // 状態の更新はこのUpdateで行う
        protected internal override void Update()
        {
            //タイマー実行？
            //時間切れになったらMyAttackStateへ
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
            //自分のCardを動かせないようにする
            //散らかったカードを戻す？
        }
    }

    private class MyAttackState : ImtStateMachine<DefineStateMachine>.State
    {
        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {
            //ダメージ+エフェクト用コルーチン？
            //処理が終わったらEnemyPlayStateへ
        }

        // 状態の更新はこのUpdateで行う
        protected internal override void Update()
        {
            //ダメージ+エフェクトの処理？
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
            //相手のHPが0ならWinシーンへ
        }
    }
}
