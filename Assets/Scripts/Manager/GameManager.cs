﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour 
{
	// ゲーム開始時
	void Awake()
	{
		// Light作成
		MyUtility.CreateDirectionalLight ();

		// UIManager作成
		Transform UIManager = new GameObject ("UIManager").transform;

		// UIカメラ作成
		Camera uiCamera = MyUtility.CreateCamera ("UICamera", UIManager);

		// UICanvas作成(UICameraの子オブジェクトにする)
		MyUtility.CreateUICanvas (uiCamera.transform);

		// イベントシステム作成
		MyUtility.CreateEventSystem (UIManager);

		// BattleManager作成
		Transform battleManager = new GameObject ("BattleManager").transform;

		// バトル用のカメラ作成
		MyUtility.CreateCamera ("BattleCamera", battleManager);

		// プレイヤーオブジェクト作成
		for (int i = 0; i < 2; i++) 
		{
			GameObject character = Character.CreateObject (battleManager, Character.CharacterType.Sword, new Vector3(i*2,0,0));

		}
	}


    void Update()
    {
        //TouchInfo touch = TouchManager.GetTouchInfo(0);
        //if (touch == TouchInfo.Began)
        //{
        //    SceneChanger sChange = new SceneChanger();
        //    sChange.ChangeToResult();
        //}
    }

}
