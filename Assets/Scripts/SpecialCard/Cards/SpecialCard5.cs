using UnityEngine;
using System.Collections;

public class SpecialCard5 : SpecialCard
{

    public static string m_howTo = "自分のラインのポイントを全て初期値に\n戻す";
    public static string m_imagePath = "path";

    private bool[] m_reset = new bool[5];
    private int m_nowCount;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            m_reset[i] = false;
        }
    }

    void Update()
    {
        ScoreManager scoreManager;
        try
        {
            scoreManager = gameManager.GetComponent<ScoreManager>();
        }
        catch
        {

            gameManager = GameObject.Find("GameManager").transform;
            scoreManager = gameManager.GetComponent<ScoreManager>();
        }
        for (int i = 1; i <= MyUtility.MAX_COLUMN; i++)
        {
            scoreManager.CountReset(m_UsedPlayerID, i, true);
            if (scoreManager.GetPointLevel(m_UsedPlayerID, i) == 0)
            {
                if(i < 6)m_reset[i - 1] = true;
            }
        }

        //全てのスコアが回復したか確認
        for (int i = 0; i < 5; i++)
        {
            //まだ回復していないスコアがあったら
            if (!m_reset[i])
            {
                break;
            }
            //スコアが全て回復していたら
            else if (i == 4)
            {
                Destroy(this);
            }

        }


    }
}
