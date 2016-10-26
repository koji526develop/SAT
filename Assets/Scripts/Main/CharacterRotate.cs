using UnityEngine;
using System.Collections;

public class CharacterRotate : State<Character> 
{
	public CharacterRotate(Character _instance) : base(_instance) {}

	float m_rotateTime;

	public override void Enter() 
	{
		m_rotateTime = 0.0f;
	}

	public override void Update()
	{
		m_rotateTime += Time.deltaTime;
		if(m_rotateTime > 0.5f)
		{
			m_instance.ChangeState (Character.CharacterState.Move);
		}
	}

	public override void Exit()
	{

	}
}