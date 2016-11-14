using UnityEngine;
using System.Collections;

public class CharacterNone : State<Character> 
{
	public CharacterNone(Character _instance) : base(_instance) {}


	public override void Enter() 
	{
		Debug.Log ("入った");
	}
	public override void Update()
	{
		
	}
	public override void Exit() 
	{
		
	}
	bool NonHit(GameObject obj1,GameObject obj2)
	{

	}
}
