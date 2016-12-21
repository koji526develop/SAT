using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecialCard7 : SpecialCard {

	public static string m_howTo = "フィールド上の自分の兵士の移動速度を\n5秒の間1.5倍にする。";
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

		List<Character> allyCharactersList = GetAllyCharactersList ();

		foreach (Character allyCharacter in allyCharactersList) 
		{
			GameObject barrierObj = GameObject.Instantiate(Resources.Load("SpecialWeapon/SpecailShield/shield_blue")) as GameObject;
			barrierObj.name = "SpeedUp";

			barrierObj.transform.SetParent (allyCharacter.transform);

			Vector3 tmp = allyCharacter.transform.position;
			tmp.y = 1.05f;
			barrierObj.transform.position = tmp;
            Destroy(barrierObj,5.0f);
        }

		Destroy(this);
		return;
	}

	List<Character> GetAllyCharactersList()
	{	
		List<Character> allyCharactersList = new List<Character> ();

		foreach(Transform child in battleManager)
		{
			if (child.tag == "Character")
			{
				Character character = child.gameObject.GetComponent<Character> ();

				if (character.status.PlayerID == m_UsedPlayerID)
				{
					allyCharactersList.Add (character);
				}
			}
		}
		return allyCharactersList;
	}

}
	
