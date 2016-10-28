using UnityEngine;
using System.Collections;

public class OperatingManager : MonoBehaviour
{
    const int MAX_SPRITE_NUM = 6;
    readonly float m_MinSprPosition = -4.0f;

    GameObject[] m_OperatingSprNumObj = new GameObject[MAX_SPRITE_NUM];

    void Awake()
    {
        Canvas canvas = MyUtility.CreateCanvas();

        GameObject uiObj = MyUtility.CreateEmpty("UI", canvas.transform);

        //イベントシステム作成
        MyUtility.CreateEventSystem(transform);

        GameObject m_OperatingSpriteObj;


        //何番目を表示しているかの画像
        for (int i = 0; i < MAX_SPRITE_NUM; i++)
        {
            m_OperatingSprNumObj[i] = MyUtility.CreateSprite(uiObj.transform, "OperatingChoiceSprite" + i, "Image/operatingNoneChoice");

            m_OperatingSprNumObj[i].AddComponent<OperatingChoice>();
            m_OperatingSprNumObj[i].transform.position = new Vector3((m_MinSprPosition + (i * 1.5f)), -1.85f, 0.0f);
            m_OperatingSprNumObj[i].GetComponent<OperatingChoice>().SpriteNum = i;
        }
        //操作説明の画像
        m_OperatingSpriteObj = MyUtility.CreateSprite(uiObj.transform, "OperatingSprite", "Image/operating1");

        m_OperatingSpriteObj.AddComponent<OperatingFlick>();
        m_OperatingSpriteObj.transform.position = new Vector3(0.0f, 2.5f, 0.0f);
        m_OperatingSpriteObj.transform.localScale = new Vector3(1.4f, 1.4f, 0.0f);

    }

    void Start()
    {


    }

    void Update()

    {

    }
}
