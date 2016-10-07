using UnityEngine;
using System.Collections;

// 指定の角度に回転させるクラス

public class RotateTo : MonoBehaviour 
{
	// 回転中かどうかのフラグ
	bool m_isRotate;
	public bool isRotate
	{
		get
		{
			return m_isRotate;
		}
	}

	// 回転時間
	float m_duration;

	// 経過時間
	float m_elapsedTime;

	// 遅らせる時間
	float m_delay;

	// 回転開始角度
	Quaternion m_startRotation;

	// 回転終了角度
	Quaternion	m_endRotation;

	void Awake()
	{
		enabled = false;
	}

	public void SetRotateTo(Vector3 _endRotation,float _duration, float _delay = 0.0f)
	{
		m_isRotate = true;
		enabled = true;
		m_elapsedTime = 0.0f;


		m_startRotation = transform.rotation;
		m_endRotation = Quaternion.Euler(_endRotation);

		m_duration = _duration;
		m_delay = _delay;

		if (m_duration <= 0) 
		{
			transform.rotation = m_endRotation;
			enabled = false;
			return;
		}
	}

	void Update ()
	{
		m_elapsedTime += Time.deltaTime;

		if (m_elapsedTime - m_delay < 0) 
			return;

		// 経過時間が回転時間を超えたら
		if (m_elapsedTime - m_delay > m_duration) 
		{
			transform.rotation = m_endRotation;
			m_isRotate = false;
			enabled = false;
		}

		// 割合
		float rate = m_elapsedTime / m_duration;

		// 割合から出した２点間の角度を入れる
		transform.rotation = Quaternion.Lerp (m_startRotation, m_endRotation, rate);
	}
}
