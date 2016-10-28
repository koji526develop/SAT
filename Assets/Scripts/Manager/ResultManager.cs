using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{

    private bool m_ChangeFlag;
    private float m_ChangeTime;

    void Awake()
    {
        Canvas canvas = MyUtility.CreateCanvas();

        GameObject uiObj = MyUtility.CreateEmpty("UI", canvas.transform);
        uiObj.AddComponent<ResultWarPotential>();

        // イベントシステム作成
        MyUtility.CreateEventSystem(transform);

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
        //m_Score[0] = WarPotential.lastWarPotential;
        //デバッグ
        m_Score[0] = 250.0f;
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
        string[] m_OpponentSoldierText = { "剣 × " + 10.ToString(), "槍 × " + 10.ToString(), "斧 × " + 10.ToString(), "盾 × " + 10.ToString() };

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
            //文字
            for (int j = 0; j < 4; j++)
            {
                MyUtility.CreateText(
                m_OpponentSoldierText[j],
                 obj.transform,
                 35,
                 new Vector3(0.0f, 0.0f, -90.0f),
                 new Vector2((25.5f - (7.0f * j)) / 32.0f, 14.0f / 25.0f),
                 new Vector2((25.5f - (7.0f * j)) / 32.0f, 14.0f / 25.0f)
                 );
            }

            //相手特殊カード詳細
            GameObject[] m_SpecialObj = new GameObject[3];
            //画像
            obj = MyUtility.CreateImage(
                "OpponentSpecialCard",
                "Image/Karie/waku6",
                new Vector2(2.0f / 32.0f, 2.0f / 25.0f),
                new Vector2(10.0f / 32.0f, 13.0f / 25.0f),
                m_ResultObj[i].transform);

            for (int j = 0; j < 3; j++)
            {
                //個別画像
                m_SpecialObj[j] = MyUtility.CreateImage(
                    "SpecialCardImg",
                    "Image/Karie/waku2",
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
        m_ResultObj[1].transform.position = new Vector3(Screen.width / 32.0f * 16, Screen.height / 25.0f * 12.5f, 0.0f);
    }

    void Start()
    {
        m_ChangeFlag = false;
        m_ChangeTime = 5.0f;
    }


    void Update()
    {
        if(m_ChangeTime >= 0.0f)
        {
            m_ChangeTime -= Time.deltaTime;
        }
        else
        {
            m_ChangeFlag = true;
        }
        if(m_ChangeFlag)
        {
            SceneChanger sChange = new SceneChanger();
            sChange.ChangeToTitle();
            m_ChangeFlag = false;
        }
    }
}
