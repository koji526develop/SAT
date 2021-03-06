﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class GameUIManager : SceneUiManagerBase
{
    Rect[,] m_coulumnRect = new Rect[2, MyUtility.MAX_COLUMN];
    RectSet[,] m_rectset = new RectSet[2, MyUtility.MAX_COLUMN];

    public void RectRender(int _playerID, bool _isSwitch)
    {
        _playerID--;
        for (int i = 0; i < MyUtility.MAX_COLUMN; i++)
        {
            m_rectset[_playerID, i].transform.gameObject.SetActive(_isSwitch);
            m_rectset[_playerID, i].m_isSelected = false;
        }

    }

    public void SetCoulumRect(Vector2 _firstPoint, Vector2 _endPoint, int _playerID, int _columnNumber)
    {
        _playerID--;
        _columnNumber--;

        m_coulumnRect[_playerID, _columnNumber] = new Rect(_firstPoint.x * Screen.width,
            _firstPoint.y * Screen.height,
            _endPoint.x * Screen.width,
            _endPoint.y * Screen.height);

        return;
    }

    public bool TargetRect(Vector3 _targetPoint, int _playerID, int _idx)
    {

        _playerID--;
        _idx--;

        if (m_coulumnRect[_playerID, _idx].x < _targetPoint.x &&
            _targetPoint.x < m_coulumnRect[_playerID, _idx].width &&
            m_coulumnRect[_playerID, _idx].y < _targetPoint.y &&
            _targetPoint.y < m_coulumnRect[_playerID, _idx].height
        )
        {
           // Debug.Log("PlayerID" + _playerID + "Cloumn" + _idx + "押されましあ");
            m_rectset[_playerID, _idx].m_isSelected = true;
            return true;
        }
        return false;
    }

    void Awake()
    {
        Transform canvasTransForm = GameObject.Find("Canvas").transform;

        GameObject obj = MyUtility.CreateButton(
            "back",
            "UI/Game/information_back",
            new Vector2(0.0f / 32.0f, 0.0f / 25.0f),
            new Vector2(3.0f / 32.0f, 25.0f / 25.0f),
            canvasTransForm
        );

        obj = MyUtility.CreateButton(
            "back",
            "UI/Game/information_back",
            new Vector2(29.0f / 32.0f, 0.0f / 25.0f),
            new Vector2(32.0f / 32.0f, 25.0f / 25.0f),
            canvasTransForm
        );

        GameObject.Find("Canvas").AddComponent<SoldierSurvival>();

        //戦力ゲージ作成スクリプト
        obj = MyUtility.CreateSlider(
            "BlueSlider",
            "UI/Game/gauge_blue",
            new Vector2(15.0f / 32.0f, 0.0f / 25.0f),
            new Vector2(17.0f / 32.0f, 25.0f / 25.0f),
            canvasTransForm
        );
        obj.GetComponentInChildren<Image>().raycastTarget = false;
        obj.GetComponent<Slider>().direction = Slider.Direction.TopToBottom;

        obj = MyUtility.CreateSlider(
            "RedSlider",
            "UI/Game/gauge_red",
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
            "UI/Game/frame",
            new Vector2(0.0f / 32.0f, 0.0f / 25.0f),
            new Vector2(3.0f / 32.0f, 4.0f / 25.0f),
            canvasTransForm);
        Color color = obj.GetComponent<Image>().color;
        color.a = 0;
        obj.GetComponent<Image>().color = color;


        obj = MyUtility.CreateImage(
            "TimeImage2",
            "UI/Game/frame",
            new Vector2(29.0f / 32.0f, 21.0f / 25.0f),
            new Vector2(32.0f / 32.0f, 25.0f / 25.0f),
            canvasTransForm);
        color = obj.GetComponent<Image>().color;
        color.a = 0;
        obj.GetComponent<Image>().color = color;

        Text timeText1P = MyUtility.CreateText(
            MyUtility.GAME_TIME.ToString(),
            canvasTransForm,
            45,
            new Vector3(0, 0, -90),
            new Vector2(0.5f / 32.0f, 2.0f / 25.0f),
            new Vector2(2.5f / 32.0f, 3.0f / 25.0f)
        );

        timeText1P.gameObject.AddComponent<GameTimeControl>();

        Text timeText2P = MyUtility.CreateText(
            MyUtility.GAME_TIME.ToString(),
            canvasTransForm,
            45,
            new Vector3(0, 0, 90),
            new Vector2(29.5f / 32.0f, 22.0f / 25.0f),
            new Vector2(31.5f / 32.0f, 25.0f / 25.0f)
        );

        timeText2P.gameObject.AddComponent<GameTimeControl>();

        //      //ここまで
        obj = MyUtility.CreateButton(
            "SpecialCardButton1",
            "UI/Game/information_card",
            new Vector2(0.0f / 32.0f, 4.0f / 25.0f),
            new Vector2(3.0f / 32.0f, 13.0f / 25.0f),
            canvasTransForm
        );

        obj.AddComponent<SpecialCardButton>().m_playerID = 1;

        GameObject setSpecialObj = MyUtility.CreateImage(
            "SetSpecial",
            "UI/Game/frame",
            new Vector2(0.3f / 32.0f, 10.0f / 25.0f),
            new Vector2(2.9f / 32.0f, 12.5f / 25.0f),
            canvasTransForm
        );
        setSpecialObj.GetComponent<Image>().raycastTarget = false;

        //表示される特殊カード
        //Player1
        for (int i = 0; i < 3; i++)
        {
            GameObject specialObj = MyUtility.CreateImage(
                "SpecialCards",
                "UI/Result/card" + SelectSpecialUIManager.SPECIALCARD_NUMBER_1[i].ToString(),
                new Vector2(0.4f / 32.0f, (10.4f - (i * 2.7f)) / 25.0f),
                new Vector2(2.8f / 32.0f, (12.1f - (i * 2.7f)) / 25.0f),
                canvasTransForm);
            specialObj.GetComponent<Image>().raycastTarget = false;
            specialObj.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
            SpecialCardsBehavior spCardbehavior = specialObj.AddComponent<SpecialCardsBehavior>();
            spCardbehavior.UseOrder = 1 + i;
            spCardbehavior.SpecialButton = obj.GetComponent<SpecialCardButton>();
            spCardbehavior.PlayerID = 1;
        }

        obj = MyUtility.CreateButton(
            "SpecialCardButton2",
            "UI/Game/information_card",
            new Vector2(29.0f / 32.0f, 12.0f / 25.0f),
            new Vector2(32.0f / 32.0f, 21.0f / 25.0f),
            canvasTransForm
        );
        obj.AddComponent<SpecialCardButton>().m_playerID = 2;

        setSpecialObj = MyUtility.CreateImage(
            "SetSpecial",
            "UI/Game/frame",
            new Vector2(29.2f / 32.0f, 12.5f / 25.0f),
            new Vector2(31.8f / 32.0f, 15.0f / 25.0f),
            canvasTransForm
        );
        setSpecialObj.GetComponent<Image>().raycastTarget = false;

        //表示される特殊カード
        //Player2
        for (int i = 0; i < 3; i++)
        {
            GameObject specialObj = MyUtility.CreateImage(
                "SpecialCards",
                "UI/Result/2card" + SelectSpecialUIManager.SPECIALCARD_NUMBER_2[i].ToString(),
                new Vector2(29.3f / 32.0f, (12.9f + (i * 2.7f)) / 25.0f),
                new Vector2(31.7f / 32.0f, (14.6f + (i * 2.7f)) / 25.0f),
                canvasTransForm);
            specialObj.GetComponent<Image>().raycastTarget = false;
            specialObj.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            SpecialCardsBehavior spCardbehavior = specialObj.AddComponent<SpecialCardsBehavior>();
            spCardbehavior.UseOrder = 1 + i;
            spCardbehavior.SpecialButton = obj.GetComponent<SpecialCardButton>();
            spCardbehavior.PlayerID = 2;
        }

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
        for (int i = 1; i <= 2; i++)
        {
            for (int j = 1; j <= 5; j++)
            {

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

            }
        }

        for (int i = 1; i <= 2; i++)
        {
            for (int j = 1; j <= 5; j++)
            {
                SetCoulumRect(new Vector2((4.0f + (13.0f * (i - 1))) / 32.0f, (20.0f - (5.0f * (j - 1))) / 25.0f),
                    new Vector2((15.0f + (13.0f * (i - 1))) / 32.0f, (25.0f - (5.0f * (j - 1))) / 25.0f), i, j);

                obj = MyUtility.CreateImage("ColumnImage",
                    "UI/Game/select_line",
                    new Vector2((4.0f + (13.0f * (i - 1))) / 32.0f, (20.0f - (5.0f * (j - 1))) / 25.0f),
                    new Vector2((15.0f + (13.0f * (i - 1))) / 32.0f, (25.0f - (5.0f * (j - 1))) / 25.0f),
                    canvasTransForm);
                RectSet local_rectset;
                local_rectset = obj.AddComponent<RectSet>();
                local_rectset.SetState(i - 1, j - 1);
                m_rectset[i - 1, j - 1] = local_rectset;
            }
        }

    }
    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
