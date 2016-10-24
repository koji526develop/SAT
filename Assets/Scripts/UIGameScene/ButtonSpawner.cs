using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ButtonSpawner : MonoBehaviour {

    public bool debug;
    Transform battleManager;

    public Character.CharacterType type;
    void Awake()
    {
       
        if (!debug) {
            this.GetComponent<Button>().onClick.AddListener(SolderChange);
            EventTrigger trigger = gameObject.GetComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.EndDrag;
            entry.callback.AddListener((data) => { Spawner(); });
            trigger.triggers.Add(entry);
        }
    }
    public void Spawner()
    {
        GameObject character = Character.CreateObject(battleManager, type);

    }
    public void SolderChange()
    {
        if (Character.CharacterType.Sword==type)
        {
            type = Character.CharacterType.Spear;
            gameObject.GetComponent<Image>().sprite = Resources.Load("Image/gauge_blue", typeof(Sprite)) as Sprite;
        }
        else if (Character.CharacterType.Spear == type)
        {
            type = Character.CharacterType.Ax;
            gameObject.GetComponent<Image>().sprite = Resources.Load("Image/gage-akapng", typeof(Sprite)) as Sprite;

        }
        else if (Character.CharacterType.Ax == type)
        {
            type = Character.CharacterType.Sword;
            gameObject.GetComponent<Image>().sprite = Resources.Load("Image/TimeWaku", typeof(Sprite)) as Sprite;
        }

    }
    // Use this for initialization
    void Start () {
        type = Character.CharacterType.Sword;
        gameObject.GetComponent<Image>().sprite = Resources.Load("Image/TimeWaku", typeof(Sprite)) as Sprite;

    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
