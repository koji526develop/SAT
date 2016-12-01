using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectSpecialUIManager : MonoBehaviour
{
    Camera m_uiCamera;

    int m_selectedCount = 0;
    GameObject[] m_selectedObj = new GameObject[3];

    Text[] m_howToText = new Text[8];

    GameObject m_touchCardObject;

    GameObject m_cardParent;

    GameObject m_moveObject;

    GameObject sceneChangerObj;
    SceneChanger sceneChanger;

    public static int[] SPECIALCARD_NUMBER_1 = new int[3];
    public static int[] SPECIALCARD_NUMBER_2 = new int[3];

    void Start()
    {
        m_uiCamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();

        /***********************************************
		// 選択された特殊カードの表示枠にあらかじめ３枚作っておき、非表示にしておく
		***********************************************/

        m_cardParent = new GameObject("CardParent");

        for (int i = 0; i < 3; i++)
        {

            GameObject cardObj = MyUtility.CreateSprite(m_cardParent.transform, "CardFrame", "Image/karie/kard");

            cardObj.transform.position = new Vector3(3.5f, 3.0f + i * -2.25f, 0.0f);

            cardObj.SetActive(false);
            m_selectedObj[i] = cardObj;
        }

        /***********************************************
		// 特殊カード８枚作成
		***********************************************/
        int num = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                GameObject cardObj = SpecialCardSprite.CreateSprite(m_cardParent.transform, "Card", "UI/SpecialSelect/card" + (num + 1).ToString());
                cardObj.transform.position = new Vector2(-5.4f + 2.08f * i, 3.3f - j * 2.9f);
                cardObj.AddComponent<SpecialCardSprite>().cardNum = num;
                num++;
            }
        }

        /***********************************************
		// 特殊カード説明枠作成
		***********************************************/
        MyUtility.CreateImage (
			"CardEffect",						 // オブジェクト名
			"UI/SpecialSelect/card_info",			 // 画像Path
			new Vector2 (3 / 32.0f, 5 / 25.0f),  // アンカーの最小値
			new Vector2 (22 / 32.0f, 9 / 25.0f), // アンカーの最大値
			transform							 // 親のTransform
		);

        m_howToText[0] = MyUtility.CreateText(SpecialCard1.m_howTo,
            transform,
            35,
            Vector3.zero,
            new Vector2(3 / 32.0f, 5 / 25.0f),
            new Vector2(21.5f / 32.0f, 8.5f / 25.0f)
        );
        m_howToText[0].gameObject.SetActive(false);

        m_howToText[1] = MyUtility.CreateText(SpecialCard2.m_howTo,
            transform,
            35,
            Vector3.zero,
            new Vector2(3 / 32.0f, 5 / 25.0f),
            new Vector2(21.5f / 32.0f, 8.5f / 25.0f)
        );
        m_howToText[1].gameObject.SetActive(false);

        m_howToText[2] = MyUtility.CreateText(SpecialCard3.m_howTo,
            transform,
            35,
            Vector3.zero,
            new Vector2(3 / 32.0f, 5 / 25.0f),
            new Vector2(21.5f / 32.0f, 8.5f / 25.0f)
        );
        m_howToText[2].gameObject.SetActive(false);

        m_howToText[3] = MyUtility.CreateText(SpecialCard4.m_howTo,
            transform,
            35,
            Vector3.zero,
            new Vector2(3 / 32.0f, 5 / 25.0f),
            new Vector2(21.5f / 32.0f, 8.5f / 25.0f)
        );
        m_howToText[3].gameObject.SetActive(false);

        m_howToText[4] = MyUtility.CreateText(SpecialCard5.m_howTo,
            transform,
            35,
            Vector3.zero,
            new Vector2(3 / 32.0f, 5 / 25.0f),
            new Vector2(21.5f / 32.0f, 8.5f / 25.0f)
        );
        m_howToText[4].gameObject.SetActive(false);

        m_howToText[5] = MyUtility.CreateText(SpecialCard6.m_howTo,
            transform,
            35,
            Vector3.zero,
            new Vector2(3 / 32.0f, 5 / 25.0f),
            new Vector2(21.5f / 32.0f, 8.5f / 25.0f)
        );
        m_howToText[5].gameObject.SetActive(false);

        m_howToText[6] = MyUtility.CreateText(SpecialCard7.m_howTo,
            transform,
            35,
            Vector3.zero,
            new Vector2(3 / 32.0f, 5 / 25.0f),
            new Vector2(21.5f / 32.0f, 8.5f / 25.0f)
        );
        m_howToText[6].gameObject.SetActive(false);

        m_howToText[7] = MyUtility.CreateText(SpecialCard8.m_howTo,
            transform,
            35,
            Vector3.zero,
            new Vector2(3 / 32.0f, 5 / 25.0f),
            new Vector2(21.5f / 32.0f, 8.5f / 25.0f)
        );
        m_howToText[7].gameObject.SetActive(false);

        for (int i = 0; i < 8; i++)
        {
            m_howToText[i].lineSpacing = 45;
            m_howToText[i].alignment = TextAnchor.UpperLeft;
        }

        /***********************************************
		// 選択された特殊カードの枠作成
		***********************************************/
		GameObject cardFrameObj = MyUtility.CreateSprite(m_cardParent.transform, "CardFrame", "UI/SpecialSelect/select_card_place");
        cardFrameObj.tag = "CardFrame";
        cardFrameObj.transform.position = new Vector2(4.2f, 0.75f);
        cardFrameObj.transform.localScale = new Vector3(1.5f, 1.23f, 1);
        cardFrameObj.AddComponent<BoxCollider2D>();
        cardFrameObj.tag = "CardFrame";

        /***********************************************
		// 戻るボタン作成
		***********************************************/
        GameObject backObj = MyUtility.CreateButton(
            "Back",                             // オブジェクト名
            "UI/Select/return",                // 画像Path
            new Vector2(3 / 32.0f, 1 / 25.0f), // アンカーの最小値
            new Vector2(9 / 32.0f, 4 / 25.0f), // アンカーの最大値
            transform                           // 親のTransform
        );
        sceneChangerObj = new GameObject();
        sceneChanger = sceneChangerObj.AddComponent<SceneChanger>();
        backObj.GetComponent<Button>().onClick.AddListener(BackProces);

        /***********************************************
		// リセットボタン作成
		***********************************************/
        GameObject resetObj = MyUtility.CreateButton(
            "Reset",
            "UI/SpecialSelect/reset",
            new Vector2(13 / 32.0f, 1 / 25.0f),
            new Vector2(19 / 32.0f, 4 / 25.0f),
            transform
        );
        resetObj.GetComponent<Button>().onClick.AddListener(Reset);

        /***********************************************
		// 決定ボタン作成
		***********************************************/
        GameObject enterObj = MyUtility.CreateButton(
            "Enter",                             // オブジェクト名
            "UI/Select/decision",                // 画像Path
            new Vector2(23 / 32.0f, 1 / 25.0f), // アンカーの最小値
            new Vector2(29 / 32.0f, 4 / 25.0f), // アンカーの最大値
            transform                            // 親のTransform
        );
        // PlayerIDが1なら2Pの兵士を選びにセレクトに戻る
        enterObj.GetComponent<Button>().onClick.AddListener(EnterProces);
    }

    public void BackProces()
    {
        AudioManager.m_instance.PlaySE("button_SE");

        //1Pが特殊選択画面から戻ったとき
        if (SelectUIManager.PlayerID == 2)
        {
            SelectUIManager.PlayerID = 1;
        }
        sceneChanger.ChangeToSelect();
    }

    public void EnterProces()
    {
        AudioManager.m_instance.PlaySE("button_SE");

        if (m_selectedCount == 3)
        {
            if (SelectUIManager.PlayerID == 2)
            {
                //リザルト用１P特殊カード情報を保存
                for (int i = 0; i < 3; i++)
                {
                    ResultManager.ResultSpecialInfo[i] = SPECIALCARD_NUMBER_1[i];

                }
                sceneChanger.ChangeToSelect();
            }
            else
            {
                //リザルト用２P特殊カード情報を保存
                for (int i = 0; i < 3; i++)
                {
                    ResultManager.ResultSpecialInfo[i+3] = SPECIALCARD_NUMBER_2[i];

                }
                sceneChanger.ChangeToGame();
            }
        }
    }


    GameObject GetTouchObject()
    {
        Vector2 point = m_uiCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D collition2d = Physics2D.OverlapPoint(point);
        if (collition2d)
        {
            GameObject touchObj = collition2d.gameObject;
            return touchObj;
        }
        return null;
    }

    GameObject CreateTouchMoveCard()
    {
        GameObject touchMoveCardObject = MyUtility.CreateSprite(m_cardParent.transform, "MoveCard", "Image/karie/kard");
        touchMoveCardObject.GetComponent<SpriteRenderer>().sprite = m_touchCardObject.GetComponent<SpriteRenderer>().sprite;
        touchMoveCardObject.tag = "Card";
        Vector3 touchPos = TouchManager.GetTouchWorldPosition(m_uiCamera, 0);
        touchPos.z = 0;
        touchPos.x -= 0.8f;
        touchMoveCardObject.transform.position = touchPos;
        touchMoveCardObject.transform.localScale = new Vector3(1.3f, 1.24f, 1);

        return touchMoveCardObject;
    }

    void MoveTouchedCard()
    {
        Vector3 touchPos = TouchManager.GetTouchWorldPosition(m_uiCamera, 0);
        touchPos.z = 0;
        touchPos.x -= 0.8f;
        m_moveObject.transform.position = touchPos;
    }

    void Update()
    {
        TouchInfo touchInfo = TouchManager.GetTouchInfo(0);

        if (touchInfo == TouchInfo.Began)
        {
            // タッチしている特殊カードを取得
            m_touchCardObject = GetTouchObject();

            // タッチしている特殊カードがなかったら抜ける
            if (m_touchCardObject == null)
                return;

            if (m_touchCardObject.tag != "Card")
                return;

            if (m_touchCardObject.GetComponent<SpriteRenderer>().color == new Color(1.0f, 1.0f, 1.0f, 0.5f))
                return;

            // タッチした特殊カードを半透明にする
            m_touchCardObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

            for (int i = 0; i < 8; i++)
                m_howToText[i].gameObject.SetActive(false);

            int num = m_touchCardObject.GetComponent<SpecialCardSprite>().cardNum;
            m_howToText[num].gameObject.SetActive(true);

            // タッチした特殊カードと同じSpriteを作る
            m_moveObject = CreateTouchMoveCard();

        }
        else if (touchInfo == TouchInfo.Moved && m_moveObject)
        {
            if (m_touchCardObject == null)
                return;

            // タッチした特殊カードSpriteを指の位置に移動させる
            MoveTouchedCard();
        }
        else if (touchInfo == TouchInfo.Ended)
        {
            if (m_touchCardObject == null || m_moveObject == null)
                return;

            Vector2 point = m_uiCamera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D collition2d = Physics2D.OverlapPoint(point);

            if (collition2d)
            {
                if (collition2d.gameObject.tag == "CardFrame" && m_selectedCount < 3)
                {
                    AudioManager.m_instance.PlaySE("cardSetting_SE");
                    if (SelectUIManager.PlayerID == 1)
                    {
                        SPECIALCARD_NUMBER_1[m_selectedCount] = m_touchCardObject.GetComponent<SpecialCardSprite>().cardNum;
                    }
                    else
                    {
                        SPECIALCARD_NUMBER_2[m_selectedCount] = m_touchCardObject.GetComponent<SpecialCardSprite>().cardNum;

                    }
                    m_selectedObj[m_selectedCount].GetComponent<SpriteRenderer>().sprite = m_touchCardObject.GetComponent<SpriteRenderer>().sprite;
                    m_selectedObj[m_selectedCount].SetActive(true);
                    m_selectedCount++;
                    Destroy(m_moveObject);
                    m_touchCardObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                }
                else
                {
                    Destroy(m_moveObject);
                    m_touchCardObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
            }
            else
            {
                Destroy(m_moveObject);
                m_touchCardObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            m_touchCardObject = null;
        }
    }

    void Reset()
    {
        m_selectedCount = 0;

        for (int i = 0; i < 8; i++)
            m_howToText[i].gameObject.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            m_selectedObj[i].SetActive(false);
        }

        foreach (Transform child in m_cardParent.transform)
        {
            if (child.tag == "Card")
            {
                child.gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }

    }

}
