using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public abstract class SceneUiManagerBase : MonoBehaviour {

	List<GameObject> allGameObjectList = new List<GameObject>();

	void Start () 
	{
		foreach (Transform child in transform) 
		{
			allGameObjectList.Add (child.gameObject);
		}
		AllUiDarken ();
	}

	// 全てのUIを明るくする
	protected void AllUiShine()
	{
		int value = 255;
		foreach (GameObject obj in allGameObjectList)
		{
			Image img = obj.GetComponent<Image> ();
			if(img != null)
				obj.GetComponent<Image> ().color = new Color (value/255.0f, value/255.0f, value/255.0f, obj.GetComponent<Image> ().color.a);
		}
	}

	protected void UiShine(string uiName)
	{
		int value = 255;
		foreach (GameObject obj in allGameObjectList)
		{
			if (obj.name == uiName)
				obj.GetComponent<Image> ().color = new Color (value/255.0f, value/255.0f, value/255.0f, obj.GetComponent<Image> ().color.a);
		}
	}

	// 作り中
	protected void UiShineAndBlink(string uiName)
	{
		foreach (GameObject obj in allGameObjectList)
		{
			if (obj.name == uiName)
				obj.transform.SetAsLastSibling ();
		}
	}

	protected void AllUiDarken()
	{
		int value = 170;
		foreach (GameObject obj in allGameObjectList)
		{
			Image img = obj.GetComponent<Image> ();
			if(img != null)
				obj.GetComponent<Image> ().color = new Color (value/255.0f, value/255.0f, value/255.0f, obj.GetComponent<Image> ().color.a);
		}
	}
}
