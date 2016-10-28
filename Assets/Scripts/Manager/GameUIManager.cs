using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GameUIManager : MonoBehaviour {


    void Awake()
    {

        //戦力ゲージ作成スクリプト
        GameObject obj = MyUtility.CreateSlider(
            "BlueSlider",
            "Image/gage-ao",
            new Vector2(5 / 32.0f, 2 / 25.0f),
            new Vector2(27 / 32.0f, 5.0f / 25.0f),
            GameObject.Find("Canvas").transform
            );

        obj = MyUtility.CreateSlider(
            "RedSlider",
            "Image/gage-akapng",
            new Vector2(5 / 32.0f, 2 / 25.0f),
            new Vector2(27 / 32.0f, 5.0f / 25.0f),
            GameObject.Find("Canvas").transform
        );

        obj.GetComponent<Slider>().direction = Slider.Direction.RightToLeft;
        GameObject.Find("Canvas").AddComponent<WarPotential>();
        //戦力ゲージ作成スクリプトここまで

        //タイムUIを作成
        obj = MyUtility.CreateImage(
            "TimeImage", 
            "Image/TimeWaku", 
            new Vector2(1 / 32.0f, 1 / 25.0f),
            new Vector2(5 / 32.0f, 5 / 25.0f), 
            GameObject.Find("Canvas").transform);

        obj = MyUtility.CreateImage(
        "TimeImage2",
        "Image/TimeWaku",
        new Vector2(27 / 32.0f, 1 / 25.0f),
        new Vector2(31 / 32.0f, 5 / 25.0f),
        GameObject.Find("Canvas").transform);

        MyUtility.CreateText(
            "60",
             GameObject.Find("Canvas").transform,
             64,
             Vector3.zero,
            new Vector2(1 / 32.0f, 1.5f / 25.0f),
            new Vector2(3.75f / 32.0f, 5 / 25.0f)

            );

        MyUtility.CreateText(
            "60",
             GameObject.Find("Canvas").transform,
             64,
           new Vector3(180, 0, 0),
           new Vector2(27.0f / 32.0f, 1.5f / 25.0f),
           new Vector2((31f - 1.25f) / 32.0f, 5f / 25.0f)

            );

        //ここまで

        //ソルジャーボタンを作成

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                obj=MyUtility.CreateButton(
                   "SoldierButton",
                   "Image/TimeWaku",
                    new Vector2((5.0f + 20.0f * j) / 32.0f, (20.0f - 3.0f * i) / 25.0f),
                    new Vector2((7.0f + 20.0f * j) / 32.0f, (23.0f - 3.0f * i) / 25.0f),
                  GameObject.Find("Canvas").transform);

					obj.tag ="Player"+(j+1).ToString();

    
                obj.AddComponent<EventTrigger>();
                obj.AddComponent<ButtonSpawner>();

            }
        }

        //ここまで
    }
    // Use this for initialization
    void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
