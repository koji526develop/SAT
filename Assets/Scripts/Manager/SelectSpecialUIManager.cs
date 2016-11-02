using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectSpecialUIManager : MonoBehaviour 
{
	Camera m_uiCamera;

	int m_selectedCount = 0;
	GameObject [] m_selectedObj = new GameObject[3];

	GameObject m_touchCardObject;

	GameObject m_cardParent;

	GameObject m_moveObject;

	void Start () 
	{
		m_uiCamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();

		/***********************************************
		// 選択された特殊カードの表示枠にあらかじめ３枚作っておき、非表示にしておく
		***********************************************/
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

		/***********************************************
		// 特殊カード８枚作成
		***********************************************/
		m_cardParent = new GameObject("CardParent");
		for (int i = 0; i < 4; i++) 
		{
			for (int j = 0; j < 2; j++)
			{
				GameObject cardObj = MyUtility.CreateSprite (m_cardParent.transform, "Card", "Image/karie/kard");
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
		GameObject cardFrameObj = MyUtility.CreateSprite (m_cardParent.transform, "CardFrame", "Image/karie/kardframe");
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
		MyUtility.AddText ("戻る", backObj.transform);
		GameObject sceneChangerObj = new GameObject();
		SceneChanger sceneChanger = sceneChangerObj.AddComponent<SceneChanger>();
		backObj.GetComponent<Button>().onClick.AddListener(sceneChanger.ChangeToSelect);

		/***********************************************
		// リセットボタン作成
		***********************************************/
		GameObject resetObj = MyUtility.CreateButton (
			"Reset",
			"Image/karie/waku5",
			new Vector2 (13 / 32.0f, 1 / 25.0f),
			new Vector2 (19 / 32.0f, 4 / 25.0f), 
			transform
		);
		MyUtility.AddText ("リセット", resetObj.transform);
		resetObj.GetComponent<Button>().onClick.AddListener(Reset);

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
		MyUtility.AddText ("決定", enterObj.transform);
        enterObj.GetComponent<Button>().onClick.AddListener(sceneChanger.ChangeToGame);
    }

	GameObject GetTouchObject()
	{
		Vector2 point = m_uiCamera.ScreenToWorldPoint (Input.mousePosition); 
		Collider2D collition2d = Physics2D.OverlapPoint (point); 
		if (collition2d) { 
			GameObject touchObj = collition2d.gameObject;
			return touchObj;
		}
		return null;
	}

	GameObject CreateTouchMoveCard()
	{
		GameObject touchMoveCardObject = MyUtility.CreateSprite (m_cardParent.transform, "MoveCard", "Image/karie/kard");
		touchMoveCardObject.tag = "Card";
		Vector3 touchPos = TouchManager.GetTouchWorldPosition (m_uiCamera, 0);
		touchPos.z = 0;
		touchPos.x -= 0.8f;
		touchMoveCardObject.transform.position = touchPos;
		touchMoveCardObject.transform.localScale = new Vector3 (1.3f, 1.24f, 1);

		return touchMoveCardObject;
	}

	void MoveTouchedCard()
	{
		Vector3 touchPos = TouchManager.GetTouchWorldPosition (m_uiCamera, 0);
		touchPos.z = 0;
		touchPos.x -= 0.8f;
		m_moveObject.transform.position = touchPos;
	}

	void Update ()
	{
		TouchInfo touchInfo = TouchManager.GetTouchInfo (0);

		if (touchInfo == TouchInfo.Began) 
		{
			// タッチしている特殊カードを取得
			m_touchCardObject = GetTouchObject();

			// タッチしている特殊カードがなかったら抜ける
			if (m_touchCardObject == null)
				return;

			if (m_touchCardObject.tag != "Card")
				return;

			// タッチした特殊カードを半透明にする
			m_touchCardObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);

			// タッチした特殊カードと同じSpriteを作る
			m_moveObject = CreateTouchMoveCard ();

		} 
		else if (touchInfo == TouchInfo.Moved && m_moveObject) 
		{
			if (m_touchCardObject == null)
				return;

			// タッチした特殊カードSpriteを指の位置に移動させる
			MoveTouchedCard ();
		}
		else if(touchInfo == TouchInfo.Ended)
		{
			if (m_touchCardObject == null)
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
					m_touchCardObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
				} 
				else
				{
					Destroy (m_moveObject);
					m_touchCardObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
				}
			}
			else
			{
				Destroy (m_moveObject);
				m_touchCardObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			}
			m_touchCardObject = null;
		}
	}

	void Reset()
	{
		m_selectedCount = 0;
		for (int i = 0; i < 3; i++) 
		{
			m_selectedObj [i].SetActive (false);
		}

		foreach (Transform child in m_cardParent.transform)
		{
			if (child.tag == "Card") 
			{
				child.gameObject.GetComponent<SpriteRenderer> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			}
		}

	}

}
