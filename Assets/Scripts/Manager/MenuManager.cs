using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
        //現在のプレイヤーではない方のボタンを半透明に
        if(SelectUIManager.PlayerID == 1) m_PlayerButton[1].GetComponent<Image>().color = new Vector4(1, 1, 1, 0.2f);
        else if (SelectUIManager.PlayerID == 2) m_PlayerButton[0].GetComponent<Image>().color = new Vector4(1, 1, 1, 0.2f);

        ////操作説明画像
        //m_scenesButtonObj[OPERATING] = MyUtility.CreateButton(
        //     "Operating",
        //     "UI/Menu/operating",
        //     new Vector2(12 / 32.0f, 19 / 25.0f),
        //     new Vector2(20 / 32.0f, 23 / 25.0f),
        //     m_MenuButton.transform);
        //m_scenesButtonObj[OPERATING].GetComponent<Button>().onClick.AddListener(OperatingProces);

        //兵士編成ボタン
        GameObject soldierFormationObj = MyUtility.CreateButton(
            "Select1P",
            "UI/Menu/soldier1",
            new Vector2(8 / 32.0f, 12 / 25.0f),
            new Vector2(16 / 32.0f, 19 / 25.0f),
            m_MenuButton.transform);
        soldierFormationObj.GetComponent<Button>().onClick.AddListener(sceneChanger.ChangeToSelect);

        //特殊カード選択ボタン
        GameObject specialCardObj = MyUtility.CreateButton(
            "Special1P",
            "UI/Menu/special1",
            new Vector2(16 / 32.0f, 8 / 25.0f),
            new Vector2(24 / 32.0f, 15 / 25.0f),
            m_MenuButton.transform);
        specialCardObj.GetComponent<Button>().onClick.AddListener(sceneChanger.ChangeToSelectSpecial);

        //チュートリアルボタン作成
        GameObject tutoObj = MyUtility.CreateButton(
            "Tutorial",
            "UI/Menu/return",
            new Vector2(6 / 32.0f, 2 / 25.0f),
            new Vector2(15 / 32.0f, 6 / 25.0f),
            m_MenuButton.transform
           );

        tutoObj.GetComponent<Button>().onClick.AddListener(sceneChanger.ChangeToTitle);

        // 開戦ボタン作成
        GameObject gameObj = MyUtility.CreateButton(
            "Enter",
            "UI/Operating/decision",
            new Vector2(17 / 32.0f, 2 / 25.0f),
            new Vector2(26 / 32.0f, 6 / 25.0f),
            m_MenuButton.transform
        );
        gameObj.GetComponent<Button>().onClick.AddListener(GameMainProces);

        //全ての兵士、カードの情報がセットされていなかったら選べないようにする
        isGame = m_playerSetting.isSoldier_1P && m_playerSetting.isSoldier_2P && m_playerSetting.isSoldier_1P && m_playerSetting.isSoldier_1P;

        if (!isGame)
        {
            gameObj.GetComponent<Image>().color = new Vector4(1, 1, 1, 0.2f);
        }
    }


    //=============================================================
    //=============================================================

    //プレイヤー1PIconが押された時
    void Player1Proces()
    {
        AudioManager.m_instance.PlaySE("button_SE");
        SelectUIManager.PlayerID = 1;
        m_PlayerButton[0].GetComponent<Image>().color = new Vector4(1, 1, 1, 1.0f);
        m_PlayerButton[1].GetComponent<Image>().color = new Vector4(1, 1, 1, 0.2f);
    }

    //プレイヤー2PIconが押された時
    void Player2Proces()
    {
        AudioManager.m_instance.PlaySE("button_SE");
        SelectUIManager.PlayerID = 2;
        m_PlayerButton[0].GetComponent<Image>().color = new Vector4(1, 1, 1, 0.2f);
        m_PlayerButton[1].GetComponent<Image>().color = new Vector4(1, 1, 1, 1.0f);
    }

    //ゲームボタンが押された時
    void GameMainProces()
    {
        AudioManager.m_instance.PlaySE("button_SE");

        //全ての兵士の情報がセットされているかチェック
        if (!isGame)
            return;

        sceneChanger.ChangeToGame();
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
