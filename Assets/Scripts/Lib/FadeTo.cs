using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeTo : MonoBehaviour 
{
	// フェード中かどうかのフラグ
	bool m_isFade;
	public bool isFade
	{
		get
		{
			return m_isFade;
		}
	}
	// レンダラー	
	Renderer m_renderer;

	// フェード時間
	float m_duration;

	// 経過時間
	float m_elapsedTime;

	// 遅らせる時間
	float m_delay;

	// 開始透明度
	float m_startAlpha;

	// 終了透明度
	float m_endAlpha;

	bool m_isForever;
	bool m_isReverse;

	void Awake()
	{
		enabled = false;
		//SetBlinkFadeForever (1.0f);
	}

	public void SetBlinkFadeForever(float _duration)
	{
		m_isForever = true;
		m_isReverse = false;
		m_isFade = true;
		enabled = true;

		m_startAlpha = 1.0f;
		m_endAlpha = 0.0f;

		m_duration = _duration;


		m_renderer = GetComponent<Renderer> ();
		m_startAlpha = m_renderer.material.color.a;
	}

	public void SetFadeTo(float _endAlpha,float _duration, float _delay = 0.0f)
	{
		m_isForever = false;
		m_isFade = true;
		enabled = true;
		m_elapsedTime = 0.0f;


		m_renderer = GetComponent<Renderer> ();
		m_startAlpha = m_renderer.material.color.a;
			
		m_endAlpha = _endAlpha;
		m_duration = _duration;
		m_delay = _delay;

		if (m_duration <= 0) 
		{
			FinishFade ();
		}
	}

	void FinishFade()
	{

		m_renderer.material.color = new Color(
			m_renderer.material.color.r,
			m_renderer.material.color.g,
			m_renderer.material.color.b,
			m_endAlpha);
		
		enabled = false;
	}

	void SetAlphaLerp(float _rate)
	{
		if(m_isForever)
		{
			if (!m_isReverse) {
				m_startAlpha = 1.0f;
				m_endAlpha = 0.0f;
			} else {
				m_startAlpha = 0.0f;
				m_endAlpha = 1.0f;
			}
		}

		m_renderer.material.color = new Color (
			m_renderer.material.color.r,
			m_renderer.material.color.g,
			m_renderer.material.color.b,
			Mathf.Lerp (m_startAlpha, m_endAlpha, _rate));
	}

	void Update ()
	{
		if (!m_isForever) {
			m_elapsedTime += Time.deltaTime;

			if (m_elapsedTime - m_delay < 0)
				return;

			// 経過時間が移動時間を超えたら
			if (m_elapsedTime - m_delay > m_duration) {
				m_isFade = false;
				FinishFade ();
			}

			// 割合
			float rate = m_elapsedTime / m_duration;

			// 割合から出した２点間の座標を入れる
			SetAlphaLerp(rate);
		} 
		else
		{
			m_elapsedTime += Time.deltaTime;
			// 経過時間が移動時間を超えたら
			if (m_elapsedTime > m_duration) {
				m_isReverse = !m_isReverse;
				m_elapsedTime = 0.0f;
			}

			// 割合
			float rate = m_elapsedTime / m_duration;

			// 割合から出した色を入れる
			SetAlphaLerp(rate);
		}
	}
}
