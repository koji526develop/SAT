using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimeControl : MonoBehaviour
{

    public float m_TimeRemaining = 60;

    private Text text;

    void Awake()
    {
        text = gameObject.GetComponent<Text>();
    }

    void Start()
    {
        
    }

    void Update()
    {

        m_TimeRemaining -= Time.deltaTime;

        if (m_TimeRemaining <= 0.0f)
        {
            m_TimeRemaining = 0.0f;
        }

        int m_IntTimeRemaing = (int)m_TimeRemaining;

        text.text = m_IntTimeRemaing.ToString();
    }
}
