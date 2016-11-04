﻿using UnityEngine;
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

        for (int i = 0; i < 2; i++)
        {
            m_SoldierNumberText[0 + (i * 4)] = SelectUIManager.SWORD_NUM.ToString();
            m_SoldierNumberText[1 + (i * 4)] = SelectUIManager.SPEAR_NUM.ToString();
            m_SoldierNumberText[2 + (i * 4)] = SelectUIManager.AX_NUM.ToString();
            m_SoldierNumberText[3 + (i * 4)] = SelectUIManager.SHIELD_NUM.ToString();
        }


        for (int i = 0; i < 2; i++)
        {
            //兵士残存数の背景画像
            m_SoldierImgObj[i] = MyUtility.CreateImage(
            "LongBar",
            "Image/karie/waku6",
            new Vector2(1 / 32.0f, 12 / 25.0f),
            new Vector2(4 / 32.0f, 23 / 25.0f),
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
                m_SoldierText[j + i] = MyUtility.CreateText(
                   m_SoldierNumberText[j + i],
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
        m_SoldierImgObj[1].transform.position = new Vector3(tmpPos.x, tmpPos.y + 92.0f, 0.0f);

    }

    void Start()
    {

    }

    void Update()
    {

    }
}
