using UnityEngine;
using System.Collections;

public class OperatingManager : MonoBehaviour
{

    GameObject[] m_OperatingSprNumObj = new GameObject[MAX_SPRITE_NUM];
    GameObject m_OperatingSpriteObj;

    const int MAX_SPRITE_NUM = 6;
    readonly float m_MinSprPosition = -4.0f;

    void Awake()
    {
        Transform SpriteManager = new GameObject("SpriteManager").transform;

        //何番目を表示しているかの画像
        for (int i = 0; i < MAX_SPRITE_NUM; i++)
        {
            m_OperatingSprNumObj[i] = MyUtility.CreateSprite(SpriteManager, "OperatingChoiceSprite" + i, "Image/operatingNoneChoice");
        }
        //操作説明の画像
        m_OperatingSpriteObj = MyUtility.CreateSprite(SpriteManager, "OperatingSprite", "Image/operating1");
    }

    void Start()
    {
        for (int i = 0; i < MAX_SPRITE_NUM; i++) 
        {
            m_OperatingSprNumObj[i].AddComponent<OperatingChoice>();
            m_OperatingSprNumObj[i].transform.position = new Vector3((m_MinSprPosition+(i*1.5f)),-1.85f,0.0f);
            m_OperatingSprNumObj[i].GetComponent<OperatingChoice>().SpriteNum = i;

        }
        m_OperatingSpriteObj.AddComponent<OperatingFlick>();
        m_OperatingSpriteObj.transform.position = new Vector3(0.0f, 2.5f, 0.0f);
        m_OperatingSpriteObj.transform.localScale = new Vector3(1.4f, 1.4f, 0.0f);
    }

    void Update()
    {

    }
}
