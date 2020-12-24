namespace Volvo.Frota.API.Dtos.Pagination
{
    public class PaginationFilterDto
    {
        public int PerPage { get; set; }
        public int Page { get; set; }
        public string Search { get; set; }
        public string OrderBy { get; set; }
        public string OrderByDirection { get; set; }
    }
}
