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

        MyUtility.CreateImage(
            "TitleBackGround",
            "UI/Title/title_back",
            new Vector2(0 / 32.0f, 0 / 25.0f),
            new Vector2(32 / 32.0f, 25 / 25.0f),
            m_TitleImageObj.transform);

        MyUtility.CreateImage(
            "Title",
            "Image/title",
            new Vector2(3 / 32.0f, 15 / 25.0f),
            new Vector2(29 / 32.0f, 23 / 25.0f),
           m_TitleImageObj.transform);

        MyUtility.CreateImage(
            "Touch",
            "Image/titleTouch",
            new Vector2(6 / 32.0f, 3 / 25.0f),
            new Vector2(26 / 32.0f, 7 / 25.0f),
            m_TitleImageObj.transform);


    }

    void Start()
    {

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
