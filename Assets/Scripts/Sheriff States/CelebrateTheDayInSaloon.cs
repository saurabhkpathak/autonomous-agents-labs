using UnityEngine;

public class CelebrateTheDayInSaloon : State<Sheriff>
{
    static readonly CelebrateTheDayInSaloon instance = new CelebrateTheDayInSaloon();

    public static CelebrateTheDayInSaloon Instance
    {
        get
        {
            return instance;
        }
    }

    static CelebrateTheDayInSaloon() { }
    private CelebrateTheDayInSaloon() { }

    public override void Enter(Sheriff sheriff)
    {
        Debug.Log("Arrived in the saloon!");
    }

    public override void Execute(Sheriff sheriff)
    {
        Debug.Log("All drinks on me today!");

        sheriff.StateMachine.ChangeState(new SheriffTravelToTarget(sheriff.ChooseNextLocation(), PatrolRandomLocation.Instance, sheriff));
    }

    public override void Exit(Sheriff sheriff)
    {
        Debug.Log("Leaving the saloon.");
    }

    public override bool OnMesssage(Sheriff agent, Telegram telegram)
    {
        return false;
    }

    public override bool OnSenseEvent(Sheriff agent, Sense sense)
    {
        return false;
    }
}