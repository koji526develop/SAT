using UnityEngine;
using System.Collections;

public class TimeDestory : MonoBehaviour {

    public float m_time = 10.0f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        m_time -= Time.deltaTime;
        if (m_time < 0.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
