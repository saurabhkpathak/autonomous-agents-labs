using UnityEngine;

public sealed class ElsaWaitState : State<Elsa> {

	static readonly ElsaWaitState instance = new ElsaWaitState();

	public static ElsaWaitState Instance {
		get {
			return instance;
		}
	}

	static ElsaWaitState () {}
	private ElsaWaitState () {}

	public override void Enter (Elsa agent) {
		Debug.Log("Elsa Waiting");
	}

	public override void Execute (Elsa agent) {
		agent.IncreaseWaitedTime(1);
		Debug.Log("...Elsa waiting for " + agent.waitedTime + " cycle" + (agent.waitedTime > 1 ? "s" : "") + " so far...");
		if (agent.WaitedLongEnough()) agent.ChangeState(ElsaCreateState.Instance);
	}

	public override void Exit (Elsa agent) {
		Debug.Log("...Elsa waited long enough!");
	}
}
