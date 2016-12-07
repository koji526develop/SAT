﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RelayManager : MonoBehaviour
{
    SceneChanger sceneChanger;

    private int m_touchScene;       //どのボタンをタッチしているか

    const int NOT_SET_SCENE = -1;
    const int SOLDIER1P = 0;
    const int SOLDIER2P = 1;
    const int SPECIAL1P = 2;
    const int SPECIAL2P = 3;
    const int OPERATING = 4;
    const int GAMEMAIN = 5;

    readonly Color m_choiceColor = new Vector4(1, 1, 1, 0.5f);        //ボタンが押されている状態の色
    readonly Color m_nonChoiceColor = new Vector4(1, 1, 1, 1);        //ボタンが押されていないときの色

    GameObject[] m_scenesButtonObj = new GameObject[6];     //それぞれのシーンのボタン

    public static bool[] isDoneSetting = new bool[4];              //兵士の情報がセットされているかどうか

    void Awake()
    {
        // ライト作成
        MyUtility.CreateDirectionalLight();
        // MAINカメラ作成
        Camera camera = MyUtility.CreateCamera("MainCamera");
        //タップエフェクト
        MyUtility.CreateTapEffect(transform);

        Canvas canvas = MyUtility.CreateCanvas();
        //イベントシステム作成
        MyUtility.CreateEventSystem(transform);

        StartCoroutine(MyUtility.SetCameraForCanvas(canvas, camera));

        GameObject m_RelayObject = MyUtility.CreateEmpty("RelayObject", canvas.transform);
        GameObject m_RelayButton = MyUtility.CreateEmpty("RelayButton", canvas.transform);

        //背景
        MyUtility.CreateImage(
            "RelayBackGround",
            "UI/Relay/relay_back",
            new Vector2(0 / 32.0f, 0 / 25.0f),
            new Vector2(32 / 32.0f, 25 / 25.0f),
            m_RelayObject.transform);

        //操作説明画像
        m_scenesButtonObj[OPERATING] = MyUtility.CreateButton(
             "Operating",
             "UI/Relay/operating",
             new Vector2(12 / 32.0f, 19 / 25.0f),
             new Vector2(20 / 32.0f, 23 / 25.0f),
             m_RelayButton.transform);
        m_scenesButtonObj[OPERATING].GetComponent<Button>().onClick.AddListener(OperatingProces);

        //兵士選択1P画像
        m_scenesButtonObj[SOLDIER1P] = MyUtility.CreateButton(
            "Select1P",
            "UI/Relay/soldier1",
            new Vector2(3 / 32.0f, 15 / 25.0f),
            new Vector2(11 / 32.0f, 19 / 25.0f),
            m_RelayButton.transform);
        m_scenesButtonObj[SOLDIER1P].GetComponent<Button>().onClick.AddListener(Soldier1Proces);

        //兵士選択2P画像
        m_scenesButtonObj[SOLDIER2P] = MyUtility.CreateButton(
            "Select2P",
            "UI/Relay/soldier2",
            new Vector2(21 / 32.0f, 15 / 25.0f),
            new Vector2(29 / 32.0f, 19 / 25.0f),
            m_RelayButton.transform);
        m_scenesButtonObj[SOLDIER2P].GetComponent<Button>().onClick.AddListener(Soldier2Proces);

        //特殊カード選択1P画像
        m_scenesButtonObj[SPECIAL1P] = MyUtility.CreateButton(
            "Special1P",
            "UI/Relay/special1",
            new Vector2(3 / 32.0f, 7 / 25.0f),
            new Vector2(11 / 32.0f, 11 / 25.0f),
            m_RelayButton.transform);
        m_scenesButtonObj[SPECIAL1P].GetComponent<Button>().onClick.AddListener(Special1Proces);

        //特殊カード選択2P画像
        m_scenesButtonObj[SPECIAL2P] = MyUtility.CreateButton(
            "Special2P",
            "UI/Relay/special2",
            new Vector2(21 / 32.0f, 7 / 25.0f),
            new Vector2(29 / 32.0f, 11 / 25.0f),
            m_RelayButton.transform);
        m_scenesButtonObj[SPECIAL2P].GetComponent<Button>().onClick.AddListener(Special2Proces);

        //ゲームメイン画像
        m_scenesButtonObj[GAMEMAIN] = MyUtility.CreateButton(
            "GameMain",
            "UI/Relay/game",
            new Vector2(12 / 32.0f, 2 / 25.0f),
            new Vector2(20 / 32.0f, 6 / 25.0f),
            m_RelayButton.transform);
        m_scenesButtonObj[GAMEMAIN].GetComponent<Button>().onClick.AddListener(GameMainProces);
        //兵士が選択されていなかったら選べないようにする
        for (int i = 0; i < 4; i++)
        {
            if (!isDoneSetting[i])
            {
                m_scenesButtonObj[GAMEMAIN].GetComponent<Image>().color = new Vector4(1, 1, 1, 0.2f);
                break;
            }
        }


        //戻るボタン作成
        GameObject backObj = MyUtility.CreateButton(
            "Back",
            "UI/Relay/return",
            new Vector2(2 / 32.0f, 1 / 25.0f),
            new Vector2(9 / 32.0f, 4 / 25.0f),
            m_RelayButton.transform
           );

        GameObject sceneChangerObj = new GameObject();
        sceneChanger = sceneChangerObj.AddComponent<SceneChanger>();
        backObj.GetComponent<Button>().onClick.AddListener(sceneChanger.ChangeToTitle);

        // 決定ボタン作成
        GameObject enterObj = MyUtility.CreateButton(
            "Enter",
            "UI/Operating/decision",
            new Vector2(23 / 32.0f, 1 / 25.0f),
            new Vector2(30 / 32.0f, 4 / 25.0f),
            m_RelayButton.transform
        );
        enterObj.GetComponent<Button>().onClick.AddListener(EnterProces);

        m_touchScene = NOT_SET_SCENE;
    }


    //=============================================================
    //=============================================================


    //操作説明が押された時
    void OperatingProces()
    {
        AudioManager.m_instance.PlaySE("button_SE");

        //選択状態にする
        if (m_touchScene != OPERATING) ChangeChoice(OPERATING, m_touchScene);
        //選択解除
        else ChangeChoice(NOT_SET_SCENE, m_touchScene);
    }

    //兵士選択1が押された時
    void Soldier1Proces()
    {
        AudioManager.m_instance.PlaySE("button_SE");

        //選択状態にする
        if (m_touchScene != SOLDIER1P)
        {
            SelectUIManager.PlayerID = 1;
            ChangeChoice(SOLDIER1P, m_touchScene);
        }
        //選択解除
        else ChangeChoice(NOT_SET_SCENE, m_touchScene);
    }

    //兵士選択2が押された時
    void Soldier2Proces()
    {
        AudioManager.m_instance.PlaySE("button_SE");

        //選択状態にする
        if (m_touchScene != SOLDIER2P)
        {
            SelectUIManager.PlayerID = 2;
            ChangeChoice(SOLDIER2P, m_touchScene);
        }
        //選択解除
        else ChangeChoice(NOT_SET_SCENE, m_touchScene);
    }

    //特殊1が押された時
    void Special1Proces()
    {
        AudioManager.m_instance.PlaySE("button_SE");

        //選択状態にする
        if (m_touchScene != SPECIAL1P)
        {
            SelectUIManager.PlayerID = 1;
            ChangeChoice(SPECIAL1P, m_touchScene);
        }
        //選択解除
        else ChangeChoice(NOT_SET_SCENE, m_touchScene);
    }

    //特殊2が押された時
    void Special2Proces()
    {
        AudioManager.m_instance.PlaySE("button_SE");

        //選択状態にする
        if (m_touchScene != SPECIAL2P)
        {
            SelectUIManager.PlayerID = 2;
            ChangeChoice(SPECIAL2P, m_touchScene);
        }
        //選択解除
        else ChangeChoice(NOT_SET_SCENE, m_touchScene);
    }

    //ゲームボタンが押された時
    void GameMainProces()
    {
        AudioManager.m_instance.PlaySE("button_SE");

        //全ての兵士の情報がセットされているかチェック
        for (int i = 0; i < 4; i++)
        {
            if (!isDoneSetting[i])
            {
                return;
            }
        }

        //選択状態にする
        if (m_touchScene != GAMEMAIN) ChangeChoice(GAMEMAIN, m_touchScene);
        //選択解除
        else ChangeChoice(NOT_SET_SCENE, m_touchScene);
    }

    //選択状態の更新
    void ChangeChoice(int _setScene, int _nowChoiceScene)
    {
        m_touchScene = _setScene;

        if (m_touchScene != NOT_SET_SCENE) m_scenesButtonObj[_setScene].GetComponent<Image>().color = m_choiceColor;

        //前回タッチされていたボタンを解除
        if (_nowChoiceScene != NOT_SET_SCENE) m_scenesButtonObj[_nowChoiceScene].GetComponent<Image>().color = m_nonChoiceColor;
    }

    //決定ボタンが押された時
    void EnterProces()
    {
        AudioManager.m_instance.PlaySE("button_SE");

        switch (m_touchScene)
        {
            case OPERATING:
                sceneChanger.ChangeToOperating();
                break;
            case SOLDIER1P:
                sceneChanger.ChangeToSelect();
                break;
            case SOLDIER2P:
                sceneChanger.ChangeToSelect();
                break;
            case SPECIAL1P:
                sceneChanger.ChangeToSelectSpecial();
                break;
            case SPECIAL2P:
                sceneChanger.ChangeToSelectSpecial();
                break;
            case GAMEMAIN:
                sceneChanger.ChangeToGame();
                break;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }
}