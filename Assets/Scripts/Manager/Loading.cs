using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Loading : MonoBehaviour 
{
	Text m_text;
	// 移動時間
	float m_duration;

	// 経過時間
	float m_elapsedTime;

	// 遅らせる時間
	float m_delay;

	// 移動開始座標
	Vector3 m_startRotate;

	// 移動終了座標
	Vector3	m_endPosition;
	void Awake () 
	{
		m_text = gameObject.GetComponent<Text> ();
	}

	void Update () 
	{
		
	}
}
