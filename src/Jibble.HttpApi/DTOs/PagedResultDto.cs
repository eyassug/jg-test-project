using System.Collections.Generic;

namespace Jibble.DTOs
{
    public class PagedResultDto<T>
    {
        public long TotalCount { get; }
        public IReadOnlyList<T> Items { get; }

        /// <summary>
        /// Creates a new <see cref="PagedResultDto{T}"/> object.
        /// </summary>
        /// <param name="totalCount">Total count of Items</param>
        /// <param name="items">List of items in current page</param>

        public PagedResultDto(long totalCount, IReadOnlyList<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }
    }
}
