using System.Collections.Generic;
using UnityEngine;


public class AStar
{
    public enum SearchType { ShortestPath, SensePropogation };
    SearchType type;

    private float PERCEPTION_THRESHOLD = 6.0f;

    private List<Node> openList;
    private List<Node> closedList;

    public List<Node> pathSet;

    public AStar(SearchType searchType = SearchType.ShortestPath)
    {
        openList = new List<Node>();
        closedList = new List<Node>();

        pathSet = new List<Node>();

        type = searchType;
    }

    // Sense propogation using attenuation properties of the tiles
    public bool PropogateSense(Vector2 startPosition, Vector2 targetPosition, TilingSystem tileMap)
    {
        //Tile startTile = TileMap.Tiles[(int)startPosition.Y][(int)startPosition.X];

        Node startNode = tileMap.grid[(int)startPosition.x, (int)startPosition.y];

        openList.Clear();
        closedList.Clear();

        openList.Add(startNode);
        while (openList.Count > 0)
        {
            Node currentNode = null;
            float minF = float.MaxValue;
            for (int i = 0; i < openList.Count; ++i)
            {
                float F = openList[i].FCost;
                if (F < minF)
                {
                    currentNode = openList[i];
                    minF = F;
                }
            }

            closedList.Add(currentNode);
            openList.Remove(currentNode);

            // Target reached?
            if (new Vector2(currentNode.x, currentNode.y) == targetPosition)
                return true;
            else if (currentNode.gCost > PERCEPTION_THRESHOLD) // Target out of perception?
                return false;

            // Check adjacent nodes
            Node adjTile;
            Vector2 adjPosition;

            //adjPosition = new Vector2(currentNode.x - 1, currentNode.y);
            //if (IsGridReachable(adjPosition, tileMap))
            //{
            //    adjTile = tileMap.grid[(int)adjPosition.y, (int)adjPosition.x];
            //    CheckAdjacentNode(new Node(tileMap.TileSprites[tileMap.tiles[(int)adjPosition.x, (int)adjPosition.y]], targetPosition, currentNode, type));
            //}

            //adjPosition = new Vector2(currentNode.x + 1, currentNode.y);
            //if (IsGridReachable(adjPosition, tileMap))
            //{
            //    adjTile = tileMap.grid[(int)adjPosition.y, (int)adjPosition.x];
            //    CheckAdjacentNode(new Node(tileMap.TileSprites[tileMap.tiles[(int)adjPosition.x, (int)adjPosition.y]], targetPosition, currentNode, type));
            //}

            //adjPosition = new Vector2(currentNode.x, currentNode.y - 1);
            //if (IsGridReachable(adjPosition, tileMap))
            //{
            //    adjTile = tileMap.grid[(int)adjPosition.y, (int)adjPosition.x];
            //    CheckAdjacentNode(new Node(tileMap.TileSprites[tileMap.tiles[(int)adjPosition.x, (int)adjPosition.y]], targetPosition, currentNode, type));
            //}

            //adjPosition = new Vector2(currentNode.x, currentNode.y + 1);
            //if (IsGridReachable(adjPosition, tileMap))
            //{
            //    adjTile = tileMap.grid[(int)adjPosition.y, (int)adjPosition.x];
            //    CheckAdjacentNode(new Node(tileMap.TileSprites[tileMap.tiles[(int)adjPosition.x, (int)adjPosition.y]], targetPosition, currentNode, type));
            //}
        }

        return false;
    }

