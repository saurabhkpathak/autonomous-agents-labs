using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public sealed class EnterMineAndDigForNugget : State<Miner>
{
    static readonly EnterMineAndDigForNugget instance = new EnterMineAndDigForNugget();

    public static EnterMineAndDigForNugget Instance
    {
        get
        {
            return instance;
        }
    }

    static EnterMineAndDigForNugget() { }
    private EnterMineAndDigForNugget() { }

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
			miner.StateMachine.ChangeState(new MinerTravelToTarget(Tiles.Bank, new VisitBankAndDepositGold(), miner));
        }
        if (miner.Thirsty())
        {
			miner.StateMachine.ChangeState(new MinerTravelToTarget(Tiles.Saloon, new QuenchThirst(), miner));
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