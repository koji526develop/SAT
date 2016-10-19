using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class SelectUIManager : MonoBehaviour 
{
	int m_num;
	int m_count;
	List<GameObject> gaugeList = new List<GameObject>();

	void Awake () 
	{
		string[] WeaponName = { "剣", "槍", "斧", "盾" };

		// 武器の名前がついた丸４つ作成
		for (int i = 0; i < 4; i++) 
		{
			GameObject obj = MyUtility.CreateImage (
				WeaponName[i],
				"Image/karie/maru",
				new Vector2 (3 / 32.0f, (21 - i * 4) / 25.0f),
				new Vector2 (6 / 32.0f, (24 - i * 4) / 25.0f), 
				transform
			);
			MyUtility.AddText (WeaponName[i], obj.transform,35);
		}

		// 配置する場所用の画像４つ作成
		for (int i = 0; i < 4; i++) 
		{
			MyUtility.CreateImage (
				"LongBar",
				"Image/karie/waku",
				new Vector2 (6 / 32.0f, (21 - i * 4) / 25.0f),
				new Vector2 (22 / 32.0f, (24 - i * 4) / 25.0f), 
				transform
			);
		}

		UnityAction[] plusFunc = { Plus, Plus, Plus, Plus };
		// +ボタン４つ作成
		for (int i = 0; i < 4; i++) 
		{
			GameObject buttonObj = MyUtility.CreateButton (
				"PlusButton",
				"Image/karie/plus",
				new Vector2 (27 / 32.0f, (21 - i * 4) / 25.0f),
				new Vector2 (30 / 32.0f, (24 - i * 4) / 25.0f), 
				transform
			);
			buttonObj.GetComponent<Button> ().onClick.AddListener (plusFunc[i]);
		}
			
		UnityAction[] minusFunc = { Minus, Minus, Minus, Minus };
		// -ボタン４つ作成
		for (int i = 0; i < 4; i++) 
		{
			GameObject buttonObj = MyUtility.CreateButton (
				"MinusButton",
				"Image/karie/minus",
				new Vector2 (23 / 32.0f, (21 - i * 4) / 25.0f),
				new Vector2 (26 / 32.0f, (24 - i * 4) / 25.0f), 
				transform
			);
			buttonObj.GetComponent<Button> ().onClick.AddListener (minusFunc[i]);
		}
	
		// ゲージ９つ作成
		for (int i = 0; i < 9; i++) 
		{
			GameObject obj = MyUtility.CreateImage 
			(
				"Gauge",
				"Image/karie/waku2",
				new Vector2 ((6 + i * 2) / 32.0f, 5 / 25.0f),
				new Vector2 ((8 + i * 2) / 32.0f, 8 / 25.0f), 
				transform
			);
			gaugeList.Add (obj);
		}

		// 戻るボタン作成
		GameObject backObj = MyUtility.CreateButton (
			"Back",
			"Image/karie/waku5",
			new Vector2 (3 / 32.0f, 1 / 25.0f),
			new Vector2 (9 / 32.0f, 4 / 25.0f), 
			transform
		);
		MyUtility.AddText ("決定", backObj.transform);

		// 決定ボタン作成
		GameObject enterObj = MyUtility.CreateButton (
			"Enter",
			"Image/karie/waku5",
			new Vector2 (23 / 32.0f, 1 / 25.0f),
			new Vector2 (29 / 32.0f, 4 / 25.0f), 
			transform
		);
		MyUtility.AddText ("戻る", enterObj.transform);
	}

	void Plus()
	{
		m_num++;

		gaugeList [m_count].GetComponent<Image> ().sprite = GetGaugeSprite (PlusOrMinus.Plus);

		if (m_num % 2 == 0)
		{
			m_count++;
		}
	}

	void Minus()
	{
		m_num--;

		if (m_num % 2 == 1) 
		{
			m_count--;
		}

		gaugeList [m_count].GetComponent<Image> ().sprite = GetGaugeSprite (PlusOrMinus.Minus);
	}

	enum PlusOrMinus
	{
		Plus,
		Minus
	}

	Sprite GetGaugeSprite(PlusOrMinus _plusOrMinus)
	{
		if (_plusOrMinus == PlusOrMinus.Plus) {
			switch (m_num % 2) {
			case 0:
				return Resources.Load ("Image/karie/waku4", typeof(Sprite)) as Sprite;
			case 1:
				return Resources.Load ("Image/karie/waku3", typeof(Sprite)) as Sprite;
			}
		} else {
			switch (m_num % 2) 
			{
			case 0:
				return Resources.Load ("Image/karie/waku2", typeof(Sprite)) as Sprite;
			case 1:
				return Resources.Load ("Image/karie/waku3", typeof(Sprite)) as Sprite;
			}
		}
		return null;
	}
}
