using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EnterMineAndDigForNugget : State<Miner>
{
    public override void Enter(Miner miner)
    {
        Debug.Log("Arrived in the goldmine");
    }

    public override void Execute(Miner miner)
    {
        miner.GoldCarrying += 1;
        miner.HowFatigued += 1;
        Debug.Log("Pickin' up a nugget");
        if (miner.PocketsFull())
        {
            //miner.StateMachine.ChangeState(new MinerTravelToTarget(Location.bank, new VisitBankAndDepositGold()));
        }
        if (miner.Thirsty())
        {
            //miner.StateMachine.ChangeState(new MinerTravelToTarget(Location.saloon, new QuenchThirst()));
        }
    }

    public override void Exit(Miner miner)
    {
        Debug.Log("Ah'm leaving the gold mine with mah pockets full o' sweet gold");
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