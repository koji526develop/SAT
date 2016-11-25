using UnityEngine;
using System.Collections;

public class SpeedUp : SpecialCard {
	private GameObject m_obj;
	float AxSpeedUp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public override void Update()
	{
		foreach (Transform childObj in battleManager)
				{
					//キャラクタータグであるならもしくは、同じ列であるなら
					if (childObj.tag == "Character")
					{
				GameObject characterObj = childObj.gameObject;
				Character charaCmp = characterObj.GetComponent<Character> ();
				Character.Status changeStatus = charaCmp.status; 
				if (charaCmp.status.PlayerID == m_UsedPlayerID)
						{

					changeStatus.moveSpeed = charaCmp.status.moveSpeed * 1.5f;
					charaCmp.status = changeStatus;
						}
					}
				}
				Destroy(this);
				return;
			}

}
