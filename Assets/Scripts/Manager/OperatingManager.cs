﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OperatingManager : MonoBehaviour
{
    const int MAX_SPRITE_NUM = 6;
    readonly float m_MinSprPosition = -3.7f;

    GameObject[] m_OperatingSprNumObj = new GameObject[MAX_SPRITE_NUM];

    void Awake()
    {
        // ライト作成
        MyUtility.CreateDirectionalLight();
        // MAINカメラ作成
        Camera camera = MyUtility.CreateCamera("MainCamera");
        //タップエフェクト
        MyUtility.CreateTapEffect(transform);
        //イベントシステム作成
        MyUtility.CreateEventSystem(transform);

        Canvas canvas = MyUtility.CreateCanvas();

        GameObject uiObj = MyUtility.CreateEmpty("UI", canvas.transform);

        GameObject opObj = MyUtility.CreateEmpty("Operating", transform); ;

        StartCoroutine(MyUtility.SetCameraForCanvas(canvas, camera));

        GameObject m_OperatingSpriteObj;


        //何番目を表示しているかの画像
        for (int i = 0; i < MAX_SPRITE_NUM; i++)
        {
            m_OperatingSprNumObj[i] = MyUtility.CreateSprite(opObj.transform, "OperatingChoiceSprite" + i, "Image/operatingNoneChoice");

            m_OperatingSprNumObj[i].AddComponent<OperatingChoice>();
            m_OperatingSprNumObj[i].transform.position = new Vector3((m_MinSprPosition + (i * 1.5f)), -3.3f, -790.0f);
            m_OperatingSprNumObj[i].GetComponent<OperatingChoice>().SpriteNum = i;
        }
        //操作説明の画像
        m_OperatingSpriteObj = MyUtility.CreateSprite(opObj.transform, "OperatingSprite", "Image/operating1");

        m_OperatingSpriteObj.AddComponent<OperatingFlick>();
        m_OperatingSpriteObj.transform.position = new Vector3(0.0f, 1.0f, -790.0f);
        m_OperatingSpriteObj.transform.localScale = new Vector3(1.4f, 1.4f, 0.0f);

        //左矢印ボタン
        GameObject leftButton = MyUtility.CreateButton(
            "LeftButton",
            "Image/operatingNoneChoice",
             new Vector2(2.0f / 32.0f, 14.0f / 25.0f),
             new Vector2(4.0f / 32.0f, 16.0f / 25.0f),
             uiObj.transform);

        GameObject obj = GameObject.Find("OperatingSprite");
        OperatingFlick operatingFlick = obj.GetComponent<OperatingFlick>();
        leftButton.GetComponent<Button>().onClick.AddListener(operatingFlick.ButtonLeft);

        //右矢印ボタン
        GameObject rightButton = MyUtility.CreateButton(
            "RightButton",
            "Image/operatingNoneChoice",
             new Vector2(28.0f / 32.0f, 14.0f / 25.0f),
             new Vector2(30.0f / 32.0f, 16.0f / 25.0f),
             uiObj.transform);
        rightButton.GetComponent<Button>().onClick.AddListener(operatingFlick.ButtonRight);

        // 戻るボタン作成
        GameObject backObj = MyUtility.CreateButton(
            "Back",
            "Image/karie/waku5",
            new Vector2(3 / 32.0f, 1 / 25.0f),
            new Vector2(9 / 32.0f, 4 / 25.0f),
            uiObj.transform
        );
        MyUtility.AddText("戻る", backObj.transform);
        GameObject sceneChangerObj = new GameObject();
        SceneChanger sceneChanger = sceneChangerObj.AddComponent<SceneChanger>();
        backObj.GetComponent<Button>().onClick.AddListener(sceneChanger.ChangeToTitle);

        // 決定ボタン作成
        GameObject enterObj = MyUtility.CreateButton(
            "Enter",
            "Image/karie/waku5",
            new Vector2(23 / 32.0f, 1 / 25.0f),
            new Vector2(29 / 32.0f, 4 / 25.0f),
            uiObj.transform
        );
        MyUtility.AddText("決定", enterObj.transform);
        enterObj.GetComponent<Button>().onClick.AddListener(sceneChanger.ChangeToSelect);

        //兵士の情報を初期化
        SelectUIManager.SWORD_NUM_1 = 0;
        SelectUIManager.SPEAR_NUM_1 = 0;
        SelectUIManager.AX_NUM_1 = 0;
        SelectUIManager.SHIELD_NUM_1 = 0;
        SelectUIManager.SWORD_NUM_2 = 0;
        SelectUIManager.SPEAR_NUM_2 = 0;
        SelectUIManager.AX_NUM_2 = 0;
        SelectUIManager.SHIELD_NUM_2 = 0;

        SelectUIManager.PlayerID = 1;

    }

    void Start()
    {


    }

    void Update()

    {

    }
}
