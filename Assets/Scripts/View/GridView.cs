using System.Collections;
using Data;
using Graph;
using UnityEngine;

namespace View
{
    public class GridView : MonoBehaviour
    {
        public static GridView Instance;
        [SerializeField] private float _xOffset;
        [SerializeField] private float _yOffset;

        private ItemView[][] _gridView;

        private void Awake()
        {
            Instance = this;
        }

        public void LoadGrid(Transform root)
        {
            var data = GridData.Instance.CurrentGridData;
            _gridView = new ItemView[data.Length][];
            var rowNum = data[0].Length;

            for (int i = 0; i < data.Length; i++)
            {
                var rowNode = new ItemView[rowNum];
                for (int j = 0; j < rowNum; j++)
                {
                    var item = Resources.Load<ItemView>("Prefab/Grids/Item");
                    if (item != null)
                    {
                        var location = data[i][j].Location;
                        var go = Instantiate(item, root);
                        go.Init(data[i][j]);
                        go.transform.SetLocalPositionAndRotation(
                            new Vector3(location.x * 100 + _xOffset, location.y * 100 + _yOffset, 0),
                            Quaternion.identity);
                        rowNode[j] = go;
                    }
                }

                _gridView[i] = rowNode;
            }
        }

        public IEnumerator DrawPath()
        {
            var dic = GridData.Instance.CameFrom;
            var end = GridData.Instance.EndNode;
            var drawNode = end;
            while (true)
            {
                if (dic.TryGetValue(drawNode, out var value))
                {
                    DrawNode(drawNode);
                    drawNode = value;
                    yield return new WaitForSeconds(0.2f);
                }
                else
                {
                    yield break;
                }
            }
        }

        private void DrawNode(AStarNode aStarNode)
        {
            _gridView[aStarNode.Location.x][aStarNode.Location.y].DrawNode();
        }
    }
}