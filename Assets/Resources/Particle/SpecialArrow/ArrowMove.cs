using UnityEngine;
using System.Collections;

public class ArrowMove : MonoBehaviour
{
    readonly float speed = 0.5f;

    int m_PlayerID;
    public int PlayerID
    {
        set
        {
            m_PlayerID = value;
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
}
