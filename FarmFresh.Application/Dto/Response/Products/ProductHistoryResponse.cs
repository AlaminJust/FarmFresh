namespace FarmFresh.Application.Dto.Response.Products;

public interface IProductHistoryResult
{
    public string Date { get; }
    public float Point { get; set; }
}

public class ProductHistoryResult : IProductHistoryResult
{
    private DateTime _date { get; set; }
    public float Point { get; set; }

    public ProductHistoryResult(DateTime date, float point)
    {
        _date = date;
        Point = point;
    }

    public string Date
    {
        get
        {
            return _date.ToString("yyyy-MM-dd");
        }
    }
}

public interface IProductHistoryResponse
{
    IList<IProductHistoryResult> PriceHistory { get; set; }
    IList<IProductHistoryResult> BuyHistory { get; set; }
}

public class ProductHistoryResponse : IProductHistoryResponse
{
    public IList<IProductHistoryResult> PriceHistory { get; set; }
    public IList<IProductHistoryResult> BuyHistory { get; set; }

    public ProductHistoryResponse()
    {
        PriceHistory = new List<IProductHistoryResult>();
        BuyHistory = new List<IProductHistoryResult>();
    }
}