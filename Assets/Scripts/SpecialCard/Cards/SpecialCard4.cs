using UnityEngine;
using System.Collections;

public class SpecialCard4 : SpecialCard {

	public static string m_howTo = "ラインを二か所を選択しそのライン上に\nお互いに兵士を出撃させることは\nできない";
	public static string m_imagePath = "path";

    private int m_checkColumn = 0;
    private int m_checkColumn2 = 0;
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
                        else if (m_checkColumn != j)
                        {
                            m_checkColumn2 = j;
                        }
                        break;
                    }

                }
            }
        }

        if (m_checkColumn != 0 && m_checkColumn2 != 0)
        {

            foreach (Transform childObj in this.transform.parent)
            {
                if (childObj.tag == "SoliderButton")
                {
                    ButtonSpawner btnCmp = childObj.GetComponent<ButtonSpawner>();

                    if(btnCmp.m_ButtonID == m_checkColumn ||
                       btnCmp.m_ButtonID == m_checkColumn2)
                    {
                        btnCmp.m_soliderStop = true;
                    }

                }
            }

            Destroy(this);
        }
    }

}
