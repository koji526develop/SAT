using UnityEngine;
using System.Collections;

public class CharacterMove :  State<Character>
{

	bool m_isCharacterTouch;

	public CharacterMove (Character _instance) : base (_instance)
	{
	}

	public override void Enter ()
	{
		m_isCharacterTouch = false;
	}

	public override void Update ()
	{
		/***********************************************
		// キャラクターの移動処理
		***********************************************/

		if (m_instance.status.PlayerID == 1)m_instance.transform.position += new Vector3 (m_instance.status.moveSpeed, 0, 0);
		else m_instance.transform.position -= new Vector3 (m_instance.status.moveSpeed, 0, 0);
		/***********************************************
		// キャラクター削除判定
		***********************************************/

		if (IsCharacterDestroy ()) m_instance.Destroy();

		/***********************************************
		// キャラクターのタッチ判定
		***********************************************/

		TouchInfo touch = TouchManager.GetTouchInfo (0);

		if (touch == TouchInfo.Began) {
			// キャラクターがタッチされているか判定
			m_isCharacterTouch = IsCharacterTouch ();
		}

		if (touch == TouchInfo.Moved) 
		{
			if (m_isCharacterTouch) 
			{
				// もし境界線を超えていたら何もしない
				if (IsBeyondCenterLine()) return;

				if (TouchManager.GetTouchMoveDistanceY (0) > 50.0f) 
				{	
					if (m_instance.mapColumn <= MyUtility.MIX_COLUMN) return;
					m_instance.rotateDirection = Character.Direction.Left;
					m_instance.ChangeState (Character.CharacterState.Rotate);
				} 
				else if (TouchManager.GetTouchMoveDistanceY (0) < -50.0f) 
				{
					if (m_instance.mapColumn >= MyUtility.MAX_COLUMN) return;
					m_instance.rotateDirection = Character.Direction.Right;
					m_instance.ChangeState (Character.CharacterState.Rotate);
				}
			}
		}
		/***********************************************
			// 全てのキャラクター同士の衝突判定
		***********************************************/
		GameObject[] gameobject = GameObject.FindGameObjectsWithTag ("Character");

		for (int i = 0; i < gameobject.Length; i++) {

			if (m_instance.gameObject == gameobject [i])
				continue;

			if (IsHit (m_instance.gameObject, gameobject [i])) 
			{
				Debug.Log ("当たったー");
			}
		}
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
		if (m_instance.status.PlayerID == 1) {
			if (m_instance.transform.position.x >= MyUtility.CENTER_LINE_X) {
				return true;
			}
		} else {
			if (m_instance.transform.position.x <= MyUtility.CENTER_LINE_X) {
				return true;
			}
		}

		return false;
	}
	bool IsCharacterDestroy()
	{
		if(m_instance.transform.position.x>=MyUtility.DESTROY_LINE_X_1P)
		{
			return true;
		}
		else if(m_instance.transform.position.x<=MyUtility.DESTROY_LINE_X_2P)
		{
			return true;
		}
		return false;
	}

	// キャラクターの衝突判定
	bool IsHit (GameObject obj1, GameObject obj2)
	{
		float distance = obj1.GetComponent<Character> ().status.attackDistance;

		if (!obj2.CompareTag ("Character"))
			return false;

		Character character1 = obj1.GetComponent<Character>();
		Character character2 = obj2.GetComponent<Character>();

		if (!(Mathf.Abs (obj1.transform.position.x - obj2.transform.position.x) < distance))
			return false;

		if (character1.status.PlayerID == character2.status.PlayerID) {
			return false;
		}

		if (character1.mapColumn != character2.mapColumn) {
			return false;
		}

		return true;
	}

	public override void Exit ()
	{
		
	}
}