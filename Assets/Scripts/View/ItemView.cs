using System;
using Data;
using Graph;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Button _btn;
        [SerializeField] private Image _itemImg;
        [SerializeField] private Text _text;

        private AStarNode _nodeData; //= new AStarNode(new Location(0, 0), NodeType.None, 0);

        private void Awake()
        {
            _btn.onClick.AddListener(OnClickItem);
        }

        public void Init(AStarNode node)
        {
            _nodeData = new AStarNode(node);
        }

        private void OnClickItem()
        {
            _nodeData.NodeType = _nodeData.NodeType == NodeType.Obstacle
                ? _nodeData.NodeType = NodeType.Normal
                : _nodeData.NodeType + 1;
            ChangeItemColor(_nodeData.NodeType);
            GridData.Instance.SetDataType(_nodeData.Location, _nodeData.NodeType);
        }

        private void ChangeItemColor(NodeType type)
        {
            switch (type)
            {
                case NodeType.Normal:
                    _itemImg.color = Color.white;
                    break;
                case NodeType.Start:
                    _itemImg.color = Color.red;
                    break;
                case NodeType.End:
                    _itemImg.color = Color.green;
                    break;
                case NodeType.Obstacle:
                    _itemImg.color = Color.black;
                    break;
            }
        }

        public void DrawNode()
        {
            _text.text = "0";
        }
    }
}