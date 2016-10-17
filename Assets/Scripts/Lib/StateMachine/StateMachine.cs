using UnityEngine;
using System.Collections;

// ステートマシーンクラス

public class StateMachine<T>
{
	private State<T> m_currentState;
	public State<T> CurrentState
	{
		// ゲッター
		get { return m_currentState; }
	}

	// コンストラクタ
	public StateMachine()
	{
		m_currentState = null;
	}

	// ステートの変更
	public void ChangeState(State<T> _state)
	{
		if (m_currentState != null) 
		{
			// 他のステートに遷移する時に一度だけ呼ばれる処理を行う
			m_currentState.Exit ();
		}

		// ステートを変更
		m_currentState = _state;

		// 遷移する時に一度だけ呼ばれる処理を行う
		m_currentState.Enter ();
	}

	// ステートの更新
	public void Update () 
	{
		if (m_currentState != null)
		{
			// ステートの更新
			m_currentState.Update ();
		}
	}
}
