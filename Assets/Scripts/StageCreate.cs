using UnityEngine;
using System.Collections;
using UnityEditor;

public class StageCreate : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

		Transform FloorFile = new GameObject ("FloorFile").transform;
		//FloorFile.transform.Rotate(new Vector3(1,0,0),-90);


		for (int i = 0; i < 9; i++) 
		{
			for (int j = 0; j < 6; j++) 
			{
//				GameObject obj = new GameObject ("Floor");
//				SpriteRenderer spr = obj.AddComponent<SpriteRenderer> ();
//				spr.sprite = Resources.Load ("Image/masu", typeof(Sprite))as Sprite;

				GameObject obj = MyUtility.CreateSprite (
					                 FloorFile,
					                 "Floor",
					                 "Image/masu");

				obj.transform.position = new Vector3 (-5.73f + (i * 1.43f), 0.0f, -4.20f+(j*2.085f));
				obj.transform.eulerAngles = new Vector3 (90.0f, 0.0f, 0.0f);
				obj.transform.localScale = new Vector3 (2.5f, 2.19f, 1.0f);

                obj.tag = "Column" + (7-(j+1));
                obj.AddComponent<BoxCollider>().isTrigger = true;
            }

		}


	}

	// Update is called once per frame
	void Update () {
	
	}
}
