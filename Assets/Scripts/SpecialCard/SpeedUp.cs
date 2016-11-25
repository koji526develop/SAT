using UnityEngine;
using System.Collections;

public class SpeedUp : SpecialCard {
	private GameObject m_obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public override void Update()
	{
		float AxSpeedUp=MyUtility.AX_MOVESPEED;
	
				foreach (Transform childObj in battleManager)
				{
					//キャラクタータグであるならもしくは、同じ列であるなら
					if (childObj.tag == "Character")
					{
						if (childObj.GetComponent<Character>().status.PlayerID == m_UsedPlayerID)
						{
					AxSpeedUp=AxSpeedUp*1.5f;							
						}
					}
				}
				Destroy(this);
				return;
			}

}
