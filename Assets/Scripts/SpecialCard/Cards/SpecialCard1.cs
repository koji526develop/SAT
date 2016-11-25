using UnityEngine;
using System.Collections;

public class SpecialCard1 : SpecialCard {

	public static string m_howTo = "選択したライン上の兵士を消滅";
	public static string m_imagePath = "path";

    private int m_checkColumn = 0;
    GameUIManager m_gameUIManager;
	// Use this for initialization
	void Start () {
        m_gameUIManager = gameManager.GetComponent<GameUIManager>();
	}
	
	// Update is called once per frame
	void Update () {

        for (int i=0;i<TouchManager.touchCount;i++)
        {
            if (TouchManager.GetTouchInfo(i) == TouchInfo.Ended)
            {
                for (int j = 1; j <= MyUtility.MAX_COLUMN; j++)
                {
                    if (m_gameUIManager.TargetRect(TouchManager.GetTouchPosition(i), m_UsedPlayerID, j))
                    {
                        m_checkColumn = j;
                        break;
                    }

                }
            }
        }

        if (m_checkColumn != 0) {

            foreach (Transform childObj in battleManager)
            {
                //キャラクタータグであるならもしくは、同じ列であるなら
                if (childObj.tag == "Character" &&
                    m_checkColumn == childObj.GetComponent<Character>().m_mapColumn)
                {
                    if (childObj.GetComponent<Character>().status.PlayerID != m_UsedPlayerID)
                    {
                        //対象を削除
                        Destroy(childObj.gameObject);
       
                    }
                }
            }
            Destroy(this);
        }
        
	}
}
