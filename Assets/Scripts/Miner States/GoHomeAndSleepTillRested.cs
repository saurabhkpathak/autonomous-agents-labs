using System;
using UnityEngine;

public sealed class GoHomeAndSleepTillRested : State<Bob>
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

    public override void Enter(Bob agent)
    {
        Debug.Log("Gathering creative energies...");
    }

    public override void Execute(Bob agent)
    {
        agent.CreateTime();
        Debug.Log("...creating more time, for a total of " + agent.createdTime + " unit" + (agent.createdTime > 1 ? "s" : "") + "...");
        agent.ChangeState(WaitState.Instance);
    }

    public override void Exit(Bob agent)
    {
        Debug.Log("...creativity spent!");
    }

    public override bool OnMesssage(Bob agent, Telegram telegram)
    {
        throw new NotImplementedException();
    }

    public override bool OnSenseEvent(Bob agent, Sense sense)
    {
        throw new NotImplementedException();
    }
}