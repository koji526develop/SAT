using UnityEngine;
using System.Collections;

public class CharacterBackRotate : State<Character> 
{
	public CharacterBackRotate(Character _instance) : base(_instance) {}

	bool m_isRotate;
	float m_rotateTime;

	public override void Enter() 
	{
		m_isRotate = false;
		m_rotateTime = 0.0f;
	}

	public override void Update()
	{
		if (!m_instance.moveTo.isMove) 
		{
			if(m_instance.rotateDirection == Character.Direction.Right)
				iTween.RotateBy (m_instance.gameObject, new Vector3 (0, (-1*GetCharacterRote()) / 4.0f, 0), 1.0f);
			else
				iTween.RotateBy (m_instance.gameObject, new Vector3 (0, (1*GetCharacterRote()) / 4.0f, 0), 1.0f);
			m_isRotate = true;
		}
			
		if (m_isRotate) 
		{
			m_rotateTime += Time.deltaTime;
			if (m_rotateTime > 0.5f) 
			{
				m_instance.ChangeState(Character.CharacterState.Move);
			}
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
