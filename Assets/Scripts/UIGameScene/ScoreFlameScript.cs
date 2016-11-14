using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class ScoreFlameScript : MonoBehaviour {

    public int m_PlayerID;
    public int m_Column;

    public int m_count;
    ScoreManager m_scoreManagerCmp;

	// Use this for initialization
	void Start () {

       m_scoreManagerCmp = GameObject.Find("GameManager").GetComponent<ScoreManager>();

	}

    // Update is called once per frame
    void Update() {


        int checkCount = m_scoreManagerCmp.GetPointLevel(m_PlayerID, m_Column);

        if (checkCount == m_count) return;

        m_count=checkCount;

        if (m_PlayerID == 1)
        {
            switch (m_count)
            {

                case 4:
                    this.GetComponent<Image>().sprite = Resources.Load("Image/ScoreFlame/Scoreflame6", typeof(Sprite)) as Sprite;
                    break;

                case 3:
                    this.GetComponent<Image>().sprite = Resources.Load("Image/ScoreFlame/Scoreflame7", typeof(Sprite)) as Sprite;
                    break;

                case 2:
                    this.GetComponent<Image>().sprite = Resources.Load("Image/ScoreFlame/Scoreflame8", typeof(Sprite)) as Sprite;
                    break;

                case 1:
                    this.GetComponent<Image>().sprite = Resources.Load("Image/ScoreFlame/Scoreflame9", typeof(Sprite)) as Sprite;
                    break;

                case 0:
                    this.GetComponent<Image>().sprite = Resources.Load("Image/ScoreFlame/Scoreflame10", typeof(Sprite)) as Sprite;
                    break;

            }
        }
        else
        {
            switch (m_count)
            {

                case 4:
                    this.GetComponent<Image>().sprite = Resources.Load("Image/ScoreFlame/Scoreflame1", typeof(Sprite)) as Sprite;
                    break;

                case 3:
                    this.GetComponent<Image>().sprite = Resources.Load("Image/ScoreFlame/Scoreflame2", typeof(Sprite)) as Sprite;
                    break;

                case 2:
                    this.GetComponent<Image>().sprite = Resources.Load("Image/ScoreFlame/Scoreflame3", typeof(Sprite)) as Sprite;
                    break;

                case 1:
                    this.GetComponent<Image>().sprite = Resources.Load("Image/ScoreFlame/Scoreflame4", typeof(Sprite)) as Sprite;
                    break;

                case 0:
                    this.GetComponent<Image>().sprite = Resources.Load("Image/ScoreFlame/Scoreflame", typeof(Sprite)) as Sprite;
                    break;

            }
        }
    }
}
