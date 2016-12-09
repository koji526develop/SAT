using UnityEngine;
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

        //背景
        MyUtility.CreateImage(
            "OperatingBackGround",
            "UI/Operating/operating_back",
            new Vector2(0 / 32.0f, 0 / 25.0f),
            new Vector2(32 / 32.0f, 25 / 25.0f),
            uiObj.transform);

        //何番目を表示しているかの画像
        //○画像
        for (int i = 0; i < MAX_SPRITE_NUM; i++)
        {
            m_OperatingSprNumObj[i] = MyUtility.CreateSprite(opObj.transform, "OperatingChoiceSprite" + i, "UI/Operating/fcircle");

            m_OperatingSprNumObj[i].AddComponent<OperatingChoice>();
            m_OperatingSprNumObj[i].transform.position = new Vector3((m_MinSprPosition + (i * 1.5f)), -3.3f, -790.0f);
            m_OperatingSprNumObj[i].transform.localScale = new Vector3(0.3f,0.3f,0.0f);
            m_OperatingSprNumObj[i].GetComponent<OperatingChoice>().SpriteNum = i;
        }
        //操作説明の画像
        //内容
        m_OperatingSpriteObj = MyUtility.CreateSprite(opObj.transform, "OperatingSprite", "Image/operating1");

        m_OperatingSpriteObj.AddComponent<OperatingFlick>();
        m_OperatingSpriteObj.transform.position = new Vector3(0.0f, 1.0f, -790.0f);
        m_OperatingSpriteObj.transform.localScale = new Vector3(1.4f, 1.4f, 0.0f);

        //左矢印ボタン
        GameObject leftButton = MyUtility.CreateButton(
            "LeftButton",
            "UI/Operating/triangle",
             new Vector2(2.0f / 32.0f, 14.0f / 25.0f),
             new Vector2(4.0f / 32.0f, 16.0f / 25.0f),
             uiObj.transform);
        leftButton.transform.localScale = new Vector3(1.5f,1.5f,0.0f);

        GameObject obj = GameObject.Find("OperatingSprite");
        OperatingFlick operatingFlick = obj.GetComponent<OperatingFlick>();
        leftButton.GetComponent<Button>().onClick.AddListener(operatingFlick.ButtonLeft);

        //右矢印ボタン
        GameObject rightButton = MyUtility.CreateButton(
            "RightButton",
            "UI/Operating/triangle",
             new Vector2(30.0f / 32.0f, 11.0f / 25.0f),
             new Vector2(32.0f / 32.0f, 13.0f / 25.0f),
             uiObj.transform);
        rightButton.transform.Rotate(new Vector3(0, 0, 1), 180.0f);
        rightButton.transform.localScale = new Vector3(1.5f, 1.5f, 0.0f);
        rightButton.GetComponent<Button>().onClick.AddListener(operatingFlick.ButtonRight);

        // 戻るボタン作成
        GameObject backObj = MyUtility.CreateButton(
            "Back",
            "UI/Operating/return",
            new Vector2(3 / 32.0f, 1 / 25.0f),
            new Vector2(9 / 32.0f, 4 / 25.0f),
            uiObj.transform
        );

        backObj.GetComponent<Button>().onClick.AddListener(BackProces);

        // 決定ボタン作成
        GameObject enterObj = MyUtility.CreateButton(
            "Enter",
            "UI/Operating/decision",
            new Vector2(23 / 32.0f, 1 / 25.0f),
            new Vector2(29 / 32.0f, 4 / 25.0f),
            uiObj.transform
        );
        //enterObj.GetComponent<Button>().onClick.AddListener(sceneChanger.ChangeToSelect);
    }

    void BackProces()
    {
        AudioManager.m_instance.PlaySE("button_SE");
        GameObject sceneChangerObj = new GameObject();
        SceneChanger sceneChanger = sceneChangerObj.AddComponent<SceneChanger>();
        sceneChanger.ChangeToMenu();
    }

    void Start()
    {


    }

    void Update()

    {

    }
}
