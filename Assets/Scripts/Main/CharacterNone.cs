using UnityEngine;
using System.Collections;

public class CharacterNone : State<Character> 
{
	public CharacterNone(Character _instance) : base(_instance) {}
	//接触した味方の情報
	GameObject m_suppoteObj;
	public GameObject suppoteObj
	{
		set
		{ 
			m_suppoteObj = value;
		}
	}

	public override void Enter() 
	{
		Debug.Log ("入った");
	}
	public override void Update()
	{
		if (!NonHit (m_instance.gameObject, m_suppoteObj)) 
		{
			m_instance.ChangeState (Character.CharacterState.Move); 

		}
	}
	public override void Exit() 
	{
		m_suppoteObj = null;
		Debug.Log ("出た");
	}
	bool NonHit(GameObject obj1,GameObject obj2)
	{
		// 衝突判定を行う距離
		float distance = obj1.GetComponent<Character> ().status.attackDistance;

		Character character1 = obj1.GetComponent<Character>();
		Character character2 = obj2.GetComponent<Character>();

		if (!obj2.CompareTag ("Character")) return true;

		// 攻撃範囲に入ってなかったら抜ける
		if (!(Mathf.Abs (obj1.transform.position.x - obj2.transform.position.x) < distance)) return true;


		// 行が違ったら抜ける
		if (character1.mapColumn != character2.mapColumn) return true;

        return false;
	}
}
