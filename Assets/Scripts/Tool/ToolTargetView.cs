using System;
using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

namespace Tool
{
    internal class ToolTargetView : MonoBehaviour
    {
        [SerializeField] private List<ToolTargetPair> _pairs = new ();

        private Tool _tool;

        [Inject]
        private void Initialize(Tool tool) =>
            _tool = tool;

        private void OnEnable()
        {
            _tool.Changed += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _tool.Changed -= UpdateView;

        private void UpdateView()
        {
            foreach (ToolTargetPair pair in _pairs)
            {
                pair.Target.SetActive(pair.Type == _tool.Type);
            }
        }

        [Serializable]
        private struct ToolTargetPair
        {
            public ToolType Type;
            public GameObject Target;
        }
    }
}