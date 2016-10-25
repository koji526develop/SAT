using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectSpecialUIManager : MonoBehaviour 
{
	Camera m_uiCamera;

	int m_selectedCount = 0;
	GameObject [] m_selectedObj = new GameObject[3];

	GameObject m_touchObject;

	GameObject m_empty;

	GameObject m_moveObject;

	void Start () 
	{
		m_uiCamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();

		/***********************************************
		// 特殊カード8つ作成
		***********************************************/
		/*for (int i = 0; i < 4; i++) 
		{
			for (int j = 0; j < 2; j++)
			{
				GameObject obj = MyUtility.CreateButton (
					"Card",						 // オブジェクト名
					"Image/karie/kard",			 // 画像Path
					new Vector2 ((3 + i * 4 + i * 1) / 32.0f, (18 - j * 6 - j * 2) / 25.0f), // アンカーの最小値
					new Vector2 ((7 + i * 4 + i * 1) / 32.0f, (24 - j * 6 - j * 2) / 25.0f), // アンカーの最大値
					transform					 // 親のTransform
	            );
				obj.tag = "Card";
				obj.AddComponent<MeshCollider> ();
				Button button = obj.GetComponent<Button> ();
				ColorBlock cb = button.colors;
				cb.pressedColor = new Color(1.0f,1.0f,1.0f,0.5f);
				button.colors = cb;
			}
		}*/

		for (int i = 0; i < 3; i++)
		{
			GameObject obj = MyUtility.CreateImage (
				"SpecialCard",						 // オブジェクト名
				"Image/karie/kard",			 // 画像Path
				new Vector2 (25 / 32.0f, (18 - i * 5 - i * 1) / 25.0f), // アンカーの最小値
				new Vector2 (28 / 32.0f, (23 - i * 5 - i * 1) / 25.0f), // アンカーの最大値
				transform					 // 親のTransform
			);
			obj.SetActive (false);
			m_selectedObj[i] = obj;
		}

		m_empty = new GameObject("CardParent");
		for (int i = 0; i < 4; i++) 
		{
			for (int j = 0; j < 2; j++)
			{
				GameObject cardObj = MyUtility.CreateSprite (m_empty.transform, "Card", "Image/karie/kard");
				cardObj.tag = "Card";
				cardObj.transform.position = new Vector2 (-5.4f + 2.08f * i, 3.3f - j * 2.9f);
				cardObj.transform.localScale = new Vector3 (1.3f, 1.24f, 1);
				cardObj.AddComponent<BoxCollider2D> ();
			}
		}
			
		/***********************************************
		// 特殊カード説明枠作成
		***********************************************/
		MyUtility.CreateImage (
			"CardEffect",						 // オブジェクト名
			"Image/karie/kardeffect",			 // 画像Path
			new Vector2 (3 / 32.0f, 5 / 25.0f),  // アンカーの最小値
			new Vector2 (22 / 32.0f, 9 / 25.0f), // アンカーの最大値
			transform							 // 親のTransform
		);

		/***********************************************
		// 選択された特殊カードの枠作成
		***********************************************/
		GameObject cardFrameObj = MyUtility.CreateSprite (m_empty.transform, "CardFrame", "Image/karie/kardframe");
		cardFrameObj.tag = "CardFrame";
		cardFrameObj.transform.position = new Vector2 (4.38f, 0.75f);
		cardFrameObj.transform.localScale = new Vector3 (3.0f, 1.67f, 1);
		cardFrameObj.AddComponent<BoxCollider2D> ();
		cardFrameObj.tag = "CardFrame";

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
		TouchInfo touchInfo = TouchManager.GetTouchInfo (0);

		if (touchInfo == TouchInfo.Began) {
			Vector2 point = m_uiCamera.ScreenToWorldPoint (Input.mousePosition); 
			Collider2D collition2d = Physics2D.OverlapPoint (point); 
			if (collition2d) { 
				GameObject touchObj = collition2d.gameObject;
				if (touchObj.tag == "Card") {
					m_touchObject = touchObj;

					m_moveObject = MyUtility.CreateSprite (m_empty.transform, "MoveCard", "Image/karie/kard");
					m_moveObject.tag = "Card";
					Vector3 touchPos = TouchManager.GetTouchWorldPosition (m_uiCamera, 0);
					touchPos.z = 0;
					touchPos.x -= 0.8f;
					m_moveObject.transform.position = touchPos;
					m_moveObject.transform.localScale = new Vector3 (1.3f, 1.24f, 1);

					touchObj.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
				} 
			}
		} else if (touchInfo == TouchInfo.Moved) {
			Vector3 touchPos = TouchManager.GetTouchWorldPosition (m_uiCamera, 0);
			touchPos.z = 0;
			touchPos.x -= 0.8f;
			m_moveObject.transform.position = touchPos;
		}
		else if(touchInfo == TouchInfo.Ended){
			if (m_touchObject == null)
				return;
			
			Vector2 point = m_uiCamera.ScreenToWorldPoint (Input.mousePosition); 
			Collider2D collition2d = Physics2D.OverlapPoint (point); 

			if (collition2d)
			{
				if (collition2d.gameObject.tag == "CardFrame") 
				{
					m_selectedObj [m_selectedCount].SetActive (true);
					m_selectedCount++;
					Destroy (m_moveObject);
					m_touchObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
				} 
				else
				{
					Destroy (m_moveObject);
					m_touchObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
				}
			}
			else
			{
				Destroy (m_moveObject);
				m_touchObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			}
			m_touchObject = null;
		}
	}
}
