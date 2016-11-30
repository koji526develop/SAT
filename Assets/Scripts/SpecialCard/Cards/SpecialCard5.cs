using UnityEngine;
using System.Collections;

public class SpecialCard5 : SpecialCard
{

	public static string m_howTo = "自分のラインのポイントを全て初期値に\n戻す";
	public static string m_imagePath = "path";

	void Start ()
    {
	
	}
	
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
        for (int i = 1; i <=MyUtility.MAX_COLUMN; i++)
        {
            scoreManager.CountReset(m_UsedPlayerID, i);
        }
        Destroy(this);
	}
}
