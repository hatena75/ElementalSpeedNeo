using System;
using IceMilkTea.Core;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
public partial class SMNew : MonoBehaviour
{
    private class EnemyState : ImtStateMachine<SMNew>.State
    {

        protected internal override void Enter()
        {
            //相手のカード移動を有効化(機構ができているなら色だけでいい)
        }
        protected internal override void Update()
        {
            //通信を受け取り、その内容によった処理を行なう。
            //通信内容はキューに入れ、pop→動作を行ない、その終了を通知→次をpopとする。(ターン終了や動きに矛盾がないようにする)
            //カードチェンジ系に終了を通知するようなコールバック関数ラッパーを作れば完全終了まで待てるか？
            //スキルの発動
        }

        protected internal override void Exit()
        {
            //相手のカード移動を無効化
        }
        
    }

}
