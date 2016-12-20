using UnityEngine;
using System.Collections;

public class SpecialCard2 : SpecialCard
{

    public static string m_howTo = "ラインを二つ選択する。\nその後選択したラインの兵士を全て消滅させる。\nただし相手に100ポイント加算される。";
    public static string m_imagePath = "path";

    private int[] m_checkColumn = new int[2];
    //private int m_checkColumn2 = 0;

    GameUIManager m_gameUIManager;

    GameObject[] m_ArrowObj = new GameObject[2];
    float[] m_arrowPosZ = new float[6];
    float m_arrowPosX;
    ArrowMove[] arrowMove = new ArrowMove[2];


    void Start()
    {
        try
        {
            m_gameUIManager = gameManager.GetComponent<GameUIManager>();
        }
        catch
        {

            gameManager = GameObject.Find("GameManager").transform;
            m_gameUIManager = gameManager.GetComponent<GameUIManager>();
        }

        m_gameUIManager.RectRender(m_UsedPlayerID, true);

        for (int i = 0; i < 2; i++)
        {
            if (m_UsedPlayerID == 1)
            {
                m_ArrowObj[i] = GameObject.Instantiate(Resources.Load("SpecialWeapon/SpecialArrow/SpecialArrow1")) as GameObject;
                m_arrowPosX = MyUtility.SOLDIER_CREATE_LINE_X_1P - 8.0f;
            }
            else
            {
                m_ArrowObj[i] = GameObject.Instantiate(Resources.Load("SpecialWeapon/SpecialArrow/SpecialArrow2")) as GameObject;
                m_arrowPosX = MyUtility.SOLDIER_CREATE_LINE_X_2P + 8.0f;
            }
            m_ArrowObj[i].name = "FireArrow";
            m_ArrowObj[i].transform.position = new Vector3(0 + (i * 1000), -30000, 0);
            m_ArrowObj[i].AddComponent<ArrowMove>();
            arrowMove[i] = m_ArrowObj[i].GetComponent<ArrowMove>();
        }

        for (int i = 0; i < 6; i++)
        {
            m_arrowPosZ[i] = 4 - (i * 2);
        }

    }


    void Update()
    {


        for (int i = 0; i < TouchManager.touchCount; i++)
        {
            if (TouchManager.GetTouchInfo(i) == TouchInfo.Ended)
            {
                for (int j = 1; j <= MyUtility.MAX_COLUMN; j++)
                {
                    if (m_gameUIManager.TargetRect(TouchManager.GetTouchPosition(i), m_UsedPlayerID, j))
                    {
                        if (m_checkColumn[0] == 0)
                        {
                            m_checkColumn[0] = j;
                        }
                        else if (m_checkColumn[0] == j)
                        {
                            m_checkColumn[0] = 0;
                        }
                        else if (m_checkColumn[1] != j)
                        {
                            m_checkColumn[1] = j;
                            for (int num = 0; num < 2; num++)
                            {
                                arrowMove[num].PlayerID = m_UsedPlayerID;
                                arrowMove[num].Column = m_checkColumn[num];
                                m_ArrowObj[num].transform.position = new Vector3(m_arrowPosX, 1, m_arrowPosZ[m_checkColumn[num] - 1]);
                            }
                            int enemyPlayerID = 0;
                            if (m_UsedPlayerID == 1) { enemyPlayerID = 2; }
                            else { enemyPlayerID = 1; }
                            gameManager.GetComponent<ScoreManager>().DirectGetPoint(enemyPlayerID, 50);
                            m_gameUIManager.RectRender(m_UsedPlayerID, false);

                            Destroy(this);
                            break;
                        }
                    }

                }
            }
        }
    }
}
