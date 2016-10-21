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

        m_SoldierNumberName[0] = "剣×" + m_SoldierNumber[0].ToString() + "槍×" + m_SoldierNumber[1].ToString() + "斧×" + m_SoldierNumber[2].ToString() + "盾×" + m_SoldierNumber[3].ToString();
        m_SoldierNumberName[1] = "剣×" + m_SoldierNumber[0].ToString() + "槍×" + m_SoldierNumber[1].ToString() + "斧×" + m_SoldierNumber[2].ToString() + "盾×" + m_SoldierNumber[3].ToString();


        for (int i = 0; i < 2; i++)
        {
            //兵士残存数の背景画像
            m_SoldierImgObj[i] = MyUtility.CreateImage(
            "LongBar",
            "Image/karie/waku6",
            new Vector2(2 / 32.0f, 12 / 25.0f),
            new Vector2(5 / 32.0f, 23 / 25.0f),
            transform);


            m_SoldierText[i] = MyUtility.CreateText(
                m_SoldierNumberName[i],
                m_SoldierImgObj[i].transform,
                30,
                new Vector3(0.0f, 0.0f, -90.0f),
                new Vector2(13 / 32.0f, 14 / 25.0f),
                new Vector2(13 / 32.0f, 14 / 25.0f)
                );
        }

        ////兵士の名前のテキスト
        //for (int j = 0; j < 4; j++)
        //{
        //    MyUtility.CreateText(
        //        SoldierName[j],
        //        m_SoldierImgObj[i].transform,
        //        30,
        //    new Vector3(0.0f, 0.0f, -90.0f),
        //    new Vector2(13.0f / 32.0f, (23.5f - (j * 6.0f)) / 25.0f),
        //    new Vector2(13.0f / 32.0f, (23.5f - (j * 6.0f)) / 25.0f));
        //    //new Vector2(3.15f / 32.0f, (22.6f - (i * 2.7f)) / 25.0f),
        //    //new Vector2(3.15f / 32.0f, (22.6f - (i * 2.7f)) / 25.0f));
        //}

        //////兵士の数のテキスト
        //for (int j = 0; j < 4; j++)
        //{
        //    m_SoldierText[j] = MyUtility.CreateText(
        //        m_SoldierNumber[j].ToString(),
        //        m_SoldierImgObj[i].transform,
        //        30,
        //        new Vector3(0.0f, 0.0f, -90.0f),
        //        new Vector2(13.0f / 32.0f, (20.5f - (j * 6.0f)) / 25.0f),
        //        new Vector2(13.0f / 32.0f, (20.5f - (j * 6.0f)) / 25.0f));
        //    //new Vector2(3.15f / 32.0f, (21.3f - (i * 2.7f)) / 25.0f),
        //    //new Vector2(3.15f / 32.0f, (21.3f - (i * 2.7f)) / 25.0f));
        //}

        //m_SoldierImgObj[1].transform.Rotate(new Vector3(0, 0, 1), 180);
        //m_SoldierImgObj[1].transform.position = new Vector3(Screen.width / 32 * 30.5f, Screen.height / 25 * 5, 0.0f);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
