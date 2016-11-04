using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	// ゲーム開始時
	void Awake()
	{
		// Light作成
		MyUtility.CreateDirectionalLight ();

		/*
		// UIManager作成
		Transform UIManager = new GameObject ("UIManager").transform;

		// UIカメラ作成
		Camera uiCamera = MyUtility.CreateCamera ("UICamera", UIManager);

		// UICanvas作成(UICameraの子オブジェクトにする)
		MyUtility.CreateUICanvas (uiCamera.transform);
		*/

		// イベントシステム作成
		//MyUtility.CreateEventSystem (UIManager);

		// BattleManager作成
		Transform battleManager = new GameObject ("BattleManager").transform;
		battleManager.tag = "BattleManager";



        // バトル用のカメラ作成
        MyUtility.CreateCamera ("BattleCamera", battleManager);

        //キャンバスを作成
        Canvas canvas=MyUtility.CreateCanvas();
        GameObject.Find("Canvas").AddComponent<SoldierSurvival>();

        //ゲーム内で使用するマネージャーを作成
        this.gameObject.AddComponent<ScoreManager>();
        this.gameObject.AddComponent<GameUIManager>();
    
    }


    void Update()
    {

    }

}
