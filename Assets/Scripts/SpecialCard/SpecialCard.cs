using UnityEngine;
using System.Collections;

public class SpecialCard : MonoBehaviour {

    public string m_howTo;
    public int m_UsedPlayerID;

    public Camera m_mainCamera;
    public Camera mainCamera
    {
        get
        {
            return m_mainCamera;
        }
    }

    public enum SpCardTag
    {
        SoliderChange,
        Archer,
        MobSpawner

    }

    // Use this for initialization
    void Start () {
        //カメラのコンポーネント取得
        m_mainCamera = GameObject.FindWithTag("BattleCamera").GetComponent<Camera>();
    }
	
	// Update is called once per frame
	public virtual void Update () {}
}
