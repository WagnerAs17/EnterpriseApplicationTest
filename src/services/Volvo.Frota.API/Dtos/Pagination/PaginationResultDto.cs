using System.Collections.Generic;

namespace Volvo.Frota.API.Dtos.Pagination
{
    public class PaginationResultDto<T>
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int TotalFiltered { get; set; }
        public int PerPage { get; set; }
        public int Page { get; set; }
        public IReadOnlyCollection<T> Data { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public int NextPageNumber { get; set; }
        public int PreviousPageNumber { get; set; }
    }
}
