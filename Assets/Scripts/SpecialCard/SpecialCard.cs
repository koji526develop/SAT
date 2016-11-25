using UnityEngine;
using System.Collections;

public class SpecialCard : MonoBehaviour {

    public int m_UsedPlayerID;

    //消去予定
    public Camera m_mainCamera;
    public Camera mainCamera
    {
        get
        {
            return m_mainCamera;
        }
    }

    Transform m_battleManager;
    public Transform battleManager
    {
        get
        {
            return m_battleManager;
        }
        set
        {
            m_battleManager = value;
        }
    }

    public bool m_IsGameNow = false;

    public enum SpCardTag
    {
        SoliderChange,
        Archer,
        MobSpawner
    }

    public void GameReady(Transform _battleManager)
    {
        
        m_battleManager = _battleManager;
        m_IsGameNow = true;

    }

    // Use this for initialization
    void Start () {
        //カメラのコンポーネント取得
        m_mainCamera = GameObject.FindWithTag("BattleCamera").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	public virtual void Update () {}
}
