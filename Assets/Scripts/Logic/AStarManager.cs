using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace Logic
{
    public class AStarManager : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private Button _findPathBtn;
        private void Awake()
        {
            _findPathBtn.onClick.AddListener(PathFinder);
            GridData.Create();
            GridData.Instance.CreateGrid(10, 10);
            GridView.Instance.LoadGrid(_root);
        }

        private void PathFinder()
        {
            
        }
    }
}