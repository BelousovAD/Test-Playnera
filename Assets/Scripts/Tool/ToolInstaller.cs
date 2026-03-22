using Reflex.Core;
using UnityEngine;

namespace Tool
{
    internal class ToolInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder) =>
            builder.RegisterValue(new Tool());
    }
}