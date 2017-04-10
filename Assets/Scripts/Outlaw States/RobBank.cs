using System;
using UnityEngine;

public class RobBank : State<Outlaw>
{
    static readonly RobBank instance = new RobBank();

    public static RobBank Instance
    {
        get
        {
            return instance;
        }
    }

    static RobBank() { }
    private RobBank() { }

    static System.Random rand = new System.Random();

    public override void Enter(Outlaw outlaw)
    {
        Debug.Log("Arrived in bank, Let's EARN some money!");
    }

    public override void Execute(Outlaw outlaw)
    {
        outlaw.GoldCarrying += rand.Next(1, 10);
        Debug.Log("Total harvest now: " + outlaw.GoldCarrying);

        outlaw.StateMachine.ChangeState(new OutlawTravelToTarget(outlaw.StateMachine.PreviousState.GetType() == typeof(LurkInCamp) ? Tiles.OutlawCamp : Tiles.Cemetery, outlaw.StateMachine.PreviousState, outlaw));
    }

    public override void Exit(Outlaw outlaw)
    {
        Debug.Log("Breaking away from the bank.");
    }

    public override bool OnMesssage(Outlaw agent, Telegram telegram)
    {
        return false;
    }

    public override bool OnSenseEvent(Outlaw agent, Sense sense)
    {
        return false;
    }
}