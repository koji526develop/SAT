using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour 
{
	public static int j=0;
	// ゲーム開始時
	void Awake()
	{
		// Light作成
		MyUtility.CreateDirectionalLight ();

		// UIManager作成
		Transform UIManager = new GameObject ("UIManager").transform;

		// UICanvas作成
		MyUtility.CreateUICanvas (UIManager);

		// UIカメラ作成
		MyUtility.CreateCamera ("UICamera", UIManager);

		// イベントシステム作成
		MyUtility.CreateEventSystem (UIManager);

		// BattleManager作成
		Transform battleManager = new GameObject ("BattleManager").transform;

		// バトル用のカメラ作成
		MyUtility.CreateCamera ("BattleCamera", battleManager);

		// プレイヤーオブジェクト作成
		for (int i = 0; i < 5; i++) 
		{
			j=j+2;

			GameObject character = Character.CreateObject (battleManager, Character.CharacterType.Sword);
		
		}
	}
}
