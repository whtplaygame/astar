using System;
using Data;
using UnityEngine;

namespace View
{
    public class GridView: MonoBehaviour
    {
        public static GridView Instance;
        [SerializeField] private float _xOffset;
        [SerializeField] private float _yOffset;

        private void Awake()
        {
            Instance = this;
        }

        public void LoadGrid(Transform root)
        {
            var data = GridData.Instance.CurrentGridData;
            foreach (var col in data)
            {
                foreach (var node in col)
                {
                    var item=Resources.Load<ItemView>("Assets/Resources/Prefab/Grids/Item.prefab");
                    if (item != null)
                    {
                        item.Init(node);
                        var location = node.Location;
                        var go = Instantiate(item, root);
                        go.transform.localPosition.Set(location.x*100+_xOffset,location.y*100+_yOffset,0);
                    }
                }
            }
        }
    }
}