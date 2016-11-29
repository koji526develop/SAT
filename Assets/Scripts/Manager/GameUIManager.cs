using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class GameUIManager : MonoBehaviour {
    
    Rect[,] m_coulumnRect = new Rect [2,MyUtility.MAX_COLUMN];



    public void SetCoulumRect(Vector2 _firstPoint ,Vector2 _endPoint, int _playerID, int _columnNumber)
    {
        _playerID--;
        _columnNumber--;

        m_coulumnRect[_playerID,_columnNumber] = new Rect(_firstPoint.x * Screen.width,
                                                _firstPoint.y * Screen.height,
                                                _endPoint.x * Screen.width,
                                                _endPoint.y * Screen.height);

        return;
    }

    public bool TargetRect(Vector3 _targetPoint, int _playerID,int _idx)
    {

        _playerID--;
        _idx--;

        if (m_coulumnRect[_playerID, _idx].x<_targetPoint.x &&
            _targetPoint.x< m_coulumnRect[_playerID, _idx].width&&
            m_coulumnRect[_playerID, _idx].y < _targetPoint.y&&
            _targetPoint.y < m_coulumnRect[_playerID, _idx].height
            ) {
            Debug.Log("PlayerID"+_playerID+"Cloumn"+_idx+"押されましあ");
            return true;
        }
        return false;
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
            "SpecialCardButton1",
            "Image/TimeWaku",
            new Vector2(0.0f / 32.0f,  4.0f / 25.0f),
            new Vector2(3.0f / 32.0f, 13.0f / 25.0f),
            canvasTransForm
            );

        obj.AddComponent<SpecialCardButton>().m_playerID = 1;


        obj = MyUtility.CreateButton(
            "SpecialCardButton2",
            "Image/TimeWaku",
            new Vector2(29.0f / 32.0f, 12.0f / 25.0f),
            new Vector2(32.0f / 32.0f, 21.0f / 25.0f),
            canvasTransForm
            );
        obj.AddComponent<SpecialCardButton>().m_playerID = 2;

        //ここまで
        for (int i = 1; i <= 5; i++)
        {
            for (int j = 1; j <= 2; j++)
            {
                obj = MyUtility.CreateImage(
                    "ScoreFlame",
                    "Image/ScoreFlame/Scoreflame",
                    new Vector2((3 + 25.0f * (j - 1)) / 32.0f, (20.0f - (5.0f * (i - 1))) / 25.0f),
                     new Vector2((4 + 25.0f * (j - 1)) / 32.0f, (25.0f - (5.0f * (i - 1))) / 25.0f),
                     canvasTransForm
                    );

                ScoreFlameScript scoreFlameCmp = obj.AddComponent<ScoreFlameScript>();
                scoreFlameCmp.m_PlayerID = j;
                scoreFlameCmp.m_Column = i;

            }
        }

        //疑似のソルジャーボタンを作成
        Transform soldierbutton = new GameObject("SoliderButton").transform;
        for (int i = 1; i <= 2; i++)
        {
            string imgName;

            if (i == 1)
            {
                imgName = "UI/Game/flick";
            }
            else
            {
                imgName = "UI/Game/flick2";
            }

            for (int j = 1; j <= 5; j++)
            {

                //obj = MyUtility.CreateSprite(
                // soldierbutton,
                // "SoldierButton",
                // imgName);

                //obj.transform.position      = new Vector3((-4.65f + (9.2f * (i - 1))), 0.0f, (4.0f - (2.0f * (j - 1))));
                //obj.transform.eulerAngles   = new Vector3(90.0f, 0.0f, 0.0f);
                //obj.transform.localScale    = new Vector3(0.1f, 0.08f, 0.1f);

                obj = MyUtility.CreateImage("SoliderImage",
                               "Image/TimeWaku",
                                new Vector2((2.5f + 22.0f * (i - 1)) / 32.0f, (20.0f - 5.0f * (j - 1)) / 25.0f),
                                 new Vector2((7.5f + 22.0f * (i - 1)) / 32.0f, (25.0f - 5.0f * (j - 1)) / 25.0f),
                                 canvasTransForm);

                GameObject buttonObj;
                buttonObj = MyUtility.CreateButton(
                    "SoldierButton",
                    "Image/TimeWaku",
                    new Vector2((4.0f + 22.0f * (i - 1)) / 32.0f, (21.5f - 5.0f * (j - 1)) / 25.0f),
                    new Vector2((6.0f + 22.0f * (i - 1)) / 32.0f, (23.5f - 5.0f * (j - 1)) / 25.0f),
                    canvasTransForm);

                ButtonSpawner btnCmp = buttonObj.AddComponent<ButtonSpawner>();
                btnCmp.m_PlayerID = i;
                btnCmp.m_ButtonID = j;
                btnCmp.m_changeSprite = obj.GetComponent<Image>();

                        SetCoulumRect(new Vector2((6.0f+(11.0f*(i-1))) / 32.0f, (20.0f-(5.0f*(j-1))) / 25.0f),
                                      new Vector2((15.0f+(11.0f*(i-1))) / 32.0f, (25.0f-(5.0f*(j-1))) / 25.0f), i, j);

            }
        }

        ////正規のソルジャーボタンを追加
        //for (int i = 1; i <= 5; i++)
        //{
        //    for (int j = 1; j <= 2; j++)
        //    {
        //        string imageName;
        //        if (j==1)
        //        {
        //            imageName = "UI/Game/center_active";
        //        }
        //        else
        //        {
        //            imageName = "UI/Game/center_active2";
        //        }

        //        obj = MyUtility.CreateButton(
        //            "SoldierButton",
        //            imageName,
        //            new Vector2((2.5f + 22.0f * (j - 1)) / 32.0f, (20.0f - 5.0f * (i - 1)) / 25.0f),
        //            new Vector2((7.5f + 22.0f * (j - 1)) / 32.0f, (25.0f - 5.0f * (i - 1)) / 25.0f),
        //            canvasTransForm);

        //        ButtonSpawner btnCmp = obj.AddComponent<ButtonSpawner>();
        //        btnCmp.m_PlayerID = j;
        //        btnCmp.m_ButtonID = i;
                
        //        SetCoulumRect(new Vector2((6.0f+(11.0f*(j-1))) / 32.0f, (20.0f-(5.0f*(i-1))) / 25.0f),
        //                      new Vector2((15.0f+(11.0f*(j-1))) / 32.0f, (25.0f-(5.0f*(i-1))) / 25.0f), j, i);
        //    }
        //}


    }
    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
