using UnityEngine;
using System.Collections;

// オブジェクトを指定の角度に回転させるクラス

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
	Vector3 m_startRotation;

	// 回転終了角度
	Vector3	m_endRotation;

	Vector3 m_diffRotation;

	void Awake()
	{
		enabled = false;
	}

	public void SetRotateTo(Vector3 _endRotation,float _duration, float _delay = 0.0f)
	{
		m_isRotate = true;
		enabled = true;
		m_elapsedTime = 0.0f;


		m_startRotation = transform.rotation.eulerAngles;
		m_endRotation = _endRotation;

		m_diffRotation = m_endRotation - m_startRotation;

		m_duration = _duration;
		m_delay = _delay;

		if (m_duration <= 0) 
		{
			transform.rotation = Quaternion.Euler(m_endRotation);
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
		/*if (m_elapsedTime - m_delay > m_duration) 
		{
			//transform.LookAt(m_diffRotation);

		}*/

		// 割合
		float rate = m_elapsedTime / m_duration;

		/*if (m_endRotation.x > 180) 
		{
			m_endRotation.x = m_endRotation.x - 180;
		}*/

		if (m_endRotation.x > 0)
		{
			if (Mathf.Abs(Mathf.DeltaAngle (transform.eulerAngles.x, m_endRotation.x)) > 0.1f) {
				transform.Rotate (new Vector3 (5f, 0f, 0f));
			} else 
			{
				m_isRotate = false;
				enabled = false;
			}
		} 
		else
		{
			if (Mathf.DeltaAngle (transform.eulerAngles.x, m_endRotation.x) < -0.1f) {
				transform.Rotate (new Vector3 (-5f, 0f, 0f));
			} else {
				m_isRotate = false;
				enabled = false;
			}
		}


	}
}
