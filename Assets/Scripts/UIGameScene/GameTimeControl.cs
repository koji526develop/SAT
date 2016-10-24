using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimeControl : MonoBehaviour {

    public  float m_TimeRemaining=60;
   
    private Text text;
   

	// Use this for initialization
	void Start () {

        text = GameObject.Find("Canvas/Text").GetComponent<Text>();
      
    }

    // Update is called once per frame
    void Update () {

        m_TimeRemaining -= Time.deltaTime;

        if (m_TimeRemaining <= 0.0f)
        {
            m_TimeRemaining = 0.0f;
        }

        int m_IntTimeRemaing = (int)m_TimeRemaining;
        text.text = m_IntTimeRemaing.ToString();
    
    }
}
