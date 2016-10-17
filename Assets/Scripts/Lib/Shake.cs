using UnityEngine;
using System.Collections;

// オブジェクトを揺らすクラス

public class Shake : MonoBehaviour 
{
	// 揺れの減衰量
	float m_shakeDecay = 0.002f;
	// 揺れの幅
	float m_shakeRange = 0.3f;
	Vector3 m_originPosition;
	Quaternion m_originRotation;
	void Update () 
	{
		if (m_shakeRange > 0) 
		{
			transform.position = m_originPosition + Random.insideUnitSphere * m_shakeRange;
			transform.rotation = new Quaternion (
				m_originRotation.x + Random.Range (-m_shakeRange, m_shakeRange) * 2.0f,
				m_originRotation.y + Random.Range (-m_shakeRange, m_shakeRange) * 2.0f,
				m_originRotation.z + Random.Range (-m_shakeRange, m_shakeRange) * 2.0f,
				m_originRotation.w + Random.Range (-m_shakeRange, m_shakeRange) * 2.0f);

			// 揺れ幅を減衰させる
			m_shakeRange -= m_shakeDecay;
		}
	}
	public void ShakeObject()
	{
		m_shakeRange = 0.3f;
		m_shakeDecay = 0.002f;
		m_originPosition = transform.position;
		m_originRotation = transform.rotation;
	}
}
