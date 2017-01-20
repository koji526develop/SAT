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

        m_count = 4;
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

                case 0:
				this.GetComponent<Image>().sprite = Resources.Load("UI/Game/score_troop1", typeof(Sprite)) as Sprite;
                    break;

                case 1:
				this.GetComponent<Image>().sprite = Resources.Load("UI/Game/score_troop2", typeof(Sprite)) as Sprite;
                    break;

                case 2:
				this.GetComponent<Image>().sprite = Resources.Load("UI/Game/score_troop3", typeof(Sprite)) as Sprite;
                    break;

                case 3:
				this.GetComponent<Image>().sprite = Resources.Load("UI/Game/score_troop4", typeof(Sprite)) as Sprite;
                    break;

                case 4:
				this.GetComponent<Image>().sprite = Resources.Load("UI/Game/score_troop5", typeof(Sprite)) as Sprite;
                    break;

            }
        }
        else
        {
            switch (m_count)
            {

                case 0:
				this.GetComponent<Image>().sprite = Resources.Load("UI/Game/score_troop1 2", typeof(Sprite)) as Sprite;
                    break;

                case 1:
				this.GetComponent<Image>().sprite = Resources.Load("UI/Game/score_troop2 2", typeof(Sprite)) as Sprite;
                    break;

                case 2:
				this.GetComponent<Image>().sprite = Resources.Load("UI/Game/score_troop3 2", typeof(Sprite)) as Sprite;
                    break;

                case 3:
				this.GetComponent<Image>().sprite = Resources.Load("UI/Game/score_troop4 2", typeof(Sprite)) as Sprite;
                    break;

                case 4:
				this.GetComponent<Image>().sprite = Resources.Load("UI/Game/score_troop5 2", typeof(Sprite)) as Sprite;
                    break;

            }
        }
    }
}
