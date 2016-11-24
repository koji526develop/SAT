using UnityEngine;
using System.Collections;

public class CharacterMove :  State<Character>
{
	bool m_isCharacterTouch;

	public CharacterMove (Character _instance) : base (_instance) {}

	public override void Enter ()
	{
		m_instance.animator.Play ("Move");

		m_isCharacterTouch = false;
	}

	public override void Update ()
	{
		// キャラクターの移動処理
		Move();

		// キャラクターがゴールに到着しているかどうかの判定
		if (IsArrivalGoal ())
		{
			// ゴール処理
			Goal ();

			// キャラクターの削除
			m_instance.Destroy ();
		}

		// 全てのキャラクター同士の衝突判定
		CollisionCheck ();

	

		TouchInfo touchInfo = TouchManager.GetTouchInfo (0);
		// タッチ開始時
		if (touchInfo == TouchInfo.Began)
		{
			// キャラクターがタッチされているか判定
			m_isCharacterTouch = IsCharacterTouch ();
		}
		// タッチ移動中
		else if (touchInfo == TouchInfo.Moved) 
		{
			// キャラクターがタッチされていなかったら何もしない
			if (!m_isCharacterTouch) return;

			// キャラクターが境界線を超えていたら何もしない
			if (IsBeyondCenterLine()) return;

			// キャラクターが上にフリックされていたら回転させる
			if (IsUpFlick () && !IsNearCharacter(Character.Direction.Up)) RotateUp ();

			// キャラクターが下にフリックされていたら回転させる
			else if (IsDownFlick() && !IsNearCharacter(Character.Direction.Down)) RotateDown();
		}

		if (m_instance.animator.GetCurrentAnimatorStateInfo (0).normalizedTime >= 1.0f)
			m_instance.animator.SetTime (0.0f);

	}

	Character.Direction GetObjectDirectionFromMyChara(GameObject _otherObj)
	{
		if (m_instance.gameObject.transform.position.x < _otherObj.transform.position.x)
			return Character.Direction.Right;
		else if (m_instance.gameObject.transform.position.x > _otherObj.transform.position.x)
			return Character.Direction.Left;
		else
			return Character.Direction.None;
	}

	// 他の味方キャラが自分のキャラから見てどの方向にいるかを返す
	Character.Direction GetAllyDirectionFromMyChara(Character _otherCharacter, Character.Direction _dir)
	{
		int playerID = m_instance.GetComponent<Character> ().status.PlayerID;
		int otherCharacterId = _otherCharacter.GetComponent<Character> ().status.PlayerID;
		Character myCharacter = m_instance.GetComponent<Character> ();

		// 自分と相手のプレイヤーIDが同じだったら
		if (playerID == otherCharacterId) {
			Character allyCharacter = _otherCharacter;
			GameObject allyGameObject = _otherCharacter.gameObject;
			int allyCharacterId = otherCharacterId;

			if (playerID == 1) {
				// 味方のキャラが自分より左にいたら
				if (GetObjectDirectionFromMyChara (allyGameObject) == Character.Direction.Left)
				{
					// チェックする行が上の時
					if (_dir == Character.Direction.Up) {
						// 他のキャラが自分より１つ上の行にいたら
						if (myCharacter.mapColumn - allyCharacter.mapColumn == 1) {
							return Character.Direction.LeftUp;
						}
					}
					// チェックする行が下の時
					else if (_dir == Character.Direction.Down) {
						// 他のキャラが自分より１つ下の行にいたら
						if (myCharacter.mapColumn - allyCharacter.mapColumn == -1)
							return Character.Direction.LeftDown;
					}
				}
				// 自分が他の味方のキャラより右にいたら
				else if (GetObjectDirectionFromMyChara (allyGameObject) == Character.Direction.Right)
				{
					// チェックする行が上の時
					if (_dir == Character.Direction.Up) {
						// 自分のキャラが他のキャラより１つ下の行にいたら
						if (myCharacter.mapColumn - allyCharacter.mapColumn == 1) {
							return Character.Direction.RightUp;
						}
					}
					// チェックする行が下の時
					else if (_dir == Character.Direction.Down) {
						// 自分のキャラが他のキャラより１つ上の行にいたら
						if (myCharacter.mapColumn - allyCharacter.mapColumn == -1) {
							return Character.Direction.RightDown;
						}
					}
				}	
			} else if (playerID == 2) {
				if (GetObjectDirectionFromMyChara (allyGameObject) == Character.Direction.Right) {
					// チェックする行が上の時
					if (_dir == Character.Direction.Up) {
						// 他のキャラが自分より１つ上の行にいたら
						if (myCharacter.mapColumn - allyCharacter.mapColumn == 1) {
							return Character.Direction.RightUp;
						}
					}
					// チェックする行が下の時
					else if (_dir == Character.Direction.Down) {
						// 他のキャラが自分より１つ下の行にいたら
						if (myCharacter.mapColumn - allyCharacter.mapColumn == -1) {
							return Character.Direction.RightDown;
						}
					}
				} else if (GetObjectDirectionFromMyChara (allyGameObject) == Character.Direction.Left) {
					// チェックする行が上の時
					if (_dir == Character.Direction.Up) {
						// 自分のキャラが他のキャラより１つ下の行にいたら
						if (myCharacter.mapColumn - allyCharacter.mapColumn == 1) {
							return Character.Direction.LeftUp;
						}
					}
					// チェックする行が下の時
					else if (_dir == Character.Direction.Down) {
						// 自分のキャラが他のキャラより１つ上の行にいたら
						if (myCharacter.mapColumn - allyCharacter.mapColumn == -1) {
							return Character.Direction.LeftDown;
						}
					}
				}
			}
		}
		return Character.Direction.None;
	}

