using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    [RequireComponent(typeof(Button))]
    public abstract class AbstractButton : MonoBehaviour
    {
        protected Button Button { get; private set; }

        protected virtual void Awake() =>
            Button = GetComponent<Button>();

        protected virtual void OnEnable() =>
            Button.onClick.AddListener(HandleClick);

        protected virtual void OnDisable() =>
            Button.onClick.RemoveListener(HandleClick);

        protected abstract void HandleClick();
    }
}