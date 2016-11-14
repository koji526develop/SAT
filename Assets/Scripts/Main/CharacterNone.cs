using UnityEngine;
using System.Collections;

public class CharacterNone : State<Character> 
{
	public CharacterNone(Character _instance) : base(_instance) {}
	GameObject m_obj;

	public GameObject obj
	{
		set 
		{
			m_obj = value;
		}
		get 
		{
			return obj;
		}
	}

	public override void Enter() 
	{
		Debug.Log ("入った");
	}
	public override void Update()
	{
		//if(IsHit(m_instance.gameObject, obj)) m_instance.ChangeState (Character.CharacterState.Move);
	}
	public override void Exit() 
	{
		obj = null;
	}
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
		// PlayerIDが同じだったら抜ける


		// 行が違ったら抜ける
		if (character1.mapColumn != character2.mapColumn) return false;

		return true;
	}
}
