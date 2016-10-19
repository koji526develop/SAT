using UnityEngine;
using System.Collections;

public class Character : StatefulObjectBase<Character, Character.CharacterState> 
{
	public struct Status
	{
		public int life;
		public int attack;
		public int attackDistance;
		public float moveSpeed;
		public CharacterType characterType;
	}

	Status m_status;
	public Status status
	{
		get
		{
			return m_status;
		}
	}


	public enum CharacterState
	{
		None,
		Attack,
		Move
	}

	public enum CharacterType
	{
		Sword,
		Spear,
		Ax,
		Shield
	}

	public CharacterType characterType
	{
		set
		{
			m_status.characterType = value;
		}
	}

	public static GameObject CreateObject(Transform _parent,CharacterType _characterType)
	{
		GameObject playerObj = GameObject.CreatePrimitive (PrimitiveType.Cube);
		Character character = playerObj.AddComponent<Character> ();


		playerObj.name = _characterType.ToString();
		playerObj.transform.SetParent (_parent);
		playerObj.transform.position=new Vector3(GameManager.j,0,0);
		playerObj.tag="Character";

		character.characterType = _characterType;



		switch (_characterType) 
		{
		case CharacterType.Sword:
			character.SetParametor (2,4,1,0.05f);
			break;
		case CharacterType.Shield:
			character.SetParametor (10,1,1,0.01f);
			break;
		case CharacterType.Ax:
			character.SetParametor (6,2,1,0.01f);
			break;
		case CharacterType.Spear:
			character.SetParametor (2,2,2,0.01f);
			break;
		}

		return playerObj;
	}
		
	public void SetParametor(int _life,int _attack,int _attackDistance,float _moveSpeed)
	{
		m_status.life = _life;
		m_status.attack = _attack;
		m_status.attackDistance = _attackDistance;
		m_status.moveSpeed = _moveSpeed;

	}

	// Use this for initialization
	void Start () {
		// ステートマシンの初期設定
		stateList.Add(new CharacterNone(this));
		stateList.Add(new CharacterAttack(this));
		stateList.Add(new CharacterMove(this));

		stateMachine = new StateMachine<Character>();

		ChangeState(CharacterState.Move);
	}
	
	// Update is called once per frame
	void Update () {
		stateMachine.Update();

	}



}
