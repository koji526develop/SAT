using UnityEngine;
using System.Collections;

public class SpecialCard3 : SpecialCard {

	public static string m_howTo = "このカードを発動した後に一度だけ、\n兵士を出す際に同じ種類の兵士を二体出す。";
	public static string m_imagePath = "path";

	// Use this for initialization
	void Start () {
        foreach (Transform childObj in this.transform.parent)
        {
            if (childObj.tag == "SoliderButton")
            {
                ButtonSpawner btnCmp = childObj.GetComponent<ButtonSpawner>();
                if (btnCmp.m_PlayerID == m_UsedPlayerID)
                {
                    btnCmp.m_soliderDouble = true;
                }
            }
        }

    }
	
	// Update is called once per frame
	void Update () {

        foreach (Transform childObj in this.transform.parent)
        {
            if (childObj.tag == "SoliderButton")
            {
                ButtonSpawner btnCmp = childObj.GetComponent<ButtonSpawner>();
                if (btnCmp.m_PlayerID == m_UsedPlayerID)
                {
                    if (btnCmp.m_soliderDouble == false)
                    {
                        Destroy(this);
                    }
                }
            }
        }
    }

    void OnDestroy()
    {
        foreach (Transform childObj in this.transform.parent)
        {
            if (childObj.tag == "SoliderButton")
            {
                ButtonSpawner btnCmp = childObj.GetComponent<ButtonSpawner>();
                if (btnCmp.m_PlayerID == m_UsedPlayerID)
                {
                    btnCmp.m_soliderDouble = false;
                }
            }
        }
    }
}
