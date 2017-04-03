using UnityEngine;

public class Sheriff : Agent
{
    public GameObject tileMap;
    public bool OutlawSpotted = false;

    private StateMachine<Sheriff> stateMachine;
    public StateMachine<Sheriff> StateMachine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }

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
        stateMachine.GlobalState = SheriffGlobalState.Instance;
        CurrentPosition = new Vector2(0, 0);
    }

    public override void Update()
    {
        stateMachine.Update();
    }

    public override bool HandleMessage(Telegram telegram)
    {
        return stateMachine.HandleMessage(telegram);
    }

    public override bool HandleSenseEvent(Sense sense)
    {
        return stateMachine.HandleSenseEvent(sense);
    }

    static System.Random rand = new System.Random();
    public Tiles ChooseNextLocation()
    {
        Tiles nextLocation;
        if (CurrentPosition == (Vector2)tileMap.GetComponent<TilingSystem>().getTilePositionByType(Tiles.GoldMine))
        {
            nextLocation = Tiles.Cemetery;
        } else if (CurrentPosition == (Vector2)tileMap.GetComponent<TilingSystem>().getTilePositionByType(Tiles.Cemetery))
        {
            nextLocation = Tiles.Shack;
        } else if (CurrentPosition == (Vector2)tileMap.GetComponent<TilingSystem>().getTilePositionByType(Tiles.Shack))
        {
            nextLocation = Tiles.Saloon;
        } else
        {
            nextLocation = Tiles.GoldMine;
        }
        return nextLocation;
    }
}