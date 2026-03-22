using Reflex.Core;
using UnityEngine;

namespace Book
{
    internal class BookInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField][Min(1)] private int _pageCount = 1;
        
        public void InstallBindings(ContainerBuilder builder) =>
            builder.RegisterValue(new Book(_pageCount));
    }
}