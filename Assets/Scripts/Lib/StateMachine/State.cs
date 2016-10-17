using UnityEngine;
using System.Collections;

// 岡田テスト
// ステートの基底クラス

public class State<T>
{
	// このステートを利用するインスタンス
	protected T m_instance;

	// コンストラクタ
	public State (T _instance) 
	{
		m_instance = _instance;
	}

	// このステートに遷移する時に一度だけ呼ばれる
	public virtual void Enter() {}

	// このステートである間、毎フレーム呼ばれる
	public virtual void Update() {}

	// このステートから他のステートに遷移する時に一度だけ呼ばれる
	public virtual void Exit() {}
}
