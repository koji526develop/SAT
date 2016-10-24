using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// 動的にオブジェクト作ったりするクラス
public class MyUtility : MonoBehaviour
{
	public readonly int SWORD_LIFE = 2;
	public readonly int SWORD_ATTACK = 4;
	public readonly int SWORD_ATTACKDISTANCE = 1;
	public readonly float SWORD_MOCESPEED = 1.0f;

	public readonly int SPEAR_LIFE = 10;
	public readonly int SPEAR_ATTACK = 1;
	public readonly int SPEAR_ATTACKDISTANCE = 1;
	public readonly float SPEAR_MOCESPEED = 1.0f;

	public readonly int AX_LIFE = 6;
	public readonly int AX_ATTACK = 2;
	public readonly int AX_ATTACKDISTANCE = 1;
	public readonly float AX_MOCESPEED = 1.0f;

	public readonly int SHIELD_LIFE = 2;
	public readonly int SHIELD_ATTACK = 2;
	public readonly int SHIELD_ATTACKDISTANCE = 2;
	public readonly float SHIELD_MOCESPEED = 1.0f;
	
	public static void CreateDirectionalLight()
	{
		GameObject lightObj = new GameObject ("DirectionalLight");
		lightObj.transform.Rotate(new Vector3 (15, 0, 0));
		Light light = lightObj.AddComponent<Light> ();
		light.type = LightType.Directional;
	}

	public static Canvas CreateCanvas(Transform _parent = null)
	{
		GameObject canvasObj = new GameObject ("Canvas");

		Canvas canvas = canvasObj.AddComponent<Canvas> ();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;

		CanvasScaler canvasScaler = canvasObj.AddComponent<CanvasScaler> ();
		canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;

		canvasObj.AddComponent<GraphicRaycaster> ();

		if(_parent) canvasObj.transform.SetParent (_parent);

		return canvas;
	}

	public static GameObject CreateEmpty(string _name, Transform _parent)
	{
		GameObject emptyObj = new GameObject (_name);
		emptyObj.transform.SetParent (_parent);

		RectTransform rectTransform = emptyObj.AddComponent<RectTransform> ();
		rectTransform.pivot = new Vector2 (0.5f, 0.5f);
		rectTransform.anchoredPosition3D = new Vector3 (0,0,0);
		rectTransform.sizeDelta = new Vector2 (0, 0);
		rectTransform.anchorMin = new Vector2 (0, 0);
		rectTransform.anchorMax =new Vector2 (1, 1);

		return emptyObj;
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

	public static Text AddText(string _text, Transform _parent)
	{
		GameObject obj = new GameObject ("Text");
		Text text = obj.AddComponent<Text> ();
		obj.transform.SetParent (_parent);

		RectTransform rectTransform = obj.GetComponent<RectTransform> ();
		rectTransform.pivot = new Vector2 (0.5f, 0.5f);
		rectTransform.anchoredPosition3D = new Vector3 (0,-10,0);
		rectTransform.sizeDelta = new Vector2 (0, 0);
		rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
		rectTransform.anchorMax = new Vector2(0.5f, 0.5f);

		text.font = Font.CreateDynamicFontFromOSFont ("Arial", 0);
		text.text = _text;
		text.fontSize = 35 * Screen.width / 1024;
		text.lineSpacing = 1;
		text.alignment = TextAnchor.MiddleCenter;
		text.horizontalOverflow = HorizontalWrapMode.Overflow;
		text.verticalOverflow = VerticalWrapMode.Overflow;
		text.color = new Color (0, 0, 0);

        return text;
	}

    public static Text CreateText(string _text, Transform _parent, int _fontSize,Vector3 _rotation, Vector2 _anchorMin, Vector2 _anchorMax)
    {
        GameObject obj = new GameObject("Text");
        Text text = obj.AddComponent<Text>();
        obj.transform.SetParent(_parent);

        RectTransform rectTransform = obj.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition3D = new Vector3(0, -10, 0);
        rectTransform.sizeDelta = new Vector2(0, 0);
        rectTransform.anchorMin = _anchorMin;
        rectTransform.anchorMax = _anchorMax;

        text.font = Font.CreateDynamicFontFromOSFont("Arial", 0);
        text.text = _text;
        text.fontSize = _fontSize * Screen.width / 1024;
        text.lineSpacing = 1;
        text.alignment = TextAnchor.MiddleCenter;
        text.horizontalOverflow = HorizontalWrapMode.Overflow;
        text.verticalOverflow = VerticalWrapMode.Overflow;
        text.color = new Color(0, 0, 0);
		text.transform.rotation = Quaternion.Euler (_rotation);

        return text;
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


    public static GameObject CreateSlider(string _name, string _imagePath, Vector2 _anchorMin, Vector2 _anchorMax, Transform _parent)
    {
        GameObject sliderObj = new GameObject(_name);
        Slider slider = sliderObj.AddComponent<Slider>();
        sliderObj.transform.SetParent(_parent);

        GameObject fillObj = new GameObject("fill");
        Image image = fillObj.AddComponent<Image>();
        image.sprite = Resources.Load(_imagePath, typeof(Sprite)) as Sprite;

        fillObj.transform.SetParent(sliderObj.transform);

        RectTransform rectTransform = sliderObj.GetComponent<RectTransform>();
        RectTransform fillRectTransform = fillObj.GetComponent<RectTransform>();

        rectTransform.pivot      = new Vector2(0, 1);
        fillRectTransform.pivot  = new Vector2(0, 1);

        rectTransform.anchoredPosition3D        = new Vector3(0, 0, 0);
        fillRectTransform.anchoredPosition3D    = new Vector3(0, 0, 0);

        rectTransform.sizeDelta             = new Vector2(0, 0);
        fillRectTransform.sizeDelta          = new Vector2(0, 0);

        rectTransform.anchorMin = _anchorMin;
        fillRectTransform.anchorMin = _anchorMin;
        rectTransform.anchorMax = _anchorMax;
        fillRectTransform.anchorMax = _anchorMax;

        slider.fillRect = fillRectTransform;

        return sliderObj;
    }

	public static GameObject CreateSprite(Transform _parent,string _name,string _resourcesFolder)
	{
		GameObject spriteObj = new GameObject(_name);
		spriteObj.AddComponent<SpriteRenderer>();
		SpriteRenderer m_SpriteRenderer = spriteObj.GetComponent<SpriteRenderer>();
		m_SpriteRenderer.sprite = Resources.Load<Sprite>(_resourcesFolder);
		spriteObj.transform.SetParent(_parent);

		return spriteObj;
	}

    public static int GetFontSizeFromWidth(GUIStyle style, GUIContent contents, float width)
    {
        int size = 0;
        for (int i = 1; ; i++)
        {
            style.fontSize = i;
            Vector2 v = style.CalcSize(contents);
            if (v.x < width) { size = i; } else { break; }
        }
        return size;
    }
}
