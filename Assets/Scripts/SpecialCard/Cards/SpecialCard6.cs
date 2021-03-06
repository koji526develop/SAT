﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecialCard6 : SpecialCard {

	public static string m_howTo = "このカードを発動した時にフィールドに出ている\n自分の兵士に兵士からの攻撃を\n一度だけ防ぐシールドをつける。";
	public static string m_imagePath = "path";

	// Use this for initialization
	void Start () 
	{
		List<Character> allyCharactersList = GetAllyCharactersList ();

		foreach (Character allyCharacter in allyCharactersList) 
		{
			allyCharacter.ChangeState (Character.CharacterState.Barrier);
		}

        Destroy(this);

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
