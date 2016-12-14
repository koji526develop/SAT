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


        //背景
        GameObject obj = MyUtility.CreateSprite(
            transform,
            "SpecialSelectBack",
            "UI/SpecialSelect/specialselect_back");

        obj.transform.localScale = new Vector3(1.5f,1.5f,0);
    }
	
	// Update is called once per frame
	void Update () {
		TouchManager.Update ();
	}
}
