using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;

public partial class DefineStateMachinePvP : MonoBehaviour
{
    private class MyPlayState : ImtStateMachine<DefineStateMachinePvP>.State
    {
        GameObject[] myHands;
        private TimerController timer = GameObject.Find("TimeCount").GetComponent<TimerController>();
        private HandResetButton reload = GameObject.Find("Button").GetComponent<HandResetButton>();

        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {
            myHands = GameObject.FindGameObjectsWithTag("Player");
            //タイマーセット 5秒
            timer.Set(5.0f);

            CanPlayHand(myHands, reload, true);

            Debug.Log("自分のプレイターン");
        }

        // 状態の更新はこのUpdateで行う
        protected internal override void Update()
        {
            if(!timer.IsActive()){
                stateMachine.SendEvent((int)StateEventId.MyPlayEnd);
            }
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
            CanPlayHand(myHands, reload, false);

            Debug.Log("自分のプレイターン終了");
        }
    }

    private class MyAttackState : ImtStateMachine<DefineStateMachinePvP>.State
    {
        private EnemyStatus eStatus = GameObject.Find("Enemy").GetComponent<EnemyStatus>();
        private async void WaitSeconds(float sec){
            await Task.Delay((int)(1000 * sec));
            stateMachine.SendEvent((int)StateEventId.MyTurnEnd);
        }

        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {

            //攻撃の表情
            GameObject.Find ("Player").GetComponent<PlayerStatus>().AttackFace();

            GameObject.Find ("Enemy").GetComponent<EnemyStatus>().DamageCal();

            //ダメージ+エフェクト待ち用
            WaitSeconds(2.5f);


            Debug.Log("自分の攻撃開始");
        }

        // 状態の更新はこのUpdateで行う
        protected internal override void Update()
        {
            //ダメージ+エフェクトの処理？
            //stateMachine.SendEvent((int)StateEventId.MyTurnEnd);
        }

        protected internal override bool GuardEvent(int eventId)
        {
            GameObject.Find ("Player").GetComponent<PlayerStatus>().NormalFace();

            // 特定のタイミングで遷移を拒否（ガード）するなら true を返せばステートマシンは遷移を諦めます
            if(!eStatus.IsAlive()){
                if(PhotonNetwork.IsMasterClient){
                    GameObject.Find ("Master").GetComponent<SceneManagerMain>().Win();
                }
                else{
                    GameObject.Find ("Master").GetComponent<SceneManagerMain>().Lose();
                }
                return true;
            }

            // 遷移を許可するなら false を返せばステートマシンは状態の遷移をします
            return false;
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
             Debug.Log("自分の攻撃終了");
        }
    }
}
