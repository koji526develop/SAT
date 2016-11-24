﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GameUIManager : MonoBehaviour {
    
    Rect[] m_coulumnRect = new Rect[MyUtility.MAX_COLUMN];

    void SetCoulumRect(Vector2 _firstPoint ,Vector2 _endPoint, int _columnNumber)
    {
<<<<<<< HEAD
        m_coulumnRect[_columnNumber] = new Rect(_firstPoint.x * Screen.width,
                                                _firstPoint.y * Screen.height,
                                                _endPoint.x * Screen.width,
                                                _endPoint.y * Screen.height);
=======
        m_coulumnRect[_columnNumber] = new Rect(_firstPoint.x / Screen.width,
                                                _firstPoint.y / Screen.height,
                                                _endPoint.x / Screen.width,
                                                _endPoint.y / Screen.height);
>>>>>>> origin/master
    }

    void Awake()
    {
		Transform canvasTransForm = GameObject.Find ("Canvas").transform;

       //戦力ゲージ作成スクリプト
        GameObject obj = MyUtility.CreateSlider(
            "BlueSlider",
            "Image/gage-ao",
            new Vector2(15.0f / 32.0f, 0.0f / 25.0f),
            new Vector2(17.0f / 32.0f, 25.0f / 25.0f),
            canvasTransForm
            );
        obj.GetComponentInChildren<Image>().raycastTarget = false;
        obj.GetComponent<Slider>().direction = Slider.Direction.TopToBottom;

        obj = MyUtility.CreateSlider(
            "RedSlider",
            "Image/gage-akapng",
            new Vector2(15.0f / 32.0f, 0.0f / 25.0f),
            new Vector2(17.0f / 32.0f, 25.0f / 25.0f),
            canvasTransForm
        );
        obj.GetComponentInChildren<Image>().raycastTarget = false;
        obj.GetComponent<Slider>().direction = Slider.Direction.BottomToTop;
        canvasTransForm.gameObject.AddComponent<WarPotential>();

        //      //戦力ゲージ作成スクリプトここまで

        //      //タイムUIを作成
            obj = MyUtility.CreateImage(
                "TimeImage",
                "Image/TimeWaku",
                new Vector2(0.0f / 32.0f, 0.0f / 25.0f),
                new Vector2(3.0f / 32.0f, 4.0f / 25.0f),
          canvasTransForm);

            obj = MyUtility.CreateImage(
                   "TimeImage2",
                "Image/TimeWaku",
                new Vector2(29.0f / 32.0f, 21.0f / 25.0f),
                new Vector2(32.0f / 32.0f, 25.0f / 25.0f),
          canvasTransForm);

        Text timeText1P = MyUtility.CreateText(
            MyUtility.GAME_TIME.ToString(),
      canvasTransForm,
            45,
            new Vector3(0, 0, -90),
            new Vector2(0.0f / 32.0f, 2.0f / 25.0f),
            new Vector2(2.0f / 32.0f, 3.0f / 25.0f)
            );

        //timeText1P.gameObject.AddComponent<GameTimeControl>();

        Text timeText2P = MyUtility.CreateText(
          MyUtility.GAME_TIME.ToString(),
     canvasTransForm,
           45,
           new Vector3(0, 0, 90),
            new Vector2(30.0f / 32.0f, 22.0f / 25.0f),
            new Vector2(32.0f / 32.0f, 25.0f / 25.0f)
           );

        //timeText2P.gameObject.AddComponent<GameTimeControl>();


        //      //ここまで
        obj = MyUtility.CreateButton(
            "SpecialCardButton",
            "Image/TimeWaku",
            new Vector2(0.0f / 32.0f,  4.0f / 25.0f),
            new Vector2(3.0f / 32.0f, 13.0f / 25.0f),
            canvasTransForm
            );

        obj.AddComponent<SpecialCardButton>().m_playerID = 1;


        obj = MyUtility.CreateButton(
            "SpecialCardButton",
            "Image/TimeWaku",
            new Vector2(29.0f / 32.0f, 12.0f / 25.0f),
            new Vector2(32.0f / 32.0f, 21.0f / 25.0f),
            canvasTransForm
            );
        obj.AddComponent<SpecialCardButton>().m_playerID = 2;

        //ソルジャーボタンを作成
        for (int i = 1; i <= 5; i++)
        {
            for (int j = 1; j <= 2; j++)
            {
                obj = MyUtility.CreateButton(
                    "SoldierButton",
                    "Image/sword_I",
                    new Vector2((4.0f + 22.0f * (j - 1)) / 32.0f, (20.0f - 5.0f * (i - 1)) / 25.0f),
                    new Vector2((6.0f + 22.0f * (j - 1)) / 32.0f, (25.0f - 5.0f * (i - 1)) / 25.0f),
                    canvasTransForm);

                ButtonSpawner btnCmp = obj.AddComponent<ButtonSpawner>();
                btnCmp.m_PlayerID = j;
                btnCmp.m_ButtonID = i;
            }
            SetCoulumRect(new Vector2((8.0f / 32.0f), (20.0f / 25.0f)),
                          new Vector2(26.0f / 32.0f, 25.0f / 25.0f),
                          i);
        }
        //ここまで

        for (int i = 1; i <= 5; i++)
        {
            for (int j = 1; j <= 2; j++)
            {
                obj = MyUtility.CreateImage(
                    "ScoreFlame",
                    "Image/ScoreFlame/Scoreflame",
                    new Vector2((3 + 25.0f * (j - 1)) / 32.0f,  (20.0f - (5.0f * (i - 1))) / 25.0f),
                     new Vector2((4 + 25.0f * (j - 1)) / 32.0f, (25.0f - (5.0f * (i - 1))) / 25.0f),
                     canvasTransForm
                    );

                ScoreFlameScript scoreFlameCmp = obj.AddComponent<ScoreFlameScript>();
                scoreFlameCmp.m_PlayerID = j;
                scoreFlameCmp.m_Column = i;

            }

          
        }
    }
    // Use this for initialization
    void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
