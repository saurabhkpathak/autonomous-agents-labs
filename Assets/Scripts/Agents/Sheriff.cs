using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Sheriff : Agent
{
    public GameObject tileMap;
    public bool OutlawSpotted = false;

    // Here is the StateMachine that the Sheriff uses to drive the agent's behaviour
    private StateMachine<Sheriff> stateMachine;
    public StateMachine<Sheriff> StateMachine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }

    // And it knows its bank balance at any point in time
    private int moneyInBank;
    public int MoneyInBank
    {
        get { return moneyInBank; }
        set { moneyInBank = value; }
    }

    public Sheriff()
    {
        stateMachine = new StateMachine<Sheriff>(this);
        stateMachine.CurrentState = PatrolRandomLocation.Instance;
        //stateMachine.GlobalState = new SheriffGlobalState();
        this.CurrentPosition = new Vector2(0, 0);
        //Location = Location.sheriffsOffice;
    }

    // This method is invoked by the Game object as a result of XNA updates 
    public override void Update()
    {
        stateMachine.Update();
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

    static System.Random rand = new System.Random();
    public Tiles ChooseNextLocation()
    {
        Tiles nextLocation;
        if (this.CurrentPosition == (Vector2)tileMap.GetComponent<TilingSystem>().getTilePositionByType(Tiles.GoldMine))
        {
            nextLocation = Tiles.Cemetery;
        } else if (this.CurrentPosition == (Vector2)tileMap.GetComponent<TilingSystem>().getTilePositionByType(Tiles.Cemetery))
        {
            nextLocation = Tiles.Shack;
        } else if (this.CurrentPosition == (Vector2)tileMap.GetComponent<TilingSystem>().getTilePositionByType(Tiles.Shack))
        {
            nextLocation = Tiles.Saloon;
        } else if (this.CurrentPosition == (Vector2)tileMap.GetComponent<TilingSystem>().getTilePositionByType(Tiles.Saloon))
        {
            nextLocation = Tiles.OutlawCamp;
        } else
        {
            nextLocation = Tiles.GoldMine;
        }
        return nextLocation;
    }
}