using Common;
using Reflex.Attributes;
using UnityEngine;

namespace Tool
{
    internal class ToolButton : AbstractButton
    {
        [SerializeField] private ToolType _type;

        private Tool _tool;

        [Inject]
        private void Initialize(Tool tool) =>
            _tool = tool;
        
        protected override void HandleClick() =>
            _tool.Select(_type);
    }
}