using UnityEngine;
using System.Collections;

public class CharacterAttack : State<Character> 
{
	public CharacterAttack(Character _instance) : base(_instance) {}

	public override void Enter() 
	{
		Debug.Log ("TestState入ったー");
		//m_instance.m_life
	}

	public override void Update()
	{
		Debug.Log ("TestStateなう");
		//m_instance.ChangeSta0te


	}

	public override void Exit() 
	{
		Debug.Log ("TestState出たー");
	}

	public void Attack(Character.CharacterType _characterType)
	{
		/*switch (_characterType) 
		{
		case 

		}*/
	}
}
