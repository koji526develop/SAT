using UnityEngine;
using System.Collections;

public class Character : StatefulObjectBase<Character, Character.CharacterState> 
{
	public int m_life;
	int m_attack;
	int m_attackDistance;
	float m_moveSpeed;
	CharacterType m_characterType;

	public enum CharacterState
	{
		None,
		Attack
	}

	public enum CharacterType
	{
		Sword,
		Spear,
		Ax,
		Shield
	}

	public Character(CharacterType _characterType)
	{
		m_characterType = _characterType;

		switch (_characterType) 
		{
		case CharacterType.Sword:
			SetParametor (2,4,1,0.5f);
			break;
		case CharacterType.Shield:
			SetParametor (10,1,1,1);
			break;
		case CharacterType.Ax:
			SetParametor (6,2,1,1);
			break;
		case CharacterType.Spear:
			SetParametor (2,2,1,1);
			break;
		}
	}

	void SetParametor(int _life,int _attack,int _attackDistance,float _moveSpeed)
	{
		m_life = _life;
		m_attack = _attack;
		m_attackDistance = _attackDistance;
		m_moveSpeed = _moveSpeed;

	}

	// Use this for initialization
	void Start () {
		// ステートマシンの初期設定
		stateList.Add(new CharacterNone(this));
		stateList.Add(new CharacterAttack(this));

		stateMachine = new StateMachine<Character>();

		ChangeState(CharacterState.None);
	}
	
	// Update is called once per frame
	void Update () {
		stateMachine.Update();
	}


}
