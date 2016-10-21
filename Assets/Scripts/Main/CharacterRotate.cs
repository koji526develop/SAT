using UnityEngine;
using System.Collections;

public class CharacterRotate : State<Character> 
{
	public CharacterRotate(Character _instance) : base(_instance) {}

	public override void Enter() 
	{
		Debug.Log ("TestState入ったー");
		if (CharacterMove.m_Rotate==1) {
			m_instance.GetComponent<RotateTo> ().SetRotateTo (new Vector3 (CharacterMove.m_CharacterRotate, 0, 0), 1.0f);
		}
		else if (CharacterMove.m_Rotate==2) {
			m_instance.GetComponent<RotateTo> ().SetRotateTo (new Vector3 (CharacterMove.m_CharacterRotate, 0, 0), 1.0f);
		}
	}

	public override void Update()
	{
		RotateTo rotateto = new RotateTo ();
		MoveTo moveto = new MoveTo ();
		if (rotateto.isRotate==false) 
		{
			m_instance.GetComponent<MoveTo> ().SetMoveTo (new Vector3 (m_instance.transform.position.x, CharacterMove.m_CharacterPosition, m_instance.transform.position.z), 1.0f);
		}
		if(moveto.isMove==false)
		{
			m_instance.ChangeState (Character.CharacterState.Mov	e);
		}


		Debug.Log ("TestStateなう");

	}

	public override void Exit()
	{
		
		Debug.Log ("TestState出たー");
	}
}