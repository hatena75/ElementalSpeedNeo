using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using System.Threading.Tasks;
using Photon.Pun;

public partial class DefineStateMachinePvP : MonoBehaviour
{
    private class EnemyPlayState : ImtStateMachine<DefineStateMachinePvP>.State
    {
        GameObject[] enemyHands;
        private TimerController timer = GameObject.Find("TimeCount").GetComponent<TimerController>();
        private HandResetButton reload = GameObject.Find("Button").GetComponent<HandResetButton>();

        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {
            enemyHands = GameObject.FindGameObjectsWithTag("Player2");
            //タイマーセット 5秒
            timer.Set(5.0f);

            Debug.Log("相手のプレイターン");
            CanPlayHand(enemyHands, reload, true);
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

            //散らかったカードを戻す
            CanPlayHand(enemyHands, reload, false);

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

        }

        protected internal override bool GuardEvent(int eventId)
        {
            GameObject.Find ("Enemy").GetComponent<EnemyStatus>().NormalFace();

            // 特定のタイミングで遷移を拒否（ガード）するなら true を返せばステートマシンは遷移を諦めます
            if(!pStatus.IsAlive()){
                if(PhotonNetwork.IsMasterClient){
                    GameObject.Find ("Master").GetComponent<SceneManagerMain>().Lose();
                }
                else{
                    GameObject.Find ("Master").GetComponent<SceneManagerMain>().Win();
                }
                return true;
            }

            // 遷移を許可するなら false を返せばステートマシンは状態の遷移をします
            return false;
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
            Debug.Log("相手のターン終了");
        }
    }
}
