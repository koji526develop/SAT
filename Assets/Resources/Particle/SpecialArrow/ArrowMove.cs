using UnityEngine;
using System.Collections;

public class ArrowMove : SpecialCard
{
    readonly float speed = 0.5f;

    int m_PlayerID;
    int m_Column;
    public int PlayerID
    {
        set
        {
            m_PlayerID = value;
        }
    }
    public int Column
    {
        set
        {
            m_Column = value;
        }
    }

    void Start()
    {
        m_PlayerID = 0;
    }

    void Update()
    {
        if (m_PlayerID == 1)
        {
            transform.position += new Vector3(speed, 0, 0);
            if (transform.position.x >= MyUtility.SOLDIER_CREATE_LINE_X_2P)
            {
                Destroy(this.gameObject);
                Debug.Log("削除");
            }
        }
        else if (m_PlayerID == 2)
        {
            transform.position += new Vector3(-speed, 0, 0);
            if (transform.position.x <= MyUtility.SOLDIER_CREATE_LINE_X_1P)
            {
                Destroy(this.gameObject);
                Debug.Log("削除");
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (m_Column != 0)
        {
            //キャラクタータグであるなら
            if (col.gameObject.tag == "Character")
            {
                foreach (Transform childObj in battleManager)
                {
                    //同じ列であるなら
                    if (m_Column == childObj.GetComponent<Character>().m_mapColumn)
                    {
                        if (childObj.GetComponent<Character>().status.PlayerID != m_PlayerID)
                        {
                            //対象を削除
                            Destroy(childObj.gameObject);
                        }
                    }
                }
            }

        }
    }
}
