using UnityEngine;
using System.Collections;

public class TestState2 : State<Player> 
{
	public TestState2(Player _instance) : base(_instance) {}

	public override void Enter() 
	{
		Debug.Log ("TestState2入ったー");
	}

	public override void Update()
	{
		Debug.Log ("TestState2なう");
	}

	public override void Exit() 
	{
		Debug.Log ("TestState2出たー");
	}
}
