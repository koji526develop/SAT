using UnityEngine;
using System.Collections;

public class MobSpawner : SpecialCard
{

   
    private GameObject m_1stSelectColumn;
    private GameObject m_2ndSelectColumn;

    int GetPlayerID(GameObject _targetObject)
    {

        return _targetObject.GetComponent<Character>().status.PlayerID;
    }


    // Use this for initialization


    // Update is called once per frame
    public override void Update () {

        if (m_1stSelectColumn == null)
        {
            if(TouchManager.GetTouchInfo(0)== TouchInfo.Ended)
            {
                 m_1stSelectColumn = TouchManager.GetRaycastHitObject(m_mainCamera, 0);
            }
        }

        else if(m_2ndSelectColumn ==null)
        {
            if (TouchManager.GetTouchInfo(0) == TouchInfo.Ended)
            {
                m_2ndSelectColumn = TouchManager.GetRaycastHitObject(m_mainCamera, 0);
            }
        }

        if (m_1stSelectColumn != null && m_2ndSelectColumn != null)
        {
            Destroy(this);
        }
    }
}
