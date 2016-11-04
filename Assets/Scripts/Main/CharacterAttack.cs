﻿using UnityEngine;
using System.Collections;

public class CharacterAttack : State<Character> 
{
	public CharacterAttack(Character _instance) : base(_instance) {}

	// 敵のキャラクターオブジェクト
	GameObject m_enemyObj;

	// 敵のキャラクター
	Character m_enemyCharacter;

	// 攻撃のインターバル
	float m_attackTime;

	//　敵の情報格納用
	Character.Status m_changeStatus; 

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
		m_changeStatus = m_enemyCharacter.status;
	}

	public override void Update()
	{
		m_attackTime += Time.deltaTime;
		Debug.Log (m_instance.status.life);
		if(m_instance.status.life<=0)
		{
			Character.Destroy(m_instance.gameObject);
			Debug.Log ("体力無し");
		}
		if(m_changeStatus.life <= 0)
		{
			m_instance.ChangeState (Character.CharacterState.Move); 
		}	
		
		if(m_attackTime>=2)
		{
			Attak ();
			Debug.Log ("攻撃なう");
			m_attackTime = 0;
		}
	}
	void Attak()
	{
		m_changeStatus.life -= m_instance.status.attack;

		m_enemyCharacter.status = m_changeStatus;
	}
	public override void Exit() 
	{
		m_enemyObj = null;
		m_enemyCharacter = null;
	}
}
