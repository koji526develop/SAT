using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour
{

	void Start ()
    {
	
	}
	
	void Update ()
    {
        TouchInfo touch = TouchManager.GetTouchInfo(0);
        if(touch == TouchInfo.Began)
        {
            SceneChanger sChange = new SceneChanger();
            sChange.ChangeToMenu();
        }
    }
}
