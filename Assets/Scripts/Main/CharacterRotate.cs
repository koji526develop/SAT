using UnityEngine;
using System.Collections;

public class CharacterRotate : State<Character> 
{
	public CharacterRotate(Character _instance) : base(_instance) {}

	float m_rotateTime;

	public override void Enter() 
	{
		m_rotateTime = 0.0f;

		// iTweenを初期化する(これないとバグる)
		iTween.Stop(m_instance.gameObject);

		// 回転させる
		if(m_instance.rotateDirection == Character.Direction.Down)
			iTween.RotateBy (m_instance.gameObject, iTween.Hash(
				"y", 1*GetCharacterRote() / 4.0f,
				"time", 1f,
				"delay", 0.01f
			));
		// 回転させる
		else
			iTween.RotateBy (m_instance.gameObject, iTween.Hash(
				"y", -1*GetCharacterRote() / 4.0f,
				"time", 1f,
				"delay", 0.01f
			));

	}

	public override void Update()
	{
		m_rotateTime += Time.deltaTime;

		if (m_rotateTime > 0.5f) 
		{
			// 回転が終わったら行を変更しその行に移動させる
			if(m_instance.rotateDirection == Character.Direction.Down)
				m_instance.ChangeColumn (Character.Direction.Down);
			else
				m_instance.ChangeColumn (Character.Direction.Up);

			// 回転を元に戻すステートへ
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