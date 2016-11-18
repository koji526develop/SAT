using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GameUIManager : MonoBehaviour {


    void Awake()
    {
		Transform canvasTransForm = GameObject.Find ("Canvas").transform;

        //戦力ゲージ作成スクリプト
        GameObject obj = MyUtility.CreateSlider(
            "BlueSlider",
            "Image/gage-ao",
            new Vector2(5 / 32.0f, 1 / 25.0f),
            new Vector2(27 / 32.0f, 5.0f / 25.0f),
			canvasTransForm
            );

        obj = MyUtility.CreateSlider(
            "RedSlider",
            "Image/gage-akapng",
            new Vector2(5 / 32.0f, 1 / 25.0f),
            new Vector2(27 / 32.0f, 5.0f / 25.0f),
			canvasTransForm
        );

        obj.GetComponent<Slider>().direction = Slider.Direction.RightToLeft;
		canvasTransForm.gameObject.AddComponent<WarPotential>();
        //戦力ゲージ作成スクリプトここまで

        //タイムUIを作成
        obj = MyUtility.CreateImage(
            "TimeImage", 
            "Image/TimeWaku", 
            new Vector2(1 / 32.0f, 1 / 25.0f),
            new Vector2(5 / 32.0f, 5 / 25.0f), 
			canvasTransForm);

        obj = MyUtility.CreateImage(
       		"TimeImage2",
        	"Image/TimeWaku",
        	new Vector2(27 / 32.0f, 1 / 25.0f),
        	new Vector2(31 / 32.0f, 5 / 25.0f),
			canvasTransForm);

        MyUtility.CreateText(
            "60",
			canvasTransForm,
            45,
            new Vector3(0,0,-90),
            new Vector2(1 / 32.0f, 1.5f / 25.0f),
            new Vector2(4 / 32.0f, 5 / 25.0f)
            );

        Text timeText = MyUtility.CreateText(
           "60",
			canvasTransForm,
            45,
            new Vector3(0, 0, 90),
           	new Vector2(28.0f / 32.0f, 1.5f / 25.0f),
            new Vector2(31 / 32.0f, 5f / 25.0f)
            );

        timeText.gameObject.name = "Text2";

        //ここまで


        obj = MyUtility.CreateButton(
            "SpecialCardButton",
            "Image/TimeWaku",
            new Vector2( 1/ 32.0f, 5/ 25.0f),
            new Vector2( 4/ 32.0f, 12/ 25.0f),
            canvasTransForm
            );

        obj.AddComponent<SpecialCardButton>().m_playerID=1;
        

        obj = MyUtility.CreateButton(
            "SpecialCardButton",
            "Image/TimeWaku",
            new Vector2(28 / 32.0f, 16 / 25.0f),
            new Vector2(31 / 32.0f, 23 / 25.0f),
            canvasTransForm
    );
        obj.AddComponent<SpecialCardButton>().m_playerID = 2;
        
        //ソルジャーボタンを作成
        for (int i = 1; i <= 6; i++)
		{
			for (int j = 1; j <= 2; j++)
			{
				obj = MyUtility.CreateButton(
					"SoldierButton",
					"Image/sword_I",
					new Vector2((5.0f + 20.0f * (j-1)) / 32.0f, (20.0f - 3.0f * (i-1)) / 25.0f),
					new Vector2((7.0f + 20.0f * (j-1)) / 32.0f, (23.0f - 3.0f * (i-1)) / 25.0f),
					canvasTransForm);

				ButtonSpawner btnCmp = obj.AddComponent<ButtonSpawner>();
				btnCmp.m_PlayerID = j;
				btnCmp.m_ButtonID = i;
			}
		}
        //ここまで

        for(int i= 1; i <= 6; i++)
        {
            for(int j = 1; j <= 2; j++)
            {
                obj = MyUtility.CreateImage(
                    "ScoreFlame",
                    "Image/ScoreFlame/Scoreflame",
                    new Vector2((4+23.0f*(j-1)) / 32.0f, (20-(3.0f * (i - 1)) )/ 25.0f),
                     new Vector2((5+23.0f*(j-1)) / 32.0f, (23-(3.0f * (i - 1))) / 25.0f),
                     canvasTransForm
                    );

                ScoreFlameScript scoreFlameCmp= obj.AddComponent<ScoreFlameScript>();
                scoreFlameCmp.m_PlayerID = j;
                scoreFlameCmp.m_Column = i;
                
            } 
        }

        canvasTransForm.gameObject.AddComponent<GameTimeControl>();
    }
    // Use this for initialization
    void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
