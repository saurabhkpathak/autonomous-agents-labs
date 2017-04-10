using UnityEngine;

public class EatStew : State<Miner>
{
    static readonly EatStew instance = new EatStew();

    public static EatStew Instance
    {
        get
        {
            return instance;
        }
    }

    static EatStew() { }
    private EatStew() { }

    public override void Enter(Miner miner)
    {
        Debug.Log("Smells Reaaal goood Elsa!");
    }

    public override void Execute(Miner miner)
    {
        Debug.Log("Tastes real good too!");
        miner.StateMachine.RevertToPreviousState();
    }

    public override void Exit(Miner miner)
    {
        Debug.Log("Thankya li'lle lady. Ah better get back to whatever ah wuz doin'");
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
