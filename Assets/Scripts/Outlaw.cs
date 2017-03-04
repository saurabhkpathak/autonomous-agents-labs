using UnityEngine;

public class Outlaw : MonoBehaviour {

	private StateMachine<Outlaw> stateMachine;

	public int createdTime = 0;
	private int goldCarried = 0;

	public void Awake () {
		this.stateMachine = new StateMachine<Outlaw>();
		this.stateMachine.Init(this, LurkInCamp.Instance);
	}

	public void CreateTime () {
		this.createdTime++;
	}

	public void ChangeState (State<Outlaw> state) {
		this.stateMachine.ChangeState(state);
	}

	public void Update () {
		this.stateMachine.Update();
	}

	public void RobBank() {
		goldCarried = goldCarried + Random.Range (1, 11);
		Debug.Log ("Robbing bank");
	}

	public int getGoldQuantity() {
		return goldCarried;
	}
}