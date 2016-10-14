using UnityEngine;
using System.Collections;

public class Player : StatefulObjectBase<Player, Player.CharaState>
{
	Camera m_mainCamera;

    public enum CharaState
    {
        None,
        Walk,
        Run
    }

    MoveTo m_moveTo;
    ScaleTo m_scaleTo;
    RotateTo m_rotateTo;

	public static GameObject CreateObject(Transform _parent)
	{
		GameObject playerObj = GameObject.CreatePrimitive (PrimitiveType.Cube);
		playerObj.AddComponent<Player> ();
		playerObj.name = "Player";
		playerObj.transform.SetParent (_parent);

		return playerObj;
	}

	void Start () 
	{
		m_mainCamera = GameObject.FindWithTag("BattleCamera").GetComponent<Camera>();

		// ステートマシンの初期設定
		stateList.Add(new TestState(this));
		stateList.Add(new TestState2(this));

        stateMachine = new StateMachine<Player>();

        m_moveTo = this.gameObject.AddComponent<MoveTo>();
        m_scaleTo = this.gameObject.AddComponent<ScaleTo>();
        m_rotateTo = this.gameObject.AddComponent<RotateTo>();

        ChangeState(CharaState.None);
    }

    void Update()
    {
		stateMachine.Update();
      
        TouchInfo touchInfo = TouchManager.GetTouchInfo(0);
		if (touchInfo == TouchInfo.Began) 
		{
			GameObject hitObject = TouchManager.GetRaycastHitObject (m_mainCamera,0);
			if (hitObject) 
			{
				Player player = hitObject.GetComponent<Player> ();
				if (player) 
				{
					Debug.Log (hitObject.ToString ());
				}
			}
		}  
    }
}
