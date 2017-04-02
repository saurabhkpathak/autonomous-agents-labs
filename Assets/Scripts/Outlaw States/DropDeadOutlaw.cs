using UnityEngine;

public class DropDeadOutlaw : State<Outlaw>
{
    static readonly DropDeadOutlaw instance = new DropDeadOutlaw();

    public static DropDeadOutlaw Instance
    {
        get
        {
            return instance;
        }
    }

    static DropDeadOutlaw() { }
    private DropDeadOutlaw() { }

    public override void Enter(Outlaw outlaw)
    {
        Debug.Log("I will come back, from dead!");
        outlaw.IsDead = true;
    }

    public override void Execute(Outlaw outlaw)
    {
    }

    public override void Exit(Outlaw outlaw)
    {
        outlaw.IsDead = false;
        outlaw.transform.position = outlaw.tileMap.GetComponent<TilingSystem>().getTilePositionByType(Tiles.OutlawCamp);

        Debug.Log("I am back, from dead!");
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