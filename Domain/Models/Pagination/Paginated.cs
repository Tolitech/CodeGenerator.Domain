using System;
using System.Collections.Generic;

namespace Tolitech.CodeGenerator.Domain.Models.Pagination
{
    public class Paginated<T>
    {
        private PaginatedList<T> _items;

        public Paginated(PaginatedList<T> items, int maxPages)
        {
            _items = items;
            PageStart = 1;
            PageStop = 1;
            PaginationRange = new List<int>();

            if (items.PageNumber <= maxPages - (maxPages / 2) + 1)
            {
                PageStart = 1;
            }
            else if (items.PageNumber >= items.PageCount - (maxPages / 2))
            {
                PageStart = items.PageCount - maxPages + 1;
            }
            else
            {
                PageStart = items.PageNumber - (maxPages / 2);
            }

            if (PageStart < 1)
                PageStart = 1;

            PageStop = Math.Min(items.PageCount, PageStart + maxPages - 1);

            for (int i = PageStart; i <= PageStop; i++)
            {
                PaginationRange.Add(i);
            }

            FirstItemOnPage = ((PageNumber - 1) * maxPages) + 1;
            LastItemOnPage = (FirstItemOnPage - 1) + maxPages;

            if (LastItemOnPage > TotalItemCount)
                LastItemOnPage = TotalItemCount;
        }

        public bool HasNextPage { get { return _items.HasNextPage; } }

        public bool HasPreviousPage { get { return _items.HasPreviousPage; } }

        public bool IsFirstPage { get { return _items.IsFirstPage; } }

        public bool IsLastPage { get { return _items.IsLastPage; } }

        public int PageCount { get { return _items.PageCount; } }

        public int PageNumber { get { return _items.PageNumber; } }

        public int PageSize { get { return _items.PageSize; } }

        public int TotalItemCount { get { return _items.TotalItemCount; } }

        public int PageStart { get; }

        public int PageStop { get; }

        public int FirstItemOnPage { get; }

        public int LastItemOnPage { get; }

        public IList<int> PaginationRange { get; }
    }
}
