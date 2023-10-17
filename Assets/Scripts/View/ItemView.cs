using System;
using Graph;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Button _btn;
        [SerializeField] private Image _itemImg;

        private AStarNode _nodeData;

        private void Awake()
        {
            _btn.onClick.AddListener(OnClickItem);
        }

        public void Init(AStarNode node)
        {
            _nodeData = node;
        }

        private void OnClickItem()
        {
            _nodeData.NodeType = _nodeData.NodeType == NodeType.Obstacle ? 
                _nodeData.NodeType = NodeType.Normal : _nodeData.NodeType++;
            ChangeItemColor(_nodeData.NodeType);
        }

        private void ChangeItemColor(NodeType type)
        {
            switch (type)
            {
                case NodeType.Normal:
                    _itemImg.color=Color.white;
                    break;
                case NodeType.Start:
                    _itemImg.color= Color.red;
                    break;
                case NodeType.End:
                    _itemImg.color= Color.green;
                    break;
                case NodeType.Obstacle:
                    _itemImg.color= Color.black;
                    break;
            }
        }
    }
}