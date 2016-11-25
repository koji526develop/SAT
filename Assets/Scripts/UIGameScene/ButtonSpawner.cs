using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ButtonSpawner : MonoBehaviour
{
    Transform m_battleManager;
    ScoreManager m_scoreManager;

    public int m_PlayerID;
    public int m_ButtonID;
    public Character.CharacterType m_type;

    private int m_nowTouchNumber;
    private bool m_startFlag = false;
    private bool m_spawnerFlag=false;

    public float m_intervalTime =0.0f;

    Color color = new Color(1f, 1f, 1f, 0f);

    void Awake()
    {

        m_scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();

        //ボタン押すことで兵種を切り替える処理を行う。  
        this.GetComponent<Button>().onClick.AddListener(ResetFlag);

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
        //EventTrigger.Entry entry4 = new EventTrigger.Entry();
        //entry4.eventID = EventTriggerType.PointerClick;
        //entry4.callback.AddListener((data) => { ResetFlag(); });
        //dragEndtrigger.triggers.Add(entry4);
    }

    void ResetFlag()
    {
        m_nowTouchNumber = 99;
        m_startFlag = false;
        m_spawnerFlag = false;

        Button btn = this.GetComponent<Button>();
        ColorBlock colorBlocks = btn.colors;
        colorBlocks.normalColor = new Color(1f, 1f, 1f, 0f);
        colorBlocks.highlightedColor = new Color(1f, 1f, 1f, 0f);
        colorBlocks.pressedColor = new Color(1f, 1f, 1f, 1f);
        colorBlocks.disabledColor = new Color(1f, 1f, 1f, 1f);

        btn.colors = colorBlocks;
    }

    public void StartFlag()
    {
        if (m_intervalTime <= 0.0f)
        {
            for (int i = 0; i <= TouchManager.touchCount; i++)
            {
                if (TouchManager.GetTouchInfo(i) == TouchInfo.Began)
                {
                    m_nowTouchNumber = i;
                    Debug.Log("タッチしましたー＞" + i);
                    m_startFlag = true;
                    Button btn = this.GetComponent<Button>();
                    ColorBlock colorBlocks = btn.colors;
                    colorBlocks.normalColor = new Color(1f, 1f, 1f, 1f);
                    colorBlocks.highlightedColor = new Color(1f, 1f, 1f, 1f);
                    btn.colors = colorBlocks;
                    return;
                }
            }
        }
    }

    public void ExitFlag()
    {

        if (m_startFlag)
        {

            if (m_PlayerID == 1)
            {
                    m_spawnerFlag = true;
                    return;

            }
            else
            {
                    m_spawnerFlag = true;
                    return;
            }
        }
        ResetFlag();
    }

    //兵士出現処理
    public void Spawner()
    {
		
        //兵士が残っていれば生成
		if(SoldierNumCheck(m_PlayerID,m_type) && m_spawnerFlag)
        {
            if (m_PlayerID == 1)
            {
                if( Mathf.Abs(TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber))> Mathf.Abs(TouchManager.GetTouchMoveDistanceY(m_nowTouchNumber))){

                    if (TouchManager.GetTouchMoveDistanceX(m_nowTouchNumber)>0)
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
                        m_type = Character.CharacterType.Shield;
                    }
                    else
                    {
                        m_type = Character.CharacterType.Sword;
                    }
                }
            }
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


            Character character = Character.CreateObject(m_battleManager, m_type, Character.GetSpawnPosition(m_PlayerID, m_ButtonID), m_PlayerID).GetComponent<Character>();
            character.mapColumn = m_ButtonID;
            m_scoreManager.SpawnerCount(m_PlayerID, m_ButtonID);
            Debug.Log("兵士出す");

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
    public void SolderChange()
    {
        switch (m_type)
        {

            //剣から槍へ
            case Character.CharacterType.Sword:

                m_type = Character.CharacterType.Spear;
                 break;

            //槍から斧へ
            case Character.CharacterType.Spear:

                m_type = Character.CharacterType.Ax;
                 break;

            //斧から盾へ
            case Character.CharacterType.Ax:

                m_type = Character.CharacterType.Shield;
                   break;

            //盾から剣へ
            case Character.CharacterType.Shield:

                m_type = Character.CharacterType.Sword;
                          break;
        }
    }

    void Start()
    {
        //初期設定を行う。
        m_battleManager = GameObject.FindWithTag("BattleManager").transform;
        m_type = Character.CharacterType.Sword;

        
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
        }
    }
}
