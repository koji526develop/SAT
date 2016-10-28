using UnityEngine;
using System.Collections;

public class CharacterMove :  State<Character> {

	bool m_CharacterTouch;

	public CharacterMove(Character _instance) : base(_instance) {}

	public override void Enter() 
	{
		m_CharacterTouch = false;
	}

	public override void Update()
	{
		m_instance.transform.position += new Vector3 (0.005f,0,0);

		TouchInfo touch = TouchManager.GetTouchInfo (0);

		/***********************************************
		// キャラクターのタッチ判定
		***********************************************/
		if(touch == TouchInfo.Began)
		{
			GameObject characterObj = TouchManager.GetRaycastHitObject (m_instance.MainCamera, 0);
			if (characterObj != null) 
			{
				m_CharacterTouch = true;
			} 
			else
			{
				m_CharacterTouch = false;
			}
		}

		if(touch == TouchInfo.Moved)
		{
			if (m_CharacterTouch) 
			{
				if (TouchManager.GetTouchMoveDistanceY (0) > 50.0f)
				{	
					iTween.RotateBy(m_instance.gameObject, new Vector3(1/4.0f,0,0),1.0f);
					m_instance.ChangeState (Character.CharacterState.Rotate);
				} 
				else if (TouchManager.GetTouchMoveDistanceY (0) < -50.0f) 
				{
					iTween.RotateBy(m_instance.gameObject, new Vector3(-1/4.0f,0,0),1.0f);
					m_instance.ChangeState (Character.CharacterState.Rotate);
				}
			}
		}
		/***********************************************
		// 全てのキャラクター同士の衝突判定
		***********************************************/
		GameObject[] gameobject = GameObject.FindGameObjectsWithTag("Character");

		for(int i=0;i<gameobject.Length;i++)
		{
			if (m_instance.gameObject == gameobject [i])
				continue;

			if(gameobject[i].CompareTag("Character") && IsHit(m_instance.gameObject, gameobject[i],m_instance.status.attackDistance))
			{
				int HitObjID = gameobject [i].GetComponent<Character> ().status.PlayerID;
				if(m_instance.status.PlayerID != HitObjID)
				{
				m_instance.ChangeState (Character.CharacterState.Attack);
				Debug.Log("当たったー");
				}
			}
		}
	}

	// キャラクターの衝突判定
	bool IsHit(GameObject obj1 , GameObject obj2, float distance)
	{
		return Mathf.Abs (obj1.transform.position.x - obj2.transform.position.x) < distance;
	}

	public override void Exit() 
	{
		
	}
}