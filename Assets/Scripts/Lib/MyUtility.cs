﻿using UnityEngine;
using UnityEngine.UI;

// 動的にオブジェクト作ったりするクラス

public class MyUtility : MonoBehaviour 
{
	public static void CreateDirectionalLight()
	{
		GameObject lightObj = new GameObject ("DirectionalLight");
		lightObj.transform.Rotate(new Vector3 (15, 0, 0));
		Light light = lightObj.AddComponent<Light> ();
		light.type = LightType.Directional;
	}

	public static Camera CreateCamera(string tagName, Transform _parent = null)
	{
		GameObject cameraObj = new GameObject (tagName);
		Camera camera = cameraObj.AddComponent<Camera> ();

		camera.transform.position = new Vector3 (0, 1, -10);
		cameraObj.tag = tagName;

		cameraObj.AddComponent<GUILayer> ();
		cameraObj.AddComponent<FlareLayer> ();
		cameraObj.AddComponent<AudioListener> ();

		if(_parent) cameraObj.transform.SetParent (_parent);

		return camera;
	}

	public static void CreateUICanvas(Transform _parent)
	{
		GameObject uiObj = Instantiate(Resources.Load("Prefabs/UI") as GameObject);
		uiObj.name = "UICanvas";
		uiObj.transform.SetParent (_parent);
	}

	public static void CreateEventSystem(Transform _parent)
	{
		GameObject eventSystemObj = Instantiate(Resources.Load("Prefabs/EventSystem") as GameObject);
		eventSystemObj.name = "EventSystem";
		eventSystemObj.transform.SetParent (_parent);
	}

	public static GameObject CreateImage(string _name, string _imagePath , Vector2 _anchorMin, Vector2 _anchorMax, Transform _parent)
	{
		GameObject imageObj = new GameObject (_name);
		Image image = imageObj.AddComponent<Image> ();
		imageObj.transform.SetParent (_parent);

		image.sprite = Resources.Load (_imagePath, typeof(Sprite)) as Sprite;

		RectTransform rectTransform = imageObj.GetComponent<RectTransform> ();
		rectTransform.pivot = new Vector2 (0, 1);
		rectTransform.anchoredPosition3D = new Vector3 (0,0,0);
		rectTransform.sizeDelta = new Vector2 (0, 0);

		rectTransform.anchorMin = _anchorMin;
		rectTransform.anchorMax = _anchorMax;

		return imageObj;
	}

	public static void AddText(string _text, Transform _parent)
	{
		GameObject obj = new GameObject ("Text");
		Text text = obj.AddComponent<Text> ();
		obj.transform.SetParent (_parent);

		RectTransform rectTransform = obj.GetComponent<RectTransform> ();
		rectTransform.pivot = new Vector2 (0.5f, 0.5f);
		rectTransform.anchoredPosition3D = new Vector3 (0,-10,0);
		rectTransform.sizeDelta = new Vector2 (0, 0);
		rectTransform.anchorMin = new Vector2 (0.5f, 0.5f);
		rectTransform.anchorMax = new Vector2 (0.5f, 0.5f);

		text.font = Font.CreateDynamicFontFromOSFont ("Arial", 0);
		text.text = _text;
		text.fontSize = 35;
		text.lineSpacing = 1;
		text.alignment = TextAnchor.MiddleCenter;
		text.horizontalOverflow = HorizontalWrapMode.Overflow;
		text.verticalOverflow = VerticalWrapMode.Overflow;
		text.color = new Color (0, 0, 0);
	}

	public static GameObject CreateButton(string _name, string _imagePath,Vector2 _anchorMin, Vector2 _anchorMax, Transform _parent)
	{
		GameObject buttonObj = new GameObject (_name);
		Button button = buttonObj.AddComponent<Button> ();
		buttonObj.transform.SetParent (_parent);

		Image image = buttonObj.AddComponent<Image> ();
		image.sprite = Resources.Load (_imagePath, typeof(Sprite)) as Sprite;

		button.targetGraphic = image;

		RectTransform rectTransform = buttonObj.GetComponent<RectTransform> ();
		rectTransform.pivot = new Vector2 (0, 1);
		rectTransform.anchoredPosition3D = new Vector3 (0,0,0);
		rectTransform.sizeDelta = new Vector2 (0, 0);

		rectTransform.anchorMin = _anchorMin;
		rectTransform.anchorMax = _anchorMax;

		return buttonObj;
	}

}
