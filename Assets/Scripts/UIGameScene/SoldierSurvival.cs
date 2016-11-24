using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class SoldierSurvival : MonoBehaviour
{
    //int[] m_SoldierNumber = new int[8];

    GameObject[] m_SoldierImgObj = new GameObject[2];

    string[] m_SoldierNumberText = { "", "", "", "", "", "", "", "" };
    string[] m_IconImagePath = { "Image/sword_I", "Image/spear_I", "Image/ax_I", "Image/shield_I" };
    Text[] m_SoldierText = new Text[8];


    void Awake()
    {

        m_SoldierNumberText[0] = SelectUIManager.SWORD_NUM_1.ToString();
        m_SoldierNumberText[1] = SelectUIManager.SPEAR_NUM_1.ToString();
        m_SoldierNumberText[2] = SelectUIManager.AX_NUM_1.ToString();
        m_SoldierNumberText[3] = SelectUIManager.SHIELD_NUM_1.ToString();
        m_SoldierNumberText[4] = SelectUIManager.SWORD_NUM_2.ToString();
        m_SoldierNumberText[5] = SelectUIManager.SPEAR_NUM_2.ToString();
        m_SoldierNumberText[6] = SelectUIManager.AX_NUM_2.ToString();
        m_SoldierNumberText[7] = SelectUIManager.SHIELD_NUM_2.ToString();


        for (int i = 0; i < 2; i++)
        {
            //兵士残存数の背景画像
            m_SoldierImgObj[i] = MyUtility.CreateImage(
            "LongBar",
            "Image/karie/waku6",
            new Vector2((1.0f /*+ (27.0f* i)*/)/ 32.0f, (13.0f  /*+ (13.0f* i)*/) / 25.0f),
            new Vector2((4.0f /*+ (27.0f* i)*/)/ 32.0f, (25.0f/* + (13.0f* i)*/) / 25.0f),
            transform);

            for (int j = 0; j < 4; j++)
            {
                GameObject imgobj = MyUtility.CreateImage(
                "SoldierIcon",
                m_IconImagePath[j],
                new Vector2(23 / 32.0f, (21.5f - (j * 6)) / 25.0f),
                new Vector2(35 / 32.0f, (24.5f - (j * 6)) / 25.0f),
                m_SoldierImgObj[i].transform);

                imgobj.transform.Rotate(new Vector3(0, 0, 1), -90);
            }

            for (int j = 0; j < 4; j++)
            {
                m_SoldierText[j + (4 * i)] = MyUtility.CreateText(
                   m_SoldierNumberText[j + (i * 4)],
                   m_SoldierImgObj[i].transform,
                   30,
                   new Vector3(0.0f, 0.0f, -90.0f),
                   new Vector2(13 / 32.0f, (20.0f - (j * 6)) / 25.0f),
                   new Vector2(13 / 32.0f, (22.0f - (j * 6)) / 25.0f)
                   );
            }
        }

        m_SoldierImgObj[1].transform.Rotate(new Vector3(0, 0, 1), 180);
        Vector2 tmpPos = new Vector2(Screen.width - m_SoldierImgObj[0].transform.position.x, Screen.height - m_SoldierImgObj[0].transform.position.y);
        m_SoldierImgObj[1].transform.position = new Vector3(tmpPos.x, tmpPos.y, 0.0f);

    }

    void Start()
    {

    }

    void Update()
    {
        m_SoldierText[0].text = SelectUIManager.SWORD_NUM_1.ToString();
        m_SoldierText[1].text = SelectUIManager.SPEAR_NUM_1.ToString();
        m_SoldierText[2].text = SelectUIManager.AX_NUM_1.ToString();
        m_SoldierText[3].text = SelectUIManager.SHIELD_NUM_1.ToString();
        m_SoldierText[4].text = SelectUIManager.SWORD_NUM_2.ToString();
        m_SoldierText[5].text = SelectUIManager.SPEAR_NUM_2.ToString();
        m_SoldierText[6].text = SelectUIManager.AX_NUM_2.ToString();
        m_SoldierText[7].text = SelectUIManager.SHIELD_NUM_2.ToString();
    }
}
