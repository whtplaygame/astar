using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Logic
{
    public class AStarManager : MonoBehaviour
    {
        private void Awake()
        {
            GridData data = new GridData();
            data.CreateGrid(10, 10);
            data.AStarPathFind(data.CurrentGridData[0][0], data.CurrentGridData[4][4]);
            Debug.Log($"{data.CameFrom.Count}");
            foreach (var node in data.CameFrom)
            {
                Debug.Log($"{node.Value.Location.x}   {node.Value.Location.y}");
            }
        }
    }
}