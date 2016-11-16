using UnityEngine;
using System.Collections;

public class CharacterNone : State<Character> 
{
	public CharacterNone(Character _instance) : base(_instance) {}
	//接触した味方の情報
	GameObject m_suppoteObj;
	public GameObject suppoteObj
	{
		set
		{ 
			m_suppoteObj = value;
		}
	}
	Character m_suppoteObjCharacter;

	public override void Enter() 
	{
		Debug.Log ("入った");
		m_suppoteObjCharacter = m_suppoteObj.GetComponent<Character> ();
	}
	public override void Update()
	{
		if (m_suppoteObjCharacter.IsEqualState(Character.CharacterState.Move)|| m_suppoteObj==null) 
		{
			m_instance.ChangeState (Character.CharacterState.Move);
		}
	}
	public override void Exit() 
	{
		m_suppoteObj = null;
		Debug.Log ("出た");
	}

}
