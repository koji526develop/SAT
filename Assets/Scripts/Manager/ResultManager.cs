using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{

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

        GameObject[] m_ResultObj = new GameObject[2];
        //スコアの計算
        float[] m_Score = new float[2];
        m_Score[0] = WarPotential.lastWarPotential;
        m_Score[1] = MyUtility.MAX_WARPOTENTIAL - m_Score[0];

        string[] m_ScoreText = { m_Score[0].ToString(), m_Score[1].ToString()};
        
        for (int i = 0; i < 2; i++)
        {
            //スコア
            m_ResultObj[i] = MyUtility.CreateImage(
                "Score",
                "Image/Karie/waku6",
                new Vector2(11.0f / 32.0f, 9.0f / 25.0f),
                new Vector2(13.0f / 32.0f, 17.0f / 25.0f),
                uiObj.transform);

            MyUtility.CreateText(
                m_ScoreText[i],
                m_ResultObj[i].transform,
                35,
                new Vector3(0.0f, 0.0f, -90.0f),
                new Vector2(11 / 32.0f, 12 / 25.0f),
                new Vector2(13 / 32.0f, 14 / 25.0f)
                );
        }


    }

    void Start()
    {

    }

    //仮タイム
    private float time = 5.0f;
    bool flag = false;

    void Update()
    {
        if(!flag)time -= Time.deltaTime;

        if(time <= 0.0f)
        {
            SceneChanger sChange = new SceneChanger();
            sChange.ChangeToTitle();
            flag = true;
        }
    }
}
