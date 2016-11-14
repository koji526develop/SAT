using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleManager : MonoBehaviour
{

    public bool Debug = false;

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
        if (touch == TouchInfo.Began && !Debug)
        {
            SceneChanger sChange = new SceneChanger();
            sChange.ChangeToOperating();
        }
    }
}
