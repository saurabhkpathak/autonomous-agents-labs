using System.Collections.Generic;
using UnityEngine;

public class SheriffTravelToTarget : TravelToTarget<Sheriff>
{


    public SheriffTravelToTarget(Tiles target, State<Sheriff> state, Sheriff sheriff)
    {
        targetPosition = sheriff.tileMap.GetComponent<TilingSystem>().getTilePositionByType(target);
        targetState = state;
    }

    public override void Enter(Sheriff sheriff)
    {
        path = GeneratePathForSheriff((int)targetPosition.x, (int)targetPosition.y, sheriff);

        for (int i = 0; i < path.Count; i++)
        {
            Debug.DrawLine(new Vector3(path[i].x, path[i].y), new Vector3(path[i + 1].x, path[i + 1].y), Color.red, 2, false);
        }
    }

    public override void Execute(Sheriff sheriff)
    {
        if (path.Count > 0)
        {
            sheriff.CurrentPosition = new Vector2(path[0].x, path[0].y);
            sheriff.GetComponent<Transform>().position = sheriff.CurrentPosition;
            path.RemoveAt(0);
        }
        else
        {
            sheriff.CurrentPosition = targetPosition;

            State<Sheriff> previousState = sheriff.StateMachine.PreviousState;
            sheriff.StateMachine.ChangeState(targetState);
            sheriff.StateMachine.PreviousState = previousState;
        }
    }

    public override void Exit(Sheriff sheriff)
    {
        path.Clear();
    }

    public override bool OnMesssage(Sheriff agent, Telegram telegram)
    {
        return false;
    }

    public override bool OnSenseEvent(Sheriff agent, Sense sense)
    {
        return false;
    }

    public List<Node> GeneratePathForSheriff(int x, int y, Sheriff miner)
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
}