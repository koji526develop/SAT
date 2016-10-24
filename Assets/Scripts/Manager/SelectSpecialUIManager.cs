using UnityEngine;
using System.Collections;

public class SelectSpecialUIManager : MonoBehaviour 
{

	void Start () 
	{
		
		for (int i = 0; i < 4; i++) 
		{
			for (int j = 0; j < 2; j++)
			{
				GameObject obj = MyUtility.CreateImage (
					                "Card",
					                "Image/karie/kard",
					new Vector2 ((3 + i * 4 + i * 1) / 32.0f, (18 - j * 6 - j * 2) / 25.0f),
					new Vector2 ((7 + i * 4 + i * 1) / 32.0f, (24 - j * 6 - j * 2) / 25.0f), 
					                transform
				                );
			}
		}

		MyUtility.CreateImage (
			"CardEffect",
			"Image/karie/kardeffect",
			new Vector2 (3 / 32.0f, 5 / 25.0f),
			new Vector2 (22 / 32.0f, 9 / 25.0f), 
			transform
		);

		MyUtility.CreateImage (
			"CardEffect",
			"Image/karie/kardframe",
			new Vector2 (24 / 32.0f, 5 / 25.0f),
			new Vector2 (29 / 32.0f, 24 / 25.0f), 
			transform
		);

		/***********************************************
		// 戻るボタン作成
		***********************************************/
		GameObject backObj = MyUtility.CreateButton (
			"Back",                             // オブジェクト名
			"Image/karie/waku5",                // 画像Path
			new Vector2 (3 / 32.0f, 1 / 25.0f), // アンカーの最小値
			new Vector2 (9 / 32.0f, 4 / 25.0f), // アンカーの最大値
			transform                           // 親のTransform
		);
		// Text追加
		MyUtility.AddText ("決定", backObj.transform);

		/***********************************************
		// 決定ボタン作成
		***********************************************/
		GameObject enterObj = MyUtility.CreateButton (
			"Enter",							 // オブジェクト名
			"Image/karie/waku5",				 // 画像Path
			new Vector2 (23 / 32.0f, 1 / 25.0f), // アンカーの最小値
			new Vector2 (29 / 32.0f, 4 / 25.0f), // アンカーの最大値
			transform							 // 親のTransform
		);
		// Text追加
		MyUtility.AddText ("戻る", enterObj.transform);
	}

	void Update ()
	{
	
	}
}
