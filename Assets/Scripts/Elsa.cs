using UnityEngine;

public class Elsa : MonoBehaviour {

	private StateMachine<Elsa> stateMachine;

	public static int WAIT_TIME = 5;
	public int waitedTime = 0;
	public int createdTime = 0;

	public void Awake () {
		this.stateMachine = new StateMachine<Elsa>();
		this.stateMachine.Init(this, ElsaCreateState.Instance);
	}

	public void IncreaseWaitedTime (int amount) {
		this.waitedTime += amount;
	}

	public bool WaitedLongEnough () {
		return this.waitedTime >= WAIT_TIME;
	}

	public void CreateTime () {
		this.createdTime++;
		this.waitedTime = 0;
	}

	public void ChangeState (State<Elsa> state) {
		this.stateMachine.ChangeState(state);
	}

	public void Update () {
		this.stateMachine.Update();
	}
}