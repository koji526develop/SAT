using UnityEngine;
using System.Collections;

// キャラクタークラス

public class Character : StatefulObjectBase<Character, Character.CharacterState> 
{
	// ステータス
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

	// キャラクターのステート
	public enum CharacterState
	{
		None,
		Attack,
		Move,
		Rotate
	}

	// キャラクターの種類
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

	RotateTo m_rotateTo;
	public RotateTo rotateTo
	{
		get{ 
			return m_rotateTo;
		}
	}

	MoveTo m_moveTo;
	public MoveTo moveTo
	{
		get{ 
			return m_moveTo;
		}
	}

	Camera m_mainCamera;
	public Camera MainCamera
	{
		get{ 
			return m_mainCamera;
		}
	}

	// キャラクターのオブジェクト作成
	public static GameObject CreateObject(Transform _parent,CharacterType _characterType, Vector3 _position)
	{
		GameObject characterObj = GameObject.CreatePrimitive (PrimitiveType.Cube);
		Character character = characterObj.AddComponent<Character> ();
	
		characterObj.name = _characterType.ToString();
		characterObj.transform.SetParent (_parent);
		characterObj.transform.position = _position;
		characterObj.tag="Character";

		SetCharacterType (character, _characterType);

		return characterObj;
	}

	// キャラクターの種類のセット
	private static void SetCharacterType(Character character, CharacterType _characterType)
	{
		character.characterType = _characterType;

		// キャラクターによってパラメータを指定
		switch (_characterType) 
		{
		case CharacterType.Sword:
			character.SetParametor (
				MyUtility.SWORD_LIFE,
				MyUtility.SWORD_ATTACK,
				MyUtility.SWORD_ATTACKDISTANCE,
				MyUtility.SWORD_MOCESPEED);
			break;
		case CharacterType.Shield:
			character.SetParametor (
				MyUtility.SHIELD_LIFE,
				MyUtility.SHIELD_ATTACK,
				MyUtility.SHIELD_ATTACKDISTANCE,
				MyUtility.SHIELD_MOCESPEED);
			break;
		case CharacterType.Ax:
			character.SetParametor (
				MyUtility.AX_LIFE,
				MyUtility.AX_ATTACK,
				MyUtility.AX_ATTACKDISTANCE,
				MyUtility.AX_MOCESPEED);
			break;
		case CharacterType.Spear:
			character.SetParametor (
				MyUtility.AX_LIFE,
				MyUtility.AX_ATTACK,
				MyUtility.AX_ATTACKDISTANCE,
				MyUtility.AX_MOCESPEED);
			break;
		}
	}
		
	// パラメーターセット
	public void SetParametor(int _life,int _attack,int _attackDistance,float _moveSpeed)
	{
		m_status.life = _life;
		m_status.attack = _attack;
		m_status.attackDistance = _attackDistance;
		m_status.moveSpeed = _moveSpeed;
	}

	void Start () 
	{
		m_rotateTo = gameObject.AddComponent<RotateTo> ();
		m_moveTo = gameObject.AddComponent<MoveTo> ();

		// ステートマシンの初期設定
		stateList.Add(new CharacterNone(this));
		stateList.Add(new CharacterAttack(this));
		stateList.Add(new CharacterMove(this));
		stateList.Add(new CharacterRotate(this));

		stateMachine = new StateMachine<Character>();

		m_mainCamera = GameObject.FindWithTag("BattleCamera").GetComponent<Camera>();

		ChangeState(CharacterState.Move);
	}

	void Update () 
	{
		// ステートマシーン更新
		stateMachine.Update();
	}
}
