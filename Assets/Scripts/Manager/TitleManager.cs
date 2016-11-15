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
    }

	void Start ()
    {

	}
	
	void Update ()
    {
        TouchInfo touch = TouchManager.GetTouchInfo(0);
        if (touch == TouchInfo.Began)
        {
            SceneChanger sChange = new SceneChanger();
            sChange.ChangeToOperating();
        }
    }
}
