using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Book
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(PageButton))]
    internal class PageButtonView : MonoBehaviour
    {
        private Button _button;
        private PageButton _pageButton;
        private Book _book;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _pageButton = GetComponent<PageButton>();
        }

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
            if (_pageButton.ToNext)
            {
                _button.interactable = _book.MaxIndex > _book.CurrentPage;
            }
            else
            {
                _button.interactable = Book.MinIndex < _book.CurrentPage;
            }
        }
    }
}