using UnityEngine;
using System.Collections;

public class CharacterSideMoveDown : State<Character> 
{
	public CharacterSideMoveDown(Character _instance) : base(_instance) {}

	public override void Enter() 
	{
		m_instance.GetComponent<Character>().mapColumn += 1;
		m_instance.animator.Play ("Move");
	}

	public override void Update()
	{
		Move ();
		if (m_instance.transform.position.z < 6.0f - (2.0f * m_instance.mapColumn)) 
		{
			m_instance.transform.position = new Vector3 (m_instance.transform.position.x, m_instance.transform.position.y, 6.0f - (2.0f * m_instance.mapColumn));
			// 回転を元に戻すステートへ
			m_instance.ChangeState(Character.CharacterState.BackRotate);
		}
		if (m_instance.animator.GetCurrentAnimatorStateInfo (0).normalizedTime >= 1.0f)
			m_instance.animator.SetTime (0.0f);
	}

	public override void Exit()
	{

	}

	// キャラクターの移動
	void Move()
	{
		m_instance.transform.position -= new Vector3 (0, 0, m_instance.status.moveSpeed);
	}
}
