using UnityEngine;

public sealed class ElsaCreateState : State<Elsa> {

	static readonly ElsaCreateState instance = new ElsaCreateState();

	public static ElsaCreateState Instance {
		get {
			return instance;
		}
	}

	static ElsaCreateState () {}
	private ElsaCreateState () {}

	public override void Enter (Elsa agent) {
		Debug.Log("Elsa Gathering creative energies...");
	}

	public override void Execute (Elsa agent) {
		agent.CreateTime();
		Debug.Log("...Elsa creating more time, for a total of " + agent.createdTime + " unit" + (agent.createdTime > 1 ? "s" : "") + "...");
		agent.ChangeState(ElsaWaitState.Instance);
	}

	public override void Exit (Elsa agent) {
		Debug.Log("...Elsa creativity spent!");
	}
}
