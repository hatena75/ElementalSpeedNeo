using System;
using IceMilkTea.Core;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Threading.Tasks;

using Photon.Pun;
using Photon.Realtime;
public partial class SMNew : MonoBehaviour
{
    private class EnemyPlayState : ImtStateMachine<SMNew>.State
    {
        GameObject[] enemyHands;
        private TimerController timer = GameObject.Find("TimeCount").GetComponent<TimerController>();
        private HandResetButton reload = GameObject.Find("Button").GetComponent<HandResetButton>();
        private SkillButton skill = GameObject.Find("Skill").GetComponent<SkillButton>();

        protected internal override void Enter()
        {
            turnCount++;

            //色変更
            enemyHands = GameObject.FindGameObjectsWithTag("Player2");
            CanPlayHand(enemyHands, reload, skill, true);

            //タイマーセット(通信遅延でズレるからオンラインでは不要？)
            if(turnCount == 1){
                timer.Set(3.0f);
            }
            else{
                timer.Set(5.0f);
            }

            Debug.Log("相手のプレイターン");

            //CPUがカードを動かすことを許可
            if(PhotonNetwork.OfflineMode){
                GameObject.Find ("Enemy").GetComponent<EnemyPlay>().enabled = true;
            }
        }

        protected internal override void Update()
        {
            //通信を受け取り、その内容により処理を行なう。
            //プレイ or スキル or リロード

            //CPU戦ならタイマーでプレイ終了、対人ならTurnEndのRaiseEventを待つ
            if(!timer.IsActive() && PhotonNetwork.OfflineMode){
                stateMachine.SendEvent((int)StateEventId.OpponentPlayEnd);
            }

        }

        protected internal override void Exit()
        {
            //CPUがカードを動かせないようにする
            if(PhotonNetwork.OfflineMode){
                GameObject.Find ("Enemy").GetComponent<EnemyPlay>().enabled = false;
            }

            //散らかったカードを戻す
            CanPlayHand(enemyHands, reload, skill, false);

            Debug.Log("相手のプレイターン終了");
        }
        
    }

    private class EnemyAttackState : ImtStateMachine<SMNew>.State
    {
        private PlayerStatus pStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        private EnemyStatus eStatus = GameObject.Find("Enemy").GetComponent<EnemyStatus>();

        private async void WaitSeconds(float sec){
            await Task.Delay((int)(1000 * sec));
            stateMachine.SendEvent((int)StateEventId.OpponentTurnEnd);
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
                GameObject.Find ("Master").GetComponent<SceneManagerMain>().Lose();
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
