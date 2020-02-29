using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using System.Threading.Tasks;

public partial class DefineStateMachinePvP : MonoBehaviour
{
    private class EnemyPlayState : ImtStateMachine<DefineStateMachinePvP>.State
    {
        GameObject[] enemyHands;
        private TimerController timer = GameObject.Find("TimeCount").GetComponent<TimerController>();


        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {
            enemyHands = GameObject.FindGameObjectsWithTag("Player2");
            //タイマーセット 5秒
            timer.Set(5.0f);

            Debug.Log("相手のプレイターン");
            //EnemyがEnemyCardを動かすことを許可
            GameObject.Find ("Enemy").GetComponent<EnemyPlay>().enabled = true;
            foreach (GameObject enemyHand in enemyHands) {
                enemyHand.GetComponent<CardModel>().MovableColor();
            }
        }

        // 状態の更新はこのUpdateで行う
        protected internal override void Update()
        {
            if(!timer.IsActive()){
                stateMachine.SendEvent((int)StateEventId.EnemyPlayEnd);
            }
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
            //EnemyがEnemyCardを動かせないようにする
            GameObject.Find ("Enemy").GetComponent<EnemyPlay>().enabled = false;

            //散らかったカードを戻す
            foreach (GameObject enemyHand in enemyHands) {
                enemyHand.GetComponent<CardModel>().ResetPos();
                enemyHand.GetComponent<CardModel>().UnMovableColor();

            }

            Debug.Log("相手のプレイターン終了");
        }
    }

    private class EnemyAttackState : ImtStateMachine<DefineStateMachinePvP>.State
    {
        private PlayerStatus pStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        

        private async void WaitSeconds(float sec){
            await Task.Delay((int)(1000 * sec));
            stateMachine.SendEvent((int)StateEventId.EnemyTurnEnd);
        }

        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {
            GameObject.Find ("Enemy").GetComponent<EnemyStatus>().AttackFace();
            //ダメージ+エフェクト用コルーチン？
            GameObject.Find ("Player").GetComponent<PlayerStatus>().DamageCal();
            //処理が終わったらMyPlayStateへ
            WaitSeconds(2.5f);
        }

        // 状態の更新はこのUpdateで行う
        protected internal override void Update()
        {
            //stateMachine.SendEvent((int)StateEventId.EnemyTurnEnd);
            //ダメージ+エフェクト処理？
        }

        protected internal override bool GuardEvent(int eventId)
        {
            GameObject.Find ("Enemy").GetComponent<EnemyStatus>().NormalFace();

            // 特定のタイミングで遷移を拒否（ガード）するなら true を返せばステートマシンは遷移を諦めます
            if(!pStatus.IsAlive()){
                GameObject.Find ("Master").GetComponent<SceneManagerMain>().Lose();
                return true;
            }

            // 遷移を許可するなら false を返せばステートマシンは状態の遷移をします
            return false;
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
            //GameObject.Find ("Enemy").GetComponent<EnemyStatus>().NormalFace();

            //プレイヤーのHPが0以下ならLoseシーンへ
            /*
            if(!pStatus.IsAlive()){
                GameObject.Find ("Master").GetComponent<SceneManagerMain>().Lose();
            }
            */

            Debug.Log("相手のターン終了");
        }
    }
}
