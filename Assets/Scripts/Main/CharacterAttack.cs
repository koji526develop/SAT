using UnityEngine;
using System.Collections;

public class CharacterAttack : State<Character> 
{
	public CharacterAttack(Character _instance) : base(_instance) {}

	GameObject m_enemyObj;
	Character m_enemyCharacter;

	float m_attackTime;

	public GameObject enemyObj
	{
		set
		{ 
			m_enemyObj = value;
		}
	}

	public override void Enter() 
	{
		m_enemyCharacter = m_enemyObj.GetComponent<Character> ();
		m_attackTime = 0.0f;
	}

	public override void Update()
	{
		m_attackTime += Time.deltaTime;
		if(m_attackTime>=2)
		{
			Attak ();
			m_attackTime = 0;
			Debug.Log ("攻撃なう");
		}
	}

	void Attak()
	{
		
		Character.Status changeStatus = m_instance.status;

		changeStatus.life -= m_enemyCharacter.status.attack;

		m_instance.status = changeStatus;
	}
	public override void Exit() 
	{
		m_enemyObj = null;
		m_enemyCharacter = null;
	}
}
