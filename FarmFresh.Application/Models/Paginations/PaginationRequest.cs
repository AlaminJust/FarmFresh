namespace FarmFresh.Application.Models.Paginations
{
    public class PaginationRequest
    {
        private const Int32 MaxPageSize = 50;
        private Int32 _PageNumber { get; set; } = 0;
        public Int32 PageNumber
        {
            get => _PageNumber;
            set => _PageNumber = (value > 0) ? value : 0;
        }

        private Int32 _PageSize = 10;
        public Int32 PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        private string _SortBy = "Id";
        public string SortBy
        {
            get => _SortBy;
            set => _SortBy = value;
        }

        private SortOrders _SortOrder = SortOrders.Ascending;
        public string SortOrder
        {
            get => _SortOrder == SortOrders.Ascending ? "asc" : "desc";
            set => _SortOrder = value == "asc" ? SortOrders.Ascending : SortOrders.Descending;
        }
        public Int32 Offset
        {
            get
            {
                return PageNumber * PageSize;
            }
        }
    }

    public enum SortOrders
    {
        Ascending,
        Descending
    }
}
