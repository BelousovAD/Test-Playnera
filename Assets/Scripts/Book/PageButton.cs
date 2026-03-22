using Common;
using Reflex.Attributes;
using UnityEngine;

namespace Book
{
    internal class PageButton : AbstractButton
    {
        [SerializeField] private bool _toNext;

        private Book _book;

        public bool ToNext => _toNext;

        [Inject]
        private void Initialize(Book book) =>
            _book = book;

        protected override void HandleClick()
        {
            if (_toNext)
            {
                _book.Next();
            }
            else
            {
                _book.Back();
            }
        }
    }
}