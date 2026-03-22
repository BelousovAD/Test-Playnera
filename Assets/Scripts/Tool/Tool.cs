using System;

namespace Tool
{
    internal class Tool
    {
        public event Action Changed;
        
        public ToolType Type { get; private set; }

        public void Select(ToolType type)
        {
            Type = type;
            Changed?.Invoke();
        }
    }
}