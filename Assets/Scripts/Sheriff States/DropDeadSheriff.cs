using UnityEngine;

public class DropDeadSheriff : State<Sheriff>
{
    static readonly DropDeadSheriff instance = new DropDeadSheriff();

    public static DropDeadSheriff Instance
    {
        get
        {
            return instance;
        }
    }

    static DropDeadSheriff() { }
    private DropDeadSheriff() { }

    public override void Enter(Sheriff sheriff)
    {
        Debug.Log("Goodbye, cruel world!");
        sheriff.IsDead = true;
    }

    public override void Execute(Sheriff sheriff)
    {
    }

    public override void Exit(Sheriff sheriff)
    {
        sheriff.IsDead = false;
        //sheriff.Location = Location.sheriffsOffice;

        Debug.Log("It's a miracle, I am alive!");
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