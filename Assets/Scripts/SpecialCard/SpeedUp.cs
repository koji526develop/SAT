using UnityEngine;
using System.Collections;

public class SpeedUp : SpecialCard {
	private GameObject m_obj;
	float AxSpeedUp;
	// Use this for initialization
	void Start () {
		AxSpeedUp=MyUtility.AX_MOVESPEED*1.5f;	
	}
	
	// Update is called once per frame
	public override void Update()
	{
		foreach (Transform childObj in battleManager)
				{
					//キャラクタータグであるならもしくは、同じ列であるなら
					if (childObj.tag == "Character")
					{
				GameObject obj = childObj.gameObject;
				Character charaCmp = obj.GetComponent<Character> ();
				Character.Status m_changeStatus = charaCmp.status; 
				if (charaCmp.status.PlayerID == m_UsedPlayerID)
						{

					m_changeStatus.moveSpeed = charaCmp.status.moveSpeed * 1.5f;
					charaCmp.status = m_changeStatus;
						}
					}
				}
				Destroy(this);
				return;
			}

}
