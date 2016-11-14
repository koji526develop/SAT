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

		/*GameObject plane = GameObject.CreatePrimitive (PrimitiveType.Plane);
		plane.transform.position = new Vector3 (0, 0, 0);
		plane.transform.rotation = Quaternion.Euler (270, 0, 0);
		plane.transform.localScale = new Vector3 (2, 1, 2);
		*/

		StartCoroutine (MyUtility.SetCameraForCanvas(canvas,camera));

		// イベントシステム作成
		MyUtility.CreateEventSystem(transform);

        //タップエフェクト
        MyUtility.CreateTapEffect(transform);
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
