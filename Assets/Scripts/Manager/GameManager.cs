using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    Canvas m_canvas;
    Touch touchh;
    bool flag = false;

    //カウントダウン用
    GameObject[] m_countDownObj = new GameObject[8];
    StartCountDown[] countDown = new StartCountDown[8];
    int m_nowCount = 0;
    public bool m_startFlag = false;
    bool m_countStart = false;

    // ゲーム開始時
    void Awake()
    {
        // Light作成
        MyUtility.CreateDirectionalLight();

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
        Transform battleManager = new GameObject("BattleManager").transform;
        battleManager.tag = "BattleManager";

        // バトル用のカメラ作成
        Camera camera = MyUtility.CreateCamera("BattleCamera", battleManager);
        //camera.transform.eulerAngles = new Vector3(90,0,0);
        camera.transform.rotation = Quaternion.Euler(50, 90, 90);
        camera.transform.position = new Vector3(-9.0f, 7.15f, 0);
        camera.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
        camera.clearFlags = CameraClearFlags.Depth;
        camera.orthographic = true;

        Camera cameraSub = MyUtility.CreateCamera("BattleCameraSub", battleManager);
        //cameraSub.transform.eulerAngles = new Vector3(90, 0, 0);
        cameraSub.transform.rotation = Quaternion.Euler(50, -90, -90);
        cameraSub.transform.position = new Vector3(9.0f, 7.15f, 0);
        cameraSub.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
        cameraSub.clearFlags = CameraClearFlags.Depth;
        cameraSub.orthographic = true;


        Camera uiCamera = MyUtility.CreateCamera("UICamera");
        uiCamera.clearFlags = CameraClearFlags.Depth;
        uiCamera.depth = 1;

        Camera specialCamera = MyUtility.CreateCamera("SpecialCamera");
        specialCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
        specialCamera.transform.position = new Vector3(0, 10, 0);
        specialCamera.clearFlags = CameraClearFlags.Depth;
        specialCamera.depth = 2;
        specialCamera.orthographic = true;
        for (int i = 0; i < 10; i++)
        {
            specialCamera.cullingMask &= ~(1 << i);
        }
        int intLayer = LayerMask.NameToLayer("SpecialEffect");
        specialCamera.cullingMask |= (1 << intLayer);

        //キャンバスを作成
        m_canvas = MyUtility.CreateCanvas();


        StartCoroutine(MyUtility.SetCameraForCanvas(m_canvas, uiCamera));

        //ゲーム内で使用するマネージャーを作成
        this.gameObject.AddComponent<ScoreManager>();
        this.gameObject.AddComponent<GameUIManager>();

        //タップエフェクト
        MyUtility.CreateTapEffect(transform);

        //AudioManager.m_instance.PlaySE ("Countdown_SE");

        //カウントダウン処理用
        string[] m_countPath = { "UI/Game/3", "UI/Game/2", "UI/Game/1", "UI/Game/fight" };

        GameObject[] CountObj = new GameObject[2];
        for (int i = 0; i < 2; i++)
        {
            CountObj[i] = MyUtility.CreateEmpty("CountUI" + i.ToString(), m_canvas.transform);

            for (int j = 0; j < 4; j++)
            {
                m_countDownObj[j + (i * 4)] = MyUtility.CreateImage(
                    "CountDown",
                    m_countPath[j],
                    new Vector2(14 / 32.0f, 9 / 25.0f),
                    new Vector2(21 / 32.0f, 16 / 25.0f),
                    CountObj[i].transform);

                m_countDownObj[j + (i * 4)].transform.Rotate(new Vector3(0, 0, 1), -90.0f);

                m_countDownObj[j + (i * 4)].AddComponent<StartCountDown>();
                countDown[j + (i * 4)] = m_countDownObj[j + (i * 4)].GetComponent<StartCountDown>();
                countDown[j + (i * 4)].FadeFlag = false;
                m_countDownObj[j + (i * 4)].SetActive(false);
            }
            m_countDownObj[m_nowCount + (i * 4)].SetActive(true);
            //countDown[m_nowCount + (i * 4)].FadeFlag = true;
        }
        //2Pように反転
        CountObj[1].transform.Rotate(new Vector3(0, 0, 1), 180);
        Vector2 tmpPos = new Vector2(Screen.width - CountObj[0].transform.position.x, Screen.height - CountObj[0].transform.position.y);
        CountObj[1].transform.position = new Vector3(tmpPos.x, tmpPos.y, 0.0f);
    }

    void Start()
    {
        GameObject debugList = Instantiate(Resources.Load("Prefabs/DebugList") as GameObject);
        debugList.name = "DebugList";
        debugList.transform.SetParent(m_canvas.transform);
        m_canvas.gameObject.AddComponent<DebugUI>();

        Invoke("DelayMethod", 0.5f);

        AudioManager.m_instance.PlayBGM("battle_BGM");

    }

    void DelayMethod()
    {
        countDown[m_nowCount].FadeFlag = true;
        countDown[m_nowCount + 4].FadeFlag = true;
        m_countStart = true;
    }

    void Update()
    {
        TouchManager.Update();

        //ゲームスタートのカウントダウン
        if (!m_startFlag && m_countStart)
        {
            if (!countDown[m_nowCount].FadeFlag)
            {
                //fightが消えたらゲーム開始
                if (m_nowCount == 3 && !countDown[m_nowCount].FadeFlag)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        Destroy(m_countDownObj[i]);
                    }
                    m_startFlag = true;
                }
                else
                {
                    m_nowCount++;
                    for (int i = 0; i < 2; i++)
                    {
                        m_countDownObj[m_nowCount + (i * 4)].SetActive(true);
                        countDown[m_nowCount + (i * 4)].FadeFlag = true;
                    }
                }
            }
        }
    }
}
