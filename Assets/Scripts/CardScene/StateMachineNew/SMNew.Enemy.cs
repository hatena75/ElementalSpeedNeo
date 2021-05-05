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
    private class EnemyState : ImtStateMachine<SMNew>.State
    {
        GameObject[] enemyHands;
        private TimerController timer = GameObject.Find("TimeCount").GetComponent<TimerController>();
        private HandResetButton reload = GameObject.Find("Button").GetComponent<HandResetButton>();
        private SkillButton skill = GameObject.Find("Skill").GetComponent<SkillButton>();

        private PlayerStatus pStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        private EnemyStatus eStatus = GameObject.Find("Enemy").GetComponent<EnemyStatus>();

        private bool cpuAttacked = false;

        protected internal override void Enter()
        {
            turnCount++;

            //色変更
            enemyHands = GameObject.FindGameObjectsWithTag("Player2");
            CanPlayHand(enemyHands, reload, skill, true);

            //タイマーセット 5秒 raiseeventでズレるからオンラインでは不要か？
            timer.Set(5.0f);

            Debug.Log("相手のプレイターン");

            //相手のカード移動を有効化(機構ができているなら色だけでいい)
            if(PhotonNetwork.OfflineMode){
                //EnemyがEnemyCardを動かすことを許可
                GameObject.Find ("Enemy").GetComponent<EnemyPlay>().enabled = true;
            }
        }
        protected internal override void Update()
        {
            //通信を受け取り、その内容によった処理を行なう。
            //通信内容はキューに入れ、pop→動作を行ない、その終了を通知→次をpopとする。(移動描画より早く出された場合バグらないようにするため)
            //カードチェンジ系に終了を通知するようなコールバック関数ラッパーを作れば完全終了まで待てるか？
            //スキルの発動


            //以下、CPU戦用の処理
            if(!timer.IsActive()){
                if(PhotonNetwork.OfflineMode && !cpuAttacked){
                    //EnemyがEnemyCardを動かせないようにする
                    GameObject.Find ("Enemy").GetComponent<EnemyPlay>().enabled = false;
                    //散らかったカードを戻す
                    CanPlayHand(enemyHands, reload, skill, false);

                    Debug.Log("相手のプレイ終了");
                    
                    
                    async void WaitSeconds(float sec){
                        await Task.Delay((int)(1000 * sec));
                        stateMachine.SendEvent((int)StateEventId.OpponentTurnEnd);
                    }
                    
                    eStatus.AttackFace();
                    //ダメージ+エフェクト用コルーチン
                    pStatus.DamageCal();
                    WaitSeconds(2.5f);
                    
                    cpuAttacked = true;
                }
            }

        }

        protected internal override bool GuardEvent(int eventId)
        {
            eStatus.NormalFace();

            if(!pStatus.IsAlive()){
                GameObject.Find ("Master").GetComponent<SceneManagerMain>().Lose();
                //遷移中止
                return true;
            }

            return false;
        }

        protected internal override void Exit()
        {
            cpuAttacked = false;

            Debug.Log("相手のターン終了");
        }
        
    }

}
