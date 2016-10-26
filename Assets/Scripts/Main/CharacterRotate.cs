using UnityEngine;
using System.Collections;

public class CharacterRotate : State<Character> 
{
	public CharacterRotate(Character _instance) : base(_instance) {}

	public override void Enter() 
	{
		
	}

	public override void Update()
	{
		if(m_instance.rotateTo.isRotate == false)
		{
			m_instance.ChangeState (Character.CharacterState.Move);
		}
	}

	public override void Exit()
	{

	}
}