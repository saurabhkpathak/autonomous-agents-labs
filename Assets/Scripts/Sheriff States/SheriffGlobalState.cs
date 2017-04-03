using UnityEngine;

public class SheriffGlobalState : State<Sheriff>
{
    static readonly SheriffGlobalState instance = new SheriffGlobalState();

    public static SheriffGlobalState Instance
    {
        get
        {
            return instance;
        }
    }

    static SheriffGlobalState() { }
    private SheriffGlobalState() { }

    static System.Random rand = new System.Random();

    public override void Enter(Sheriff sheriff)
    {
    }

    public override void Execute(Sheriff sheriff)
    {
    }

    public override void Exit(Sheriff sheriff)
    {

    }

    public override bool OnMesssage(Sheriff sheriff, Telegram telegram)
    {
        switch (telegram.messageType)
        {
            case MessageType.Gunfight:
                // Notify the undertaker
                Message.DispatchMessage(0, sheriff.Id, Agent.AgentsCount - 1, MessageType.Gunfight);

                // Gunfight
                Outlaw outlaw = (AgentManager.GetAgent(telegram.Sender) as Outlaw);

                if (rand.Next(10) == 1) // sheriff dies
                {

                    outlaw.GoldCarrying += sheriff.GoldCarrying;
                    sheriff.GoldCarrying = 0;

                    Message.DispatchMessage(0, sheriff.Id, sheriff.Id, MessageType.Dead);
                }
                else // outlaw dies
                {
                    Debug.Log("I am not coward, but I am so strong. It is hard to die.");

                    sheriff.GoldCarrying += outlaw.GoldCarrying;
                    outlaw.GoldCarrying = 0;

                    Message.DispatchMessage(0, sheriff.Id, outlaw.Id, MessageType.Dead);

                    sheriff.StateMachine.ChangeState(new SheriffTravelToTarget(Tiles.Bank, StopByBankAndDepositGold.Instance, sheriff));
                }

                sheriff.OutlawSpotted = false;

                return true;
            case MessageType.Dead:
                sheriff.StateMachine.ChangeState(DropDeadSheriff.Instance);
                return true;
            case MessageType.Respawn:
                sheriff.StateMachine.ChangeState(PatrolRandomLocation.Instance);
                return true;
            default:
                return false;
        }
    }

    public override bool OnSenseEvent(Sheriff sheriff, Sense sense)
    {
        if (!sheriff.IsDead)
        {
            if (typeof(Outlaw) == AgentManager.GetAgent(sense.Sender).GetType()) // outlaw spotted
            {
                if (!sheriff.OutlawSpotted && !AgentManager.GetAgent(sense.Sender).IsDead)
                {
                    Debug.Log("Sure glad to see you bandit, but hand me those guns.");
                    sheriff.OutlawSpotted = true;
                    Message.DispatchMessage(0, sheriff.Id, sense.Sender, MessageType.SheriffEncountered);

                    return true;
                }
            }
            else // greetings
            {
                Debug.Log("Good day, townie!");
                Message.DispatchMessage(0, sheriff.Id, sense.Sender, MessageType.SheriffEncountered);

                return true;
            }
        }

        return false;
    }
}