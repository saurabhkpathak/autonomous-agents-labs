using UnityEngine;

public class LookForDeadBodies : State<Undertaker>
{
    static readonly LookForDeadBodies instance = new LookForDeadBodies();

    public static LookForDeadBodies Instance
    {
        get
        {
            return instance;
        }
    }

    static LookForDeadBodies() { }
    private LookForDeadBodies() { }

    public override void Enter(Undertaker undertaker)
    {
        //Debug.Log("Arrived in " + LocationProperties.ToString(undertaker.Location) + ".");
    }

    public override void Execute(Undertaker undertaker)
    {
        for (int i = 0; i < Agent.AgentsCount; ++i)
        {
            //if (AgentManager.GetAgent(i).IsDead)
            //{
            //    if (undertaker.CurrentPosition != AgentManager.GetAgent(i).CurrentPosition)
            //    {
            //        undertaker.StateMachine.ChangeState(new UndertakerTravelToTarget(AgentManager.GetAgent(i).CurrentPosition, new LookForDeadBodies()));
            //        return;
            //    }
            //    undertaker.CorpseID = i;
            //}
        }

        //Debug.Log(undertaker.Id, "Found the corpse of " + AgentManager.GetAgent(undertaker.CorpseID).GetType().Name + ".");

        if (undertaker.CorpseID >= 0)
        {
            undertaker.StateMachine.ChangeState(new UndertakerTravelToTarget(Tiles.Cemetery, DragOffTheBody.Instance, undertaker));
        }
    }

    public override void Exit(Undertaker undertaker)
    {
        //if (undertaker.Location != Location.cemetery)
        //{
        //    Debug.Log(undertaker.Id, "Leaving " + LocationProperties.ToString(undertaker.Location) + ".");
        //}
    }

    public override bool OnMesssage(Undertaker undertaker, Telegram telegram)
    {
        return false;
    }

    public override bool OnSenseEvent(Undertaker agent, Sense sense)
    {
        return false;
    }
}