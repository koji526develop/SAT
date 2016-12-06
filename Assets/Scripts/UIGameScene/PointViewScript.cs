using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PointViewScript : MonoBehaviour {

    public Sprite[] m_number;

    public void number(int _number,int _playerID)
    {
    //    GameObject parentobj;
    //    parentobj = new GameObject();

    //    //this.transform.parent=

    //    GameObject obj;
    //    obj =MyUtility.CreateSprite(
    //        this.transform,
    //        "TestNumber",
    //        m_number[_number]
    //        );

    //    obj.transform.localScale = new Vector3(2, 2, 2);
    //    obj.transform.eulerAngles = new Vector3(90, 0, 0);

    //    obj.transform.position = new Vector3(2, 0, 0);
    }

    void Awake()
    {
        m_number = Resources.LoadAll<Sprite>("UI/number");
    }
	// Use this for initialization
	void Start () {
        number(1,1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
