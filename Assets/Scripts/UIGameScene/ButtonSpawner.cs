using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ButtonSpawner : MonoBehaviour
{
    Transform m_battleManager;
    GameManager m_gameManager;
    ScoreManager m_scoreManager;

    public int m_PlayerID;
    public int m_ButtonID;
    public Character.CharacterType m_type;

    private int m_nowTouchNumber;
    private bool m_startFlag = false;
    private bool m_spawnerFlag=false;

    public float m_intervalTime =0.0f;

    //特殊カードのフラグ
    public bool m_soliderStop = false;

    public bool m_soliderDouble = false;
    public bool m_soliderDoubleStart = false;
    private Character.CharacterType m_beforeType;

    public Image m_changeSprite;
    Sprite[] m_sprite = new Sprite[6];
    void Awake()
    {

        this.transform.tag = "SoliderButton";

        Transform gameManagerTrans=GameObject.Find("GameManager").transform;

        this.GetComponent<Button>().onClick.AddListener(ResetFlag);

        m_gameManager = gameManagerTrans.GetComponent<GameManager>();
        m_scoreManager = gameManagerTrans.GetComponent<ScoreManager>();

        //スライドで出現処理を行えるようにする処理を行う。
        EventTrigger dragEndtrigger = gameObject.AddComponent<EventTrigger>();

        //スライドで出現処理を行えるようにする処理を行う。
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerExit;
        entry.callback.AddListener((data) => { ExitFlag(); });
        dragEndtrigger.triggers.Add(entry);

        EventTrigger.Entry entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.EndDrag;
        entry2.callback.AddListener((data) => { Spawner(); });
        dragEndtrigger.triggers.Add(entry2);

        //スライドで出現処理を行えるようにする処理を行う。
        EventTrigger.Entry entry3 = new EventTrigger.Entry();
        entry3.eventID = EventTriggerType.PointerDown;
        entry3.callback.AddListener((data) => { StartFlag(); });
        dragEndtrigger.triggers.Add(entry3);

        //スライドで出現処理を行えるようにする処理を行う。
        EventTrigger.Entry entry4 = new EventTrigger.Entry();
        entry4.eventID = EventTriggerType.PointerEnter;
        entry4.callback.AddListener((data) => { EnterFlag(); });
        dragEndtrigger.triggers.Add(entry4);


    }

    void ResetFlag()
    {
        m_nowTouchNumber = 99;
        m_startFlag = false;
        m_spawnerFlag = false;

       
        Image image = this.GetComponent<Image>();

        m_changeSprite.sprite = m_sprite[0];

    }

    public void StartFlag()
    {
        if (m_intervalTime <= 0.0f && !m_soliderStop && m_gameManager.m_startFlag)
        {
            for (int i = 0; i <= TouchManager.touchCount; i++)
            {
                if (TouchManager.GetTouchInfo(i) == TouchInfo.Began)
                {
                    m_nowTouchNumber = i;
                    Debug.Log("タッチしましたー＞" + i);
                    

                    Image image = this.GetComponent<Image>();

                    m_changeSprite.sprite = m_sprite[1];

                    m_startFlag = true;
                    return;
                }
            }
        }
    }

    public void EnterFlag()
    {
        m_spawnerFlag = false;

        if (!m_startFlag) { return; }
        Image image = this.GetComponent<Image>();

        m_changeSprite.sprite = m_sprite[1];
    }

    public void ExitFlag()
    {

        if (m_startFlag)
        {
            m_spawnerFlag = true;

        }
        else
        {
            ResetFlag();
        }
    }

    //兵士出現処理
    public void Spawner()
    {
		
        //兵士が残っていれば生成
		if(SoldierNumCheck(m_PlayerID,m_type) && m_spawnerFlag)
        {
            if (m_PlayerID == 1)
            {
                if (Mathf.Abs(TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber)) > Mathf.Abs(TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber)))
                {

                    if (TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber) > 0)
                    {
                        m_type = Character.CharacterType.Ax;
                    }
                    else
                    {
                        m_type = Character.CharacterType.Spear;
                    }
                }
                else
                {
                    if (TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber) > 0)
                    {
                        m_type = Character.CharacterType.Sword;
                    }
                    else
                    {
                        m_type = Character.CharacterType.Shield;
                    }
                }
            }
            else
            {
                if (Mathf.Abs(TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber)) > Mathf.Abs(TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber)))
                {

                    if (TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber) > 0)
                    {
                        m_type = Character.CharacterType.Spear;
                    }
                    else
                    {
                        m_type = Character.CharacterType.Ax;
                    }
                }
                else
                {
                    if (TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber) > 0)
                    {
                        m_type = Character.CharacterType.Shield;
                    }
                    else
                    {
                        m_type = Character.CharacterType.Sword;
                    }
                }
            }


            Character character = Character.CreateObject(m_battleManager, m_type, Character.GetSpawnPosition(m_PlayerID, m_ButtonID), m_PlayerID).GetComponent<Character>();
            character.mapColumn = m_ButtonID;
            m_scoreManager.SpawnerCount(m_PlayerID, m_ButtonID);
            Debug.Log("兵士出す");

            if (m_soliderDouble)
            {
                m_beforeType = m_type;
                m_soliderDouble = false;
                m_soliderDoubleStart = true;
            }

           //インターバル時間を追加
            m_intervalTime = MyUtility.SPAWNER_INTERVAL_TIME;
           
        }
        ResetFlag();
    }

    //兵士が残っているかチェック
    //残っていれば true が返る
    bool SoldierNumCheck(int _playerID,Character.CharacterType _type)
    {
        switch (_type)
        {
            case Character.CharacterType.Sword:
                if (_playerID == 1)if (SelectUIManager.SWORD_NUM_1 <= 0)return false;
                if (_playerID == 2) if (SelectUIManager.SWORD_NUM_2 <= 0) return false;
                break;
            case Character.CharacterType.Spear:
                if (_playerID == 1) if (SelectUIManager.SPEAR_NUM_1 <= 0) return false;
                if (_playerID == 2) if (SelectUIManager.SPEAR_NUM_2 <= 0) return false;

                break;
            case Character.CharacterType.Ax:
                if (_playerID == 1) if (SelectUIManager.AX_NUM_1 <= 0) return false;
                if (_playerID == 2) if (SelectUIManager.AX_NUM_2 <= 0) return false;

                break;
            case Character.CharacterType.Shield:
                if (_playerID == 1) if (SelectUIManager.SHIELD_NUM_1 <= 0) return false;
                if (_playerID == 2) if (SelectUIManager.SHIELD_NUM_2 <= 0) return false;

                break;
        }

        return true;
    }

	//兵士を出してもよいか前方をチェック
	bool CheckFrontArea(int _playerID,Character.CharacterType _type)
	{
		return false;
	}

    //兵士の選択を変える処理に入る
    public void SolderImageChange()
    {
        Image image = this.GetComponent<Image>();


        if (m_PlayerID == 1)
        {
            if (Mathf.Abs(TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber)) > Mathf.Abs(TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber)))
            {

                if (TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber) > 0)
                {
                    m_changeSprite.sprite = m_sprite[2];
                    if (m_PlayerID == 1)
                    {
                        m_type = Character.CharacterType.Ax;
                    }
                    else
                    {
                        m_type = Character.CharacterType.Spear;
                    }
                }
                else
                {
                    m_changeSprite.sprite = m_sprite[3];
                    if (m_PlayerID == 1)
                    {
                        m_type = Character.CharacterType.Spear;
                    }
                    else
                    {
                        m_type = Character.CharacterType.Ax;
                    }
                }
            }
            else
            {
                if (TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber) > 0)
                {
                    m_changeSprite.sprite = m_sprite[4];
                    if(m_PlayerID==1)
                    m_type = Character.CharacterType.Sword;
                    else
                        m_type = Character.CharacterType.Shield;
                }
                else
                {
                    m_changeSprite.sprite = m_sprite[5];
                    if(m_PlayerID==1)
                    m_type = Character.CharacterType.Shield;
                    else
                        m_type = Character.CharacterType.Sword;
                }
            }
        }
        else {
            if (Mathf.Abs(TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber)) > Mathf.Abs(TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber)))
            {

                if (TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber) > 0)
                {
                    m_changeSprite.sprite = m_sprite[2];
                    m_type = Character.CharacterType.Spear;
                }
                else
                {
                    m_changeSprite.sprite = m_sprite[3];
                    m_type = Character.CharacterType.Ax;
                }
            }
            else
            {
                if (TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber) > 0)
                {
                    m_changeSprite.sprite = m_sprite[4];
                    m_type = Character.CharacterType.Shield;
                }
                else
                {
                    m_changeSprite.sprite = m_sprite[5];
                    m_type = Character.CharacterType.Sword;
                }
            }
        }
    }



    void Start()
    {
        //初期設定を行う。
        m_battleManager = GameObject.FindWithTag("BattleManager").transform;
        m_type = Character.CharacterType.Sword;

        Button btn = this.GetComponent<Button>();

        ColorBlock colorBlocks = btn.colors;
        colorBlocks.normalColor = new Color(1f, 1f, 1f, 0f);
        colorBlocks.highlightedColor = new Color(1f, 1f, 1f, 0f);
        colorBlocks.pressedColor = new Color(1f, 1f, 1f, 0f);
        colorBlocks.disabledColor = new Color(1f, 1f, 1f, 0f);

        btn.colors = colorBlocks;

        if (m_PlayerID == 1)
        {
            m_sprite[0] = Resources.Load("UI/Game/flick", typeof(Sprite)) as Sprite;
            m_sprite[1] = Resources.Load("UI/Game/center_active", typeof(Sprite)) as Sprite;
            m_sprite[2] = Resources.Load("UI/Game/ax_active", typeof(Sprite)) as Sprite;
            m_sprite[3] = Resources.Load("UI/Game/spear_active", typeof(Sprite)) as Sprite;
            m_sprite[4] = Resources.Load("UI/Game/sword_active", typeof(Sprite)) as Sprite;
            m_sprite[5] = Resources.Load("UI/Game/shield_active", typeof(Sprite)) as Sprite;
        }
        else
        {
            m_sprite[0] = Resources.Load("UI/Game/flick2", typeof(Sprite)) as Sprite;
            m_sprite[1] = Resources.Load("UI/Game/center_active2", typeof(Sprite)) as Sprite;
            m_sprite[2] = Resources.Load("UI/Game/spear_active2", typeof(Sprite)) as Sprite;
            m_sprite[3] = Resources.Load("UI/Game/ax_active2", typeof(Sprite)) as Sprite;
            m_sprite[4] = Resources.Load("UI/Game/shield_active2", typeof(Sprite)) as Sprite;
            m_sprite[5] = Resources.Load("UI/Game/sword_active2", typeof(Sprite)) as Sprite;
        }

        ResetFlag();
    }

    void Update()
    {

        if (m_intervalTime > 0.0f)
        {
            m_intervalTime -= Time.deltaTime;
        }
        else
        {
            m_intervalTime = 0.0f;

            if (m_soliderDoubleStart)
            {

                Character character = Character.CreateObject(m_battleManager, m_type, Character.GetSpawnPosition(m_PlayerID, m_ButtonID), m_PlayerID).GetComponent<Character>();
                character.mapColumn = m_ButtonID;

                switch (m_beforeType)
                {
                    case Character.CharacterType.Sword:
                        if (m_PlayerID == 1) SelectUIManager.SWORD_NUM_1++;
                        if (m_PlayerID == 2) SelectUIManager.SWORD_NUM_2++;
                        break;
                    case Character.CharacterType.Spear:
                        if (m_PlayerID == 1) SelectUIManager.SPEAR_NUM_1++;
                        if (m_PlayerID == 2) SelectUIManager.SPEAR_NUM_2++;
                        break;
                    case Character.CharacterType.Ax:
                        if (m_PlayerID == 1) SelectUIManager.AX_NUM_1++;
                        if (m_PlayerID == 2) SelectUIManager.AX_NUM_2++;

                        break;
                    case Character.CharacterType.Shield:
                        if (m_PlayerID == 1) SelectUIManager.SHIELD_NUM_1++;
                        if (m_PlayerID == 2) SelectUIManager.SHIELD_NUM_2++;

                        break;
                }
                m_soliderDoubleStart = false;
            }
        }

        if ( m_spawnerFlag)
        {
            SolderImageChange();
        }
    }
}
