using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecialCard8 : SpecialCard {

	public static string m_howTo = "10秒間自分の獲得時のポイントが＋10する。";
	public static string m_imagePath = "path";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        ScoreManager scoreManager;
        try
        {
            scoreManager = gameManager.GetComponent<ScoreManager>();
        }
        catch
        {

            gameManager = GameObject.Find("GameManager").transform;
            scoreManager = gameManager.GetComponent<ScoreManager>();
        }


		List<Character> allyCharactersList = GetAllyCharactersList ();

		foreach (Character allyCharacter in allyCharactersList) 
		{
			GameObject barrierObj = GameObject.Instantiate(Resources.Load("SpecialWeapon/SpecailShield/shield_pink")) as GameObject;
			barrierObj.name = "Barrier";

			barrierObj.transform.SetParent (allyCharacter.transform);

			Vector3 tmp = allyCharacter.transform.position;
			tmp.y = 1.05f;
			barrierObj.transform.position = tmp;
		}
			
        scoreManager.pointBouns(m_UsedPlayerID,10.0f ,10.0f);
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
