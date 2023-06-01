using AutoMapper;
using FarmFresh.Application.Dto.Request.Products;
using FarmFresh.Application.Dto.Response.Products;
using FarmFresh.Application.Enums;
using FarmFresh.Application.Interfaces.Services.Products;
using FarmFresh.Domain.Entities.Products;
using FarmFresh.Domain.RepoInterfaces.Products;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Infrastructure.Service.Services.Products;

public class ProductHistoryService: IProductHistoryService
{
    private readonly IProductHistoryRepository _productHistoryRepository;
    private readonly IMapper _mapper;

    public ProductHistoryService(
            IProductHistoryRepository productHistoryRepository,
            IMapper mapper
        )
    {
        _productHistoryRepository = productHistoryRepository;
        _mapper = mapper;
    }

    #region Get
  
    public async Task<ProductHistoryResponse> GetHistoryByDateRange(int productId, int dateRange, int column = 30)
    {
        var response = new ProductHistoryResponse();
        var startDate = DateTime.Today.AddDays(-dateRange * column);
        var endDate = DateTime.Today;

        var productHistory = await _productHistoryRepository.GetByConditionAsync(x =>
                x.ProductId == productId && x.CreatedOn >= startDate && x.CreatedOn <= endDate)
               .ConfigureAwait(false);

        var columnHeaders = Enumerable.Range(0, column)
            .Select(offset => new DateRange(startDate.AddDays(offset * dateRange), startDate.AddDays((offset + 1) * dateRange)))
            .ToList();

        var groupedHistory = productHistory.GroupBy(x => x.HistoryType);

        foreach (var group in groupedHistory)
        {
            var historiesByType = group.ToList();

            foreach (var columnHeader in columnHeaders)
            {
                var historiesForDate = historiesByType
                    .Where(h => h.CreatedOn.Date >= columnHeader.StartDate && h.CreatedOn.Date < columnHeader.EndDate)
                    .ToList();

                switch (group.Key)
                {
                    case ProductHistoryType.Buy:
                        var buySum = historiesForDate.Sum(h => h.Point);
                        response.BuyHistory.Add(new ProductHistoryResult(columnHeader.StartDate, buySum));
                        break;

                    case ProductHistoryType.PriceChange:
                        var priceAvg = historiesForDate.Average(h => h.Point);
                        response.PriceHistory.Add(new ProductHistoryResult(columnHeader.StartDate, priceAvg));
                        break;

                    case ProductHistoryType.Review:
                        // Handle Review history type
                        break;

                    case ProductHistoryType.AddToFavourite:
                        // Handle AddToFavourite history type
                        break;
                }
            }
        }

        return response;
    }
    
    #endregion Get

    #region Save
    public async Task AddAsync(ProductHistoryRequest request, int updatedBy)
    {
        var productHistory = _mapper.Map<ProductHistory>(request);
        productHistory.CreatedOn = DateTime.UtcNow;
        productHistory.UpdateBy = updatedBy;

        await _productHistoryRepository.AddAsync(productHistory);
        await _productHistoryRepository.SaveChangesAsync();
    }
    #endregion Save

    
}
internal interface IDateRange
{
    DateTime StartDate { get; set; }
    DateTime EndDate { get; set; }
}

internal class DateRange : IDateRange
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public DateRange(DateTime startDate, DateTime endDate)
    {
        this.StartDate = startDate;
        this.EndDate = endDate;
    }
}