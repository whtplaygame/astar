using System;
using System.Collections.Generic;
using Data.Extension;
using Graph;
using Unity.VisualScripting;

namespace Data
{
    public class GridData
    {
        public static GridData Instance;

        public static void Create()
        {
            Instance = new GridData();
            Instance.Init();
        }

        private void Init()
        {
        }

        public static void Destroy()
        {
            Instance.Release();
            Instance = null;
        }

        private void Release()
        {
        }

        public AStarNode[][] CurrentGridData;
        public Dictionary<AStarNode, AStarNode> CameFrom = new Dictionary<AStarNode, AStarNode>();
        public Dictionary<AStarNode, double> CostSoFar = new Dictionary<AStarNode, double>();

        public Location StartLocation;
        public Location EndLocation;

        public AStarNode StartNode
        {
            get
            {
                if (CheckNodeIsLegalById(StartLocation))
                {
                    return CurrentGridData[StartLocation.x][StartLocation.y];
                }

                return CurrentGridData[0][0];
            }
        }

        public AStarNode EndNode
        {
            get
            {
                if (CheckNodeIsLegalById(EndLocation))
                {
                    return CurrentGridData[EndLocation.x][EndLocation.y];
                }

                return CurrentGridData[0][0];
            }
        }

        public static readonly Location[] DIRS = new[]
        {
            new Location(1, 0),
            new Location(0, -1),
            new Location(-1, 0),
            new Location(0, 1)
        };

        public void CreateGrid(int width, int height)
        {
            CurrentGridData = new AStarNode[height][];
            for (var i = 0; i < height; i++)
            {
                var row = new AStarNode[width];
                for (var j = 0; j < width; j++)
                {
                    AStarNode aStarNode = new AStarNode(new Location(i, j), NodeType.Normal, 1f);
                    row[j] = aStarNode;
                }

                CurrentGridData[i] = row;
            }
        }

        public void AddObstacle(List<Location> locations)
        {
            foreach (var location in locations)
            {
                if (IsBound(location))
                {
                    CurrentGridData[location.x][location.y].NodeType = NodeType.Obstacle;
                }
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
            gridHeight = CurrentGridData.Length;
            if (gridHeight > 0)
            {
                gridWidth = CurrentGridData[0].Length;
            }

            if (aStarNode.Location.x < 0 || aStarNode.Location.y < 0 || aStarNode.Location.x >= gridWidth ||
                aStarNode.Location.y >= gridHeight)
            {
                return false;
            }

            return true;
        }

        private bool IsBound(Location location)
        {
            var gridWidth = 0;
            var gridHeight = 0;
            gridHeight = CurrentGridData.Length;
            if (gridHeight > 0)
            {
                gridWidth = CurrentGridData[0].Length;
            }

            if (location.x < 0 || location.y < 0 || location.x >= gridWidth || location.y >= gridHeight)
            {
                return false;
            }

            return true;
        }

        public bool CheckNodeIsLegalById(Location location)
        {
            var gridWidth = 0;
            var gridHeight = 0;
            gridHeight = CurrentGridData.Length;
            if (gridHeight > 0)
            {
                gridWidth = CurrentGridData[0].Length;
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

        public static double Heuristic(Location a, Location b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }

        public void AStarPathFind()
        {
            var priorityQueue = new PriorityQueue<AStarNode, double>();
            priorityQueue.Enqueue(StartNode, 0);

            CameFrom[StartNode] = StartNode;
            CostSoFar[StartNode] = 0;

            while (priorityQueue.Count > 0)
            {
                var current = priorityQueue.Dequeue();

                if (current.Equals(EndNode))
                {
                    break;
                }

                var neighbors = Neighbors(current);

                foreach (var next in neighbors)
                {
                    double newCost = CostSoFar[current] + 1; //Cost(current, next);
                    if (!CostSoFar.ContainsKey(next) || newCost < CostSoFar[next])
                    {
                        CostSoFar[next] = newCost;
                        double priority = newCost + Heuristic(next.Location, EndNode.Location);
                        priorityQueue.Enqueue(next, priority);
                        CameFrom[next] = current;
                    }
                }
            }
        }


        //-------------------------------------------------------------------------------
        //处理棋盘数据修改
        //-------------------------------------------------------------------------------

        public void SetDataType(Location location, NodeType type)
        {
            if (!IsBound(location))
            {
                return;
            }

            CurrentGridData[location.x][location.y].NodeType = type;
            if (type == NodeType.Start)
            {
                SetStartLocation(location);
            }

            if (type == NodeType.End)
            {
                SetEndLocation(location);
            }
        }

        public bool SetStartLocation(Location location)
        {
            if (StartLocation != new Location(0, 0))
            {
                return false;
            }

            StartLocation = location;
            return true;
        }

        public bool SetEndLocation(Location location)
        {
            if (EndLocation != new Location(0, 0))
            {
                return false;
            }

            EndLocation = location;
            return true;
        }
    }
}