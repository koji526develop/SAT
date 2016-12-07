using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PointViewScript : MonoBehaviour {

    public Sprite[] m_number;

    //数字スプライトの表示
    public void number(int _number,int _playerID,Vector3 _vector,float _timeDestroy)
    {


        //親オブジェクトを作成
        GameObject parentobj;
        parentobj = new GameObject();
        Destroy(parentobj, _timeDestroy);

        GameObject numberObj;

        int i = 0;
        while (0 < _number)
        {
            int outputNumber;
            outputNumber = _number % 10;

            numberObj = MyUtility.CreateSprite(
                parentobj.transform,
                "number",
                m_number[outputNumber]
                );

            numberObj.transform.localScale = new Vector3(2, 2, 2);
            numberObj.transform.eulerAngles = new Vector3(90, 0, 0);
            numberObj.transform.position = new Vector3(0.3f - (0.6f * i), 0, 0);

            _number = _number / 10;
            i++;
        }
        parentobj.transform.parent = this.transform;
        parentobj.transform.position = new Vector3(2, 0, 0);
        if (_playerID == 1)
        {
            parentobj.transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            parentobj.transform.eulerAngles = new Vector3(0, -90, 0);
        }
    }

    void Awake()
    {
        m_number = Resources.LoadAll<Sprite>("UI/number");
    }
	// Use this for initialization
	void Start () {
        number(26,1,new Vector3(2f,0f,0f),2f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
