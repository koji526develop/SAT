﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SpecialCardButton : MonoBehaviour {

    public int m_playerID=0;
    
    public SpecialCard.SpCardTag[] m_keepSpCard = new SpecialCard.SpCardTag[3];
    private SpecialCard m_usedSpCardState;
    private int m_UsedCard;

    Transform m_GameManager;
    Transform m_battleManager;


    void StartSpCard(SpecialCard.SpCardTag _getStartTag)
    {

        switch (_getStartTag)
        {
            case SpecialCard.SpCardTag.SoliderChange:
                this.gameObject.AddComponent<SpecialCard8>().m_UsedPlayerID = m_playerID;
                break;

            case SpecialCard.SpCardTag.Archer:
                this.gameObject.AddComponent<Archer>().m_UsedPlayerID = m_playerID;
                break;

            case SpecialCard.SpCardTag.MobSpawner:
                this.gameObject.AddComponent<MobSpawner>().m_UsedPlayerID = m_playerID;
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

        m_keepSpCard[0] = SpecialCard.SpCardTag.SoliderChange;
        m_keepSpCard[1] = SpecialCard.SpCardTag.Archer;
        m_keepSpCard[2] = SpecialCard.SpCardTag.MobSpawner;

       
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
