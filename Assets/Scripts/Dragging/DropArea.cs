using Animation;
using UnityEngine;

namespace Dragging
{
    internal class DropArea : MonoBehaviour
    {
        [SerializeField] private MakeupAnimation _animation;

        public void PlayAnimation() =>
            _animation.ApplyMakeup();
    }
}