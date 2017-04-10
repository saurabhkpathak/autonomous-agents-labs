using System;
using UnityEngine;

public class OutlawGlobalState : State<Outlaw>
{
    static readonly OutlawGlobalState instance = new OutlawGlobalState();

    public static OutlawGlobalState Instance
    {
        get
        {
            return instance;
        }
    }

    static OutlawGlobalState() { }
    private OutlawGlobalState() { }

    static System.Random rand = new System.Random();

    public override void Enter(Outlaw outlaw)
    {
    }

    public override void Execute(Outlaw outlaw)
    {
        if (!outlaw.IsDead)
        {
            if (outlaw.StateMachine.CurrentState.GetType() != typeof(OutlawTravelToTarget))
            {
                if (rand.Next(20) == 1 && !outlaw.StateMachine.IsInState(RobBank.Instance))
                {
                    outlaw.StateMachine.ChangeState(new OutlawTravelToTarget(Tiles.Bank, RobBank.Instance, outlaw));
                }
            }
        }
    }

    public override void Exit(Outlaw outlaw)
    {
    }

    public override bool OnMesssage(Outlaw outlaw, Telegram telegram)
    {

        switch (telegram.messageType)
        {
            case MessageType.SheriffEncountered:
                Debug.Log("Prepare to die instead, sheriff!");
                Message.DispatchMessage(0, outlaw.Id, telegram.Sender, MessageType.Gunfight);
                return true;
            case MessageType.Dead:
                outlaw.StateMachine.ChangeState(DropDeadOutlaw.Instance);
                return true;
            case MessageType.Respawn:
                outlaw.StateMachine.ChangeState(LurkInCamp.Instance);
                return true;
        }
        return false;
    }

    public override bool OnSenseEvent(Outlaw agent, Sense sense)
    {
        return false;
    }
}