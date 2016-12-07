using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    //タッチを受け取っていいか
    private bool m_TouchFlag;
    //タッチを取得し始めるまでの時間
    private float m_TouchCoolTime;

    string[] m_IconImagePath = { "UI/Result/sword_icon", "UI/Result/spear_icon", "UI/Result/ax_icon", "UI/Result/shield_icon" };
    string[] m_SpecialImagePath = { "", "", "", "", "", "" };

    //リザルトで表示する兵士の数を記憶しておくためのもの
    public static int[] ResultSoldierNum = new int[8];
    //リザルトで表示する特殊カードの情報を記憶しておくためのもの
    public static int[] ResultSpecialInfo = new int[6];

    GameObject sceneChangerObj;
    SceneChanger sceneChanger;

    GameObject buttonObj;

    GameObject rematchObj;
    GameObject soldierSelectObj;

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

        //背景
        MyUtility.CreateImage(
             "ResultBackGround",
             "UI/Result/result_back",
             new Vector2(0 / 32.0f, 0 / 25.0f),
             new Vector2(32 / 32.0f, 25 / 25.0f),
             uiObj.transform);

        ////
        //戦力ゲージ作成
        GameObject slideroObj = MyUtility.CreateSlider(
            "BlueSlider",
            "UI/Game/gauge_blue",
            new Vector2(13.0f / 32.0f, 2 / 25.0f),
            new Vector2(19.0f / 32.0f, 23.0f / 25.0f),
            uiObj.transform
            );
        slideroObj.GetComponent<Slider>().direction = Slider.Direction.TopToBottom;
        slideroObj.GetComponentInChildren<Image>().raycastTarget = false;

        slideroObj = MyUtility.CreateSlider(
            "RedSlider",
            "UI/Game/gauge_red",
            new Vector2(13.0f / 32.0f, 2 / 25.0f),
            new Vector2(19.0f / 32.0f, 23.0f / 25.0f),
            uiObj.transform
        );
        slideroObj.GetComponent<Slider>().direction = Slider.Direction.BottomToTop;
        slideroObj.GetComponentInChildren<Image>().raycastTarget = false;
        ////
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
                "UI/Result/information_back",
                new Vector2((11.0f + (i * 8)) / 32.0f, 9.0f / 25.0f),
                new Vector2((13.0f + (i * 8)) / 32.0f, 17.0f / 25.0f),
                uiObj.transform);
            //文字
            Text text = MyUtility.CreateText(
                m_ScoreText[i],
                scoreObj.transform,
                35,
                new Vector3(0.0f, 0.0f, (i == 0) ? -90.0f : 90.0f),
                new Vector2((11 + (i * 11)) / 32.0f, 12 / 25.0f),
                new Vector2((13 + (i * 11)) / 32.0f, 14 / 25.0f)
                );
            text.transform.position += (i == 0) ? new Vector3(10, 0, 0) : new Vector3(-10, 0, 0);
        }

        //// プレイヤーの戦闘で使用した兵、カード
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

        //特殊カードの画像パス
        for (int i = 0; i < 6; i++)
        {
            m_SpecialImagePath[i] = "UI/Result/card" + ResultSpecialInfo[i].ToString();
        }

        for (int i = 0; i < 2; i++)
        {
            m_ResultObj[i] = MyUtility.CreateEmpty("ResultUI" + i.ToString(), uiObj.transform);

            //相手兵士詳細
            //画像

            GameObject obj = MyUtility.CreateImage(
                "OpponentSoldier",
                "UI/Result/information_back",
                new Vector2(2.0f / 32.0f, 16.0f / 25.0f),
                new Vector2(10.0f / 32.0f, 23.0f / 25.0f),
                m_ResultObj[i].transform);

            for (int j = 0; j < 4; j++)
            {
                GameObject imgobj = MyUtility.CreateImage(
                "SoldierIcon",
                m_IconImagePath[j],
                new Vector2((30.5f - (j * 7.5f)) / 32.0f, 16.5f / 25.0f),
                new Vector2((36.5f - (j * 7.5f)) / 32.0f, 22.5f / 25.0f),
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
                 new Vector2((27.5f - (7.5f * j)) / 32.0f, 12.0f / 25.0f),
                 new Vector2((27.5f - (7.5f * j)) / 32.0f, 12.0f / 25.0f)
                 );
            }

            //相手特殊カード詳細
            GameObject[] m_SpecialObj = new GameObject[3];
            //画像
            obj = MyUtility.CreateImage(
                "OpponentSpecialCard",
                "UI/Result/information_back",
                new Vector2(2.0f / 32.0f, 2.0f / 25.0f),
                new Vector2(10.0f / 32.0f, 15.0f / 25.0f),
                m_ResultObj[i].transform);

            for (int j = 0; j < 3; j++)
            {
                //個別画像
                m_SpecialObj[j] = MyUtility.CreateImage(
                    "SpecialCardImg",
                    m_SpecialImagePath[j + (i * 3)],
                    new Vector2(3.0f / 32.0f, (2.0f + (j * 7.5f)) / 25.0f),
                    new Vector2(29.0f / 32.0f, (8.0f + (j * 7.5f)) / 25.0f),
                    obj.transform);
            }
        }
        //作ったもの一つを対になるように配置
        m_ResultObj[1].transform.Rotate(new Vector3(0, 0, 1), 180);
        Vector2 tmpPos = new Vector2(Screen.width - m_ResultObj[0].transform.position.x, Screen.height - m_ResultObj[0].transform.position.y);
        m_ResultObj[1].transform.position = new Vector3(tmpPos.x, tmpPos.y, 0.0f);

        ////
        buttonObj = MyUtility.CreateEmpty("Button", canvas.transform);

        //再戦ボタン
        rematchObj = MyUtility.CreateButton(
            "Rematch",
            "UI/Result/restart",
            new Vector2(10 / 32.0f, 14 / 25.0f),
            new Vector2(22 / 32.0f, 23 / 25.0f),
            buttonObj.transform);

        sceneChangerObj = new GameObject();
        sceneChanger = sceneChangerObj.AddComponent<SceneChanger>();
        rematchObj.GetComponent<Button>().onClick.AddListener(RematchProces);

        //兵士選択へボタン
        soldierSelectObj = MyUtility.CreateButton(
            "SoldierSelect",
            "UI/Result/reselect",
            new Vector2(10 / 32.0f, 2 / 25.0f),
            new Vector2(22 / 32.0f, 11 / 25.0f),
            buttonObj.transform);

        soldierSelectObj.GetComponent<Button>().onClick.AddListener(RelayProces);

        buttonObj.SetActive(false);

    }

    //再戦
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
        for (int i = 0; i < 3; i++)
        {
            SelectSpecialUIManager.SPECIALCARD_NUMBER_1[i] = ResultSpecialInfo[i];
            SelectSpecialUIManager.SPECIALCARD_NUMBER_2[i] = ResultSpecialInfo[i + 3];
        }


        sceneChanger.ChangeToGame();
    }

    //Relayシーンへ
    public void RelayProces()
    {
        //兵士の情報を初期化
        SelectUIManager.SWORD_NUM_1 = 0;
        SelectUIManager.SPEAR_NUM_1 = 0;
        SelectUIManager.AX_NUM_1 = 0;
        SelectUIManager.SHIELD_NUM_1 = 0;
        SelectUIManager.SWORD_NUM_2 = 0;
        SelectUIManager.SPEAR_NUM_2 = 0;
        SelectUIManager.AX_NUM_2 = 0;
        SelectUIManager.SHIELD_NUM_2 = 0;

        //特殊カードの情報を初期化
        for (int i = 0; i < 3; i++)
        {
            SelectSpecialUIManager.SPECIALCARD_NUMBER_1[i] = 0;
            SelectSpecialUIManager.SPECIALCARD_NUMBER_2[i] = 0;
        }
        for (int i = 0; i < 4; i++)
        {
            RelayManager.isDoneSetting[i] = false;
        }


        sceneChanger.ChangeToRelay();
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
            //タッチしてよい && ボタンが表示されていない
            if (m_TouchFlag && !buttonObj.active)
            {
                buttonObj.SetActive(true);
            }
            //タッチ場所がボタンの上でなければ
            else if (!MyUtility.IsContainPoint(rematchObj.GetComponent<RectTransform>()) && !MyUtility.IsContainPoint(soldierSelectObj.GetComponent<RectTransform>()))
            {
                buttonObj.SetActive(false);
            }
        }
    }
}
