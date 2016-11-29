using UnityEngine;
using System.Collections;

public class SpecialCard1 : SpecialCard
{

    public static string m_howTo = "選択したライン上の兵士を消滅";
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
        m_ArrowObj.name = "IceArrow";
        m_ArrowObj.transform.position = new Vector3(30000, -30000, 30000);
        m_ArrowObj.AddComponent<ArrowMove>();
        arrowMove = m_ArrowObj.GetComponent<ArrowMove>();

        if (m_UsedPlayerID == 1) m_arrowPosX = MyUtility.SOLDIER_CREATE_LINE_X_1P - 8.0f;
        else m_arrowPosX = MyUtility.SOLDIER_CREATE_LINE_X_2P;
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
                        Debug.Log("特殊１セット");
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

        //if (m_checkColumn != 0)
        //{
        //    foreach (Transform childObj in battleManager)
        //    {
        //        //キャラクタータグであるならもしくは、同じ列であるなら
        //        if (childObj.tag == "Character" &&
        //            m_checkColumn == childObj.GetComponent<Character>().m_mapColumn)
        //        {
        //            if (childObj.GetComponent<Character>().status.PlayerID != m_UsedPlayerID)
        //            {
        //                if()
        //                {
        //                    //対象を削除
        //                    Destroy(childObj.gameObject);
        //                }
        //            }
        //        }
        //    }
        //    Destroy(this);
        //}
    }
}
