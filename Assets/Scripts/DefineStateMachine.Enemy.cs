using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class DefineStateMachine : MonoBehaviour
{
    private class EnemyPlayState : ImtStateMachine<DefineStateMachine>.State
    {
        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {
            //タイマーセット5秒
            //EnemyがEnemyCardを動かすことを許可
        }

        // 状態の更新はこのUpdateで行う
        protected internal override void Update()
        {
            //タイマー更新
            //時間切れでEnemyAttackStateへ
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
            //カードの位置を初期位置にする
            //EnemyがEnemyCardを動かせないようにする
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
            //ダメージ+エフェクト処理？
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
            //プレイヤーのHPが0以下ならLoseシーンへ
        }
    }
}
