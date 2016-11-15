using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimeControl : MonoBehaviour
{

    public float m_TimeRemaining = 60;

    private Text[] text = new Text[2];


    void Start()
    {

        text[0] = GameObject.Find("Canvas/Text").GetComponent<Text>();
        text[1] = GameObject.Find("Canvas/Text2").GetComponent<Text>();
    }

    void Update()
    {

        m_TimeRemaining -= Time.deltaTime;

        if (m_TimeRemaining <= 0.0f)
        {
            m_TimeRemaining = 0.0f;
        }

        int m_IntTimeRemaing = (int)m_TimeRemaining;
        for (int i = 0; i < 2; i++)
        {
            text[i].text = m_IntTimeRemaing.ToString();
        }


    }
}
