using System;
using UnityEngine;

public sealed class RobBank : State<Outlaw> {

	static readonly RobBank instance = new RobBank();

	public static RobBank Instance {
		get {
			return instance;
		}
	}

	static RobBank () {}
	private RobBank () {}

	public override void Enter (Outlaw agent) {
		Debug.Log("Outlaw is going to rob a bank");
	}

	public override void Execute (Outlaw agent) {
		agent.CreateTime();
		agent.RobBank ();
		Debug.Log("Outlaw robbed " + agent.getGoldQuantity() + " gold nuggets");
		agent.ChangeState(LurkInCamp.Instance);
	}

	public override void Exit (Outlaw agent) {
		Debug.Log("Outlaw is leaving bank after robbing it");
	}

    public override bool OnMesssage(Outlaw agent, Telegram telegram)
    {
        throw new NotImplementedException();
    }

    public override bool OnSenseEvent(Outlaw agent, Sense sense)
    {
        throw new NotImplementedException();
    }
}
