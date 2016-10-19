using UnityEngine;
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

    public static GameObject CreateSprite(Transform _parent,string _name,string _resourcesFolder)
    {
        GameObject spriteObj = new GameObject(_name);
        spriteObj.AddComponent<SpriteRenderer>();
        SpriteRenderer m_SpriteRenderer = spriteObj.GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = Resources.Load<Sprite>(_resourcesFolder);
        spriteObj.transform.SetParent(_parent);

        return spriteObj;
    }
}
