using UnityEngine;
using System.Collections;

public class CharacterBarrier : State<Character> 
{
	public CharacterBarrier(Character _instance) : base(_instance) {}

	public override void Enter() 
	{
		//m_instance.animator.Play ("Attack");
		GameObject barrierObj = GameObject.Instantiate(Resources.Load("SpecialWeapon/SpecailShield/shield")) as GameObject;
		barrierObj.name = "Barrier";

		barrierObj.transform.SetParent (m_instance.transform);

		Vector3 tmp = m_instance.transform.position;
		tmp.y = 1.05f;
		barrierObj.transform.position = tmp;

		m_instance.Barrier = true;

		m_instance.ChangeState (Character.CharacterState.Move);
	}

	public override void Update()
	{

	}

	public override void Exit() 
	{

	}
}
