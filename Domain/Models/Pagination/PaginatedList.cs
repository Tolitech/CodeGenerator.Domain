using System;
using System.Collections.Generic;

namespace Tolitech.CodeGenerator.Domain.Models.Pagination
{
    public class PaginatedList<T> : List<T>
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public int PageCount { get; private set; }

        public int TotalItemCount { get; private set; }

        public PaginatedList(IList<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            PageCount = (int)Math.Ceiling(count / (double)pageSize);
            TotalItemCount = count;

            AddRange(items);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < PageCount);
            }
        }

        public bool IsFirstPage
        {
            get
            {
                return (PageNumber <= 1);
            }
        }

        public bool IsLastPage
        {
            get
            {
                return (PageNumber >= PageCount);
            }
        }
    }
}