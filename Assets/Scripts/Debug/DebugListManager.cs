﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class DebugListManager : MonoBehaviour
{
	[SerializeField]
	RectTransform prefab = null;
	const int MAX_ITEM_NUM = 50;
	const int MAX_COLUMN_NUM = 3;

	enum Item
	{
		Close,
		SwordMax,
		SpearMax,
		AxMax,
		ShieldMax,
		Test6,
		Test7,
		Test8,
		Test9,
		Test10,
		Test11,
		Test12,
		Test13,
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
		case Item.SwordMax:return SwordMax;
		case Item.SpearMax:return SpearMax;
		case Item.AxMax:return AxMax;
		case Item.ShieldMax:return ShieldMax;
			default: return A;
		}
	}
	void SwordMax()
	{
		SelectUIManager.SWORD_NUM_1 = 99;
		SelectUIManager.SWORD_NUM_2 = 99;
	}
	void SpearMax()
	{
		SelectUIManager.SPEAR_NUM_1 = 99;
		SelectUIManager.SPEAR_NUM_2 = 99;
	}
	void AxMax()
	{
		SelectUIManager.AX_NUM_1 = 99;
		SelectUIManager.AX_NUM_2 = 99;
	}
	void ShieldMax()
	{
		SelectUIManager.SHIELD_NUM_1 = 99;
		SelectUIManager.SHIELD_NUM_2 = 99;
	}
	void A()
	{
		
	}
	void Close()
	{
		gameObject.SetActive (false);
	}
}
