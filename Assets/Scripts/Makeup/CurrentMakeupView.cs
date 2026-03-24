namespace Makeup
{
    internal class CurrentMakeupView : MakeupImageView
    {
        private void OnEnable()
        {
            Makeup.Applied += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            Makeup.Applied -= UpdateView;

        private void UpdateView()
        {
            Image.sprite = Makeup.Current != Makeup.MinIndex ? Sprites[Makeup.Current] : DefaultSprite;
            Image.color = Image.sprite ? VisibleColor : InvisibleColor;
        }
    }
}