	// 他の敵キャラが自分のキャラから見てどの方向にいるかを返す
	Character.Direction GetEnemyDirectionFromMyChara(Character _otherCharacter, Character.Direction _dir)
	{
		int playerID = m_instance.GetComponent<Character> ().status.PlayerID;
		int otherCharacterId = _otherCharacter.GetComponent<Character> ().status.PlayerID;
		Character myCharacter = m_instance.GetComponent<Character> ();

		// 自分と相手のプレイヤーIDが同じだったら
		if (playerID != otherCharacterId) {
			Character enemyCharacter = _otherCharacter;
			GameObject enemyGameObject = _otherCharacter.gameObject;
			int enemyCharacterId = otherCharacterId;

			if (playerID == 1) 
			{
				// 敵のキャラが自分より左にいたら
				if (GetObjectDirectionFromMyChara (enemyGameObject) == Character.Direction.Left)
				{
					// チェックする行が上の時
					if (_dir == Character.Direction.Up) {
						// 敵のキャラが自分より１つ上の行にいたら
						if (myCharacter.mapColumn - enemyCharacter.mapColumn == 1) {
							return Character.Direction.LeftUp;
						}
					}
					// チェックする行が下の時
					else if (_dir == Character.Direction.Down) {
						// 敵のキャラが自分より１つ下の行にいたら
						if (myCharacter.mapColumn - enemyCharacter.mapColumn == -1)
							return Character.Direction.LeftDown;
					}
				}
				// 自分が他の敵のキャラより右にいたら
				else if (GetObjectDirectionFromMyChara (enemyGameObject) == Character.Direction.Right) {
					// チェックする行が上の時
					if (_dir == Character.Direction.Up) {
						// 自分のキャラが敵のキャラより１つ下の行にいたら
						if (myCharacter.mapColumn - enemyCharacter.mapColumn == 1) {
							return Character.Direction.RightUp;
						}
					}
					// チェックする行が下の時
					else if (_dir == Character.Direction.Down) {
						// 自分のキャラが他のキャラより１つ上の行にいたら
						if (myCharacter.mapColumn - enemyCharacter.mapColumn == -1) {
							return Character.Direction.RightDown;
						}
					}

				}
			} else if (playerID == 2) {
				if (GetObjectDirectionFromMyChara (enemyGameObject) == Character.Direction.Right) {
					// チェックする行が上の時
					if (_dir == Character.Direction.Up) {
						// 他のキャラが自分より１つ上の行にいたら
						if (myCharacter.mapColumn - enemyCharacter.mapColumn == 1) {
							return Character.Direction.RightUp;
						}
					}
					// チェックする行が下の時
					else if (_dir == Character.Direction.Down) {
						// 敵のキャラが自分より１つ下の行にいたら
						if (myCharacter.mapColumn - enemyCharacter.mapColumn == -1) {
							return Character.Direction.RightDown;
						}
					}
				} else if (GetObjectDirectionFromMyChara (enemyGameObject) == Character.Direction.Left) {
					// チェックする行が上の時
					if (_dir == Character.Direction.Up) {
						// 自分のキャラが敵のキャラより１つ下の行にいたら
						if (myCharacter.mapColumn - enemyCharacter.mapColumn == 1) {
							return Character.Direction.LeftUp;
						}
					}
					// チェックする行が下の時
					else if (_dir == Character.Direction.Down) {
						// 自分のキャラが敵のキャラより１つ上の行にいたら
						if (myCharacter.mapColumn - enemyCharacter.mapColumn == -1) {
							return Character.Direction.LeftDown;
						}
					}
				}
			}
		}
		return Character.Direction.None;
	}

