using FarmFresh.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Application.Models.Paginations
{
    public class PaginationResponse<T>
    {
        public Int32 PageNumber { get; set; }
        public Int32 PageSize { get; set; }
        public Int32 TotalPages { get; set; }
        public Int32 TotalRecords { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public List<T> Items { get; set; }
        public PaginationResponse()
        {

        }
        public PaginationResponse(List<T> items, Int32 count, Int32 pageNumber, Int32 pageSize, string sortBy, string sortOrder)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalRecords = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            SortBy = sortBy;
            SortOrder = sortOrder;
            Items = items;
        }
        public static Task<PaginationResponse<T>> CreateAsync(IQueryable<T> source, PaginationRequest paginationRequest)
        {
            var count = source.Count();
            var items = source.Skip(paginationRequest.Offset).Take(paginationRequest.PageSize);
            items = items.OrderBy(paginationRequest.SortBy, paginationRequest.SortOrder);
            return Task.FromResult(new PaginationResponse<T>(items.ToList(), count, paginationRequest.PageNumber, paginationRequest.PageSize, paginationRequest.SortBy, paginationRequest.SortOrder));
        }
    }
}
