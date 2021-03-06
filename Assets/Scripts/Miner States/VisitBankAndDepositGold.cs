﻿using UnityEngine;

public class VisitBankAndDepositGold : State<Miner>
{
    static readonly VisitBankAndDepositGold instance = new VisitBankAndDepositGold();

    public static VisitBankAndDepositGold Instance
    {
        get
        {
            return instance;
        }
    }

    static VisitBankAndDepositGold() { }
    private VisitBankAndDepositGold() { }

    public override void Enter(Miner miner)
    {
        Debug.Log("Here is the bank. Yes siree");
    }

    public override void Execute(Miner miner)
    {
        miner.MoneyInBank += miner.GoldCarrying;
        miner.GoldCarrying = 0;
        Debug.Log("Depositing gold. Total savings now: " + miner.MoneyInBank);
        if (miner.Rich())
        {
            Debug.Log("WooHoo! Rich enough for now. Back home to mah li'lle lady");
            miner.StateMachine.ChangeState(new MinerTravelToTarget(Tiles.Shack, GoHomeAndSleepTillRested.Instance, miner));
        }
        else
        {
            miner.StateMachine.ChangeState(new MinerTravelToTarget(Tiles.GoldMine, EnterMineAndDigForNugget.Instance, miner));
        }
    }

    public override void Exit(Miner miner)
    {
        Debug.Log("Leavin' the Bank");
    }

    public override bool OnMesssage(Miner agent, Telegram telegram)
    {
        return false;
    }

    public override bool OnSenseEvent(Miner agent, Sense sense)
    {
        return false;
    }
}