using UnityEngine;

public class DragOffTheBody : State<Undertaker>
{
    static readonly DragOffTheBody instance = new DragOffTheBody();

    public static DragOffTheBody Instance
    {
        get
        {
            return instance;
        }
    }

    static DragOffTheBody() { }
    private DragOffTheBody() { }

    public override void Enter(Undertaker undertaker)
    {
        Debug.Log("Carrying the body to the tombs in the cemetery!");
    }

    public override void Execute(Undertaker undertaker)
    {
        Debug.Log("Dragging the body off. . . R.I.P.");

        Message.DispatchMessage(10, undertaker.Id, undertaker.CorpseID, MessageType.Respawn);
        undertaker.CorpseID = -1;

        undertaker.StateMachine.ChangeState(new UndertakerTravelToTarget(Tiles.Undertakers, HoverInTheOffice.Instance, undertaker));
    }

    public override void Exit(Undertaker undertaker)
    {
        Debug.Log("Leaving the cemetery");
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