using UnityEngine;
using System.Collections;

public class NotSpawner : MonoBehaviour {

    public float m_time;
    ButtonSpawner m_buttonSawner;

	// Use this for initialization
	void Start () {

        m_buttonSawner = this.GetComponent<ButtonSpawner>();
	}
	
	// Update is called once per frame
	void Update () {

        m_time -= Time.deltaTime;

	}
}
