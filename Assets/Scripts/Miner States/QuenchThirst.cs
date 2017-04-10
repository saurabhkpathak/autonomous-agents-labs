using UnityEngine;

public class QuenchThirst : State<Miner>
{
    static readonly QuenchThirst instance = new QuenchThirst();

    public static QuenchThirst Instance
    {
        get
        {
            return instance;
        }
    }

    static QuenchThirst() { }
    private QuenchThirst() { }

    public override void Enter(Miner miner)
    {
        Debug.Log("Boy, ah sure is thusty! Arrived the saloon");
    }

    public override void Execute(Miner miner)
    {
        // Buying whiskey costs 2 gold but quenches thirst altogether
        miner.HowThirsty = 0;
        miner.MoneyInBank -= 2;
        Debug.Log("That's mighty fine sippin' liquer");
        miner.StateMachine.ChangeState(new MinerTravelToTarget(Tiles.GoldMine, EnterMineAndDigForNugget.Instance, miner));
    }

    public override void Exit(Miner miner)
    {
        Debug.Log("Leaving the saloon, feelin' good");
    }

    public override bool OnMesssage(Miner agent, Telegram telegram)
    {
        return false;
    }

    public override bool OnSenseEvent(Miner agent, Sense sense)
    {
        return false;
    }
}
