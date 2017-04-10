using UnityEngine;

public class UndertakerGlobalState : State<Undertaker>
{
    static readonly UndertakerGlobalState instance = new UndertakerGlobalState();

    public static UndertakerGlobalState Instance
    {
        get
        {
            return instance;
        }
    }

    static UndertakerGlobalState() { }
    private UndertakerGlobalState() { }

    static System.Random rand = new System.Random();

    public override void Enter(Undertaker undertaker)
    {
    }

    public override void Execute(Undertaker undertaker)
    {
    }

    public override void Exit(Undertaker undertaker)
    {

    }

    public override bool OnMesssage(Undertaker undertaker, Telegram telegram)
    {
        switch (telegram.messageType)
        {
            case MessageType.SheriffEncountered:
                Debug.Log("Thank you sheriff, any 'sad' news?");
                return true;
            default:
                return false;
        }
    }

    public override bool OnSenseEvent(Undertaker agent, Sense sense)
    {
        return false;
    }
}