using UnityEngine;
using System.Collections;

public class SpecialCardSprite : MonoBehaviour {

	public int cardNum;

	public static GameObject CreateSprite(Transform _parent, string _name, string _resourcesFolder)
	{
		GameObject cardObj = MyUtility.CreateSprite (_parent, _name, _resourcesFolder);
		cardObj.tag = "Card";
		cardObj.transform.localScale = new Vector3 (1.3f, 1.24f, 1);
		cardObj.AddComponent<BoxCollider2D> ();

		return cardObj;
	}

	// Use this for initialization
	void Start () {

	}  
	
	// Update is called once per frame
	void Update () {
	
	}
}
