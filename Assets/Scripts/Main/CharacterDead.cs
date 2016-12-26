using UnityEngine;
using System.Collections;

public class CharacterDead : State<Character> 
{
	public CharacterDead(Character _instance) : base(_instance) {}


	public override void Enter()
	{
		//m_instance.animator.Play ("Attack");
		AudioManager.m_instance.PlaySE ("Vanish_SE");
		m_instance.Destroy ();

       	 if (this.m_instance.status.PlayerID == 1) {
            GameObject.Find("GameManager").GetComponent<ScoreManager>().DirectGetPoint(2, 5, this.m_instance.transform.position);
        }
        else
        {
            GameObject.Find("GameManager").GetComponent<ScoreManager>().DirectGetPoint(1, 5, this.m_instance.transform.position);

        }

    }

	public override void Update()
	{

	}

	public override void Exit() 
	{
		
	}
}
