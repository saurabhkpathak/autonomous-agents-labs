﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MinerTravelToTarget : TravelToTarget<Miner>
{
	public MinerTravelToTarget(Tiles target, State<Miner> state, Miner miner)
    {
        targetPosition = miner.tileMap.GetComponent<TilingSystem>().getTilePositionByType(target);
        targetState = state;
    }

    public override void Enter(Miner miner)
    {
        path = miner.tileMap.GetComponent<TilingSystem>().GeneratePathTo((int)targetPosition.x, (int)targetPosition.y);

        //Debug.Log("Walkin' to " + LocationProperties.ToString(LocationProperties.GetLocation(targetPosition)) + ".");
    }

    public override void Execute(Miner miner)
    {
        if (path.Count > 0)
        {
            //for (int i = 0; i < path.Count; ++i)
            //{
            //    path[i].TintColor = Color.Blue;
            //    path[i].TintAlpha = 0.5f;
            //}

            miner.CurrentPosition = new Vector2(path[0].x, path[0].y);
            path.RemoveAt(0);
        }
        else
        {
            miner.CurrentPosition = targetPosition;

            State<Miner> previousState = miner.StateMachine.PreviousState;
            miner.StateMachine.ChangeState(targetState);
            miner.StateMachine.PreviousState = previousState;
        }
    }

    public override void Exit(Miner miner)
    {
		Debug.Log (path.Count);
        path.Clear();
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
