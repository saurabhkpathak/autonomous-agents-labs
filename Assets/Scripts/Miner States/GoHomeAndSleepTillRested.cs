using System;
using UnityEngine;

public sealed class GoHomeAndSleepTillRested : State<Miner>
{

    static readonly GoHomeAndSleepTillRested instance = new GoHomeAndSleepTillRested();

    public static GoHomeAndSleepTillRested Instance
    {
        get
        {
            return instance;
        }
    }

    static GoHomeAndSleepTillRested() { }
    private GoHomeAndSleepTillRested() { }

	public override void Enter(Miner agent)
    {
        Debug.Log("Gathering creative energies...");
    }

	public override void Execute(Miner agent)
    {
		agent.StateMachine.ChangeState(GoHomeAndSleepTillRested.Instance);
    }

	public override void Exit(Miner agent)
    {
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