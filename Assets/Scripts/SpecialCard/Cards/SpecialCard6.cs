using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecialCard6 : SpecialCard {

	public static string m_howTo = "フィールドに出ている味方兵士に攻撃を一度だけ防ぐシールドを付ける";
	public static string m_imagePath = "path";

	// Use this for initialization
	void Start () 
	{
		List<Character> allyCharactersList = GetAllyCharactersList ();

		int a = 0;

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
