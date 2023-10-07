using System;
using UnityEngine;

namespace View
{
    public class GridView: MonoBehaviour
    {
        
        public static GridView Instance;

        private void Awake()
        {
            Instance = this;
        }
        
        
    }
}