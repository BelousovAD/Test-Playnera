using System;

namespace Makeup
{
    public class Makeup
    {
        public const int MinIndex = -1;
        
        public Makeup(MakeupType type) =>
            Type = type;

        public event Action Applied;
        
        public event Action Selected;

        public MakeupType Type { get; }

        public int Current { get; private set; } = MinIndex;

        public int Selection { get; private set; } = MinIndex;

        public void Apply()
        {
            if (Selection == MinIndex)
            {
                throw new InvalidOperationException($"Can not apply {nameof(Selection)}:{Selection}");
            }

            Current = Selection;
            Applied?.Invoke();
            Select(MinIndex);
        }

        public void Clear()
        {
            Current = MinIndex;
            Applied?.Invoke();
        }

        public void Select(int index)
        {
            if (index < MinIndex)
            {
                throw new ArgumentOutOfRangeException(nameof(index), $"Can not be less than {MinIndex}");
            }

            Selection = index;
            Selected?.Invoke();
        }
    }
}