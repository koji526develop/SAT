using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ButtonSpawner : MonoBehaviour
{
    Transform m_battleManager;
	Camera m_battleCamera;
    public int m_PlayerID;
    public int m_ButtonID;
	public GameObject changeImgObj;
    public Character.CharacterType m_type;

    void Awake()
    {
     
            //ボタン押すことで兵種を切り替える処理を行う。  
            this.GetComponent<Button>().onClick.AddListener(SolderChange);

        //スライドで出現処理を行えるようにする処理を行う。

        EventTrigger dragEndtrigger = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.EndDrag;
        entry.callback.AddListener((data) => {Spawner();});
        dragEndtrigger.triggers.Add(entry);
    }

    //兵士出現処理
    public void Spawner()
    {
     
            Character character = Character.CreateObject(m_battleManager, m_type, Character.GetSpawnPosition(m_PlayerID, m_ButtonID), m_PlayerID).GetComponent<Character>();
        character.mapColumn = m_ButtonID;
    }
		
	void OnGUI() {
		Matrix4x4 matrix = GUI.matrix;
		{
			if (m_PlayerID == 1) {
				GUIUtility.RotateAroundPivot (90,
					new Vector2 (
						gameObject.GetComponent<RectTransform> ().localPosition.x + Screen.width / 2,
						-gameObject.GetComponent<RectTransform> ().localPosition.y + Screen.height / 2
					)
				);
				GUI.DrawTexture (new Rect (gameObject.GetComponent<RectTransform> ().localPosition.x + Screen.width / 2, -gameObject.GetComponent<RectTransform> ().localPosition.y + Screen.height / 2 - 64, 46.08f, 64),
					changeImgObj.GetComponent<Image>().mainTexture);
			} else {
				GUIUtility.RotateAroundPivot (-90,
					new Vector2 (
						gameObject.GetComponent<RectTransform> ().localPosition.x + Screen.width / 2,
						-gameObject.GetComponent<RectTransform> ().localPosition.y + Screen.height / 2
					)
				);
				GUI.DrawTexture (new Rect (gameObject.GetComponent<RectTransform> ().localPosition.x + Screen.width / 2 - (92.16f), -gameObject.GetComponent<RectTransform> ().localPosition.y + Screen.height / 2, 46.08f, 64),
					changeImgObj.GetComponent<Image>().mainTexture);
			}
		}
		GUI.matrix = matrix;
	}

    //兵士の選択を変える処理に入る
    public void SolderChange()
    {
        switch (m_type)
        {
            
            //剣から槍へ
		case Character.CharacterType.Sword:

			m_type = Character.CharacterType.Spear;
			changeImgObj.GetComponent<Image> ().sprite = Resources.Load ("Image/spear_I", typeof(Sprite)) as Sprite;
                break;

            //槍から斧へ
            case Character.CharacterType.Spear:

                m_type = Character.CharacterType.Ax;
			changeImgObj.GetComponent<Image>().sprite = Resources.Load("Image/ax_I", typeof(Sprite)) as Sprite;
                break;
            
            //斧から盾へ
            case Character.CharacterType.Ax:

                m_type = Character.CharacterType.Shield;
			changeImgObj.GetComponent<Image>().sprite = Resources.Load("Image/shield_I", typeof(Sprite)) as Sprite;
                break;

            //盾から剣へ
            case Character.CharacterType.Shield:

                m_type = Character.CharacterType.Sword;
			changeImgObj.GetComponent<Image>().sprite = Resources.Load("Image/sword_I", typeof(Sprite)) as Sprite;
                break;
        }
    }

    // Use this for initialization
    void Start()
    {
        //初期設定を行う。
        m_battleManager = GameObject.FindWithTag("BattleManager").transform;
		m_battleCamera = GameObject.FindWithTag ("BattleCamera").GetComponent<Camera> ();
        m_type = Character.CharacterType.Sword;
    }
}
