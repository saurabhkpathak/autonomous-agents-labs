using System;
using UnityEngine;

public class LurkInCemetery : State<Outlaw>
{
    static readonly LurkInCemetery instance = new LurkInCemetery();

    public static LurkInCemetery Instance
    {
        get
        {
            return instance;
        }
    }

    static LurkInCemetery() { }
    private LurkInCemetery() { }

    static System.Random rand = new System.Random();

    public override void Enter(Outlaw outlaw)
    {
        Debug.Log("Arrived in the cemetery!");

        outlaw.BoredomCountdown = rand.Next(1, 10);
    }

    public override void Execute(Outlaw outlaw)
    {
        Debug.Log("Lurking in the cemetery.");

        if (outlaw.Bored())
        {
            outlaw.StateMachine.ChangeState(new OutlawTravelToTarget(Tiles.OutlawCamp, LurkInCamp.Instance, outlaw));
        }
    }

    public override void Exit(Outlaw outlaw)
    {
        Debug.Log("Leaving the cemetery.");
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