using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Dragging
{
    [RequireComponent(typeof(Image))]
    internal class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Canvas _canvas;
        private RectTransform _rectTransform;
        private Image _image;
        
        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _rectTransform = transform as RectTransform;
            _image = GetComponent<Image>();
        }

        public void OnBeginDrag(PointerEventData eventData) =>
            _image.raycastTarget = false;

        public void OnDrag(PointerEventData eventData) =>
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

        public void OnEndDrag(PointerEventData eventData)
        {
            _image.raycastTarget = true;
            RaycastResult raycast = eventData.pointerCurrentRaycast;

            if (raycast.gameObject.TryGetComponent(out DropArea dropArea))
            {
                dropArea.PlayAnimation();
            }
        }
    }
}