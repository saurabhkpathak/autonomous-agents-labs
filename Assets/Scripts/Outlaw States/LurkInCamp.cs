using System;
using UnityEngine;

public sealed class LurkInCamp : State<Outlaw> {

	static readonly LurkInCamp instance = new LurkInCamp();

	public static LurkInCamp Instance {
		get {
			return instance;
		}
	}

	static LurkInCamp () {}
	private LurkInCamp () {}

	public override void Enter (Outlaw agent) {
		Debug.Log("Outlaw is going to lurk in camp");
	}

	public override void Execute (Outlaw agent) {
		agent.CreateTime();
		Debug.Log("Outlaw lurking in camp");
		agent.ChangeState(LurkInCemetery.Instance);
	}

	public override void Exit (Outlaw agent) {
		Debug.Log("Outlaw stopped lurking in camp");
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
