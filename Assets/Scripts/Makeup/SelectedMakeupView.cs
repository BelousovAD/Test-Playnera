namespace Makeup
{
    internal class SelectedMakeupView : MakeupImageView
    {
        private void OnEnable()
        {
            Makeup.Selected += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            Makeup.Selected -= UpdateView;

        private void UpdateView()
        {
            Image.color = InvisibleColor;
            Image.sprite = Makeup.Selection != Makeup.MinIndex ? Sprites[Makeup.Selection] : DefaultSprite;
        }
    }
}