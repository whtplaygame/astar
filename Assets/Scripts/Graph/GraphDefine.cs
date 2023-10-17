using UnityEditor;

namespace Graph
{
    public class GraphDefine
    {
    }

    public class AStarNode
    {
        public Location Location;
        public NodeType NodeType = NodeType.Normal;
        public float Cost;

        public AStarNode(Location location, NodeType type, float cost)
        {
            Location = location;
            NodeType = type;
            Cost = cost;
        }
    }

    public struct Location
    {
        public int x, y;

        public Location(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Location operator +(Location a, Location b)
        {
            return new Location(a.x + b.x, a.y + b.y);
        }
    }

    public enum NodeType
    {
        None,
        Normal,
        Start,
        End,
        Obstacle,
        Length,
    }
}