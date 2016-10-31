using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ButtonSpawner : MonoBehaviour
{

	public bool debug;
	Transform battleManager;

    public int PlayerID;
    public int ButtonID;
	public Character.CharacterType type;

	void Awake ()
	{
       
		if (!debug) {
			this.GetComponent<Button> ().onClick.AddListener (SolderChange);
			EventTrigger trigger = gameObject.GetComponent<EventTrigger> ();

			EventTrigger.Entry entry = new EventTrigger.Entry ();
			entry.eventID = EventTriggerType.EndDrag;
			entry.callback.AddListener ((data) => {
				Spawner ();
			});
			trigger.triggers.Add (entry);
		}
	}

	public void Spawner ()
	{
		for (int i = 1; i < 3; i++) {
			if(transform.tag == "Player"+i.ToString())
			{

	
				GameObject character = Character.CreateObject (battleManager, type, transform.position, i);


                Vector3 SpawnerPos;

                if (i==1)
                {
                    SpawnerPos = new Vector3(-7.0f,0.0f, 6.0f - (2.0f * ButtonID));
                }
                else
                {
                    SpawnerPos = new Vector3(7.0f, 0.0f, 6.0f - (2.0f * ButtonID));
                }
				//GameObject character = Character.CreateObject (battleManager, type, SpawnerPos, i);


			}


		}

	}

	public void SolderChange ()
	{
		if (Character.CharacterType.Sword == type) {
			type = Character.CharacterType.Spear;
			gameObject.GetComponent<Image> ().sprite = Resources.Load ("Image/gauge_blue", typeof(Sprite)) as Sprite;
		} else if (Character.CharacterType.Spear == type) {
			type = Character.CharacterType.Ax;
			gameObject.GetComponent<Image> ().sprite = Resources.Load ("Image/gage-akapng", typeof(Sprite)) as Sprite;

		} else if (Character.CharacterType.Ax == type) {
			type = Character.CharacterType.Sword;
			gameObject.GetComponent<Image> ().sprite = Resources.Load ("Image/TimeWaku", typeof(Sprite)) as Sprite;
		}

	}
	// Use this for initialization
	void Start ()
	{
		type = Character.CharacterType.Sword;
		gameObject.GetComponent<Image> ().sprite = Resources.Load ("Image/TimeWaku", typeof(Sprite)) as Sprite;

	}
	
	// Update is called once per frame
	void Update ()
	{
       
	}
}
