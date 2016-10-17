using UnityEngine;
using System.Collections;

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

	public enum RendererType
	{
		Mesh,
		Sprite
	}
	RendererType m_rendererType;

	MeshRenderer m_meshRenderer;
	SpriteRenderer m_spriteRenderer;

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
	}

	public void SetBlinkFadeForever(RendererType _rendererType, float _duration, float _delay = 0.0f)
	{
		/*m_isForever = true;
		m_isReverse = false;
		m_isFade = true;
		enabled = true;
		m_rendererType = _rendererType;*/
	}

	public void SetFadeTo(RendererType _rendererType, float _endAlpha,float _duration, float _delay = 0.0f)
	{
		m_isForever = false;
		m_isFade = true;
		enabled = true;
		m_elapsedTime = 0.0f;

		m_rendererType = _rendererType;

		switch (m_rendererType)
		{
		case RendererType.Mesh:
			m_meshRenderer = GetComponent<MeshRenderer> ();
			m_startAlpha = m_meshRenderer.material.color.a;
			break;
		case RendererType.Sprite:
			m_spriteRenderer = GetComponent<SpriteRenderer> ();
			m_startAlpha = m_spriteRenderer.material.color.a;
			break;
		}
			
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
		switch (m_rendererType)
		{
		case RendererType.Mesh:
			m_meshRenderer.material.color = new Color(
				m_meshRenderer.material.color.r,
				m_meshRenderer.material.color.g,
				m_meshRenderer.material.color.b,
				m_endAlpha);
			break;
		case RendererType.Sprite:
			m_spriteRenderer.material.color = new Color(
				m_spriteRenderer.material.color.r,
				m_spriteRenderer.material.color.g,
				m_spriteRenderer.material.color.b,
				m_endAlpha);
			break;
		}
		enabled = false;
	}

	void Update ()
	{
		m_elapsedTime += Time.deltaTime;

		if (m_elapsedTime - m_delay < 0) 
			return;

		// 経過時間が移動時間を超えたら
		if (m_elapsedTime - m_delay > m_duration) 
		{
			m_isFade = false;
			FinishFade ();
		}

		// 割合
		float rate = m_elapsedTime / m_duration;

		// 割合から出した２点間の座標を入れる
		switch (m_rendererType)
		{
		case RendererType.Mesh:
			m_meshRenderer.material.color = new Color(
				m_meshRenderer.material.color.r,
				m_meshRenderer.material.color.g,
				m_meshRenderer.material.color.b,
				Mathf.Lerp(m_startAlpha,m_endAlpha,rate));
			break;
		case RendererType.Sprite:
			m_spriteRenderer.material.color = new Color(
				m_spriteRenderer.material.color.r,
				m_spriteRenderer.material.color.g,
				m_spriteRenderer.material.color.b,
				Mathf.Lerp(m_startAlpha,m_endAlpha,rate));
			break;
		}
	}
}
