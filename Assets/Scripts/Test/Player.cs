using UnityEngine;
using System.Collections;

public class Player : StatefulObjectBase<Player, Player.CharaState>
{
    public enum CharaState
    {
        None,
        Walk,
        Run
    }

    MoveTo m_moveTo;
    ScaleTo m_scaleTo;
    RotateTo m_rotateTo;

<<<<<<< HEAD
    void Start()
    {
        // ステートマシンの初期設定
        stateList.Add(new TestState(this));
        stateList.Add(new TestState2(this));
=======
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
		// ステートマシンの初期設定
		stateList.Add(new TestState(this));
		stateList.Add(new TestState2(this));
>>>>>>> Okada_dev

        stateMachine = new StateMachine<Player>();

        m_moveTo = this.gameObject.AddComponent<MoveTo>();
        m_scaleTo = this.gameObject.AddComponent<ScaleTo>();
        m_rotateTo = this.gameObject.AddComponent<RotateTo>();

        ChangeState(CharaState.None);
    }

    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            TouchInfo touchInfo = TouchManager.GetTouchInfo(i);
            stateMachine.Update();
            if (touchInfo == TouchInfo.Began)
            {
                if (IsEqualState(CharaState.None))
                {
                    ChangeState(CharaState.Walk);
                    m_moveTo.SetMoveTo(new Vector3(0, 0, 0), 1.0f);
                    m_scaleTo.SetScaleTo(new Vector3(0, 0, 0), 1.0f);
                    m_rotateTo.SetRotateTo(new Vector3(90, 0, 0), 1.0f);
                }
                else
                {
                    ChangeState(CharaState.None);
                    m_moveTo.SetMoveTo(new Vector3(1, 0, 0), 1.0f);
                    m_scaleTo.SetScaleTo(new Vector3(1, 1, 1), 1.0f);
                    m_rotateTo.SetRotateTo(new Vector3(0, 0, 0), 1.0f);
                }
            }
        }
    }
}
