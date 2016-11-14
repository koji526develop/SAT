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
        public int PlayerID;

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
        Left
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


    // キャラクターのステート
    public enum CharacterState
    {
        None,
        Attack,
        Move,
        Rotate,
        BackRotate
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

    MoveTo m_moveTo;
    public MoveTo moveTo
    {
        get
        {
            return m_moveTo;
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

    // キャラクターのオブジェクト作成
    public static GameObject CreateObject(Transform _parent, CharacterType _characterType, Vector3 _position, int _playerID)
    {
        GameObject characterObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Character character = characterObj.AddComponent<Character>();

        characterObj.name = _characterType.ToString();
        characterObj.transform.SetParent(_parent);
        characterObj.transform.position = _position;
        characterObj.tag = "Character";

        SetCharacterType(character, _characterType, _playerID);

        return characterObj;
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
                    MyUtility.SWORD_MOCESPEED,
                    _playerID);
                SoldierUse(_playerID,_characterType);
                break;
            case CharacterType.Shield:
                character.SetParametor(
                    MyUtility.SHIELD_LIFE,
                    MyUtility.SHIELD_ATTACK,
                    MyUtility.SHIELD_ATTACKDISTANCE,
                    MyUtility.SHIELD_MOCESPEED,
                    _playerID);
                SoldierUse(_playerID, _characterType);
                break;
            case CharacterType.Ax:
                character.SetParametor(
                    MyUtility.AX_LIFE,
                    MyUtility.AX_ATTACK,
                    MyUtility.AX_ATTACKDISTANCE,
                    MyUtility.AX_MOCESPEED,
                    _playerID);
                SoldierUse(_playerID, _characterType);
                break;
            case CharacterType.Spear:
                character.SetParametor(
                    MyUtility.SPEAR_LIFE,
                    MyUtility.SPEAR_ATTACK,
                    MyUtility.SPEAR_ATTACKDISTANCE,
                    MyUtility.SPEAR_MOCESPEED,
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
            if (type == CharacterType.Sword) SelectUIManager.SWORD_NUM_1--; if (SelectUIManager.SWORD_NUM_1 < 0) SelectUIManager.SWORD_NUM_1 = 0;
            if (type == CharacterType.Spear) SelectUIManager.SPEAR_NUM_1--; if (SelectUIManager.SPEAR_NUM_1 < 0) SelectUIManager.SPEAR_NUM_1 = 0;
            if (type == CharacterType.Ax) SelectUIManager.AX_NUM_1--; if (SelectUIManager.AX_NUM_1 < 0) SelectUIManager.AX_NUM_1 = 0;
            if (type == CharacterType.Shield) SelectUIManager.SHIELD_NUM_1--; if (SelectUIManager.SHIELD_NUM_1 < 0) SelectUIManager.SHIELD_NUM_1 = 0;
        }
        else
        {
            if (type == CharacterType.Sword) SelectUIManager.SWORD_NUM_2--; if (SelectUIManager.SWORD_NUM_2 < 0) SelectUIManager.SWORD_NUM_2 = 0;
            if (type == CharacterType.Spear) SelectUIManager.SPEAR_NUM_2--; if (SelectUIManager.SPEAR_NUM_2 < 0) SelectUIManager.SPEAR_NUM_2 = 0;
            if (type == CharacterType.Ax) SelectUIManager.AX_NUM_2--; if (SelectUIManager.AX_NUM_2 < 0) SelectUIManager.AX_NUM_2 = 0;
            if (type == CharacterType.Shield) SelectUIManager.SHIELD_NUM_2--; if (SelectUIManager.SHIELD_NUM_2 < 0) SelectUIManager.SHIELD_NUM_2 = 0;
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
        m_moveTo = gameObject.AddComponent<MoveTo>();

        // ステートマシンの初期設定
        stateList.Add(new CharacterNone(this));
        m_characterAttackState = new CharacterAttack(this);
        stateList.Add(m_characterAttackState);
        stateList.Add(new CharacterMove(this));
        stateList.Add(new CharacterRotate(this));
        stateList.Add(new CharacterBackRotate(this));

        stateMachine = new StateMachine<Character>();

        m_mainCamera = GameObject.FindWithTag("BattleCamera").GetComponent<Camera>();

        ChangeState(CharacterState.Move);
    }

    void Update()
    {
        // ステートマシーン更新
        stateMachine.Update();
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

    public static Vector3 GetSpawnPosition(int _playerID, int _Column)
    {
        if (_playerID == 1)
        {
            return new Vector3(MyUtility.SOLDIER_CREATE_LINE_X_1P, 0.0f, 8.0f - (2.0f * _Column));
        }
        else
        {
            return new Vector3(MyUtility.SOLDIER_CREATE_LINE_X_2P, 0.0f, 8.0f - (2.0f * _Column));
        }
    }

    // 行の変更(変更した行に移動も兼ねる)
    public void ChangeColumn(Direction _direction)
    {
        if (_direction == Direction.Up)
        {
            mapColumn -= 1;
        }
        else
        {
            mapColumn += 1;
        }

        // 変更した行に移動
        moveTo.SetMoveTo(new Vector3(transform.position.x, transform.position.y, 8.0f - (2.0f * mapColumn)), 1.0f);
    }
}
