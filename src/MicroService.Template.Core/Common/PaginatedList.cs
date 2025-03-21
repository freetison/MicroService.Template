namespace MicroService.Template.Core.Common
{
    using Microsoft.EntityFrameworkCore;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PaginatedList<T>
    {
        /// <summary>
        /// Gets the Items.
        /// </summary>
        public List<T> Items { get; }

        /// <summary>
        /// Gets the PageNumber.
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// Gets the TotalPages.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Gets the TotalCount.
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
        /// </summary>
        /// <param name="items">The items<see cref="List{T}"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <param name="pageNumber">The pageNumber<see cref="int"/>.</param>
        /// <param name="pageSize">The pageSize<see cref="int"/>.</param>
        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }

        /// <summary>
        /// Gets a value indicating whether HasPreviousPage.
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Gets a value indicating whether HasNextPage.
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;

        /// <summary>
        /// The CreateAsync.
        /// </summary>
        /// <param name="source">The source<see cref="IQueryable{T}"/>.</param>
        /// <param name="pageNumber">The pageNumber<see cref="int"/>.</param>
        /// <param name="pageSize">The pageSize<see cref="int"/>.</param>
        /// <returns>The Task.</returns>
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
