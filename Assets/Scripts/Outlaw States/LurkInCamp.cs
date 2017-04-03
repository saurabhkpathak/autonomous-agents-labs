using UnityEngine;

public class LurkInCamp : State<Outlaw>
{
    static readonly LurkInCamp instance = new LurkInCamp();

    public static LurkInCamp Instance
    {
        get
        {
            return instance;
        }
    }

    static LurkInCamp() { }
    private LurkInCamp() { }

    static System.Random rand = new System.Random();

    public override void Enter(Outlaw outlaw)
    {
        Debug.Log("Back home, sweet home!");

        outlaw.BoredomCountdown = rand.Next(1, 10);
    }

    public override void Execute(Outlaw outlaw)
    {
        Debug.Log("Chilling in " + outlaw.StateMachine.CurrentState.ToString() + ".");

        if (outlaw.Bored())
        {
            outlaw.StateMachine.ChangeState(new OutlawTravelToTarget(Tiles.Cemetery, LurkInCemetery.Instance, outlaw));
        }
    }

    public override void Exit(Outlaw outlaw)
    {
        Debug.Log("Leaving the camp.");
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