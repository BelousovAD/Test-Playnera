using System.Collections.Generic;
using Common;
using Reflex.Attributes;

namespace Makeup
{
    internal class ResetButton : AbstractButton
    {
        private List<Makeup> _makeups;

        [Inject]
        private void Initialize(IEnumerable<Makeup> makeups) =>
            _makeups = new List<Makeup>(makeups);
        
        protected override void HandleClick() =>
            _makeups.ForEach(makeup => makeup.Clear());
    }
}