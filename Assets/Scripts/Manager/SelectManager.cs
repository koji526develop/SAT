using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectManager : MonoBehaviour 
{
	void Awake () 
	{
		// ライト作成
		MyUtility.CreateDirectionalLight ();

		Canvas canvas = MyUtility.CreateCanvas ();

		// MAINカメラ作成
		Camera camera = MyUtility.CreateCamera ("MainCamera");

		GameObject uiObj = MyUtility.CreateEmpty ("UI", canvas.transform);
		uiObj.AddComponent<SelectUIManager> ();

		StartCoroutine (MyUtility.SetCameraForCanvas(canvas,camera));

		// イベントシステム作成
		MyUtility.CreateEventSystem(transform);
    }

	public IEnumerator SetCameraForCanvas(Canvas _canvas, Camera _camera)
	{
		return null;
		_canvas.worldCamera = _camera;
	}

	void Update () 
	{
	}
}
