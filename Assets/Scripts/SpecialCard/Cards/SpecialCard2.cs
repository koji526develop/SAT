using UnityEngine;
using System.Collections;

public class SpecialCard2 : SpecialCard {

	public static string m_howTo = "ラインを二か所選択し、そのライン上の\n兵士を消滅";
	public static string m_imagePath = "path";

    private int m_checkColumn = 0;
    private int m_checkColumn2= 0;

    GameUIManager m_gameUIManager;
    // Use this for initialization
    void Start () {
        try
        {
            m_gameUIManager = gameManager.GetComponent<GameUIManager>();
        }
        catch
        {

            gameManager = GameObject.Find("GameManager").transform;
            m_gameUIManager = gameManager.GetComponent<GameUIManager>();
        }
    }
	
	// Update is called once per frame
	void Update () {


        for (int i = 0; i < TouchManager.touchCount; i++)
        {
            if (TouchManager.GetTouchInfo(i) == TouchInfo.Ended)
            {
                for (int j = 1; j <= MyUtility.MAX_COLUMN; j++)
                {
                    if (m_gameUIManager.TargetRect(TouchManager.GetTouchPosition(i), m_UsedPlayerID, j))
                    {
                        if (m_checkColumn == 0)
                        {
                            m_checkColumn = j;
                        }
                        else if(m_checkColumn!=j){
                            m_checkColumn2 = j;
                        }
                        break;
                    }

                }
            }
        }

        if (m_checkColumn != 0 && m_checkColumn2 != 0)
        {

            foreach (Transform childObj in battleManager)
            {
                //キャラクタータグであるならもしくは、同じ列であるなら
                if (childObj.tag == "Character" &&
                    (m_checkColumn == childObj.GetComponent<Character>().m_mapColumn ||
                    m_checkColumn2 == childObj.GetComponent<Character>().m_mapColumn) )
                {
                    if (childObj.GetComponent<Character>().status.PlayerID != m_UsedPlayerID)
                    {
                        //対象を削除
                        Character.Status status = childObj.GetComponent<Character>().status;

                        status.life = 0;

                        childObj.GetComponent<Character>().status = status;

                    }
                }
            }

            int enemyPlayerID = 0;
            if (m_UsedPlayerID == 1)
            {
                enemyPlayerID = 2;
            }
            else
            {
                enemyPlayerID = 1;
            }
            gameManager.GetComponent<ScoreManager>().DirectGetPoint(enemyPlayerID,100);
            Destroy(this);
        }
    }
}
