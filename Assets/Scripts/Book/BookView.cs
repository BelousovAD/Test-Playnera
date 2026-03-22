using System.Collections.Generic;
using Reflex.Attributes;
using UnityEngine;

namespace Book
{
    internal class BookView : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _pages = new ();

        private Book _book;

        [Inject]
        private void Initialize(Book book) =>
            _book = book;

        private void OnEnable()
        {
            _book.PageChanged += UpdateView;
            UpdateView();
        }

        private void OnDisable() =>
            _book.PageChanged -= UpdateView;

        private void UpdateView()
        {
            for (int i = 0; i < _pages.Count; i++)
            {
                _pages[i].SetActive(_book.CurrentPage == i);
            }
        }
    }
}