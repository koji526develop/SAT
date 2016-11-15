﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResultManager : MonoBehaviour
{

    private bool m_TouchFlag;
    private float m_TouchCoolTime;
    string[] m_IconImagePath = { "Image/sword_I", "Image/spear_I", "Image/ax_I", "Image/shield_I" };

    public static int[] ResultSoldierNum = new int[8];

    GameObject sceneChangerObj;
    SceneChanger sceneChanger;

    GameObject buttonObj;

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

        StartCoroutine(MyUtility.SetCameraForCanvas(canvas, camera));

        GameObject uiObj = MyUtility.CreateEmpty("UI", canvas.transform);
        uiObj.AddComponent<ResultWarPotential>();


        //戦力ゲージ作成
        GameObject slideroObj = MyUtility.CreateSlider(
            "BlueSlider",
            "Image/gage-ao",
            new Vector2(13.0f / 32.0f, 2 / 25.0f),
            new Vector2(19.0f / 32.0f, 23.0f / 25.0f),
            uiObj.transform
            );
        slideroObj.GetComponent<Slider>().direction = Slider.Direction.TopToBottom;

        slideroObj = MyUtility.CreateSlider(
            "RedSlider",
            "Image/gage-akapng",
            new Vector2(13.0f / 32.0f, 2 / 25.0f),
            new Vector2(19.0f / 32.0f, 23.0f / 25.0f),
            uiObj.transform
        );
        slideroObj.GetComponent<Slider>().direction = Slider.Direction.BottomToTop;

        //スコア
        //スコアの計算
        float[] m_Score = new float[2];
        m_Score[0] = WarPotential.lastWarPotential;
        m_Score[1] = MyUtility.MAX_WARPOTENTIAL - m_Score[0];
        //スコアのテキスト
        string[] m_ScoreText = { m_Score[0].ToString(), m_Score[1].ToString() };

        for (int i = 0; i < 2; i++)
        {
            //画像
            GameObject scoreObj = MyUtility.CreateImage(
                "Score",
                "Image/Karie/waku6",
                new Vector2((11.0f + (i * 8)) / 32.0f, 9.0f / 25.0f),
                new Vector2((13.0f + (i * 8)) / 32.0f, 17.0f / 25.0f),
                uiObj.transform);
            //文字
            MyUtility.CreateText(
                m_ScoreText[i],
                scoreObj.transform,
                35,
                new Vector3(0.0f, 0.0f, (i == 0) ? -90.0f : 90.0f),
                new Vector2((9 + (i * 11)) / 32.0f, 12 / 25.0f),
                new Vector2((11 + (i * 11)) / 32.0f, 14 / 25.0f)
                );
        }

        ////
        //使用した兵士、特殊カード
        GameObject[] m_ResultObj = new GameObject[2];

        //相手兵士の詳細テキスト
        string[] m_OpponentSoldierText = {
            "×   " + ResultSoldierNum[0].ToString(),
            "×   " + ResultSoldierNum[1].ToString(),
            "×   " + ResultSoldierNum[2].ToString(),
            "×   " + ResultSoldierNum[3].ToString(),
            "×   " + ResultSoldierNum[4].ToString(),
            "×   " + ResultSoldierNum[5].ToString(),
            "×   " + ResultSoldierNum[6].ToString(),
            "×   " + ResultSoldierNum[7].ToString()
        };

        for (int i = 0; i < 2; i++)
        {
            m_ResultObj[i] = MyUtility.CreateEmpty("ResultUI" + i.ToString(), uiObj.transform);

            //相手兵士詳細
            //画像
            GameObject obj = MyUtility.CreateImage(
                "OpponentSoldier",
                "Image/Karie/waku6",
                new Vector2(2.0f / 32.0f, 16.0f / 25.0f),
                new Vector2(10.0f / 32.0f, 23.0f / 25.0f),
                m_ResultObj[i].transform);


            for (int j = 0; j < 4; j++)
            {
                GameObject imgobj = MyUtility.CreateImage(
                "SoldierIcon",
                m_IconImagePath[j],
                new Vector2((30.5f - (j * 7.5f)) / 32.0f, 18.5f / 25.0f),
                new Vector2((36.5f - (j * 7.5f)) / 32.0f, 24.5f / 25.0f),
                obj.transform);

                imgobj.transform.Rotate(new Vector3(0, 0, 1), -90);
            }

            //文字
            for (int j = 0; j < 4; j++)
            {
                MyUtility.CreateText(
                m_OpponentSoldierText[j + (i * 4)],
                 obj.transform,
                 35,
                 new Vector3(0.0f, 0.0f, -90.0f),
                 new Vector2((26.0f - (7.5f * j)) / 32.0f, 14.0f / 25.0f),
                 new Vector2((26.0f - (7.5f * j)) / 32.0f, 14.0f / 25.0f)
                 );
            }

            //相手特殊カード詳細
            GameObject[] m_SpecialObj = new GameObject[3];
            //画像
            obj = MyUtility.CreateImage(
                "OpponentSpecialCard",
                "Image/Karie/waku6",
                new Vector2(2.0f / 32.0f, 2.0f / 25.0f),
                new Vector2(10.0f / 32.0f, 15.0f / 25.0f),
                m_ResultObj[i].transform);

            for (int j = 0; j < 3; j++)
            {
                //個別画像
                m_SpecialObj[j] = MyUtility.CreateImage(
                    "SpecialCardImg",
                    "Image/resultSpecial",
                    new Vector2(3.0f / 32.0f, (2.0f + (j * 7.5f)) / 25.0f),
                    new Vector2(29.0f / 32.0f, (8.0f + (j * 7.5f)) / 25.0f),
                    obj.transform);
                //文字
                MyUtility.CreateText(
                    "特殊カード",
                    m_SpecialObj[j].transform,
                    35,
                    new Vector3(0.0f, 0.0f, 0.0f),
                    new Vector2(16.0f / 32.0f, 12.5f / 25.0f),
                    new Vector2(16.0f / 32.0f, 12.5f / 25.0f)
                    );
            }
        }
        //作ったもの一つを対になるように配置
        m_ResultObj[1].transform.Rotate(new Vector3(0, 0, 1), 180);
        Vector2 tmpPos = new Vector2(Screen.width - m_ResultObj[0].transform.position.x, Screen.height - m_ResultObj[0].transform.position.y);
        m_ResultObj[1].transform.position = new Vector3(tmpPos.x, tmpPos.y, 0.0f);

        buttonObj = MyUtility.CreateEmpty("Button", canvas.transform);

        //再戦ボタン
        GameObject rematchObj = MyUtility.CreateButton(
            "Rematch",
            "Image/Karie/waku5",
            new Vector2(10 / 32.0f, 14 / 25.0f),
            new Vector2(22 / 32.0f, 23 / 25.0f),
            buttonObj.transform);
        MyUtility.CreateText(
            "再戦",
            rematchObj.transform,
            35,
            new Vector3(0, 0, -90),
            new Vector2(16 / 32.0f, 12.5f / 25.0f),
            new Vector2(16 / 32.0f, 12.5f / 25.0f)
            );
        sceneChangerObj = new GameObject();
        sceneChanger = sceneChangerObj.AddComponent<SceneChanger>();
        rematchObj.GetComponent<Button>().onClick.AddListener(RematchProces);

        //兵士選択へボタン
        GameObject soldierSelectObj = MyUtility.CreateButton(
            "Rematch",
            "Image/Karie/waku5",
            new Vector2(10 / 32.0f, 2 / 25.0f),
            new Vector2(22 / 32.0f, 11 / 25.0f),
            buttonObj.transform);
        MyUtility.CreateText(
            "兵士選択へ",
            soldierSelectObj.transform,
            35,
            new Vector3(0, 0, -90),
            new Vector2(16 / 32.0f, 12.5f / 25.0f),
            new Vector2(16 / 32.0f, 12.5f / 25.0f)
            );
        soldierSelectObj.GetComponent<Button>().onClick.AddListener(sceneChanger.ChangeToSelect);

        buttonObj.SetActive(false);
    }

    public void RematchProces()
    {
        //兵士の情報を設定
        SelectUIManager.SWORD_NUM_1 = ResultManager.ResultSoldierNum[0];
        SelectUIManager.SPEAR_NUM_1 = ResultManager.ResultSoldierNum[1];
        SelectUIManager.AX_NUM_1 = ResultManager.ResultSoldierNum[2];
        SelectUIManager.SHIELD_NUM_1 = ResultManager.ResultSoldierNum[3];
        SelectUIManager.SWORD_NUM_2 = ResultManager.ResultSoldierNum[4];
        SelectUIManager.SPEAR_NUM_2 = ResultManager.ResultSoldierNum[5];
        SelectUIManager.AX_NUM_2 = ResultManager.ResultSoldierNum[6];
        SelectUIManager.SHIELD_NUM_2 = ResultManager.ResultSoldierNum[7];

        //特殊カード設定

        sceneChanger.ChangeToGame();
    }

    void Start()
    {
        m_TouchFlag = false;
        m_TouchCoolTime = 3.0f;
    }


    void Update()
    {
        if (m_TouchCoolTime >= 0.0f)
        {
            m_TouchCoolTime -= Time.deltaTime;
        }
        else
        {
            m_TouchFlag = true;
        }

        TouchInfo touch = TouchManager.GetTouchInfo(0);
        if (touch == TouchInfo.Began)
        {
            if (m_TouchFlag && !buttonObj.active)
            {
                buttonObj.SetActive(true);
            }
            else if(!IsPointerOverUGUIObject(0))
            {
                buttonObj.SetActive(false);
            }
            
        }
    }

    private bool IsPointerOverUGUIObject(int fingerId)
    {
        EventSystem eventSystem = EventSystem.current;
        if (eventSystem != null)
        {
            // マウスでの判定
            if (eventSystem.IsPointerOverGameObject())
            {
                return true;
            }

            // タッチでの判定
            if (eventSystem.IsPointerOverGameObject(fingerId))
            {
                return true;
            }
        }
        return false;
    }
}
