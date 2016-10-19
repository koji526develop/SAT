using UnityEngine;
using System.Collections;

public class SelectUIManager : MonoBehaviour 
{
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

		// +ボタン４つ作成
		for (int i = 0; i < 4; i++) 
		{
			MyUtility.CreateButton (
				"PlusButton",
				"Image/karie/plus",
				new Vector2 (23 / 32.0f, (21 - i * 4) / 25.0f),
				new Vector2 (26 / 32.0f, (24 - i * 4) / 25.0f), 
				transform
			);
		}

		// -ボタン４つ作成
		for (int i = 0; i < 4; i++) 
		{
			MyUtility.CreateButton (
				"MinusButton",
				"Image/karie/minus",
				new Vector2 (27 / 32.0f, (21 - i * 4) / 25.0f),
				new Vector2 (30 / 32.0f, (24 - i * 4) / 25.0f), 
				transform
			);
		}

		// ゲージ９つ作成
		for (int i = 0; i < 9; i++) 
		{
			MyUtility.CreateImage 
			(
				"Gauge",
				"Image/karie/waku2",
				new Vector2 ((6 + i * 2) / 32.0f, 5 / 25.0f),
				new Vector2 ((8 + i * 2) / 32.0f, 8 / 25.0f), 
				transform
			);
		}

		// 戻るボタン作成
		MyUtility.CreateButton (
			"Back",
			"Image/karie/waku5",
			new Vector2 (3 / 32.0f, 1 / 25.0f),
			new Vector2 (9 / 32.0f, 4 / 25.0f), 
			transform
		);

		// 決定ボタン作成
		MyUtility.CreateButton (
			"Enter",
			"Image/karie/waku5",
			new Vector2 (23 / 32.0f, 1 / 25.0f),
			new Vector2 (29 / 32.0f, 4 / 25.0f), 
			transform
		);
	
	}

	void Update ()
	{
	
	}
}
