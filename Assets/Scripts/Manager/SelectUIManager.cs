using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class SelectUIManager : MonoBehaviour 
{
	int m_num;
	int m_count;

	int m_swordNum = 0;
	int m_spearNum = 0;
	int m_shieldNum = 0;
	int m_axNum = 0;

	Text [] m_soldierText = new Text[4];
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
			MyUtility.AddText (WeaponName[i], obj.transform);
		}

		float [,] value = {
			{ MyUtility.SWORD_LIFE, MyUtility.SWORD_ATTACK, MyUtility.SWORD_ATTACKDISTANCE, MyUtility.SWORD_MOCESPEED,m_swordNum },
			{ MyUtility.SPEAR_LIFE, MyUtility.SPEAR_ATTACK, MyUtility.SPEAR_ATTACKDISTANCE, MyUtility.SPEAR_MOCESPEED,m_spearNum },
			{ MyUtility.SHIELD_LIFE, MyUtility.SHIELD_ATTACK, MyUtility.SHIELD_ATTACKDISTANCE, MyUtility.SHIELD_MOCESPEED,m_shieldNum },
			{ MyUtility.AX_LIFE, MyUtility.AX_ATTACK, MyUtility.AX_ATTACKDISTANCE, MyUtility.AX_MOCESPEED,m_axNum }
		};
		for (int i = 0; i < 5; i++) 
		{
			for (int j = 0; j < 4; j++) 
			{
				Text text = MyUtility.CreateText (
				"Text",
				transform,
		        35,
		        Vector3.zero,
					new Vector2 ((6 + i * 3) / 32.0f, (21 - j * 4) / 25.0f),
					new Vector2 ((9 + i * 3) / 32.0f, (24 - j * 4) / 25.0f));
				text.text = value[j,i].ToString();
				if (i == 4)
					m_soldierText [j] = text;
			}
		}

		UnityAction[] plusFunc = { SwordPlus, SpearPlus, ShieldPlus, AxPlus };
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
			
		UnityAction[] minusFunc = { SwordMinus, SpearMinus, ShieldMinus, AxMinus };
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

	void SwordPlus()
	{
		Plus ();
		m_swordNum++;
		m_soldierText [0].text = m_swordNum.ToString();
	}

	void SpearPlus()
	{
		Plus ();
		m_spearNum++;
		m_soldierText [1].text = m_spearNum.ToString();
	}

	void ShieldPlus()
	{
		Plus ();
		m_shieldNum++;
		m_soldierText [2].text = m_shieldNum.ToString();
	}

	void AxPlus()
	{
		Plus ();
		m_axNum++;
		m_soldierText [3].text = m_axNum.ToString();
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

	void SwordMinus()
	{
		Minus ();
		m_swordNum--;
		m_soldierText [0].text = m_swordNum.ToString();
	}

	void SpearMinus()
	{
		Minus ();
		m_spearNum--;
		m_soldierText [1].text = m_spearNum.ToString();
	}

	void ShieldMinus()
	{
		Minus ();
		m_shieldNum--;
		m_soldierText [2].text = m_shieldNum.ToString();
	}

	void AxMinus()
	{
		Minus ();
		m_axNum--;
		m_soldierText [3].text = m_axNum.ToString();
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
