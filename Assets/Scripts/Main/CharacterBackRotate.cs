using UnityEngine;
using System.Collections;

public class CharacterBackRotate : State<Character> 
{
	public CharacterBackRotate(Character _instance) : base(_instance) {}

	float m_rotateTime;

	public override void Enter() 
	{
		m_rotateTime = 0.0f;

		// 回転
		if(m_instance.rotateDirection == Character.Direction.Down)
			iTween.RotateBy (m_instance.gameObject, new Vector3 (0, (-1*GetCharacterRote()) / 4.0f, 0), 1.0f);
		// 回転
		else if(m_instance.rotateDirection == Character.Direction.Up)
			iTween.RotateBy (m_instance.gameObject, new Vector3 (0, (1*GetCharacterRote()) / 4.0f, 0), 1.0f);

		// 回転方向の指定をなくす
		m_instance.rotateDirection = Character.Direction.None;

	}

	public override void Update()
	{
		m_rotateTime += Time.deltaTime;
		if (m_rotateTime > 0.5f) 
		{
			m_instance.ChangeState(Character.CharacterState.Move);
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
