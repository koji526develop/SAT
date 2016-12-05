using UnityEngine;
using System.Collections;

public class NotSpawner : MonoBehaviour {

    public float m_time=10.0f;
    ButtonSpawner m_buttonSawner;

    void OnDestroy()
    {
        m_buttonSawner.m_soliderStop = false;
    }
	// Use this for initialization
	void Start () {

        m_buttonSawner = this.GetComponent<ButtonSpawner>();
        m_buttonSawner.m_soliderStop = true;
	}
	
	// Update is called once per frame
	void Update () {

        m_time -= Time.deltaTime;

        if (m_time < 0.0f)
        {
            Destroy(this);
        }
	}
}
