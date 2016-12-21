using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    SceneChanger sceneChanger;

    /// <summary>
    /// //兵士の情報がセットされているかどうか
    /// </summary>
    public struct PlayerSetting
    {
        public bool isSoldier_1P;
        public bool isSoldier_2P;
        public bool isSpecial_1P;
        public bool isSpecial_2P;
        public PlayerSetting(bool init)
        {
            isSoldier_1P = init;
            isSoldier_2P = init;
            isSpecial_1P = init;
            isSpecial_2P = init;
        }
        public void Reset()
        {
            isSoldier_1P = false;
            isSoldier_2P = false;
            isSpecial_1P = false;
            isSpecial_2P = false;
        }
    }

    public static PlayerSetting m_playerSetting = new PlayerSetting(false);

    bool isGame;            //ゲーム開始できるか

    GameObject[] m_PlayerButton = new GameObject[2];    //プレイヤー選択ボタン格納

    Color m_buttonClearColor = new Color(1, 1, 1, 120.0f / 255.0f);

    void Awake()
    {
        // ライト作成
        MyUtility.CreateDirectionalLight();
        // MAINカメラ作成
        Camera camera = MyUtility.CreateCamera("MainCamera");

        Canvas canvas = MyUtility.CreateCanvas();
        //イベントシステム作成
        MyUtility.CreateEventSystem(transform);

        StartCoroutine(MyUtility.SetCameraForCanvas(canvas, camera));

        //シーンチェンジャー
        GameObject sceneChangerObj = new GameObject();
        sceneChanger = sceneChangerObj.AddComponent<SceneChanger>();

        GameObject m_MenuObject = MyUtility.CreateEmpty("MenuObject", canvas.transform);
        GameObject m_MenuButton = MyUtility.CreateEmpty("MenuButton", canvas.transform);

        //背景
        MyUtility.CreateImage(
            "MenuBackGround",
            "UI/Menu/menu_back",
            new Vector2(0 / 32.0f, 0 / 25.0f),
            new Vector2(32 / 32.0f, 25 / 25.0f),
            m_MenuObject.transform);

        //プレイヤーアイコン1P
        m_PlayerButton[0] = MyUtility.CreateButton(
            "Player1PIcon",
            "UI/Select/1p",
            new Vector2(1 / 32.0f, 21 / 25.0f),
            new Vector2(5 / 32.0f, 24 / 25.0f),
            m_MenuButton.transform
            );
        m_PlayerButton[0].GetComponent<Button>().onClick.AddListener(Player1Proces);

        //プレイヤーアイコン2P
        m_PlayerButton[1] = MyUtility.CreateButton(
            "Player2PIcon",
            "UI/Select/2p",
            new Vector2(5 / 32.0f, 21 / 25.0f),
            new Vector2(9 / 32.0f, 24 / 25.0f),
            m_MenuButton.transform
            );
        m_PlayerButton[1].GetComponent<Button>().onClick.AddListener(Player2Proces);
        //現在のプレイヤーではない方のボタンを半透明かつ小さく
        if (SelectUIManager.PlayerID == 1)
        {
            m_PlayerButton[1].GetComponent<Image>().color = m_buttonClearColor;
            m_PlayerButton[1].transform.localScale = new Vector3(0.8f,0.8f,1.0f);
        }
        else if (SelectUIManager.PlayerID == 2)
        {
            m_PlayerButton[0].GetComponent<Image>().color = m_buttonClearColor;
            m_PlayerButton[0].transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
        }

        //プレイヤーアイコンPivot変更
        for (int i = 0; i < 2; i++)
        {
            m_PlayerButton[i].GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        }

        //テキスト
        MyUtility.CreateText("プレイヤー選択",
            m_MenuObject.transform,
            30,
            Vector3.zero,
            new Vector2(1 / 32.0f, 20 / 25.0f),
            new Vector2(9 / 32.0f, 21 / 25.0f));

        //兵士編成ボタン
        GameObject soldierFormationObj = MyUtility.CreateButton(
            "formation",
            "UI/Menu/formation",
            new Vector2(6 / 32.0f, 5.5f / 25.0f),
            new Vector2(20 / 32.0f, 19.5f / 25.0f),
            m_MenuButton.transform);
        soldierFormationObj.GetComponent<Image>().raycastTarget = false;
        soldierFormationObj.GetComponent<Button>().onClick.AddListener(sceneChanger.ChangeToSelect);
        //兵士編成ボタンタッチ取得範囲
        ButtonRectSet(soldierFormationObj.transform, new Vector2(0, 0), new Vector2(0.425f, 1), new Vector3(0, 0, 0));
        ButtonRectSet(soldierFormationObj.transform, new Vector2(0.425f, 0.623f), new Vector2(0.81f, 1), new Vector3(0, 0, 0));
        ButtonRectSet(soldierFormationObj.transform, new Vector2(0.425f, 0), new Vector2(0.81f, 0.4f), new Vector3(0, 0, 135));
        ButtonRectSet(soldierFormationObj.transform, new Vector2(0.6f, 0.55f), new Vector2(0.94f, 0.89f), new Vector3(0, 0, 0));

        //特殊カード選択ボタン
        GameObject specialCardObj = MyUtility.CreateButton(
            "Special",
            "UI/Menu/card",
            new Vector2(12 / 32.0f, 5.5f / 25.0f),
            new Vector2(26 / 32.0f, 19.5f / 25.0f),
            m_MenuButton.transform);
        specialCardObj.GetComponent<Image>().raycastTarget = false;
        specialCardObj.GetComponent<Button>().onClick.AddListener(sceneChanger.ChangeToSelectSpecial);
        //特殊カード選択ボタンタッチ取得範囲
        ButtonRectSet(specialCardObj.transform, new Vector2(0.58f, 0), new Vector2(1, 1), new Vector3(0, 0, 0));
        ButtonRectSet(specialCardObj.transform, new Vector2(0.18f, 0), new Vector2(0.6f, 0.3f), new Vector3(0, 0, 0));
        ButtonRectSet(specialCardObj.transform, new Vector2(0.36f, 0.16f), new Vector2(0.72f, 0.35f), new Vector3(0, 0, 45));
        ButtonRectSet(specialCardObj.transform, new Vector2(0.04f, 0.08f), new Vector2(0.42f, 0.43f), new Vector3(0, 0, 0));

        //チュートリアルボタン作成
        GameObject tutoObj = MyUtility.CreateButton(
            "Tutorial",
            "UI/Menu/tutorial",
            new Vector2(6 / 32.0f, 1 / 25.0f),
            new Vector2(15 / 32.0f, 5 / 25.0f),
            m_MenuButton.transform
           );

        tutoObj.GetComponent<Button>().onClick.AddListener(sceneChanger.ChangeToTutorial);

        // 開戦ボタン作成
        GameObject gameObj = MyUtility.CreateButton(
            "warfare",
            "UI/Menu/warfare",
            new Vector2(17 / 32.0f, 1 / 25.0f),
            new Vector2(26 / 32.0f, 5 / 25.0f),
            m_MenuButton.transform
        );
        gameObj.GetComponent<Button>().onClick.AddListener(GameMainProces);

        //全ての兵士、カードの情報がセットされていなかったら選べないようにする
        isGame = m_playerSetting.isSoldier_1P && m_playerSetting.isSoldier_2P && m_playerSetting.isSpecial_1P && m_playerSetting.isSpecial_2P;

        if (!isGame)
        {
            gameObj.GetComponent<Image>().color = m_buttonClearColor;
        }
    }


    //=============================================================
    //=============================================================
    /// <summary>
    /// プレイヤー1PIconが押された時
    /// </summary>
    void Player1Proces()
    {
        AudioManager.m_instance.PlaySE("button_SE");
        SelectUIManager.PlayerID = 1;
        //選択側
        m_PlayerButton[0].GetComponent<Image>().color = new Vector4(1, 1, 1, 1.0f); //α値をもとに戻す
        m_PlayerButton[0].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);     //大きく
        //非選択側
        m_PlayerButton[1].GetComponent<Image>().color = m_buttonClearColor;         //選択されていない方を薄く
        m_PlayerButton[1].transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);     //小さく
        
    }
    /// <summary>
    /// プレイヤー2PIconが押された時
    /// </summary>
    void Player2Proces()
    {
        AudioManager.m_instance.PlaySE("button_SE");
        SelectUIManager.PlayerID = 2;
        //選択側
        m_PlayerButton[1].GetComponent<Image>().color = new Vector4(1, 1, 1, 1.0f);
        m_PlayerButton[1].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);     //大きく
        //非選択側
        m_PlayerButton[0].GetComponent<Image>().color = m_buttonClearColor;
        m_PlayerButton[0].transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
    }
    /// <summary>
    /// ゲームボタンが押された時
    /// </summary>
    void GameMainProces()
    {
        AudioManager.m_instance.PlaySE("button_SE");

        //全ての兵士の情報がセットされているかチェック
        if (!isGame)
            return;

        sceneChanger.ChangeToGame();
    }
    /// <summary>
    /// ボタンタッチ範囲指定
    /// </summary>
    /// <param name="_transform"></param>
    /// <param name="_anchorMin"></param>
    /// <param name="_anchorMax"></param>
    /// <param name="_rotate"></param>
    void ButtonRectSet(Transform _transform, Vector2 _anchorMin, Vector2 _anchorMax, Vector3 _rotate)
    {
        GameObject m_touchRectObj = MyUtility.CreateEmpty("rectObj", _transform);

        m_touchRectObj.AddComponent<CanvasRenderer>();
        m_touchRectObj.AddComponent<InvisibleGraphic>();
        RectTransform rectTransform = m_touchRectObj.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(0, 1);
        rectTransform.anchorMin = _anchorMin;
        rectTransform.anchorMax = _anchorMax;
        rectTransform.Rotate(_rotate);
    }
    ///// <summary>
    ///// 円形判定のボタン
    ///// </summary>
    //void CircleProces()
    //{
    //    Vector3 touchPos = TouchManager.GetTouchPosition(0);
    //    Vector3 circlePos = camera.WorldToScreenPoint(circleObj.transform.position);
    //    if (Vector2.Distance(touchPos, circlePos) < 43.0f)
    //    {
    //        Debug.Log("円内");
    //        sceneChanger.ChangeToSelect();
    //    }
    //}

    void Start()
    {

    }

    void Update()
    {

    }
}
