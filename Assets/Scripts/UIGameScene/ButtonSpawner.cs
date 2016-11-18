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

    void Awake()
    {

        m_scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();

        //ボタン押すことで兵種を切り替える処理を行う。  
        this.GetComponent<Button>().onClick.AddListener(SolderChange);

        //スライドで出現処理を行えるようにする処理を行う。
        EventTrigger dragEndtrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.EndDrag;
        entry.callback.AddListener((data) => { Spawner(); });
        dragEndtrigger.triggers.Add(entry);
    }

    //兵士出現処理
    public void Spawner()
    {
		
        //兵士が残っていれば生成
		if(SoldierNumCheck(m_PlayerID,m_type))// && CheckFrontArea(m_PlayerID,m_type))
        {
            Character character = Character.CreateObject(m_battleManager, m_type, Character.GetSpawnPosition(m_PlayerID, m_ButtonID), m_PlayerID).GetComponent<Character>();
            character.mapColumn = m_ButtonID;
            m_scoreManager.SpawnerCount(m_PlayerID, m_ButtonID);
            Debug.Log("兵士出す");
        }
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

    void OnGUI()
    {
        RectTransform uiRectTransForm = gameObject.GetComponent<RectTransform>();
        Vector2 uiPos = new Vector2(uiRectTransForm.localPosition.x + Screen.width / 2,
                                      -uiRectTransForm.localPosition.y + Screen.height / 2);
        Texture changeTexture = gameObject.GetComponent<Image>().mainTexture;

        // UIのテクスチャの向きを1Pと2P用に帰る処理
        Matrix4x4 matrix = GUI.matrix;
        {
            if (m_PlayerID == 1)
            {
                GUIUtility.RotateAroundPivot(90, uiPos);
                GUI.DrawTexture(new Rect(uiPos.x, uiPos.y - 64, 92.16f, 64), changeTexture);
            }
            else
            {
                GUIUtility.RotateAroundPivot(-90, uiPos);
                GUI.DrawTexture(new Rect(uiPos.x - 92.16f, uiPos.y, 92.16f, 64), changeTexture);
            }
        }
        GUI.matrix = matrix;
    }

    //兵士の選択を変える処理に入る
    public void SolderChange()
    {
        switch (m_type)
        {

            //剣から槍へ
            case Character.CharacterType.Sword:

                m_type = Character.CharacterType.Spear;
                gameObject.GetComponent<Image>().sprite = Resources.Load("Image/spear_I", typeof(Sprite)) as Sprite;
                break;

            //槍から斧へ
            case Character.CharacterType.Spear:

                m_type = Character.CharacterType.Ax;
                gameObject.GetComponent<Image>().sprite = Resources.Load("Image/ax_I", typeof(Sprite)) as Sprite;
                break;

            //斧から盾へ
            case Character.CharacterType.Ax:

                m_type = Character.CharacterType.Shield;
                gameObject.GetComponent<Image>().sprite = Resources.Load("Image/shield_I", typeof(Sprite)) as Sprite;
                break;

            //盾から剣へ
            case Character.CharacterType.Shield:

                m_type = Character.CharacterType.Sword;
                gameObject.GetComponent<Image>().sprite = Resources.Load("Image/sword_I", typeof(Sprite)) as Sprite;
                break;
        }
    }

    void Start()
    {
        //初期設定を行う。
        m_battleManager = GameObject.FindWithTag("BattleManager").transform;
        m_type = Character.CharacterType.Sword;
    }
}
