using System;
using UnityEngine;

public sealed class CollectGold : State<Miner> {

	static readonly CollectGold instance = new CollectGold();

	public static CollectGold Instance {
		get {
			return instance;
		}
	}

	static CollectGold () {}
	private CollectGold () {}

	public override void Enter (Miner agent) {
		Debug.Log("Gathering creative energies...");
	}

	public override void Execute (Miner agent) {
		//agent.CreateTime();
		//Debug.Log("...creating more time, for a total of " + agent.createdTime + " unit" + (agent.createdTime > 1 ? "s" : "") + "...");
		agent.StateMachine.ChangeState(GoHomeAndSleepTillRested.Instance);
	}

	public override void Exit (Miner agent) {
		Debug.Log("...creativity spent!");
	}

	public override bool OnMesssage(Miner agent, Telegram telegram)
	{
		throw new NotImplementedException();
	}

	public override bool OnSenseEvent(Miner agent, Sense sense)
	{
		throw new NotImplementedException();
	}
}
