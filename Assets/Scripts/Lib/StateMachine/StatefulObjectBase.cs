using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* 取り得るステートのリストを持ち、
   enum型を引数とするChangeState関数で、
   他のステートに遷移することができるようにするクラス */

public abstract class StatefulObjectBase<T, TEnum> : MonoBehaviour 
	where T : class where TEnum : System.IConvertible
{
	// ステートのリスト
	protected List<State<T>> stateList = new List<State<T>> ();

	// ステートマシーン
	protected StateMachine<T> stateMachine = new StateMachine<T>();

	// ステートの変更
	public void ChangeState(TEnum _state)
	{
		if (stateMachine == null) return;

		stateMachine.ChangeState (stateList [_state.ToInt32 (null)]);
	}

	// 現在のステートと同じかどうか判定
	protected bool IsEqualState(TEnum _state)
	{
		if (stateMachine == null) return false;

		return stateMachine.CurrentState == stateList [_state.ToInt32 (null)];
	}

	// 更新
	protected void Update()
	{
		if (stateMachine != null)
		{
			stateMachine.Update ();
		}
	}
}
