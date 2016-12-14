using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleManager : MonoBehaviour
{
    void Awake()
    {
        // ライト作成
        MyUtility.CreateDirectionalLight();
        // MAINカメラ作成
        Camera camera = MyUtility.CreateCamera("MainCamera");

        Canvas canvas = MyUtility.CreateCanvas();
        //イベントシステム作成
        MyUtility.CreateEventSystem(transform);

        StartCoroutine(MyUtility.SetCameraForCanvas(canvas, camera));

        GameObject m_TitleImageObj = MyUtility.CreateEmpty("TitleObject", canvas.transform);

        //背景
        MyUtility.CreateImage(
            "TitleBackGround",
            "UI/Title/title_back",
            new Vector2(0 / 32.0f, 0 / 25.0f),
            new Vector2(32 / 32.0f, 25 / 25.0f),
            m_TitleImageObj.transform);
        //タイトル文字
        MyUtility.CreateImage(
            "Title",
            "UI/Title/title_new",
            new Vector2(0 / 32.0f, 0 / 25.0f),
            new Vector2(32/ 32.0f, 25 / 25.0f),
            m_TitleImageObj.transform);
        //タッチの文字
        GameObject obj = MyUtility.CreateImage(
            "Touch",
            "UI/Title/touch",
            new Vector2(8.0f / 32.0f, 3.1f / 25.0f),
            new Vector2(24.0f / 32.0f, 6.9f / 25.0f),
            m_TitleImageObj.transform);

        FadeTo fadeTo = obj.AddComponent<FadeTo>();     //タッチの文字を点滅
        fadeTo.SetBlinkFadeForever(0.7f,true);      

        //兵士の情報を初期化
        SelectUIManager.SWORD_NUM_1 = 0;
        SelectUIManager.SPEAR_NUM_1 = 0;
        SelectUIManager.AX_NUM_1 = 0;
        SelectUIManager.SHIELD_NUM_1 = 0;
        SelectUIManager.SWORD_NUM_2 = 0;
        SelectUIManager.SPEAR_NUM_2 = 0;
        SelectUIManager.AX_NUM_2 = 0;
        SelectUIManager.SHIELD_NUM_2 = 0;

        SelectUIManager.PlayerID = 1;
    }

    void Start()
    {
		AudioManager.m_instance.PlayBGM ("title_BGM");
    }

    void Update()
    {
        TouchInfo touch = TouchManager.GetTouchInfo(0);
        if (touch == TouchInfo.Began)
        {
            SceneChanger sChange = new SceneChanger();
            sChange.ChangeToMenu();
        }
    }
}
