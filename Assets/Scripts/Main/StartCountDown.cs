using UnityEngine;
using System.Collections;
using UnityEngine.UI;


//ゲーム始めのカウントダウン

public class StartCountDown : MonoBehaviour
{
    bool m_startFlag = false;
    float m_countTime = 3.0f;

    bool StartFlag
    {
        get
        {
            return m_startFlag;
        }
    }

    void Awake()
    {

    }

	void Start ()
    {
	
	}
	
	void Update ()
    {
        m_countTime -= Time.deltaTime;

        if(m_countTime <= 0.0f)
        {
            m_startFlag = true;
        }
	}
}
