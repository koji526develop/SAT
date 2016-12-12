using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class DebugListManager : MonoBehaviour
{
	[SerializeField]
	RectTransform prefab = null;
	const int MAX_ITEM_NUM = 50;
	const int MAX_COLUMN_NUM = 3;
	GameObject m_battleManager;

	enum Item
	{
		Close,
		SoldierMax,
		ResultScene,
		Title,
		Barrier,
		SpecialCard1,
		SpecialCard2,
		SpecialCard3,
		SpecialCard4,
		SpecialCard5,
		SpecialCard6,
		SpecialCard7,
		SpecialCard8,
		Test14,
		Test15,
		Test16,
		Test17,
		Test18,
		Test19,
		Test20,
		Test21,
		Test22,
		MaxItemNum
	}
	void Start()
	{
		m_battleManager=GameObject.Find("BattleManager");
	}
	void Awake ()
	{
		GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width, Screen.height);

		Transform contentTransForm = transform.FindChild ("Viewport/Content");

		GridLayoutGroup layout = contentTransForm.GetComponent<GridLayoutGroup> ();
		layout.constraintCount = MAX_COLUMN_NUM;
		layout.cellSize = new Vector2 (Screen.width / MAX_COLUMN_NUM * 0.9f, 100);
		layout.spacing = new Vector2 (Screen.width / MAX_COLUMN_NUM * 0.1f, 50);

		for (int i = 0; i < (int)Item.MaxItemNum; i++) {
			RectTransform item = GameObject.Instantiate (prefab) as RectTransform;
			item.SetParent (contentTransForm, false);

			Text text = item.GetComponentInChildren<Text> ();
			Item itemName = (Item)i;
			text.text = itemName.ToString ();

			Button button = item.GetComponent<Button> ();
			button.onClick.AddListener (GetOnClickFunc ((Item)i));
		

		}
	}

	UnityAction GetOnClickFunc(Item _item)
	{
		switch (_item) 
		{
			case Item.Close: return Close;
		case Item.SoldierMax:return SoldierMax;
		case Item.ResultScene:return ResultScene;
		case Item.Title:return Title;
		case Item.Barrier:return Barrier;
		case Item.SpecialCard1:return SpecialCard1;
		case Item.SpecialCard2:return SpecialCard2;
		case Item.SpecialCard3:return SpecialCard3;
		case Item.SpecialCard4:return SpecialCard4;
		case Item.SpecialCard5:return SpecialCard5;
		case Item.SpecialCard6:return SpecialCard6;
		case Item.SpecialCard7:return SpecialCard7;
		case Item.SpecialCard8:return SpecialCard8;
			default: return A;
		}
	}
	void SoldierMax()
	{
		SelectUIManager.SWORD_NUM_1 = 99;
		SelectUIManager.SWORD_NUM_2 = 99;
		SelectUIManager.SPEAR_NUM_1 = 99;
		SelectUIManager.SPEAR_NUM_2 = 99;
		SelectUIManager.AX_NUM_1 = 99;
		SelectUIManager.AX_NUM_2 = 99;
		SelectUIManager.SHIELD_NUM_1 = 99;
		SelectUIManager.SHIELD_NUM_2 = 99;
	}
	void A()
	{
		
	}
	void ResultScene()
	{
		SceneChanger sChange = new SceneChanger();
		sChange.ChangeToResult();

	}
	void Barrier()
	{
		foreach (Transform child in m_battleManager.transform) 
		{
			if (child.tag=="Character") 
			{
				Character character = child.gameObject.GetComponent<Character> ();
				character.ChangeState (Character.CharacterState.Barrier);


			}
		}
	}
	void Title()
	{
		MySceneManager.m_instance.LoadLevel("Title", 0.5f);
	}
	void Close()
	{
		gameObject.SetActive (false);
	}

	void SpecialCard1()
	{
        SpecialCard m_usedSpCardState;

        GameObject SpecialCardButtonObj = GameObject.Find ("Canvas/SpecialCardButton1");
        m_usedSpCardState = SpecialCardButtonObj.AddComponent<SpecialCard1>();

        m_usedSpCardState.m_UsedPlayerID = 1;
		m_usedSpCardState.GameReady(GameObject.Find("BattleManager").transform,GameObject.Find("GameManager").transform);

	}

	void SpecialCard2()
	{
        SpecialCard m_usedSpCardState;

        GameObject SpecialCardButtonObj = GameObject.Find("Canvas/SpecialCardButton1");
        m_usedSpCardState = SpecialCardButtonObj.AddComponent<SpecialCard2>();

        m_usedSpCardState.m_UsedPlayerID = 1;
        m_usedSpCardState.GameReady(GameObject.Find("BattleManager").transform, GameObject.Find("GameManager").transform);
    }

    void SpecialCard3()
	{
        SpecialCard m_usedSpCardState;

        GameObject SpecialCardButtonObj = GameObject.Find("Canvas/SpecialCardButton1");
        m_usedSpCardState = SpecialCardButtonObj.AddComponent<SpecialCard3>();

        m_usedSpCardState.m_UsedPlayerID = 1;
       
        m_usedSpCardState.GameReady(GameObject.Find("BattleManager").transform,GameObject.Find("GameManager").transform);
	}

	void SpecialCard4()
	{
        SpecialCard m_usedSpCardState;

        GameObject SpecialCardButtonObj = GameObject.Find("Canvas/SpecialCardButton1");
        m_usedSpCardState = SpecialCardButtonObj.AddComponent<SpecialCard4>();

        m_usedSpCardState.m_UsedPlayerID = 1;
        m_usedSpCardState.GameReady(GameObject.Find("BattleManager").transform, GameObject.Find("GameManager").transform);

    }

    void SpecialCard5()
	{
        SpecialCard m_usedSpCardState;

        GameObject SpecialCardButtonObj = GameObject.Find("Canvas/SpecialCardButton1");
        m_usedSpCardState = SpecialCardButtonObj.AddComponent<SpecialCard5>();

        m_usedSpCardState.m_UsedPlayerID = 1;
        m_usedSpCardState.GameReady(GameObject.Find("BattleManager").transform, GameObject.Find("GameManager").transform);

    }

    void SpecialCard6()
	{
        SpecialCard m_usedSpCardState;

        GameObject SpecialCardButtonObj = GameObject.Find("Canvas/SpecialCardButton1");
        m_usedSpCardState = SpecialCardButtonObj.AddComponent<SpecialCard6>();

        m_usedSpCardState.m_UsedPlayerID = 1;
        m_usedSpCardState.GameReady(GameObject.Find("BattleManager").transform, GameObject.Find("GameManager").transform);

    }

    void SpecialCard7()
	{
        SpecialCard m_usedSpCardState;

        GameObject SpecialCardButtonObj = GameObject.Find("Canvas/SpecialCardButton1");
        m_usedSpCardState = SpecialCardButtonObj.AddComponent<SpecialCard7>();

        m_usedSpCardState.m_UsedPlayerID = 1;
        m_usedSpCardState.GameReady(GameObject.Find("BattleManager").transform, GameObject.Find("GameManager").transform);

    }

    void SpecialCard8()
	{
        SpecialCard m_usedSpCardState;

        GameObject SpecialCardButtonObj = GameObject.Find("Canvas/SpecialCardButton1");
        m_usedSpCardState = SpecialCardButtonObj.AddComponent<SpecialCard8>();

        m_usedSpCardState.m_UsedPlayerID = 1;
        m_usedSpCardState.GameReady(GameObject.Find("BattleManager").transform, GameObject.Find("GameManager").transform);

    }

}
