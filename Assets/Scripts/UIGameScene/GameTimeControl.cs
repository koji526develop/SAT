using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimeControl : MonoBehaviour {

    public float m_TimeRemaining;

    private Slider m_blueTimeSliber;
    private Slider m_redTimeSliber;

	// Use this for initialization
	void Start () {

        //青と赤ゲージのコンポーネントをそれぞれ検索。格納する。
        m_blueTimeSliber        = transform.FindChild("BlueTimeGauge").GetComponent<Slider>();
        m_redTimeSliber         = transform.FindChild("RedTimeGauge").GetComponent<Slider>();

        m_blueTimeSliber.maxValue = m_redTimeSliber.maxValue    = m_TimeRemaining;
        m_blueTimeSliber.value    = m_redTimeSliber.value       = m_TimeRemaining;

    }

    // Update is called once per frame
    void Update () {

        m_TimeRemaining -= Time.deltaTime;

        if (m_TimeRemaining <= 0.0f)
        {
            m_TimeRemaining = 0.0f;
        }
        m_blueTimeSliber.value = m_redTimeSliber.value = m_TimeRemaining;

    }
}
