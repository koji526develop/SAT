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

        ChangeState(CharaState.None);

		Shake shake = gameObject.AddComponent<Shake> ();
		shake.ShakeObject ();
		FadeTo fade = gameObject.AddComponent<FadeTo> ();
		fade.SetFadeTo (FadeTo.RendererType.Mesh,0.0f,1.0f);
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
