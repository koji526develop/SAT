using UnityEngine;
using System.Collections;

public class SpecialCard5 : SpecialCard
{

    public static string m_howTo = "このカードを発動した後\n自分の全てのラインのスコアを初期値に戻す。";
    public static string m_imagePath = "path";

    private bool[] m_reset = new bool[5];
    private int m_nowCount;

    GameObject m_starObj;
    float m_starPosX;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            m_reset[i] = false;
        }

        if (m_UsedPlayerID == 1) m_starPosX = MyUtility.SOLDIER_CREATE_LINE_X_1P + 0.8f;
        else m_starPosX = MyUtility.SOLDIER_CREATE_LINE_X_2P - 0.8f;

        m_starObj = Instantiate(Resources.Load("SpecialWeapon/SpeciaRecoveryScore/star")) as GameObject;
        m_starObj.transform.position = new Vector3(m_starPosX, 1, 0);
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
                if (i < 6) m_reset[i - 1] = true;
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
                Destroy(m_starObj, 1.5f);
                Destroy(this);
            }

        }


    }
}
