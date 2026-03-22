using System;

namespace Book
{
    internal class Book
    {
        public const int MinIndex = 0;
        
        public Book(int pageCount)
        {
            if (pageCount < MinIndex + 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageCount), $"Can not be less than {MinIndex + 1}");
            }

            MaxIndex = pageCount - 1;
        }
        
        public event Action PageChanged;
        
        public int CurrentPage { get; private set; }
        
        public int MaxIndex { get; }

        public void Next()
        {
            if (CurrentPage < MaxIndex)
            {
                CurrentPage++;
                PageChanged?.Invoke();
            }
        }

        public void Back()
        {
            if (CurrentPage > MinIndex)
            {
                CurrentPage--;
                PageChanged?.Invoke();
            }
        }
    }
}