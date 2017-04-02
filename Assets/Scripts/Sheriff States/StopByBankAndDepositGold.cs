using UnityEngine;

public class StopByBankAndDepositGold : State<Sheriff>
{
    static readonly StopByBankAndDepositGold instance = new StopByBankAndDepositGold();

    public static StopByBankAndDepositGold Instance
    {
        get
        {
            return instance;
        }
    }

    static StopByBankAndDepositGold() { }
    private StopByBankAndDepositGold() { }

    public override void Enter(Sheriff sheriff)
    {
        Debug.Log("Arrived in bank.");
    }

    public override void Execute(Sheriff sheriff)
    {
        sheriff.MoneyInBank += sheriff.GoldCarrying;
        sheriff.GoldCarrying = 0;
        Debug.Log("Depositing gold. Total savings now: " + sheriff.MoneyInBank);

        sheriff.StateMachine.ChangeState(new SheriffTravelToTarget(Tiles.Saloon, CelebrateTheDayInSaloon.Instance, sheriff));
    }

    public override void Exit(Sheriff sheriff)
    {
        Debug.Log("Leaving the Bank, time to celebrate!");
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