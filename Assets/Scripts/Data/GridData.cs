using System;
using System.Collections.Generic;
using Data.Extension;
using Graph;

namespace Data
{
    public class GridData
    {
        public List<List<AStarNode>> CurrentGridData;

        public Dictionary<AStarNode, AStarNode> CameFrom = new Dictionary<AStarNode, AStarNode>();
        public Dictionary<AStarNode, double> CostSoFar = new Dictionary<AStarNode, double>();

        public static readonly Location[] DIRS = new[]
        {
            new Location(1, 0),
            new Location(0, -1),
            new Location(-1, 0),
            new Location(0, 1)
        };

        public void CreateGrid(int width, int height)
        {
            CurrentGridData = new List<List<AStarNode>>(height);
            for (var i = 0; i < height; i++)
            {
                var row = new List<AStarNode>(width);
                for (var j = 0; j < width; j++)
                {
                    AStarNode aStarNode = new AStarNode(new Location(i, j), NodeType.Normal, 1f);
                    row.Add(aStarNode);
                }

                CurrentGridData.Add(row);
            }
        }

        public bool CreateGridByConfig(GridConfig gridConfig)
        {
            if (gridConfig == null)
            {
                return false;
            }

            return true;
        }

        public bool CheckNodeIsLegal(AStarNode aStarNode)
        {
            if (aStarNode.NodeType == NodeType.Obstacle)
            {
                return false;
            }

            if (CurrentGridData == null)
            {
                return false;
            }

            var gridWidth = 0;
            var gridHeight = 0;
            gridHeight = CurrentGridData.Count;
            if (gridHeight > 0)
            {
                gridWidth = CurrentGridData[0].Count;
            }

            if (aStarNode.Location.x < 0 || aStarNode.Location.y < 0 || aStarNode.Location.x >= gridWidth ||
                aStarNode.Location.y >= gridHeight)
            {
                return false;
            }

            return true;
        }

        public bool CheckNodeIsLegalById(Location location)
        {
            var gridWidth = 0;
            var gridHeight = 0;
            gridHeight = CurrentGridData.Count;
            if (gridHeight > 0)
            {
                gridWidth = CurrentGridData[0].Count;
            }

            if (location.x < 0 || location.y < 0 || location.x >= gridWidth || location.y >= gridHeight)
            {
                return false;
            }

            if (CurrentGridData[location.x][location.y].NodeType == NodeType.Obstacle)
            {
                return false;
            }

            return true;
        }

        public List<AStarNode> Neighbors(AStarNode current)
        {
            var neighbors = new List<AStarNode>();
            foreach (var dir in DIRS)
            {
                var l = dir + current.Location;
                if (CheckNodeIsLegalById(l))
                {
                    neighbors.Add(CurrentGridData[l.x][l.y]);
                }
            }

            return neighbors;
        }

        static public double Heuristic(Location a, Location b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        public void AStarPathFind(AStarNode start, AStarNode end)
        {
            var priorityQueue = new PriorityQueue<AStarNode, double>();
            priorityQueue.Enqueue(start, 0);

            CameFrom[start] = start;
            CostSoFar[start] = 0;

            while (priorityQueue.Count > 0)
            {
                var current = priorityQueue.Dequeue();

                if (current.Equals(end))
                {
                    break;
                }

                foreach (var next in Neighbors(current))
                {
                    double newCost = CostSoFar[current] + 1; //Cost(current, next);
                    if (!CostSoFar.ContainsKey(next) || newCost < CostSoFar[next])
                    {
                        CostSoFar[next] = newCost;
                        double priority = newCost + Heuristic(next.Location, end.Location);
                        priorityQueue.Enqueue(next, priority);
                        CameFrom[next] = current;
                    }
                }
            }
        }
    }
}