using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	Canvas m_canvas;

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
        //camera.transform.eulerAngles = new Vector3(90,0,0);
        camera.transform.rotation = Quaternion.Euler(50,90,90);
        camera.transform.position = new Vector3(-9.1f, 7.15f, 0);
        camera.rect= new Rect(0.0f, 0.0f, 0.5f, 1.0f);
        camera.clearFlags = CameraClearFlags.Depth;
		camera.orthographic = true;
       
        Camera cameraSub = MyUtility.CreateCamera("BattleCameraSub", battleManager);
        //cameraSub.transform.eulerAngles = new Vector3(90, 0, 0);
        cameraSub.transform.rotation = Quaternion.Euler(50, -90, -90);
        cameraSub.transform.position = new Vector3(9.1f, 7.15f, 0);
        cameraSub.rect= new Rect(0.5f, 0.0f, 0.5f, 1.0f);
        cameraSub.clearFlags = CameraClearFlags.Depth;
		cameraSub.orthographic = true;


        Camera uiCamera = MyUtility.CreateCamera ("UICamera");
		uiCamera.clearFlags = CameraClearFlags.Depth;
		uiCamera.depth = 1;

        //キャンバスを作成
		m_canvas=MyUtility.CreateCanvas();
        GameObject.Find("Canvas").AddComponent<SoldierSurvival>();

		StartCoroutine (MyUtility.SetCameraForCanvas(m_canvas,uiCamera));

        //ゲーム内で使用するマネージャーを作成
        this.gameObject.AddComponent<ScoreManager>();
        this.gameObject.AddComponent<GameUIManager>();

		//タップエフェクト
		MyUtility.CreateTapEffect(transform);
    
    }

	void Start()
	{
		GameObject debugList = Instantiate (Resources.Load("Prefabs/DebugList") as GameObject);
		debugList.name = "DebugList";
		debugList.transform.SetParent (m_canvas.transform);
		m_canvas.gameObject.AddComponent<DebugUI> ();
	}


    void Update()
    {
        //デバッグ用
        if (Input.GetKeyDown("return"))
        {
            SceneChanger sChange = new SceneChanger();
            sChange.ChangeToResult();
        }
    }

}
