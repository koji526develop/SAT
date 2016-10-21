using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUIManager : MonoBehaviour {


    void Awake()
    {
        GameObject m_transformObj = new GameObject("WarPotential");
        RectTransform t = m_transformObj.AddComponent<RectTransform>();
        t.transform.position = Vector3.zero;

        m_transformObj.transform.SetParent(GameObject.Find("Canvas").transform);
        GameObject obj = MyUtility.CreateSlider(
            "BlueSlider",
            "Image/gage-ao",
            new Vector2(5 / 32.0f, 1 / 25.0f),
            new Vector2(27 / 32.0f, 5.0f / 25.0f),
           m_transformObj.transform
            );

    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
