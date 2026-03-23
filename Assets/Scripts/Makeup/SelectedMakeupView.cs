using UnityEngine;

namespace Makeup
{
    internal class SelectedMakeupView : MakeupImageView
    {
        [SerializeField] private bool _isVisible = true;
        
        private void OnEnable()
        {
            Makeup.Selected += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            Makeup.Selected -= UpdateView;

        private void UpdateView()
        {
            if (Makeup.Selection != Makeup.MinIndex)
            {
                Image.sprite = Sprites[Makeup.Current];
                Image.color = _isVisible ? VisibleColor : InvisibleColor;
            }
            else
            {
                Image.color = _isVisible && DefaultSprite is not null ? VisibleColor : InvisibleColor;
                Image.sprite = DefaultSprite;
            }
        }
    }
}