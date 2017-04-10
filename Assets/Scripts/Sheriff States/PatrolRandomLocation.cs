using UnityEngine;

public class PatrolRandomLocation : State<Sheriff>
{
    static readonly PatrolRandomLocation instance = new PatrolRandomLocation();

    public static PatrolRandomLocation Instance
    {
        get
        {
            return instance;
        }
    }

    static PatrolRandomLocation() { }
    private PatrolRandomLocation() { }

    static System.Random rand = new System.Random();

    public override void Enter(Sheriff sheriff)
    {
        Debug.Log("Sheriff Arrived!");
    }

    public override void Execute(Sheriff sheriff)
    {
        //Debug.Log("Patrolling in " + LocationProperties.ToString(sheriff.Location) + ".");

        if (!sheriff.OutlawSpotted)
        {
            sheriff.StateMachine.ChangeState(new SheriffTravelToTarget(sheriff.ChooseNextLocation(), new PatrolRandomLocation(), sheriff));
        }
    }

    public override void Exit(Sheriff sheriff)
    {
        //Printer.Print(sheriff.Id, "Leaving " + LocationProperties.ToString(sheriff.Location) + ".");
    }

    public override bool OnMesssage(Sheriff agent, Telegram telegram)
    {
        return false;
    }

    public override bool OnSenseEvent(Sheriff sheriff, Sense sense)
    {
        return false;
    }
}