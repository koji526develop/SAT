using UnityEngine;
using System.Collections;

public class CharacterBarrier : State<Character> 
{
	public CharacterBarrier(Character _instance) : base(_instance) {}


	public override void Enter() 
	{
		//m_instance.animator.Play ("Attack");
	}

	public override void Update()
	{

	}

	public override void Exit() 
	{

	}
}
