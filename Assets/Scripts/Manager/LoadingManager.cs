using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{

	void Start ()
    {
        //タップエフェクト
        MyUtility.CreateTapEffect(transform);

        string[] str = { "N", "o", "w", " ", "L", "o", "a", "d", "i", "n", "g" };
		for(int i = 0; i < str.GetLength(0);i++)
		{
			GameObject nowLoadingObj = MyUtility.CreateText (str [i], transform, 65, Vector3.zero, new Vector3 ((14 + i * 1.5f) / 32.0f, 3 / 25.0f), new Vector3 ((14 + i * 1.5f) / 32.0f, 3 / 25.0f)).gameObject;
			NowLoading nowLoading = nowLoadingObj.AddComponent<NowLoading> ();
			nowLoading.m_delayCount = i * 24;
            nowLoadingObj.GetComponent<Text> ().color = new Color32 (255, 255, 255, 255);
		}
	}
	
	void Update ()
    {
	
	}
}
