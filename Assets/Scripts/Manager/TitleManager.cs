using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleManager : MonoBehaviour
{
    void Awake()
    {
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
