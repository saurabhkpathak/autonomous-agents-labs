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
    }

    public override void Execute(Miner agent)
    {
    }

    public override void Exit(Miner agent)
    {
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