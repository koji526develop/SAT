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
					iTween.RotateBy (m_instance.gameObject, new Vector3 (0, (-1*GetCharacterRote()) / 4.0f, 0), 1.0f);
					m_instance.ChangeState (Character.CharacterState.Rotate);
				} 
				else if (TouchManager.GetTouchMoveDistanceY (0) < -50.0f) 
				{
					iTween.RotateBy (m_instance.gameObject, new Vector3 (0, (1*GetCharacterRote()) / 4.0f, 0), 1.0f);
					m_instance.ChangeState (Character.CharacterState.Rotate);
				}
			}
			/***********************************************
			// 全てのキャラクター同士の衝突判定
			***********************************************/
			GameObject[] gameobject = GameObject.FindGameObjectsWithTag ("Character");

			for (int i = 0; i < gameobject.Length; i++) {
			
				if (m_instance.gameObject == gameobject [i])
					continue;

				if (gameobject [i].CompareTag ("Character") && IsHit (m_instance.gameObject, gameobject [i], m_instance.status.attackDistance)) {
					int HitObjID = gameobject [i].GetComponent<Character> ().status.PlayerID;
					if (m_instance.status.PlayerID != HitObjID) {
						m_instance.ChangeState (Character.CharacterState.Attack);
						Debug.Log ("当たったー");
					}
				}
			}
		}
	}

	// キャラクターの回転方向取得(1Pと2Pで回転させる方向を変えるため)
	int GetCharacterRote()
	{
		if (m_instance.status.PlayerID == 1) return 1;
		else return -1;
	}

	// キャラクターがタッチされたかどうかの判定
	bool IsCharacterTouch()
	{
		GameObject characterObj = TouchManager.GetRaycastHitObject (m_instance.MainCamera, 0);
		if (characterObj) return true;
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
	bool IsHit (GameObject obj1, GameObject obj2, float distance)
	{
		return Mathf.Abs (obj1.transform.position.x - obj2.transform.position.x) < distance;
	}

	public override void Exit ()
	{
		
	}
}