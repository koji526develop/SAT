using UnityEngine;
using System.Collections;

public class SpecialCard7 : SpecialCard {

	public static string m_howTo = "フィールドに出ている味方兵士の移動\n速度を1.5倍する";
	public static string m_imagePath = "path";

	// Use this for initialization
	void Start () {

        if (battleManager == null)
        {
            gameManager = GameObject.Find("GameManager").transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
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
