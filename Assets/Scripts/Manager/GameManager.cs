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
        Camera camera = MyUtility.CreateCamera ("BattleCamera", battleManager);
		camera.transform.eulerAngles = new Vector3(90,0,0);
		camera.transform.position = new Vector3(0,15f,0);

		Camera uiCamera = MyUtility.CreateCamera ("UICamera");
		uiCamera.clearFlags = CameraClearFlags.Depth;
		uiCamera.depth = 1;

        //キャンバスを作成
        Canvas canvas=MyUtility.CreateCanvas();
        GameObject.Find("Canvas").AddComponent<SoldierSurvival>();

		StartCoroutine (MyUtility.SetCameraForCanvas(canvas,uiCamera));

        //ゲーム内で使用するマネージャーを作成
        this.gameObject.AddComponent<ScoreManager>();
        this.gameObject.AddComponent<GameUIManager>();

		//タップエフェクト
		MyUtility.CreateTapEffect(transform);
    
    }


    void Update()
    {
        //デバッグ用
        if(Input.GetMouseButtonDown(1))
        {
            SceneChanger sChange = new SceneChanger();
            sChange.ChangeToResult();
        }
    }

}
