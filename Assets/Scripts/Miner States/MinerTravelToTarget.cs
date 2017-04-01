using System.Collections.Generic;
using UnityEngine;

public class MinerTravelToTarget : TravelToTarget<Miner>
{
	public MinerTravelToTarget(Tiles target, State<Miner> state, Miner miner)
    {
        targetPosition = miner.tileMap.GetComponent<TilingSystem>().getTilePositionByType(target);
        targetState = state;
    }

    public List<Node> GeneratePathForMiner(int x, int y, Miner miner)
    {
        path = null;

        if (miner.tileMap.GetComponent<TilingSystem>().UnitCanEnterTile(x, y) == false)
        {
            return null;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = miner.tileMap.GetComponent<TilingSystem>().grid[(int)miner.GetComponent<Transform>().position.x, (int)miner.GetComponent<Transform>().position.y];
        Node target = miner.tileMap.GetComponent<TilingSystem>().grid[x, y];

        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in miner.tileMap.GetComponent<TilingSystem>().grid)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;

            }
            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //float alt = dist [u] + u.DistanceTo (v);
                float alt = dist[u] + miner.tileMap.GetComponent<TilingSystem>().CostToEnterTile(u.x, u.y, v.x, v.y);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }
        if (prev[target] == null)
        {
            return null;
        }
        List<Node> currentPath = new List<Node>();
        Node curr = target;

        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse();

        return currentPath;
    }

    public override void Enter(Miner miner)
    {
        path = GeneratePathForMiner((int)targetPosition.x, (int)targetPosition.y, miner);
    }

    public override void Execute(Miner miner)
    {
        if (path.Count > 0)
        {
            miner.CurrentPosition = new Vector2(path[0].x, path[0].y);
            miner.GetComponent<Transform>().position = miner.CurrentPosition;
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
