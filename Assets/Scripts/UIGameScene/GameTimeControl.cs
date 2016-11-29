using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimeControl : MonoBehaviour
{

    public float m_TimeRemaining = MyUtility.GAME_TIME;
    GameManager m_gameManagar;

    private Text text;

    void Awake()
    {
        text = gameObject.GetComponent<Text>();
        
    }

    void Start()
    {
        m_gameManagar = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (!m_gameManagar.m_startFlag)
        {
            return;
        }

        m_TimeRemaining -= Time.deltaTime;

        if (m_TimeRemaining <= 0.0f)
        {
            m_TimeRemaining = 0.0f;
            SceneChanger sChange = new SceneChanger();
            sChange.ChangeToResult();
        }

        int m_IntTimeRemaing = (int)m_TimeRemaining;

        text.text = m_IntTimeRemaing.ToString();
    }
}
