﻿using UnityEngine;
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

		GameObject plane = GameObject.CreatePrimitive (PrimitiveType.Plane);
		plane.transform.position = new Vector3 (0, 0, 0);
		plane.transform.rotation = Quaternion.Euler (270, 0, 0);
		plane.transform.localScale = new Vector3 (2, 1, 2);

		// MAINカメラ作成
		Camera mainCamera = MyUtility.CreateCamera ("MainCamera");

		// イベントシステム作成
		MyUtility.CreateEventSystem(transform);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
