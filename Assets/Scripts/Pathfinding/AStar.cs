using System.Collections.Generic;
using UnityEngine;


public class AStar
{
    public enum SearchType { ShortestPath, SensePropogation };
    SearchType type;

    private float PERCEPTION_THRESHOLD = 6.0f;

    // Generic Node class to be used in AStar
    private class Node
    {
        public TileSprite tile;
        public Node parent;
        public float G;
        public float H;

        public Node(TileSprite tile, Vector2 targetPosition, Node parent, SearchType type)
        {
        }

        public float GetF()
        {
            return G + H;
        }
    };

    private List<Node> openList;
    private List<Node> closedList;

    public AStar(SearchType searchType = SearchType.ShortestPath)
    {
        openList = new List<Node>();
        closedList = new List<Node>();

        type = searchType;
    }

    // Sense propogation using attenuation properties of the tiles
    public bool PropogateSense(Vector2 startPosition, Vector2 targetPosition)
    {
        return false;
    }

    // Finds the shortest path with respect to the given tile costs
    public List<TileSprite> FindPath(Vector2 startPosition, Vector2 targetPosition)
    {
        return null;
    }

    // Checks if the adjacent node is proper
    void CheckAdjacentNode(Node adjNode)
    {
        bool flag = true;

        for (int i = 0; i < openList.Count; ++i)
        {
            if (openList[i].tile == adjNode.tile)
            {
                if (adjNode.G < openList[i].G)
                {
                    openList[i] = adjNode;
                }

                flag = false;
                break;
            }
        }
        foreach (Node node in closedList)
        {
            if (adjNode.tile == node.tile)
            {
                flag = false;
                break;
            }
        }

        if (flag)
        {
            openList.Add(adjNode);
        }
    }

    // Checks map boundaries
    bool IsGridReachable(Vector2 p)
    {
        return true;
    }

    // Returns the shortest path as a list
    List<TileSprite> GetPathPositions(Node node)
    {
        return null;
    }
}