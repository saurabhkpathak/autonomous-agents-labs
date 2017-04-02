using UnityEngine;

public class WifesGlobalState : State<MinersWife>
{
    static readonly WifesGlobalState instance = new WifesGlobalState();

    public static WifesGlobalState Instance
    {
        get
        {
            return instance;
        }
    }

    static WifesGlobalState() { }
    private WifesGlobalState() { }

    static System.Random rand = new System.Random();

    public override void Enter(MinersWife minersWife)
    {

    }

    public override void Execute(MinersWife minersWife)
    {
        // There's always a 10% chance of a state blip in which MinersWife goes to the bathroom
        if (rand.Next(10) == 1 && !minersWife.StateMachine.IsInState(VisitBathroom.Instance))
        {
            minersWife.StateMachine.ChangeState(VisitBathroom.Instance);
        }
    }

    public override void Exit(MinersWife minersWife)
    {

    }

    public override bool OnMesssage(MinersWife minersWife, Telegram telegram)
    {
        switch (telegram.messageType)
        {
            case MessageType.HiHoneyImHome:
                Debug.Log("Hi honey. Let me make you some of mah fine country stew");
                minersWife.StateMachine.ChangeState(CookStew.Instance);
                return true;
            case MessageType.StewsReady:
                return false;
            case MessageType.SheriffEncountered:
                //Printer.PrintMessageData("Message handled by " + minersWife.Id + " at time ");
                Debug.Log("Good day to you too, sir!");
                return true;
            default:
                return false;
        }
    }

    public override bool OnSenseEvent(MinersWife agent, Sense sense)
    {
        return false;
    }
}