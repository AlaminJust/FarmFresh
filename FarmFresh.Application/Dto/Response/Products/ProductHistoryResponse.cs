namespace FarmFresh.Application.Dto.Response.Products;

public interface IProductHistoryResult
{
    public DateTime Date { get; set; }
    public float Point { get; set; }
}

public class ProductHistoryResult : IProductHistoryResult
{
    public DateTime Date { get; set; }
    public float Point { get; set; }

    public ProductHistoryResult(DateTime date, float point)
    {
        Date = date;
        Point = point;
    }
}

public interface IProductHistoryResponse
{
    IList<IProductHistoryResult> PriceHistory { get; set; }
    IList<IProductHistoryResult> BuyHistory { get; set; }
}

public class ProductHistoryResponse: IProductHistoryResponse
{
    public IList<IProductHistoryResult> PriceHistory { get; set; }
    public IList<IProductHistoryResult> BuyHistory { get; set; }
    
    public ProductHistoryResponse()
    {
        PriceHistory = new List<IProductHistoryResult>();
        BuyHistory = new List<IProductHistoryResult>();
    }
}