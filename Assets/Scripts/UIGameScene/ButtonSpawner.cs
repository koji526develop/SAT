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

    Canvas m_canvas;

    public int m_PlayerID;
    public int m_ButtonID;
    public Character.CharacterType m_type;

    private int m_nowTouchNumber;
    private bool m_startFlag = false;
    private bool m_spawnerFlag = false;

    public float m_intervalTime = 0.0f;

    //特殊カードのフラグ
    public bool m_soliderStop = false;

    public bool m_soliderDouble = false;
    public bool m_soliderDoubleStart = false;
    private Character.CharacterType m_beforeType;

    public Image m_changeSprite;
    Sprite[] m_sprite = new Sprite[6];

    private int m_specialSoldier = 0;

    void Awake()
    {

        this.transform.tag = "SoliderButton";

        Transform gameManagerTrans = GameObject.Find("GameManager").transform;

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
                    //Debug.Log("タッチしましたー＞" + i);

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
        //特殊カード3兵士増強
        if (m_soliderDouble)
        {
            m_beforeType = m_type;
            m_soliderDouble = false;
            m_soliderDoubleStart = true;
            m_specialSoldier = 2;
        }
        //兵士が残っていれば生成
        else if (SoldierNumCheck(m_PlayerID, m_type) && m_spawnerFlag)
        {

            m_changeSprite.transform.gameObject.AddComponent<Anten>();

            if (m_PlayerID == 1)
            {
                if (Mathf.Abs(TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber)) > Mathf.Abs(TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber)))
                {

                    if (TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber) > 0)
                    {
                        m_type = Character.CharacterType.Sword;
                    }
                    else
                    {
                        m_type = Character.CharacterType.Shield;
                    }
                }
                else
                {
                    if (TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber) > 0)
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
                if (Mathf.Abs(TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber)) > Mathf.Abs(TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber)))
                {

                    if (TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber) > 0)
                    {
                        m_type = Character.CharacterType.Shield;
                    }
                    else
                    {
                        m_type = Character.CharacterType.Sword;
                    }
                }
                else
                {
                    if (TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber) > 0)
                    {
                        m_type = Character.CharacterType.Ax;
                    }
                    else
                    {
                        m_type = Character.CharacterType.Spear;
                    }
                }
            }


            Character character = Character.CreateObject(m_battleManager, m_type, Character.GetSpawnPosition(m_PlayerID, m_ButtonID), m_PlayerID).GetComponent<Character>();
            character.mapColumn = m_ButtonID;
            m_scoreManager.SpawnerCount(m_PlayerID, m_ButtonID);
            //Debug.Log("兵士出す");
			//兵士出現音
            //AudioManager.m_instance.PlaySE("button_SE");

            //インターバル時間を追加
            m_intervalTime = MyUtility.SPAWNER_INTERVAL_TIME;

        }
        ResetFlag();
    }

    //兵士が残っているかチェック
    //残っていれば true が返る
    bool SoldierNumCheck(int _playerID, Character.CharacterType _type)
    {
        switch (_type)
        {
            case Character.CharacterType.Sword:
                if (_playerID == 1) if (SelectUIManager.SWORD_NUM_1 <= 0) return false;
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
    bool CheckFrontArea(int _playerID, Character.CharacterType _type)
    {
        return false;
    }

    //兵士の選択を変える処理に入る
    public void SolderImageChange()
    {
        if (m_PlayerID == 1)
        {
            if (Mathf.Abs(TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber)) > Mathf.Abs(TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber)))
            {

                if (TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber) > 0)
                {
                    m_changeSprite.sprite = m_sprite[2];
                    if (m_PlayerID == 1)
                    {
                        m_type = Character.CharacterType.Sword;
                    }
                    else
                    {
                        m_type = Character.CharacterType.Shield;
                    }
                }
                else
                {
                    m_changeSprite.sprite = m_sprite[3];
                    if (m_PlayerID == 1)
                    {
                        m_type = Character.CharacterType.Shield;
                    }
                    else
                    {
                        m_type = Character.CharacterType.Sword;
                    }
                }
            }
            else
            {
                if (TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber) > 0)
                {
                    m_changeSprite.sprite = m_sprite[4];
                    if (m_PlayerID == 1)
                        m_type = Character.CharacterType.Spear;
                    else
                        m_type = Character.CharacterType.Ax;
                }
                else
                {
                    m_changeSprite.sprite = m_sprite[5];
                    if (m_PlayerID == 1)
                        m_type = Character.CharacterType.Ax;
                    else
                        m_type = Character.CharacterType.Spear;
                }
            }
        }
        else
        {
            if (Mathf.Abs(TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber)) > Mathf.Abs(TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber)))
            {

                if (TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber) > 0)
                {
                    m_changeSprite.sprite = m_sprite[2];
                    m_type = Character.CharacterType.Shield;
                }
                else
                {
                    m_changeSprite.sprite = m_sprite[3];
                    m_type = Character.CharacterType.Sword;
                }
            }
            else
            {
                if (TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber) > 0)
                {
                    m_changeSprite.sprite = m_sprite[4];
					m_type = Character.CharacterType.Ax;
                }
                else
                {
                    m_changeSprite.sprite = m_sprite[5];
					m_type = Character.CharacterType.Spear;
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
            m_sprite[1] = Resources.Load("UI/Game/flick_center", typeof(Sprite)) as Sprite;
            m_sprite[2] = Resources.Load("UI/Game/flick_sword", typeof(Sprite)) as Sprite;
            m_sprite[3] = Resources.Load("UI/Game/flick_shield", typeof(Sprite)) as Sprite;
            m_sprite[4] = Resources.Load("UI/Game/flick_spear", typeof(Sprite)) as Sprite;
            m_sprite[5] = Resources.Load("UI/Game/flick_ax", typeof(Sprite)) as Sprite;
        }
        else
        {
            m_sprite[0] = Resources.Load("UI/Game/flick2", typeof(Sprite)) as Sprite;
            m_sprite[1] = Resources.Load("UI/Game/flick_center2", typeof(Sprite)) as Sprite;
            m_sprite[2] = Resources.Load("UI/Game/flick_shield2", typeof(Sprite)) as Sprite;
            m_sprite[3] = Resources.Load("UI/Game/flick_sword2", typeof(Sprite)) as Sprite;
            m_sprite[4] = Resources.Load("UI/Game/flick_ax2", typeof(Sprite)) as Sprite;
            m_sprite[5] = Resources.Load("UI/Game/flick_spear2", typeof(Sprite)) as Sprite;
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
                //Debug.Log("兵士増強");
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

                m_specialSoldier -= 1;
                //インターバル時間を追加
                m_intervalTime = MyUtility.SPAWNER_INTERVAL_TIME;

                if (m_specialSoldier <= 0) m_soliderDoubleStart = false;
            }


        }

        if (m_spawnerFlag)
        {
            SolderImageChange();
        }
    }
}
