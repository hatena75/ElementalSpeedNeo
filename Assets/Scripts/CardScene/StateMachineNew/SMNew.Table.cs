using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class SMNew : MonoBehaviour
{

    // ステートマシンの入力（イベント）を判り易くするために列挙型で定義
    public enum StateEventId
    {
        StandBy,
        GameStart_P1,
        GameStart_P2,
        MyPlayEnd,
        MyTurnEnd,
        OpponentTurnEnd,
        Finish,
    }

    private void Awake()
    {
        // ステートマシンのインスタンスを生成して遷移テーブルを構築
        stateMachine = new ImtStateMachine<SMNew>(this); // 自身がコンテキストになるので自身のインスタンスを渡す
        stateMachine.AddTransition<InitialState, StandByState>((int)StateEventId.StandBy);
        // 先攻後攻
        stateMachine.AddTransition<StandByState, MyPlayState>((int)StateEventId.GameStart_P1);
        stateMachine.AddTransition<StandByState, EnemyState>((int)StateEventId.GameStart_P2);
        
        stateMachine.AddTransition<MyPlayState, MyAttackState>((int)StateEventId.MyPlayEnd);
        stateMachine.AddTransition<MyAttackState, EnemyState>((int)StateEventId.MyTurnEnd);
        stateMachine.AddTransition<EnemyState, MyPlayState>((int)StateEventId.OpponentTurnEnd);
        
        //HP0
        stateMachine.AddTransition<MyAttackState, EndState>((int)StateEventId.Finish);
        stateMachine.AddTransition<EnemyState, EndState>((int)StateEventId.Finish);



        // 起動ステートを設定
        stateMachine.SetStartState<InitialState>();
    }
}
