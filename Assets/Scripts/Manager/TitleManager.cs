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
        //タップエフェクト
        MyUtility.CreateTapEffect(transform);

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

        //MyUtility.CreateImage(
        //    "Title",
        //    "UI/Title/title",
        //    new Vector2(3 / 32.0f, 15 / 25.0f),
        //    new Vector2(29 / 32.0f, 23 / 25.0f),
        //   m_TitleImageObj.transform);

        GameObject obj = MyUtility.CreateImage(
            "Touch",
            "UI/Title/touch",
            new Vector2(8.0f / 32.0f, 3.1f / 25.0f),
            new Vector2(24.0f / 32.0f, 6.9f / 25.0f),
            m_TitleImageObj.transform);

        FadeTo fadeTo = obj.AddComponent<FadeTo>();
        fadeTo.SetBlinkFadeForever(0.7f,true);
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
            sChange.ChangeToOperating();
        }
    }
}
