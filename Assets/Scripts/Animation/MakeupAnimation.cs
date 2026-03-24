using UnityEngine;

namespace Animation
{
    public abstract class MakeupAnimation : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        protected CanvasGroup CanvasGroup => _canvasGroup;
        
        public abstract void ApplyMakeup();
    }
}