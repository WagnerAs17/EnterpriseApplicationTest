using AutoMapper;
using System.Collections.Generic;
using Volvo.Frota.API.Dtos.Pagination;

namespace Volvo.Frota.API.Utils.Pagination
{
    public static class PagedListExtensions
    {
        public static PaginationResultDto<TDestination> ToResult<TSource, TDestination>(this PagedList<TSource> pagedList, IMapper mapper)
        {
            return new PaginationResultDto<TDestination>
            {
                HasNextPage = pagedList.HasNextPage,
                HasPreviousPage = pagedList.HasPreviousPage,
                Data = mapper.Map<IReadOnlyCollection<TDestination>>(pagedList.Data),
                NextPageNumber = pagedList.NextPageNumber,
                PreviousPageNumber = pagedList.PreviousPageNumber,
                Page = pagedList.Page,
                PerPage = pagedList.PerPage,
                TotalFiltered = pagedList.TotalFiltered,
                TotalItems = pagedList.TotalItems,
                TotalPages = pagedList.TotalPages
            };
        }
    }
}
