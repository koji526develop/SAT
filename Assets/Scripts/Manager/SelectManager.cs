using UnityEngine;
using System.Collections;

public class SelectManager : MonoBehaviour 
{
	void Awake () 
	{
		// ライト作成
		MyUtility.CreateDirectionalLight ();

		// MAINカメラ作成
		Camera mainCamera = MyUtility.CreateCamera ("MainCamera");

		// イベントシステム作成
		MyUtility.CreateEventSystem(transform);
	}

	void Update () 
	{
	
	}
}
