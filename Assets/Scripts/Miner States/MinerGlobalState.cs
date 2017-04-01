using System;
using UnityEngine;

public sealed class MinerGlobalState : State<Miner>
{

	static readonly MinerGlobalState instance = new MinerGlobalState();

	public static MinerGlobalState Instance
	{
		get
		{
			return instance;
		}
	}

	static MinerGlobalState() { }
	private MinerGlobalState() { }

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
        switch (telegram.messageType)
        {
            case MessageType.SheriffEncountered:
                //Printer.PrintMessageData("Message handled by " + agent.Id + " at time ");
                Debug.Log("Good day to you too, sheriff!");
                return true;
            default:
                return false;
        }
    }

	public override bool OnSenseEvent(Miner agent, Sense sense)
	{
        return false;
	}
}