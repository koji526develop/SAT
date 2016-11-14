using UnityEngine;
using System.Collections;

public class CharacterMove :  State<Character>
{
	bool m_isCharacterTouch;

	public CharacterMove (Character _instance) : base (_instance) {}

	public override void Enter ()
	{
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
			if (IsUpFlick () && !IsNearEnemy(Character.Direction.Up)) RotateUp ();

			// キャラクターが下にフリックされていたら回転させる
			else if (IsDownFlick() && !IsNearEnemy(Character.Direction.Down)) RotateDown();
		}
	}

	bool IsNearEnemy(Character.Direction _dir)
	{
		GameObject[] charaObj = GameObject.FindGameObjectsWithTag ("Character");

		for (int i = 0; i < charaObj.Length; i++)
		{
			if (m_instance.gameObject == charaObj [i]) continue;

			if (m_instance.gameObject.GetComponent<Character> ().status.PlayerID == charaObj [i].GetComponent<Character> ().status.PlayerID)
				continue;

			if (_dir == Character.Direction.Up) {
				if (m_instance.GetComponent<Character> ().status.PlayerID == 1 &&
					m_instance.gameObject.transform.position.x < charaObj [i].transform.position.x &&
					m_instance.GetComponent<Character> ().mapColumn - charaObj [i].GetComponent<Character> ().mapColumn == 1 &&
					Mathf.Abs (m_instance.gameObject.transform.position.x) - Mathf.Abs (charaObj [i].transform.position.x) - 0.3f < 60.0f * MyUtility.SIDEMOVE_TIME * m_instance.GetComponent<Character> ().status.moveSpeed) {
					return true;
				} else if (m_instance.GetComponent<Character> ().status.PlayerID == 2 &&
					m_instance.gameObject.transform.position.x > charaObj [i].transform.position.x &&
					m_instance.GetComponent<Character> ().mapColumn - charaObj [i].GetComponent<Character> ().mapColumn == 1 &&
					Mathf.Abs (m_instance.gameObject.transform.position.x) - Mathf.Abs (charaObj [i].transform.position.x) - 0.3f < 60.0f * MyUtility.SIDEMOVE_TIME * m_instance.GetComponent<Character> ().status.moveSpeed) {
					return true;
				}
			} else {
				if (m_instance.GetComponent<Character> ().status.PlayerID == 1 &&
					m_instance.gameObject.transform.position.x < charaObj [i].transform.position.x &&
					m_instance.GetComponent<Character> ().mapColumn - charaObj [i].GetComponent<Character> ().mapColumn == -1 &&
					Mathf.Abs (m_instance.gameObject.transform.position.x) - Mathf.Abs (charaObj [i].transform.position.x) - 0.3f < 60.0f * MyUtility.SIDEMOVE_TIME * m_instance.GetComponent<Character> ().status.moveSpeed) {
					return true;
				} else if (m_instance.GetComponent<Character> ().status.PlayerID == 2 &&
					m_instance.gameObject.transform.position.x > charaObj [i].transform.position.x &&
					m_instance.GetComponent<Character> ().mapColumn - charaObj [i].GetComponent<Character> ().mapColumn == -1 &&
					Mathf.Abs (m_instance.gameObject.transform.position.x) - Mathf.Abs (charaObj [i].transform.position.x) - 0.3f < 60.0f * MyUtility.SIDEMOVE_TIME * m_instance.GetComponent<Character> ().status.moveSpeed) {
					return true;
				}
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
					//m_instance.ChangeState (Character.CharacterState.None); 
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
		if (id == 1 && charaPosX >= MyUtility.SOLDIER_CREATE_LINE_X_2P) return true;

		// プレイヤー２のキャラがプレイヤー１のキャラクター生成ラインより左に行ったら
		else if (id == 2 && charaPosX <= MyUtility.SOLDIER_CREATE_LINE_X_1P) return true;

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