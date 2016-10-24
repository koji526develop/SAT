using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoldierSurvival : MonoBehaviour
{
    string[] SoldierName = { "剣", "槍", "斧", "盾" };

    GameObject m_SoldierTextObj;
    //Text[] m_SoldierText = new Text[4];

    void Awake()
    {
        //兵士残存数の背景画像
        m_SoldierTextObj = MyUtility.CreateImage(
            "LongBar",
            "Image/karie/waku",
            new Vector2(2 / 32.0f, 12 / 25.0f),
            new Vector2(5 / 32.0f, 23 / 25.0f),
            transform
        );
        for (int i = 0; i < 1; i++)
        {
            MyUtility.CreateText(
                SoldierName[i],
                transform,
                35,
            new Vector3(0.0f, 0.0f, -90.0f),
            new Vector2(3.25f / 32.0f, 22.5f / 25.0f),
            new Vector2(3.25f / 32.0f, 22.5f / 25.0f));
    }
}

void Start()
{
    //for (int i = 0; i < 1; i++)
    //{
    //    m_SoldierText[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
    //}


}

void Update()
{

}
}
