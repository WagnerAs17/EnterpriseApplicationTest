namespace Volvo.Frota.API.Utils.Pagination
{
    public class PaginationFilter
    {
        public int PerPage { get; set; }
        public int Page { get; set; }
        public string Search { get; set; }
        public string OrderBy { get; set; }
        public string OrderByDirection { get; set; }
    }
}
