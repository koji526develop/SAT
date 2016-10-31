using UnityEngine;
using System.Collections;

public class CharacterRotate : State<Character> 
{
	public CharacterRotate(Character _instance) : base(_instance) {}

	float m_rotateTime;

	bool flag = false;

	public override void Enter() 
	{
		m_rotateTime = 0.0f;
		flag = false;
	}

	public override void Update()
	{
		m_rotateTime += Time.deltaTime;

		if(m_rotateTime > 0.5f && !flag)
		{
			flag = true;
			m_instance.ChangeColumn (Character.Direction.Down);
		}

		if(!m_instance.moveTo.isMove && flag)
			//iTween.RotateBy (m_instance.gameObject, new Vector3 (0, (1*GetCharacterRote()) / 4.0f, 0), 1.0f);
			m_instance.ChangeState (Character.CharacterState.Move);
	}

	public override void Exit()
	{

	}
}