	float GetDirection(Vector3 _pos1, Vector3 _pos2)
	{
		return Mathf.Sqrt (Mathf.Pow(_pos2.x - _pos1.x, 2.0f) + Mathf.Pow(_pos2.y - _pos1.y, 2.0f));
	}

	bool IsNearCharacter(Character.Direction _dir)
	{
		GameObject[] otherCharaObj = GameObject.FindGameObjectsWithTag ("Character");
		Character myChara = m_instance.GetComponent<Character> ();
		Vector3 myCharaPos = m_instance.transform.position;

		for (int i = 0; i < otherCharaObj.Length; i++)
		{
			Character otherCharacter = otherCharaObj [i].GetComponent<Character> ();
			Vector3 otherCharaPos = otherCharaObj [i].transform.position;

			if (m_instance.gameObject == otherCharaObj [i]) continue;

			if (GetAllyDirectionFromMyChara (otherCharacter, _dir) == Character.Direction.RightDown) 
			{
				Debug.Log ("味方が右下にいる");
				if (myChara.status.PlayerID == 1)
					if (GetDirection (myCharaPos, otherCharaPos) < 10.0f * myChara.status.moveSpeed) return true;
			}
			else if (GetAllyDirectionFromMyChara (otherCharacter, _dir) == Character.Direction.RightUp)
			{
				Debug.Log ("味方が右上にいる");
				if (myChara.status.PlayerID == 1)
					if (GetDirection (myCharaPos, otherCharaPos) < 10.0f * myChara.status.moveSpeed) return true;
			
			}
			else if (GetAllyDirectionFromMyChara (otherCharacter, _dir) == Character.Direction.LeftDown) 
			{
				Debug.Log ("味方が左下にいる");
				if (myChara.status.PlayerID == 2)
					if (GetDirection (myCharaPos, otherCharaPos) < 10.0f * myChara.status.moveSpeed) return true;
			}
			else if (GetAllyDirectionFromMyChara (otherCharacter, _dir) == Character.Direction.LeftUp) 
			{
				Debug.Log ("味方が左上にいる");
				if (myChara.status.PlayerID == 2)
					if (GetDirection (myCharaPos, otherCharaPos) < 10.0f * myChara.status.moveSpeed) return true;
			}
			else if (GetEnemyDirectionFromMyChara (otherCharacter, _dir) == Character.Direction.RightDown)
			{
				Debug.Log ("敵が右下にいる");
				if (GetDirection(myCharaPos, otherCharaPos) < 90.0f * myChara.status.moveSpeed) return true;
			}
			else if (GetEnemyDirectionFromMyChara (otherCharacter, _dir) == Character.Direction.RightUp)
			{
				Debug.Log ("敵が右上にいる");
				if (GetDirection(myCharaPos, otherCharaPos) < 90.0f * myChara.status.moveSpeed) return true;
			}
			else if (GetEnemyDirectionFromMyChara (otherCharacter, _dir) == Character.Direction.LeftDown)
			{
				Debug.Log ("敵が左下にいる");
				if (GetDirection(myCharaPos, otherCharaPos) < 90.0f * myChara.status.moveSpeed) return true;
			}
			else if (GetEnemyDirectionFromMyChara (otherCharacter, _dir) == Character.Direction.LeftUp) 
			{
				Debug.Log ("敵が左上にいる");
				if (GetDirection(myCharaPos, otherCharaPos) < 90.0f * myChara.status.moveSpeed) return true;
			}
		}

		return false;
	}

	public override void Exit ()
	{

	}

	void Goal()
	{
		int id = m_instance.status.PlayerID;
		int column = m_instance.mapColumn;

		GameObject.Find("GameManager").GetComponent<ScoreManager>().GetPoint(id,column);
	}

	// キャラクターの移動
	void Move()
	{
		if (m_instance.status.PlayerID == 1)m_instance.transform.position += new Vector3 (m_instance.status.moveSpeed, 0, 0);
		else m_instance.transform.position -= new Vector3 (m_instance.status.moveSpeed, 0, 0);
	}

