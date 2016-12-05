using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Anten : MonoBehaviour {

    public Image m_changeSprite;

    public float m_maxTime = 2.0f;
    float m_nowTime = 0.0f;

    // Use this for initialization
    void Start () {

        m_changeSprite=this.gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        m_nowTime += Time.deltaTime;

        if (m_nowTime > m_maxTime)
        {
            m_changeSprite.color = new Color(1f, 1f, 1f, 1f);
            Destroy(this);
        }
        else
        {
            float colorNum = m_nowTime / m_maxTime;
            m_changeSprite.color = new Color(colorNum, colorNum, colorNum, 1f);
        }
	
	}
}
