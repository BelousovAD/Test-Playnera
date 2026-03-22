using System.Collections.Generic;
using Common;
using Reflex.Attributes;
using UnityEngine;

namespace Makeup
{
    internal class MakeupButton : AbstractButton
    {
        [SerializeField] private MakeupType _type;
        [SerializeField][Min(0)] private int _index;

        private Makeup _makeup;

        [Inject]
        private void Initialize(IEnumerable<Makeup> makeups)
        {
            foreach (Makeup makeup in makeups)
            {
                if (makeup.Type == _type)
                {
                    _makeup = makeup;
                    break;
                }
            }
        }
        
        protected override void HandleClick() =>
            _makeup.Select(_index);
    }
}