	// キャラクターを回転させるステートにする
	void RotateUp()
	{
		m_instance.rotateDirection = Character.Direction.Up;
		m_instance.ChangeState (Character.CharacterState.Rotate);
	}

	// キャラクターを回転させるステートにする
	void RotateDown()
	{
		m_instance.rotateDirection = Character.Direction.Down;
		m_instance.ChangeState (Character.CharacterState.Rotate);
	}

	// 全てのキャラクター同士の衝突判定
	void CollisionCheck()
	{
		GameObject[] charaObj = GameObject.FindGameObjectsWithTag ("Character");

		for (int i = 0; i < charaObj.Length; i++)
		{
			if (m_instance.gameObject == charaObj [i]) continue;

			if (IsHit (m_instance.gameObject, charaObj [i]))
			{
				// PlayerIDが同じだったら
				if (m_instance.gameObject.GetComponent<Character> ().status.PlayerID == charaObj [i].GetComponent<Character> ().status.PlayerID) 
				{
					m_instance.characterNoneState.suppoteObj = charaObj [i];
					m_instance.ChangeState (Character.CharacterState.None); 
					Debug.Log("仲間にアタック");
				} 
				else
				{
					m_instance.characterAttackState.enemyObj = charaObj [i];
					Debug.Log("アタック");
					m_instance.ChangeState (Character.CharacterState.Attack); 
				}
			}
		
		}
	}

	// キャラクターが上にフリックされたかどうかの判定
	bool IsUpFlick()
	{
		if (!(TouchManager.GetTouchMoveDistanceY (0) > 50.0f)) return false;

		if (m_instance.mapColumn <= MyUtility.MIX_COLUMN) return false;

		return true;
	}

	// キャラクターが下にフリックされたかどうかの判定
	bool IsDownFlick()
	{
		if (!(TouchManager.GetTouchMoveDistanceY (0) < -50.0f)) return false;

		if (m_instance.mapColumn >= MyUtility.MAX_COLUMN) return false;

		return true;

	}

	// キャラクターがタッチされたかどうかの判定
	bool IsCharacterTouch()
	{
		GameObject characterObj = TouchManager.GetRaycastHitObject (m_instance.MainCamera, 0);
		if (characterObj && characterObj == m_instance.gameObject) return true;
		return false;
	}

	// 境界線を超えてるかどうかの判定
	bool IsBeyondCenterLine ()
	{
		int id = m_instance.status.PlayerID;
		float charaPosX = m_instance.transform.position.x;

		// プレイヤー１のキャラがセンターラインより右にいたら
		if (id == 1 && charaPosX >= MyUtility.CENTER_LINE_X) return true;

		// プレイヤー２のキャラがセンターラインより左にいたら
		else if (id == 2 && charaPosX <= MyUtility.CENTER_LINE_X) return true;

		return false;
	}

	// キャラクターがゴール（相手の適正性ボタンの位置）についたかどうか
	bool IsArrivalGoal()
	{
		int id = m_instance.status.PlayerID;
		int column = m_instance.mapColumn;
		float charaPosX = m_instance.transform.position.x;

		// プレイヤー１のキャラがプレイヤー２のキャラクター生成ラインより右に行ったら
		if (id == 1 && charaPosX >= MyUtility.SOLDIER_CREATE_LINE_X_2P-1) return true;

		// プレイヤー２のキャラがプレイヤー１のキャラクター生成ラインより左に行ったら
		else if (id == 2 && charaPosX <= MyUtility.SOLDIER_CREATE_LINE_X_1P+1) return true;

		return false;
	}

	// キャラクターの衝突判定
	bool IsHit (GameObject obj1, GameObject obj2)
	{
		// 衝突判定を行う距離
		float distance = obj1.GetComponent<Character> ().status.attackDistance;

		Character character1 = obj1.GetComponent<Character>();
		Character character2 = obj2.GetComponent<Character>();

		// キャラクターでなかったら抜ける
		if (!obj2.CompareTag ("Character")) return false;

		// 攻撃範囲に入ってなかったら抜ける
		if (!(Mathf.Abs (obj1.transform.position.x - obj2.transform.position.x) < distance)) return false;

	
		// 行が違ったら抜ける
		if (character1.mapColumn != character2.mapColumn) return false;

		return true;
	}



}