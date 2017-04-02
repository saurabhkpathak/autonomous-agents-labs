using UnityEngine;

public class VisitBathroom : State<MinersWife>
{
    static readonly VisitBathroom instance = new VisitBathroom();

    public static VisitBathroom Instance
    {
        get
        {
            return instance;
        }
    }

    static VisitBathroom() { }
    private VisitBathroom() { }

    public override void Enter(MinersWife minersWife)
    {
        Debug.Log("Walkin' to the can. Need to powda mah pretty li'lle nose");
    }

    public override void Execute(MinersWife minersWife)
    {
        Debug.Log("Ahhhhhh! Sweet relief!");
        minersWife.StateMachine.RevertToPreviousState();  // this completes the state blip
    }

    public override void Exit(MinersWife minersWife)
    {
        Debug.Log("Leavin' the Jon");
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