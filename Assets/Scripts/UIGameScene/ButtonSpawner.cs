using UnityEngine;
using System.Collections;

public class ButtonSpawner : MonoBehaviour {

    Transform battleManager;
    void Awake()
    {
        // BattleManager作成
        battleManager = new GameObject("BattleManager").transform;
    }
    public void Spawner()
    {
        GameObject character = Character.CreateObject(battleManager, Character.CharacterType.Sword);

    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
