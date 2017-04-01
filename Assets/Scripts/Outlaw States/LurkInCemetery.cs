using System;
using UnityEngine;

public sealed class LurkInCemetery : State<Outlaw> {

	static readonly LurkInCemetery instance = new LurkInCemetery();

	public static LurkInCemetery Instance {
		get {
			return instance;
		}
	}

	static LurkInCemetery () {}
	private LurkInCemetery () {}

	public override void Enter (Outlaw agent) {
		Debug.Log("Outlaw is going to lurk in cemetery");
	}

	public override void Execute (Outlaw agent) {
		agent.CreateTime();
		Debug.Log("Outlaw lurking in cemetery");
		agent.ChangeState(RobBank.Instance);
	}

	public override void Exit (Outlaw agent) {
		Debug.Log("Outlaw stopped lurking in cemetery");
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
