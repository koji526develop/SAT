using UnityEngine;
using System.Collections;

public class StageCreate : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
		for (int i = 0; i < 15; i++) 
		{
			for (int j = 0; j < 15; j++) 
			{
				GameObject obj = new GameObject ("Floor");
				SpriteRenderer spr = obj.AddComponent<SpriteRenderer> ();
				spr.sprite = Resources.Load ("Image/masu", typeof(Sprite))as Sprite;
				obj.transform.position = new Vector3 (-7.0f + (i * 1), 0.0f, -5.0f+(j*1));
				obj.transform.eulerAngles = new Vector3 (90.0f, 0.0f, 0.0f);
				obj.transform.localScale = new Vector3 (2.0f, 1.0f, 1.0f);
			}

		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
