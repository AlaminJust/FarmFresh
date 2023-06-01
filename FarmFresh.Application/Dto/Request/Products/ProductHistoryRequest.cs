using FarmFresh.Application.Enums;

namespace FarmFresh.Application.Dto.Request.Products;

public class ProductHistoryRequest
{
    public int ProductId { get; set; }

    public float Point { get; set; }
    
    public int UpdateBy { get; set; }
    
    public ProductHistoryType HistoryType { get; set; }
}