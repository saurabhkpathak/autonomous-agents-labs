using UnityEngine;

public class DoHouseWork : State<MinersWife>
{
    static readonly DoHouseWork instance = new DoHouseWork();

    public static DoHouseWork Instance
    {
        get
        {
            return instance;
        }
    }

    static DoHouseWork() { }
    private DoHouseWork() { }

    static System.Random rand = new System.Random();

    public override void Enter(MinersWife minersWife)
    {
        Debug.Log("Time to do some more housework!");
    }

    public override void Execute(MinersWife minersWife)
    {
        switch (rand.Next(3))
        {
            case 0:
                Debug.Log("Moppin' the floor");
                break;
            case 1:
                Debug.Log("Washin' the dishes");
                break;
            case 2:
                Debug.Log("Makin' the bed");
                break;
            default:
                break;
        }
    }

    public override void Exit(MinersWife minersWife)
    {

    }

    public override bool OnMesssage(MinersWife minersWife, Telegram telegram)
    {
        return false;
    }

    public override bool OnSenseEvent(MinersWife agent, Sense sense)
    {
        return false;
    }
}