using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SpecialCardButton : MonoBehaviour {

    public int m_playerID=0;
    
    public int[] m_keepSpCard = new int[3];
    private SpecialCard m_usedSpCardState;
    int m_UsedCard;
    public int UsedCard
    {
        get
        {
            return m_UsedCard;
        }
    }

    Transform m_GameManager;
    Transform m_battleManager;

    ColorBlock colorBlocks;
    void StartSpCard(int _specialCardNumber)
    {

        switch (_specialCardNumber)
        {
            case 0:

                m_usedSpCardState = this.gameObject.AddComponent<SpecialCard1>();
                break;

            case 1:
                m_usedSpCardState = this.gameObject.AddComponent<SpecialCard2>();
                break;

            case 2:
                m_usedSpCardState = this.gameObject.AddComponent<SpecialCard3>();
                break;

            case 3:
                m_usedSpCardState = this.gameObject.AddComponent<SpecialCard4>();
                break;

            case 4:
                m_usedSpCardState = this.gameObject.AddComponent<SpecialCard5>();
                break;

            case 5:
                m_usedSpCardState = this.gameObject.AddComponent<SpecialCard6>();
                break;

            case 6:
                m_usedSpCardState = this.gameObject.AddComponent<SpecialCard7>();
                break;

            case 7:
                m_usedSpCardState = this.gameObject.AddComponent<SpecialCard8>();
                break;
        }

        m_usedSpCardState.m_UsedPlayerID = m_playerID;
        m_usedSpCardState.GameReady(m_battleManager,m_GameManager);
    }

    void Awake()
    {
        //ボタン押すことで兵種を切り替える処理を行う。  
        this.GetComponent<Button>().onClick.AddListener(UseSpecialCard);
        m_UsedCard = 0;
        
    }



    void UseSpecialCard()
    {
        bool isStart = m_GameManager.GetComponent<GameManager>().m_startFlag;
        if (m_UsedCard > 2 || !isStart)
        {
            return;
        }

        if (m_usedSpCardState != null)
        {
            Destroy(m_usedSpCardState);
            m_UsedCard--;
            return;
        }
        else {
            StartSpCard(m_keepSpCard[m_UsedCard]);
            m_UsedCard++;
        }
    }


	// Use this for initialization
	void Start () {

        m_GameManager = GameObject.Find("GameManager").transform;
        m_battleManager = GameObject.Find("BattleManager").transform;

        for (int i = 0; i < 3; i++)
        {
            Debug.Log("プレイヤーID");
            Debug.Log(m_playerID);
            if (m_playerID == 1)
            {
                m_keepSpCard[i] = SelectSpecialUIManager.SPECIALCARD_NUMBER_1[i];
            }
            else
            {
                m_keepSpCard[i] = SelectSpecialUIManager.SPECIALCARD_NUMBER_2[i];
            }
        }

    }
	
	// Update is called once per frame
	void Update () {

        colorBlocks = this.GetComponent<Button>().colors;
        if (null == m_usedSpCardState)
        {
            colorBlocks.normalColor = new Color(1f, 1f, 1f, 1f);
            colorBlocks.highlightedColor = new Color(1f, 1f, 1f, 1f);
            colorBlocks.pressedColor = new Color(1f, 1f, 1f, 1f);
            colorBlocks.disabledColor = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        else
        {
            colorBlocks.normalColor = new Color(0.5f, 0.5f, 0.5f, 1f);
            colorBlocks.highlightedColor = new Color(0.5f, 0.5f, 0.5f, 1f);
            colorBlocks.pressedColor = new Color(0.5f, 0.5f, 0.5f, 1f);
            colorBlocks.disabledColor = new Color(0.5f, 0.5f, 0.5f, 1f);
        }
        this.GetComponent<Button>().colors = colorBlocks;
    }
}
