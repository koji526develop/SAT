using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DebugUI : MonoBehaviour 
{
	// タッチ開始座標
	Vector3 m_touchBeganPos;

	// タッチ終了座標
	Vector3 m_touchEndedPos;

	void Start()
	{
		m_touchBeganPos.x = Screen.width/2;
		m_touchEndedPos.x = Screen.width/2;
	}

	void Update () 
	{
		if (IsDebugAction()) 
		{
			// ここでデバッグリストを表示させる
			transform.FindChild ("DebugList").gameObject.SetActive (true);
		}
	}

	bool IsDebugAction()
	{
		bool result = false;
        for (int i = 0; i < 2; i++)
        {
            TouchInfo touchInfo = TouchManager.GetTouchInfo(i);
            if (touchInfo == TouchInfo.Began)
            {
                m_touchBeganPos = TouchManager.GetTouchPosition(i);
            }
            else if (touchInfo == TouchInfo.Ended)
            {
                m_touchEndedPos = TouchManager.GetTouchPosition(i);
            }

			if (m_touchBeganPos.x < Screen.width * 2.0f/10 && m_touchEndedPos.x > Screen.width * 8.0f/10)
            {
                result = true;
            }

            if (touchInfo == TouchInfo.Ended)
            {
				m_touchBeganPos.x = Screen.width/2;
				m_touchEndedPos.x = Screen.height/2;
            }
        }


		return result;
	}
}
