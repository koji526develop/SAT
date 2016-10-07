using UnityEngine;
using System.Collections;

public class TestState : State<Player> 
{
	public TestState(Player _instance) : base(_instance) {}

	public override void Enter() 
	{
		Debug.Log ("TestState入ったー");
	}

	public override void Update()
	{
		Debug.Log ("TestStateなう");
	}

	public override void Exit() 
	{
		Debug.Log ("TestState出たー");
	}
}
