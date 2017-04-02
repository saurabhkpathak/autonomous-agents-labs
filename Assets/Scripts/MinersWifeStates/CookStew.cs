using UnityEngine;

public class CookStew : State<MinersWife>
{
    static readonly CookStew instance = new CookStew();

    public static CookStew Instance
    {
        get
        {
            return instance;
        }
    }

    static CookStew() { }
    private CookStew() { }

    public override void Enter(MinersWife minersWife)
    {
        if (!minersWife.Cooking)
        {
            // MinersWife sends a delayed message to herself to arrive when the food is ready
            Debug.Log("Putting the stew in the oven");
            Message.DispatchMessage(2, minersWife.Id, minersWife.Id, MessageType.StewsReady);
            minersWife.Cooking = true;
        }
    }

    public override void Execute(MinersWife minersWife)
    {
        Debug.Log("Fussin' over food");
    }

    public override void Exit(MinersWife minersWife)
    {
        Debug.Log("Puttin' the stew on the table");
    }

    public override bool OnMesssage(MinersWife minersWife, Telegram telegram)
    {
        switch (telegram.messageType)
        {
            case MessageType.HiHoneyImHome:
                // Ignored here; handled in WifesGlobalState below
                return false;
            case MessageType.StewsReady:
                // Tell Miner that the stew is ready now by sending a message with no delay
                Debug.Log("StewReady! Lets eat");
                Message.DispatchMessage(0, minersWife.Id, minersWife.HusbandId, MessageType.StewsReady);
                minersWife.Cooking = false;
                minersWife.StateMachine.ChangeState(DoHouseWork.Instance);
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