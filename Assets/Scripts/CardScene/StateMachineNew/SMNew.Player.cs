using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;

public partial class SMNew : MonoBehaviour
{
    private class MyPlayState : ImtStateMachine<SMNew>.State
    {
        GameObject[] myHands;
        private TimerController timer = GameObject.Find("TimeCount").GetComponent<TimerController>();
        private HandResetButton reload = GameObject.Find("Button").GetComponent<HandResetButton>();
        private SkillButton skill = GameObject.Find("Skill").GetComponent<SkillButton>();
        
        protected internal override void Enter()
        {
            turnCount++;

            myHands = GameObject.FindGameObjectsWithTag("Player");
            CanPlayHand(myHands, reload, skill, true);

            //先行1ターン目のデメリット処理
            if(turnCount == 1){
                //タイマーセット 3秒
                timer.Set(3.0f);
                
                skill.DeActivate();
            }
            else{
                //タイマーセット 5秒
                timer.Set(5.0f);
            }

            Debug.Log("自分のプレイターン");
        }
        protected internal override void Update()
        {
            //自分だけで数えるため遅延用処理無し
            if(!timer.IsActive()){
                stateMachine.SendEvent((int)StateEventId.MyPlayEnd);
                //相手にターン終了を通知

            }
        }
        protected internal override void Exit()
        {
            CanPlayHand(myHands, reload, skill, false);

            //プレイ終了を相手に通知
            if(!PhotonNetwork.OfflineMode){
                PlayEnd();
            }

            Debug.Log("自分のプレイターン終了");
        }
    }

    private class MyAttackState : ImtStateMachine<SMNew>.State
    {
        private EnemyStatus eStatus = GameObject.Find("Enemy").GetComponent<EnemyStatus>();
        private PlayerStatus pStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();

        private async void WaitSeconds(float sec){
            await Task.Delay((int)(1000 * sec));
            stateMachine.SendEvent((int)StateEventId.MyTurnEnd);
        }
        protected internal override void Enter()
        {

            //攻撃の表情
            pStatus.AttackFace();

            eStatus.DamageCal();

            //ダメージ+エフェクト待ち用
            WaitSeconds(2.5f);


            Debug.Log("自分の攻撃開始");
        }
        protected internal override void Update()
        {
            //ダメージ+エフェクトの処理？
            //stateMachine.SendEvent((int)StateEventId.MyTurnEnd);
        }

        protected internal override bool GuardEvent(int eventId)
        {
            pStatus.NormalFace();

            if(!eStatus.IsAlive()){
                GameObject.Find ("Master").GetComponent<SceneManagerMain>().Win();
                //遷移の中断
                return true;
            }

            //falseで通常遷移
            return false;
        }

        protected internal override void Exit()
        {
            Debug.Log("自分の攻撃終了");
        }
    }
}
