using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
        public int PlayerID;

    }
	CharacterNone m_characterNoneState;
	public CharacterNone characterNoneState
	{
		get
		{
			return m_characterNoneState;
		}
	}
    CharacterAttack m_characterAttackState;
    public CharacterAttack characterAttackState
    {
        get
        {
            return m_characterAttackState;
        }
    }
    public enum Direction
    {
        None,
        Up,
        Down,
        Right,
        Left,
		RightUp,
		RightDown,
		LeftUp,
		LeftDown
    }

    Direction m_rotateDirection;
    public Direction rotateDirection
    {
        get
        {
            return m_rotateDirection;
        }
        set
        {
            m_rotateDirection = value;
        }
    }
    Status m_status;
    public Status status
    {
        get
        {
            return m_status;
        }
        set
        {
            m_status = value;
        }
    }



    public int m_mapColumn;
    public int mapColumn
    {
        get
        {
            return m_mapColumn;
        }
        set
        {
            m_mapColumn = value;
        }
    }

	bool m_isBarrier = false;
	public bool Barrier
	{
		get
		{
			return m_isBarrier;
		}
		set
		{
			m_isBarrier = value;
		}
	}


    // キャラクターのステート
    public enum CharacterState
    {
        None,
        Attack,
        Move,
        Rotate,
        BackRotate,
		SideMpveUp,
		SideMpveDown,
		Dead,
		Barrier
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
        get
        {
            return m_status.characterType;
        }
        set
        {
            m_status.characterType = value;
        }
    }
    Camera m_mainCamera;
    public Camera MainCamera
    {
        get
        {
            return m_mainCamera;
        }
    }

	Animator m_animator;
	public Animator animator
	{
		set
		{
			m_animator = value;
		}
		get
		{
			return m_animator;
		}
	}

    // キャラクターのオブジェクト作成
    public static GameObject CreateObject(Transform _parent, CharacterType _characterType, Vector3 _position, int _playerID)
    {
		GameObject characterObj = Instantiate (Resources.Load (GetCharacterObjPath(_characterType, _playerID))as GameObject);
        Character character = characterObj.AddComponent<Character>();
		BoxCollider boxCollider = characterObj.AddComponent<BoxCollider> ();
		characterObj.AddComponent<MeshRenderer> ();

		boxCollider.center = new Vector3 (0,2,0);
		boxCollider.size = new Vector3 (2,3,2);

        characterObj.name = _characterType.ToString();
        characterObj.transform.SetParent(_parent);
        characterObj.transform.position = _position;
        characterObj.tag = "Character";


		if (_playerID == 1)
			characterObj.transform.eulerAngles = new Vector3 (0, 90, 0);
		else
			characterObj.transform.eulerAngles = new Vector3 (0, -90, 0);
			
		characterObj.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        SetCharacterType(character, _characterType, _playerID);

        return characterObj;
    }
	private static RuntimeAnimatorController GetAnimController(CharacterType _characterType, int _playerID)
	{
		string colorStr;
		if(_playerID == 1) colorStr = "Blue";
		else 			   colorStr = "Red";

		switch (_characterType)
		{
		case CharacterType.Sword:  return (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Animator/Sword_" + colorStr));
		case CharacterType.Shield:  return (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Animator/Shield_" + colorStr));
		case CharacterType.Ax:      return (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Animator/Ax_" + colorStr));
		case CharacterType.Spear:  return (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Animator/Spear_" + colorStr));
		}
		return null;
	}

	private static string GetCharacterObjPath(CharacterType _characterType, int _playerID)
	{
		string colorStr;
		if(_playerID == 1) colorStr = "blue";
		else 			   colorStr = "red";

		switch (_characterType)
		{
		case CharacterType.Sword:  return "CharacterData/character_sword_" + colorStr;
		case CharacterType.Shield: return "CharacterData/character_shield_" + colorStr;
		case CharacterType.Ax:     return "CharacterData/character_ax_" + colorStr;
		case CharacterType.Spear:  return "CharacterData/character_spear_" + colorStr;
		}
		return null;
	}

    // キャラクターの種類のセット
    private static void SetCharacterType(Character character, CharacterType _characterType, int _playerID)
    {
        character.characterType = _characterType;

        // キャラクターによってパラメータを指定
        switch (_characterType)
        {
            case CharacterType.Sword:
                character.SetParametor(
                    MyUtility.SWORD_LIFE,
                    MyUtility.SWORD_ATTACK,
                    MyUtility.SWORD_ATTACKDISTANCE,
				MyUtility.SWORD_MOVESPEED,
                    _playerID);
                SoldierUse(_playerID,_characterType);
                break;
            case CharacterType.Shield:
                character.SetParametor(
                    MyUtility.SHIELD_LIFE,
                    MyUtility.SHIELD_ATTACK,
                    MyUtility.SHIELD_ATTACKDISTANCE,
				MyUtility.SHIELD_MOVESPEED,
                    _playerID);
                SoldierUse(_playerID, _characterType);
                break;
            case CharacterType.Ax:
                character.SetParametor(
                    MyUtility.AX_LIFE,
                    MyUtility.AX_ATTACK,
                    MyUtility.AX_ATTACKDISTANCE,
				MyUtility.AX_MOVESPEED,
                    _playerID);
                SoldierUse(_playerID, _characterType);
                break;
            case CharacterType.Spear:
                character.SetParametor(
                    MyUtility.SPEAR_LIFE,
                    MyUtility.SPEAR_ATTACK,
                    MyUtility.SPEAR_ATTACKDISTANCE,
				MyUtility.SPEAR_MOVESPEED,
                    _playerID);
                SoldierUse(_playerID, _characterType);
                break;
        }
    }

    //兵士の残数減少処理
    static void SoldierUse(int _playerID, CharacterType type)
    {
        if (_playerID == 1)
        {
            if (type == CharacterType.Sword) SelectUIManager.SWORD_NUM_1--; if (SelectUIManager.SWORD_NUM_1 <= 0) SelectUIManager.SWORD_NUM_1 = 0;
            if (type == CharacterType.Spear) SelectUIManager.SPEAR_NUM_1--; if (SelectUIManager.SPEAR_NUM_1 <= 0) SelectUIManager.SPEAR_NUM_1 = 0;
            if (type == CharacterType.Ax) SelectUIManager.AX_NUM_1--; if (SelectUIManager.AX_NUM_1 < 0) SelectUIManager.AX_NUM_1 = 0;
            if (type == CharacterType.Shield) SelectUIManager.SHIELD_NUM_1--; if (SelectUIManager.SHIELD_NUM_1 <= 0) SelectUIManager.SHIELD_NUM_1 = 0;
        }
        else
        {
            if (type == CharacterType.Sword) SelectUIManager.SWORD_NUM_2--; if (SelectUIManager.SWORD_NUM_2 <= 0) SelectUIManager.SWORD_NUM_2 = 0;
            if (type == CharacterType.Spear) SelectUIManager.SPEAR_NUM_2--; if (SelectUIManager.SPEAR_NUM_2 <= 0) SelectUIManager.SPEAR_NUM_2 = 0;
            if (type == CharacterType.Ax) SelectUIManager.AX_NUM_2--; if (SelectUIManager.AX_NUM_2 <= 0) SelectUIManager.AX_NUM_2 = 0;
            if (type == CharacterType.Shield) SelectUIManager.SHIELD_NUM_2--; if (SelectUIManager.SHIELD_NUM_2 <= 0) SelectUIManager.SHIELD_NUM_2 = 0;
        }
    }

    // パラメーターセット
    public void SetParametor(int _life, int _attack, int _attackDistance, float _moveSpeed, int _playerID)
    {
        m_status.life = _life;
        m_status.attack = _attack;
        m_status.attackDistance = _attackDistance;
        m_status.moveSpeed = _moveSpeed;
        m_status.PlayerID = _playerID;
    }

    void Start()
    {
      

        // ステートマシンの初期設定
		m_characterNoneState=new CharacterNone(this);
		stateList.Add(m_characterNoneState);
        m_characterAttackState = new CharacterAttack(this);
        stateList.Add(m_characterAttackState);
        stateList.Add(new CharacterMove(this));
        stateList.Add(new CharacterRotate(this));
        stateList.Add(new CharacterBackRotate(this));
		stateList.Add(new CharacterSideMoveUp(this));
		stateList.Add(new CharacterSideMoveDown(this));
		stateList.Add(new CharacterDead(this));
		stateList.Add(new CharacterBarrier(this));

        stateMachine = new StateMachine<Character>();

        m_mainCamera = GameObject.FindWithTag("BattleCamera").GetComponent<Camera>();

		m_animator = gameObject.GetComponent<Animator> ();
		m_animator.runtimeAnimatorController = GetAnimController (characterType, status.PlayerID);

        ChangeState(CharacterState.Move);

    }

    void Update()
    {
        // ステートマシーン更新
        stateMachine.Update();

		this.GetComponent<Text> ().text =status.life.ToString();

		if(status.life <= 0)
		{
			ChangeState (CharacterState.Dead);
		}
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

    public static Vector3 GetSpawnPosition(int _playerID, int _Column)
    {
        if (_playerID == 1)
        {
            return new Vector3(MyUtility.SOLDIER_CREATE_LINE_X_1P, 0.0f, 6.0f - (2.0f * _Column));
        }
        else
        {
            return new Vector3(MyUtility.SOLDIER_CREATE_LINE_X_2P, 0.0f, 6.0f - (2.0f * _Column));
        }
    }
}
