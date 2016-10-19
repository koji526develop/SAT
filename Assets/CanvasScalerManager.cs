using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasScalerManager : MonoBehaviour {
	CanvasScaler m_canvasScaler;
	// Use this for initialization
	void Start () {
		m_canvasScaler = GetComponent<CanvasScaler> ();
	}
	
	// Update is called once per frame
	void Update () {
		m_canvasScaler.referenceResolution = new Vector2 (Screen.width, Screen.height);
	}
}
