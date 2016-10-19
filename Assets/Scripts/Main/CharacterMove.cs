using UnityEngine;
using System.Collections;

public class CharacterMove :  State<Character> {
	Camera m_mainCamera;
	float RoteteTime=3;
	public CharacterMove(Character _instance) : base(_instance) {}

	public override void Enter() 
	{
		Debug.Log ("TestState入ったー");
	}

	public override void Update()
	{
		RoteteTime += Time.deltaTime;
		m_mainCamera = GameObject.FindWithTag("BattleCamera").GetComponent<Camera>();
		 GameObject[] gameobject = GameObject.FindGameObjectsWithTag("Character");

		//this.m_instance.transform.position += new Vector3 (m_instance.status.moveSpeed,0,0);
		this.m_instance.transform.position += new Vector3 (0.005f,0,0);

		if(TouchManager.GetRaycastHitObject (m_mainCamera,1))
		{	 
			if(TouchInfo.Moved>0)
			{
				
					this.m_instance.transform.rotation = Quaternion.Euler (0, 90, 0);
				

				//this.m_instance.transform.position += new Vector3 (0, 1, 0);
				//m_instance.ChangeState (Character.CharacterState.None);
			}
//			else if(TouchInfo.Moved>0)
//			{
//				this.m_instance.transform.position -= new Vector3 (0, 1, 0);
//				m_instance.ChangeState (Character.CharacterState.None);
//			}
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