using System;
using System.Collections.Generic;

namespace Volvo.Frota.API.Utils.Pagination
{
    public class PagedList<T>
    {
        public PagedList(int totalItems, int totalFiltered, int page, int perPage, List<T> data)
        {
            this.TotalItems = totalItems;
            this.TotalFiltered = totalFiltered;
            this.Page = page;
            this.PerPage = perPage;
            this.Data = data;
        }

        public int TotalItems { get; set; }
        public int TotalFiltered { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public IReadOnlyCollection<T> Data { get; set; }
        public int TotalPages => (int)Math.Ceiling(this.TotalItems / (double)this.PerPage);
        public bool HasPreviousPage => this.Page > 1;
        public bool HasNextPage => this.Page < this.TotalPages;
        public int NextPageNumber => this.HasNextPage ? this.Page + 1 : 0;
        public int PreviousPageNumber => this.HasPreviousPage ? this.Page - 1 : 0;
    }
}
