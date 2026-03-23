using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Makeup
{
    [RequireComponent(typeof(Image))]
    internal class MakeupImageView : MonoBehaviour
    {
        protected static readonly Color VisibleColor = Color.white;
        protected static readonly Color InvisibleColor = new (1f, 1f, 1f, 0f);
        
        [SerializeField] private MakeupType _type;
        [SerializeField] private Sprite _defaultSprite;
        [SerializeField] private List<Sprite> _sprites = new ();

        protected Sprite DefaultSprite => _defaultSprite;
        
        protected Image Image { get; private set; }
        
        protected Makeup Makeup { get; private set; }

        protected IReadOnlyList<Sprite> Sprites => _sprites;

        [Inject]
        private void Initialize(IEnumerable<Makeup> makeups)
        {
            foreach (Makeup makeup in makeups)
            {
                if (makeup.Type == _type)
                {
                    Makeup = makeup;
                    break;
                }
            }
        }

        private void Awake() =>
            Image = GetComponent<Image>();
    }
}