    // Finds the shortest path with respect to the given tile costs
    public void FindPath(Vector2 startPosition, Vector2 targetPosition, TilingSystem TileMap)
    {
        Node startNode = TileMap.grid[(int)startPosition.x, (int)startPosition.y];
        Node targetNode = TileMap.grid[(int)targetPosition.x, (int)targetPosition.y];

        openList.Clear();
        closedList.Clear();

        openList.Add(startNode);
        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            //Node currentNode = null;
            //float minF = float.MaxValue;

            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].getFCost(targetNode) < currentNode.getFCost(targetNode) || openList[i].getFCost(targetNode) == currentNode.getFCost(targetNode) && openList[i].getHCost(targetNode) < currentNode.getHCost(targetNode))
                {
                    currentNode = openList[i];
                }
            }

            //for (int i = 0; i < openList.Count; ++i)
            //{
            //    float F = openList[i].fCost;
            //    if (F < minF)
            //    {
            //        currentNode = openList[i];
            //        minF = F;
            //    }
            //}

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            // Target reached?
            //if (new Vector2(currentNode.x, currentNode.y) == targetPosition)
            //{
            //    return GetPathPositions(currentNode);
            //    return;
            //}
            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }
            else if (closedList.Count > 500)
            { // just in case (avoid infinite loop)
                break;
            }

            foreach(Node neighbour in currentNode.neighbours)
            {
                if(TileMap.TileSprites[TileMap.tiles[neighbour.x, neighbour.y]].isWalkable || closedList.Contains(neighbour))
                {
                    continue; 
                }

                float newMovementCostToNeighbour = currentNode.getGCost(targetNode, TileMap) + currentNode.getDistance(neighbour);

                if(newMovementCostToNeighbour < neighbour.getGCost(targetNode, TileMap) || openList.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = neighbour.getDistance(targetNode);
                    neighbour.parent = currentNode;

                    if(!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }

            // Check adjacent nodes
            //Node adjTile;
            //Vector2 adjPosition;

            //if(currentNode.x > 0)
            //{
            //    adjPosition = new Vector2(currentNode.x - 1, currentNode.y);
            //    if (IsGridReachable(adjPosition, TileMap))
            //    {
            //        adjTile = TileMap.grid[(int)adjPosition.x, (int)adjPosition.y];
            //        CheckAdjacentNode(new Node(TileMap.TileSprites[TileMap.tiles[(int)adjPosition.x, (int)adjPosition.y]], targetPosition, currentNode, type));
            //    }
            //}

            //if(currentNode.x < TileMap.MapSizeX)
            //{
            //    adjPosition = new Vector2(currentNode.x + 1, currentNode.y);
            //    if (IsGridReachable(adjPosition, TileMap))
            //    {
            //        adjTile = TileMap.grid[(int)adjPosition.x, (int)adjPosition.y];
            //        CheckAdjacentNode(new Node(TileMap.TileSprites[TileMap.tiles[(int)adjPosition.x, (int)adjPosition.y]], targetPosition, currentNode, type));
            //    }
            //}

            //if(currentNode.y > 0)
            //{
            //    adjPosition = new Vector2(currentNode.x, currentNode.y - 1);
            //    if (IsGridReachable(adjPosition, TileMap))
            //    {
            //        adjTile = TileMap.grid[(int)adjPosition.x, (int)adjPosition.y];
            //        CheckAdjacentNode(new Node(TileMap.TileSprites[TileMap.tiles[(int)adjPosition.x, (int)adjPosition.y]], targetPosition, currentNode, type));
            //    }
            //}

            //if(currentNode.y < TileMap.MapSizeY)
            //{
            //    adjPosition = new Vector2(currentNode.x, currentNode.y + 1);
            //    if (IsGridReachable(adjPosition, TileMap))
            //    {
            //        adjTile = TileMap.grid[(int)adjPosition.x, (int)adjPosition.y];
            //        CheckAdjacentNode(new Node(TileMap.TileSprites[TileMap.tiles[(int)adjPosition.x, (int)adjPosition.y]], targetPosition, currentNode, type));
            //    }
            //}
        }

        //return GetPathPositions(closedList[closedList.Count - 1]);
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        pathSet = path;
    }

    // Checks if the adjacent node is proper
    void CheckAdjacentNode(Node adjNode)
    {
        bool flag = true;

        for (int i = 0; i < openList.Count; ++i)
        {
            if (openList[i] == adjNode)
            {
                if (adjNode.gCost < openList[i].gCost)
                {
                    openList[i] = adjNode;
                }

                flag = false;
                break;
            }
        }
        foreach (Node node in closedList)
        {
            if (adjNode == node)
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
    bool IsGridReachable(Vector2 p, TilingSystem TileMap)
    {
        int x = (int)p.x;
        int y = (int)p.y;
        return (x >= 0 && x < TileMap.MapSizeX && y >= 0 && y < TileMap.MapSizeY);
    }

    // Returns the shortest path as a list
    List<Node> GetPathPositions(Node node)
    {
        List<Node> positions = new List<Node>();

        while (node.parent != null)
        {
            positions.Insert(0, node);
            node = node.parent;
        }

        return positions;
    }
}