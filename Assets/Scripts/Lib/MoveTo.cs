using UnityEngine;
using System.Collections;

// オブジェクトを指定の座標に移動させるクラス

public class MoveTo : MonoBehaviour 
{
	// 移動中かどうかのフラグ
	bool m_isMove;
	public bool isMove
	{
		get
		{
			return m_isMove;
		}
	}

	// 移動時間
	float m_duration;

	// 経過時間
	float m_elapsedTime;

	// 遅らせる時間
	float m_delay;

	// 移動開始座標
	Vector3 m_startPosition;

	// 移動終了座標
	Vector3	m_endPosition;

	void Awake()
	{
		enabled = false;
	}

	public void SetMoveTo(Vector3 _endPosition,float _duration, float _delay = 0.0f)
	{
		m_isMove = true;
		enabled = true;
		m_elapsedTime = 0.0f;

		m_startPosition = transform.position;
		m_endPosition = _endPosition;
		m_duration = _duration;
		m_delay = _delay;

		if (m_duration <= 0) 
		{
			transform.position = m_endPosition;
			enabled = false;
			return;
		}
	}

	void Update ()
	{
		m_elapsedTime += Time.deltaTime;

		if (m_elapsedTime - m_delay < 0) 
			return;

		// 経過時間が移動時間を超えたら
		if (m_elapsedTime - m_delay > m_duration) 
		{
			transform.position = m_endPosition;
			m_isMove = false;
			enabled = false;
		}

		// 割合
		float rate = m_elapsedTime / m_duration;

		// 割合から出した２点間の座標を入れる
		transform.position = Vector3.Lerp (m_startPosition, m_endPosition, rate);
	}
}
