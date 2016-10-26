using UnityEngine;
using System.Collections;

public class StageCreate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject obj=new GameObject ("Floor");
		SpriteRenderer spr= obj.AddComponent<SpriteRenderer> ();
		spr.sprite = Resources.Load ("Resources/Image/masu", typeof(Sprite))as Sprite;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
