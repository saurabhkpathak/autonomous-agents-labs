using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class VisitBankAndDepositGold : State<Miner>
{
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
            //miner.StateMachine.ChangeState(new MinerTravelToTarget(Location.shack, new GoHomeAndSleepTillRested()));
        }
        else
        {
            //miner.StateMachine.ChangeState(new MinerTravelToTarget(Location.goldMine, new EnterMineAndDigForNugget()));
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