using UnityEngine;
using System.Collections;

public class SpecialCard1 : SpecialCard
{

    public static string m_howTo = "ラインを1つ選択する。\nその後選択したライン上の兵士を全て消滅させる。\nこのときポイントは加算されない。";
    public static string m_imagePath = "path";

    private int m_checkColumn = 0;
    GameUIManager m_gameUIManager;

    GameObject m_ArrowObj;
    float[] m_arrowPosZ = new float[6];
    float m_arrowPosX;
    ArrowMove arrowMove;

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

        m_ArrowObj = GameObject.Instantiate(Resources.Load("Particle/SpecialArrow/SpecialArrow")) as GameObject;
        m_ArrowObj.name = "FireArrow";
        m_ArrowObj.transform.position = new Vector3(30000, -30000, 30000);
        m_ArrowObj.AddComponent<ArrowMove>();
        arrowMove = m_ArrowObj.GetComponent<ArrowMove>();

        if (m_UsedPlayerID == 1) m_arrowPosX = MyUtility.SOLDIER_CREATE_LINE_X_1P - 8.0f;
        else m_arrowPosX = MyUtility.SOLDIER_CREATE_LINE_X_2P + 8.0f;
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
                        m_checkColumn = j;
                        arrowMove.PlayerID = m_UsedPlayerID;
                        arrowMove.Column = m_checkColumn;
                        m_ArrowObj.transform.position = new Vector3(m_arrowPosX, 1, m_arrowPosZ[m_checkColumn - 1]);
                        Destroy(this);
                        break;
                    }
                }
            }
        }
    }
}
