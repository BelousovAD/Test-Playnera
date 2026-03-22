using Reflex.Core;
using UnityEngine;

namespace Makeup
{
    internal class MakeupInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.RegisterValue(new Makeup(MakeupType.Blush));
            builder.RegisterValue(new Makeup(MakeupType.Cream));
            builder.RegisterValue(new Makeup(MakeupType.Lipstick));
            builder.RegisterValue(new Makeup(MakeupType.Shadow));
        }
    }
}