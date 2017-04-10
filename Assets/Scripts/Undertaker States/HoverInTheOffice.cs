using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HoverInTheOffice : State<Undertaker>
{
    static readonly HoverInTheOffice instance = new HoverInTheOffice();

    public static HoverInTheOffice Instance
    {
        get
        {
            return instance;
        }
    }

    static HoverInTheOffice() { }
    private HoverInTheOffice() { }

    static System.Random rand = new System.Random();

    public override void Enter(Undertaker undertaker)
    {
        Debug.Log("Arrived in the office!");
    }

    public override void Execute(Undertaker undertaker)
    {
        Debug.Log("Hovering in the office.");
    }

    public override void Exit(Undertaker undertaker)
    {
        Debug.Log("Leaving the office.");
    }

    public override bool OnMesssage(Undertaker undertaker, Telegram telegram)
    {
        switch (telegram.messageType)
        {
            case MessageType.Gunfight:
                Debug.Log("Let's get down to business!");

                //undertaker.StateMachine.ChangeState(new UndertakerTravelToTarget(AgentManager.GetAgent(telegram.Sender).CurrentPosition, LookForDeadBodies.Instance));

                return true;
            default:
                return false;
        }
    }

    public override bool OnSenseEvent(Undertaker agent, Sense sense)
    {
        return false;
    }
}