using UnityEngine;

public class Miner : Agent
{
    public GameObject tileMap;
    public int MaxNuggets = 3;
    public int ThirstLevel = 5;
    public int ComfortLevel = 5;
    public int TirednessThreshold = 5;

    private StateMachine<Miner> stateMachine;
    public StateMachine<Miner> StateMachine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }

    private int wifeId;
    public int WifeId
    {
        get { return wifeId; }
        set { wifeId = value; }
    }

    private int moneyInBank;
    public int MoneyInBank
    {
        get { return moneyInBank; }
        set { moneyInBank = value; }
    }

    private int howThirsty;
    public int HowThirsty
    {
        get { return howThirsty; }
        set { howThirsty = value; }
    }

    private int howFatigued;
    public int HowFatigued
    {
        get { return howFatigued; }
        set { howFatigued = value; }
    }

    public Miner()
    {
        stateMachine = new StateMachine<Miner>(this);
        stateMachine.CurrentState = GoHomeAndSleepTillRested.Instance;
        stateMachine.GlobalState = MinerGlobalState.Instance;
        wifeId = Id + 1;
        CurrentPosition = new Vector2(0, 0);
    }

    public override void Update()
    {
        Vector2 shackPosition = tileMap.GetComponent<TilingSystem>().getTilePositionByType(Tiles.Shack);
        if (CurrentPosition != (Vector2)tileMap.GetComponent<TilingSystem>().getTilePositionByType(Tiles.Shack))
        {
            howThirsty += 1;
        }
        StateMachine.Update();
    }

    public override bool HandleMessage(Telegram telegram)
    {
        return stateMachine.HandleMessage(telegram);
    }

    public override bool HandleSenseEvent(Sense sense)
    {
        return stateMachine.HandleSenseEvent(sense);
    }

    public bool PocketsFull()
    {
        if (goldCarrying >= MaxNuggets)
            return true;
        else
            return false;
    }

    public bool Thirsty()
    {
        if (howThirsty >= ThirstLevel)
            return true;
        else
            return false;
    }

    public bool Fatigued()
    {
        if (howFatigued >= TirednessThreshold)
            return true;
        else
            return false;
    }

    public bool Rich()
    {
        if (moneyInBank >= ComfortLevel)
            return true;
        else
            return false;
    }
}