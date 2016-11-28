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

    Transform m_gameManager;
    public Transform gameManager
    {
        get
        {
            return m_gameManager;
        }
        set
        {
            m_gameManager = value;
        }
    }

    public bool m_IsGameNow = false;


    public void GameReady(Transform _battleManager,Transform _gameManager)
    {
        
        m_battleManager = _battleManager;
        m_gameManager = _gameManager;

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
