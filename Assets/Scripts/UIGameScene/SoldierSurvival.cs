using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoldierSurvival : MonoBehaviour
{
    int[] m_SoldierNumber = new int[4];

    GameObject[] m_SoldierImgObj = new GameObject[2];

    string[] m_SoldierNumberName = { "", "" };
    Text[] m_SoldierText = new Text[2];

    void Awake()
    {

        //兵士の数を仮で数値代入
        for (int i = 0; i < 4; i++)
        {
            m_SoldierNumber[i] = i * 10;
        }

        m_SoldierNumberName[0] = m_SoldierNumber[0].ToString() + "      " + m_SoldierNumber[1].ToString() + "      " + m_SoldierNumber[2].ToString() + "      " + m_SoldierNumber[3].ToString();
        m_SoldierNumberName[1] = m_SoldierNumber[0].ToString() + "      " + m_SoldierNumber[1].ToString() + "      " + m_SoldierNumber[2].ToString() + "      " + m_SoldierNumber[3].ToString();


        MyUtility.CreateImage(
            "SoldierIcon",
            "Image/Icon1",
            new Vector2(1 / 32.0f, 23 / 25.0f),
            new Vector2(3 / 32.0f, 21 / 25.0f),
            transform);


        for (int i = 0; i < 2; i++)
        {
            //兵士残存数の背景画像
            m_SoldierImgObj[i] = MyUtility.CreateImage(
            "LongBar",
            "Image/karie/waku6",
            new Vector2(1 / 32.0f, 12 / 25.0f),
            new Vector2(4 / 32.0f, 23 / 25.0f),
            transform);

            //for (int j = 1; j < 2; j++)
            //{
            //    MyUtility.CreateImage(
            //    "SoldierIcon",
            //    "Image/Icon" + j.ToString(),
            //    new Vector2(0.5f, (1.0f - (j * 0.2f))),
            //    new Vector2(0.5f, (1.0f - (j * 0.2f))),
            //    m_SoldierImgObj[i].transform);
            //}

            //m_SoldierText[i] = MyUtility.CreateText(
            //    m_SoldierNumberName[i],
            //    m_SoldierImgObj[i].transform,
            //    30,
            //    new Vector3(0.0f, 0.0f, -90.0f),
            //    new Vector2(13 / 32.0f, 11.5f / 25.0f),
            //    new Vector2(13 / 32.0f, 11.5f / 25.0f)
            //    );
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
