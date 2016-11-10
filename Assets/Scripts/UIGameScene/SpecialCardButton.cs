using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SpecialCardButton : MonoBehaviour {

    public int m_playerID=0;
    public SpecialCard m_usedSpCardState;


    void Awake()
    {
        //ボタン押すことで兵種を切り替える処理を行う。  
        this.GetComponent<Button>().onClick.AddListener(UseSpecialCard);
    }

    void UseSpecialCard()
    {
        if (m_usedSpCardState != null)
        {
            Destroy(m_usedSpCardState);
            return;
        }
        this.gameObject.AddComponent<Archer>().m_UsedPlayerID = m_playerID;

        m_usedSpCardState = this.GetComponent<SpecialCard>();
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
