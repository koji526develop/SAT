using UnityEngine;
using System.Collections;

public class SpecialCard5 : SpecialCard {

	public static string m_howTo = "自分のラインのポイントを全て初期値に戻す";
	public static string m_imagePath = "path";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        ScoreManager scoreManager = gameManager.GetComponent<ScoreManager>();

        for (int i = 1; i <=MyUtility.MAX_COLUMN; i++)
        {
            scoreManager.CountReset(m_UsedPlayerID, i);
        }
        Destroy(this);
	}
}
