using UnityEngine;
using System.Collections;

public class SelectSpecialManager : MonoBehaviour 
{
	void Awake () 
	{
		// ライト作成
		MyUtility.CreateDirectionalLight ();

		Canvas canvas = MyUtility.CreateCanvas ();

		GameObject uiObj = MyUtility.CreateEmpty ("UI", canvas.transform);
		uiObj.AddComponent<SelectSpecialUIManager> ();

		// イベントシステム作成
		MyUtility.CreateEventSystem(transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
