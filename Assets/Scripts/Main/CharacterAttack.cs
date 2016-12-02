using UnityEngine;
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

	int MAX_HP;
	float MAX_SCALE_Y;

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

		m_instance.animator.Play ("Attack");


		switch (m_instance.status.characterType) {
		case Character.CharacterType.Sword:
			MAX_HP = MyUtility.SWORD_LIFE;
			break;
		case Character.CharacterType.Spear:
			MAX_HP = MyUtility.SPEAR_LIFE;
			break;
		case Character.CharacterType.Ax:
			MAX_HP = MyUtility.AX_LIFE;
			break;
		case Character.CharacterType.Shield:
			MAX_HP = MyUtility.SHIELD_LIFE;
			break;
		}

		MAX_SCALE_Y = ButtonSpawner.hp.transform.localScale.y;
	}

	public override void Update()
	{
		m_attackTime += Time.deltaTime;
	
		if(m_instance.animator.GetCurrentAnimatorStateInfo (0).normalizedTime >= 1.0f)
		{
			m_instance.animator.SetTime (0.0f);
			Attak ();
			Debug.Log ("攻撃なう");
			m_attackTime = 0;

		}

		if(m_enemyCharacter == null)
			m_instance.ChangeState (Character.CharacterState.Move);
	}
	void Attak()
	{
		if (m_enemyCharacter.Barrier) {
			foreach (Transform child in m_enemyCharacter.transform) {
				if (child.name == "Barrier")
					Character.Destroy (child.gameObject);
			}
			m_enemyCharacter.Barrier = false;
			return;
		} else {

			switch (m_instance.status.characterType) 
			{
			case Character.CharacterType.Sword:
				AudioManager.m_instance.PlaySE ("sowrd_SE");
				break;
			case Character.CharacterType.Spear:
				AudioManager.m_instance.PlaySE ("spear_SE");
				break;
			case Character.CharacterType.Shield:
				AudioManager.m_instance.PlaySE ("shield_SE");
				break;
			case Character.CharacterType.Ax:
				AudioManager.m_instance.PlaySE ("sowrd_SE");
				break;
			}

			m_changeStatus.life -= m_instance.status.attack;

			m_enemyCharacter.status = m_changeStatus;

			float changeY = MAX_SCALE_Y / (MAX_HP / m_instance.status.life);

			ButtonSpawner.hp.transform.localScale = new Vector3 (ButtonSpawner.hp.transform.localScale.x,changeY,ButtonSpawner.hp.transform.localScale.z);


		}
	}

	public override void Exit() 
	{
		m_enemyObj = null;
		m_enemyCharacter = null;
	}
}
