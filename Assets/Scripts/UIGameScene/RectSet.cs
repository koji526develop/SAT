using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class RectSet : MonoBehaviour {

    public int m_playerID;
    public int m_columnID;

    public bool m_isSelected=false;
    public float alpha = 1.0f;
    public void SetState(int _playerID,int _columnID)
    {
        m_playerID = _playerID;
        m_columnID = _columnID;
    }

	// Use this for initialization
	void Start () {

        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

      
        if (!m_isSelected)
        {
            this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            alpha = alpha + Time.deltaTime * 2;
            this.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.Abs(Mathf.Cos(alpha)) +0.3f);
        }

	}
}
