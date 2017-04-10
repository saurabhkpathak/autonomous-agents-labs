using UnityEngine;

public class UndertakerTravelToTarget : TravelToTarget<Undertaker>
{
    static readonly UndertakerTravelToTarget instance = new UndertakerTravelToTarget();

    public static UndertakerTravelToTarget Instance
    {
        get
        {
            return instance;
        }
    }

    static UndertakerTravelToTarget() { }
    private UndertakerTravelToTarget() { }

    public UndertakerTravelToTarget(Tiles target, State<Undertaker> state, Undertaker undertaker)
    {
        //targetPosition = undertaker.tileMap.GetComponent<TilingSystem>().getTilePositionByType(target);
        targetState = state;
    }

    public override void Enter(Undertaker undertaker)
    {
        //path = pathFinder.FindPath(undertaker.CurrentPosition, targetPosition);

        //Printer.Print(undertaker.Id, "Walkin' to " + LocationProperties.ToString(LocationProperties.GetLocation(targetPosition)) + ".");
    }

    public override void Execute(Undertaker undertaker)
    {
        if (path.Count > 0)
        {
            undertaker.CurrentPosition = new Vector2(path[0].x, path[0].y);
            undertaker.GetComponent<Transform>().position = undertaker.CurrentPosition;
            path.RemoveAt(0);
        }
        else
        {
            undertaker.CurrentPosition = targetPosition;

            State<Undertaker> previousState = undertaker.StateMachine.PreviousState;
            undertaker.StateMachine.ChangeState(targetState);
            undertaker.StateMachine.PreviousState = previousState;
        }

        if (undertaker.CorpseID >= 0)
        {
            //AgentManager.GetAgent(undertaker.CorpseID).CurrentPosition = undertaker.CurrentPosition;
        }
    }

    public override void Exit(Undertaker undertaker)
    {
        path.Clear();
    }

    public override bool OnMesssage(Undertaker agent, Telegram telegram)
    {
        return false;
    }

    public override bool OnSenseEvent(Undertaker agent, Sense sense)
    {
        return false;
    }
}