﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class Life : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Text> ().text="点数";
	}
}