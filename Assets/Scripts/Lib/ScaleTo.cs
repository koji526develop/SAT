using UnityEngine;
using System.Collections;

// 指定のスケールに拡縮させるクラス

public class ScaleTo : MonoBehaviour 
{
	// 拡縮中かどうかのフラグ
	bool m_isScale;
	public bool isScale
	{
		get
		{
			return m_isScale;
		}
	}

	// 拡縮時間
	float m_duration;

	// 経過時間
	float m_elapsedTime;

	// 遅らせる時間
	float m_delay;

	// 拡縮開始スケール
	Vector3 m_startScale;

	// 拡縮終了スケール
	Vector3	m_endScale;

	void Awake()
	{
		enabled = false;
	}

	public void SetScaleTo(Vector3 _endScale,float _duration, float _delay = 0.0f)
	{
		m_isScale = true;
		enabled = true;
		m_elapsedTime = 0.0f;

		m_startScale = transform.localScale;
		m_endScale = _endScale;
		m_duration = _duration;
		m_delay = _delay;

		if (m_duration <= 0) 
		{
			transform.localScale = m_endScale;
			enabled = false;
			return;
		}
	}

	void Update ()
	{
		m_elapsedTime += Time.deltaTime;

		if (m_elapsedTime - m_delay < 0) 
			return;

		// 経過時間が拡縮時間を超えたら
		if (m_elapsedTime - m_delay > m_duration) 
		{
			transform.localScale = m_endScale;
			m_isScale = false;
			enabled = false;
		}

		// 割合
		float rate = m_elapsedTime / m_duration;

		// 割合から出した２点間のスケールを入れる
		transform.localScale = Vector3.Lerp (m_startScale, m_endScale, rate);
	}
}
