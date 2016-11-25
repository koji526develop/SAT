using UnityEngine;
using System.Collections;

public class MobSpawner : SpecialCard
{


    private GameObject m_1stSelectColumn;
    private GameObject m_2ndSelectColumn;

    int GetPlayerID(GameObject _targetObject)
    {

        return int.Parse(_targetObject.tag.Substring(_targetObject.tag.Length - 1));
    }


    // Use this for initialization


    // Update is called once per frame
    public override void Update()
    {

        if (m_1stSelectColumn == null)
        {
            if (TouchManager.GetTouchInfo(0) == TouchInfo.Ended)
            {
                if (m_1stSelectColumn = TouchManager.GetRaycastHitObject(m_mainCamera, 0))
                {

                    if (m_1stSelectColumn.name != "Floor")
                    {
                        m_1stSelectColumn = null;
                        return;
                    }
                }
            }

            
        }

        else if (m_2ndSelectColumn == null)
        {
            if (TouchManager.GetTouchInfo(0) == TouchInfo.Ended)
            {
               if( m_2ndSelectColumn = TouchManager.GetRaycastHitObject(m_mainCamera, 0))
                {
                    if (m_2ndSelectColumn.name != "Floor")
                    {
                        m_2ndSelectColumn = null;
                        return;
                    }
                }
            }
        }

            if (m_1stSelectColumn != null && m_2ndSelectColumn != null)
            {
                Character character = Character.CreateObject(
                GameObject.FindWithTag("BattleManager").transform,
                Character.CharacterType.Sword,
                Character.GetSpawnPosition(m_UsedPlayerID, GetPlayerID(m_1stSelectColumn)),
                m_UsedPlayerID).GetComponent<Character>();
                character.mapColumn = GetPlayerID(m_1stSelectColumn);

                character = Character.CreateObject(
                GameObject.FindWithTag("BattleManager").transform,
                Character.CharacterType.Sword,
                Character.GetSpawnPosition(m_UsedPlayerID, GetPlayerID(m_2ndSelectColumn)),
                m_UsedPlayerID).GetComponent<Character>();
                character.mapColumn = GetPlayerID(m_2ndSelectColumn);

                Destroy(this);
            }
        }
    }

