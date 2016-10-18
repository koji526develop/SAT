using UnityEngine;
using System.Collections;

public class CharacterNone : State<Character> 
{
	public CharacterNone(Character _instance) : base(_instance) {}

	public override void Enter() 
	{
		Debug.Log ("TestState入ったー");
	}

	public override void Update()
	{
		Debug.Log ("TestStateなう");
		m_instance.ChangeState (Character.CharacterState.Move);
//		if (Input.GetMouseButtonDown (0)) {
//			m_instance.ChangeState (Character.CharacterState.Attack);
//		}

	}

	public override void Exit() 
	{
		Debug.Log ("TestState出たー");
	}
}
