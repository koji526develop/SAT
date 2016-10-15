using UnityEngine;
using System.Collections;

public class CharacterMove :  State<Character> {

	public CharacterMove(Character _instance) : base(_instance) {}

	public override void Enter() 
	{
		Debug.Log ("TestState入ったー");
	}

	public override void Update()
	{
		GameObject[] gameobject = GameObject.FindGameObjectsWithTag("Character");


		for(int i=0;i<gameobject.Length;i++)
		{
//			if(gameobject[i].)
//			{
//
//
//		}
		}

	}

	public override void Exit() 
	{
		Debug.Log ("TestState出たー");
	}
	//public Collide
}