using UnityEngine;
using System.Collections;

public class SpecialCard8 : SpecialCard {

	public static string m_howTo = "一定時間自分の獲得出来るポイント数が増える";
	public static string m_imagePath = "path";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ScoreManager scoreManager = gameManager.GetComponent<ScoreManager>();

        scoreManager.pointBouns(m_UsedPlayerID,2.0f ,10.0f);
        Destroy(this);
    }
}
