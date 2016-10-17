using UnityEngine;
using System.Collections;

public class Character : StatefulObjectBase<Character, Character.CharacterState> 
{
	public int m_life;
	int m_attack;
	int m_attackDistance;
	float m_moveSpeed;
	CharacterType m_characterType;
	float dis;

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
			m_characterType = value;
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

		Vector2 playerpos = playerObj.transform.position;
		Vector2 Enemypos  = playerObj.transform.position;
		character.dis = Vector3.Distance(playerpos,Enemypos);

		switch (_characterType) 
		{
		case CharacterType.Sword:
			character.SetParametor (2,4,1,0.5f);
			break;
		case CharacterType.Shield:
			character.SetParametor (10,1,1,1);
			break;
		case CharacterType.Ax:
			character.SetParametor (6,2,1,1);
			break;
		case CharacterType.Spear:
			character.SetParametor (2,2,2,1);
			break;
		}

		return playerObj;
	}
		
	public void SetParametor(int _life,int _attack,int _attackDistance,float _moveSpeed)
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
		stateList.Add(new CharacterMove(this));

		stateMachine = new StateMachine<Character>();

		ChangeState(CharacterState.Move);
	}
	
	// Update is called once per frame
	void Update () {
		stateMachine.Update();
		hit ();
	}
	public void hit()
	{

		//Debug.Log("Distance : " + dis);
	}



}
