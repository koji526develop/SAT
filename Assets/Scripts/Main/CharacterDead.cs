using UnityEngine;
using System.Collections;

public class CharacterDead : State<Character> 
{
	public CharacterDead(Character _instance) : base(_instance) {}


	public override void Enter() 
	{
		//m_instance.animator.Play ("Attack");
		Debug.Log("死んだ");
		m_instance.Destroy ();
	}

	public override void Update()
	{

	}

	public override void Exit() 
	{
		
	}
}
