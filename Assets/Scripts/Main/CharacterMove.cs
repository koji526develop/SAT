using UnityEngine;
using System.Collections;

public class CharacterMove :  State<Character> {
	Camera m_mainCamera;
	float RoteteTime=3;
	Vector3 move;

	bool m_CharacterTouch;
	public static int m_Rotate;

	public static float m_CharacterPosition;
	public static float m_CharacterRotate;

	GameObject m_CharacterObj = null;


	public CharacterMove(Character _instance) : base(_instance) {}

	public override void Enter() 
	{
		Debug.Log ("TestState入ったー");
		m_CharacterTouch = false;
		 m_Rotate = 0;

	}

	public override void Update()
	{
		RoteteTime += Time.deltaTime;
		m_mainCamera = GameObject.FindWithTag("BattleCamera").GetComponent<Camera>();
		GameObject[] gameobject = GameObject.FindGameObjectsWithTag("Character");

		//m_instance.transform.position += new Vector3 (m_instance.status.moveSpeed,0,0);

		m_instance.transform.position += new Vector3 (0.005f,0,0);


		TouchInfo touch = TouchManager.GetTouchInfo (0);
		if(touch == TouchInfo.Moved)
		{
			if(TouchManager.GetTouchMoveDistanceY (0)>50.0f)
			{
				if(m_CharacterTouch)
				{
					//m_instance.transform.position += new Vector3 (0, 1, 0);
					//m_CharacterObj.transform.position += new Vector3 (0, 1, 0);
					m_Rotate=1;
					m_CharacterPosition = m_instance.transform.position.y+1.0f;
					m_CharacterRotate = m_instance.transform.rotation.y+90.0f;
					m_instance.ChangeState (Character.CharacterState.Rotate);
					Debug.Log("上");
					m_CharacterTouch = false;
					m_CharacterObj = null;
				}
			}
			else if(TouchManager.GetTouchMoveDistanceY (0) < -50.0f)
			{
				if(m_CharacterTouch)
				{
					//m_instance.transform.position += new Vector3 (0, 1, 0);
					//m_CharacterObj.transform.position -= new Vector3 (0, 1, 0);
					m_Rotate=2;
					m_CharacterPosition = m_instance.transform.position.y-1.0f;
					m_CharacterRotate = m_instance.transform.rotation.y-90.0f;
					m_instance.ChangeState (Character.CharacterState.Rotate);
					Debug.Log("下");
					m_CharacterTouch = false;
					m_CharacterObj = null;
				}
			}
		}
		 


		if(touch == TouchInfo.Began)
		{
			m_CharacterObj = TouchManager.GetRaycastHitObject (m_mainCamera, 0);
			if(m_CharacterObj != null)
			{
				m_CharacterTouch = true;
			}
				
			//m_instance.ChangeState (Character.CharacterState.None);
		}
		for(int i=0;i<gameobject.Length;i++)
		{
			if (m_instance.gameObject == gameobject [i])
				continue;

			if(gameobject[i].CompareTag("Character") && IsHit(m_instance.gameObject, gameobject[i],m_instance.status.attackDistance)) 
			{
			m_instance.ChangeState (Character.CharacterState.Attack);
				Debug.Log("当たったー");
			}
		}
	}

	bool IsHit(GameObject obj1 , GameObject obj2, float distance)
	{
		return Mathf.Abs (obj1.transform.position.x - obj2.transform.position.x) < distance;
	}

	public override void Exit() 
	{
		Debug.Log ("TestState出たー");
	}
}