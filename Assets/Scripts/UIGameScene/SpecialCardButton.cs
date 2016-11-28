using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SpecialCardButton : MonoBehaviour {

    public int m_playerID=0;
    
    public int[] m_keepSpCard = new int[3];
    private SpecialCard m_usedSpCardState;
    private int m_UsedCard;

    Transform m_GameManager;
    Transform m_battleManager;


    void StartSpCard(int _specialCardNumber)
    {

        switch (_specialCardNumber)
        {
            case 1:
			this.gameObject.AddComponent<SpecialCard1>().m_UsedPlayerID = m_playerID;
                break;

            case 2:
                this.gameObject.AddComponent<SpecialCard2>().m_UsedPlayerID = m_playerID;
                break;

            case 3:
                this.gameObject.AddComponent<SpecialCard3>().m_UsedPlayerID = m_playerID;
                break;

            case 4:
                this.gameObject.AddComponent<SpecialCard4>().m_UsedPlayerID = m_playerID;
                break;

            case 5:
                this.gameObject.AddComponent<SpecialCard5>().m_UsedPlayerID = m_playerID;
                break;

            case 6:
                this.gameObject.AddComponent<SpecialCard6>().m_UsedPlayerID = m_playerID;
                break;

            case 7:
                this.gameObject.AddComponent<SpecialCard7>().m_UsedPlayerID = m_playerID;
                break;

            case 8:
                this.gameObject.AddComponent<SpecialCard8>().m_UsedPlayerID = m_playerID;
                break;
        }

        m_usedSpCardState = this.GetComponent<SpecialCard>();
        m_usedSpCardState.GameReady(m_battleManager,m_GameManager);
    }

    void Awake()
    {
        //ボタン押すことで兵種を切り替える処理を行う。  
        this.GetComponent<Button>().onClick.AddListener(UseSpecialCard);
        m_UsedCard = 0;

        m_keepSpCard[0] = 1;
        m_keepSpCard[1] = 2;
        m_keepSpCard[2] = 3;
        
    }

    void UseSpecialCard()
    {
        if (m_UsedCard > 2)
        {
            return;
        }

        if (m_usedSpCardState != null)
        {
            Destroy(m_usedSpCardState);
            m_UsedCard--;
            return;
        }

        StartSpCard(m_keepSpCard[m_UsedCard]);
        
        m_UsedCard++;
    }


	// Use this for initialization
	void Start () {

        m_GameManager = GameObject.Find("GameManager").transform;
        m_battleManager = GameObject.Find("BattleManager").transform;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
