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
        private SkillButton skill = GameObject.Find("Skill").GetComponent<SkillButton>();


        protected internal override void Enter()
        {
            turnCount++;
            
            enemyHands = GameObject.FindGameObjectsWithTag("Player2");
            CanPlayHand(enemyHands, reload, skill, true);

            //タイマーセット 5秒
            timer.Set(5.0f);

            Debug.Log("相手のプレイターン");
            
            if(PhotonNetwork.OfflineMode){
                //EnemyがEnemyCardを動かすことを許可
                GameObject.Find ("Enemy").GetComponent<EnemyPlay>().enabled = true;
            }

        }

        protected internal override void Update()
        {
            //自分がマスターなら攻撃終了通知を待つ、そうでなければ通知を送る。
            if(!PhotonNetwork.OfflineMode){
                if(PhotonNetwork.IsMasterClient){
                    if(!timer.IsActive() && attackEnd){
                        attackEnd = false;
                        stateMachine.SendEvent((int)StateEventId.EnemyPlayEnd);
                    }
                }
                else
                {
                    if(!timer.IsActive()){
                        AttackEndSync(true);
                        stateMachine.SendEvent((int)StateEventId.EnemyPlayEnd);
                    }
                }
            }
            else{
                if(!timer.IsActive()){
                    stateMachine.SendEvent((int)StateEventId.EnemyPlayEnd);
                }
            }
    
        }

        protected internal override void Exit()
        {
            if(PhotonNetwork.OfflineMode){
                //EnemyがEnemyCardを動かせないようにする
                GameObject.Find ("Enemy").GetComponent<EnemyPlay>().enabled = false;
            }

            //散らかったカードを戻す
            CanPlayHand(enemyHands, reload, skill, false);

            Debug.Log("相手のプレイターン終了");
        }
    }

    private class EnemyAttackState : ImtStateMachine<DefineStateMachinePvP>.State
    {
        private PlayerStatus pStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        private EnemyStatus eStatus = GameObject.Find("Enemy").GetComponent<EnemyStatus>();

        private async void WaitSeconds(float sec){
            await Task.Delay((int)(1000 * sec));
            stateMachine.SendEvent((int)StateEventId.EnemyTurnEnd);
        }

        protected internal override void Enter()
        {
            eStatus.AttackFace();
            //ダメージ+エフェクト用コルーチン？
            pStatus.DamageCal();
            //処理が終わったらMyPlayStateへ
            WaitSeconds(2.5f);
        }

        protected internal override void Update()
        {

        }

        protected internal override bool GuardEvent(int eventId)
        {
            eStatus.NormalFace();

            if(!pStatus.IsAlive()){
                if(PhotonNetwork.IsMasterClient){
                    GameObject.Find ("Master").GetComponent<SceneManagerMain>().Lose();
                }
                else{
                    GameObject.Find ("Master").GetComponent<SceneManagerMain>().Win();
                }
                return true;
            }


            return false;
        }

        protected internal override void Exit()
        {
            Debug.Log("相手のターン終了");
        }
    }
}
