using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Outlaw : Agent
{
    public int BoredomCountdown = 0;
    public GameObject tileMap;

    // Here is the StateMachine that the Outlaw uses to drive the agent's behaviour
    private StateMachine<Outlaw> stateMachine;
    public StateMachine<Outlaw> StateMachine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }

    public Outlaw()
    {
        stateMachine = new StateMachine<Outlaw>(this);
        stateMachine.CurrentState = LurkInCamp.Instance;
        //stateMachine.GlobalState = new OutlawGlobalState();
        this.CurrentPosition = new Vector2(0, 0);
        //Location = Location.outlawCamp;
    }

    // This method is invoked by the Game object as a result of XNA updates 
    public override void Update()
    {
        //if (Location >= 0)
        //{
        //    BoredomCountdown -= 1;
        //}

        stateMachine.Update();
    }

    public bool Bored()
    {
        return (BoredomCountdown <= 0);
    }

    // This method is invoked when the agent receives a message
    public override bool HandleMessage(Telegram telegram)
    {
        return stateMachine.HandleMessage(telegram);
    }

    // This method is invoked when the agent senses
    public override bool HandleSenseEvent(Sense sense)
    {
        return stateMachine.HandleSenseEvent(sense);
    }
}