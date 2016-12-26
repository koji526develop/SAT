using UnityEngine;
using System.Collections;

public class ArrowMove : MonoBehaviour
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
		//兵士削除
        if (m_PlayerID == 1)
        {
            transform.position += new Vector3(speed, 0, 0);
            if (transform.position.x >= MyUtility.SOLDIER_CREATE_LINE_X_2P)
            {
                Destroy(this.gameObject);
                
            }
        }
        else if (m_PlayerID == 2)
        {
            transform.position += new Vector3(-speed, 0, 0);
            if (transform.position.x <= MyUtility.SOLDIER_CREATE_LINE_X_1P)
            {
                Destroy(this.gameObject);
                
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (m_Column != 0)
        {
            //キャラクタータグであるなら
            if (col.gameObject.tag == "Character")
            {
                //対象を削除
                Destroy(col.gameObject);
            }

        }
    }
}
