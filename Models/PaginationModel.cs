using Data.Contracts.Pagination;

namespace Host.Models
{
    public class PaginationModel : IPaginationModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
