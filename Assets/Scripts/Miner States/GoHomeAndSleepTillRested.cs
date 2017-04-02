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

    public override void Enter(Miner miner)
    {
        Debug.Log("Arrived Home");
        Message.DispatchMessage(0, miner.Id, miner.WifeId, MessageType.HiHoneyImHome);
    }

    public override void Execute(Miner miner)
    {
        if (miner.HowFatigued < miner.TirednessThreshold)
        {
            Debug.Log("All mah fatigue has drained away. Time to find more gold!");
            miner.StateMachine.ChangeState(new MinerTravelToTarget(Tiles.GoldMine, EnterMineAndDigForNugget.Instance, miner));
        }
        else
        {
            miner.HowFatigued--;
            Debug.Log("ZZZZZ....");
        }
    }

    public override void Exit(Miner miner)
    {

    }

    public override bool OnMesssage(Miner miner, Telegram telegram)
    {
        switch (telegram.messageType)
        {
            case MessageType.HiHoneyImHome:
                return false;
            case MessageType.StewsReady:
                Debug.Log("Message handled by " + miner.Id + " at time ");
                Debug.Log("Okay Hun, ahm a comin'!");
                miner.StateMachine.ChangeState(EatStew.Instance);
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