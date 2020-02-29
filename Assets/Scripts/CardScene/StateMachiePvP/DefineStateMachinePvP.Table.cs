using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class DefineStateMachinePvP : MonoBehaviour
{

    // ステートマシンの入力（イベント）を判り易くするために列挙型で定義
    public enum StateEventId
    {
        Start,
        MyPlayEnd,
        MyTurnEnd,
        EnemyPlayEnd,
        EnemyTurnEnd,
        Finish,
    }

    private void Awake()
    {
        // ステートマシンのインスタンスを生成して遷移テーブルを構築
        stateMachine = new ImtStateMachine<DefineStateMachinePvP>(this); // 自身がコンテキストになるので自身のインスタンスを渡す
        stateMachine.AddTransition<InitialState, MyPlayState>((int)StateEventId.Start);
        stateMachine.AddTransition<MyPlayState, MyAttackState>((int)StateEventId.MyPlayEnd);
        stateMachine.AddTransition<MyAttackState, EnemyPlayState>((int)StateEventId.MyTurnEnd);
        stateMachine.AddTransition<EnemyPlayState, EnemyAttackState>((int)StateEventId.EnemyPlayEnd);
        stateMachine.AddTransition<EnemyAttackState, MyPlayState>((int)StateEventId.EnemyTurnEnd);
        stateMachine.AddTransition<MyAttackState, EndState>((int)StateEventId.Finish);
        stateMachine.AddTransition<EnemyAttackState, EndState>((int)StateEventId.Finish);



        // 起動ステートを設定
        stateMachine.SetStartState<InitialState>();
    }
}
