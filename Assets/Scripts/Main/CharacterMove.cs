using UnityEngine;
using System.Collections;

public class CharacterMove :  State<Character> {

	public CharacterMove(Character _instance) : base(_instance) {}

	public override void Enter() 
	{
		Debug.Log ("TestState入ったー");
	}

	public override void Update()
	{
		GameObject[] gameobject = GameObject.FindGameObjectsWithTag("Character");

		this.m_instance.transform.position += new Vector3 (0.01f,0,0);
		if (Input.GetMouseButtonDown (0))
		{
			m_instance.ChangeState (Character.CharacterState.None);
			this.m_instance.transform.position += new Vector3 (0, 1, 0);
		}

		for(int i=0;i<gameobject.Length;i++)
		{
			if (m_instance.gameObject == gameobject [i])
				continue;

			if(gameobject[i].CompareTag("Character") && IsHit(m_instance.gameObject, gameobject[i], 3.0f)) 
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