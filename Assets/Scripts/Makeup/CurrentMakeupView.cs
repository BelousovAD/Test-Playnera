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
            if (Makeup.Current != Makeup.MinIndex)
            {
                Image.sprite = Sprites[Makeup.Current];
                Image.color = VisibleColor;
            }
            else
            {
                Image.color = DefaultSprite is not null ? VisibleColor : InvisibleColor;
                Image.sprite = DefaultSprite;
            }
        }
    }
}