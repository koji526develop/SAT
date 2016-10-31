using UnityEngine;
using System.Collections;

public class CharacterRotate : State<Character> 
{
	public CharacterRotate(Character _instance) : base(_instance) {}

	float m_rotateTime;

	public override void Enter() 
	{
		m_rotateTime = 0.0f;

		if(m_instance.rotateDirection == Character.Direction.Right)
			iTween.RotateBy (m_instance.gameObject, new Vector3 (0, (1*GetCharacterRote()) / 4.0f, 0), 1.0f);
		else
			iTween.RotateBy (m_instance.gameObject, new Vector3 (0, (-1*GetCharacterRote()) / 4.0f, 0), 1.0f);

	}

	public override void Update()
	{
		m_rotateTime += Time.deltaTime;

		if (m_rotateTime > 0.5f) 
		{
			if(m_instance.rotateDirection == Character.Direction.Right)
				m_instance.ChangeColumn (Character.Direction.Down);
			else
				m_instance.ChangeColumn (Character.Direction.Up);
			m_instance.ChangeState(Character.CharacterState.BackRotate);
		}
	}

	public override void Exit()
	{

	}

	// キャラクターの回転方向取得(1Pと2Pで回転させる方向を変えるため)
	int GetCharacterRote()
	{
		if (m_instance.status.PlayerID == 1) return 1;
		else return -1;
	}
}