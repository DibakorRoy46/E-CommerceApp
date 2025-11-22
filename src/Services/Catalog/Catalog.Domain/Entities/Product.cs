

using Catalog.Domain.Enums;

namespace Catalog.Domain.Entities;

public class Product :BaseEntity
{
    public string Description { get;private set; }
    public ProductUnitEnum UnitId { get;private set; }
    public decimal Weight { get;private set; }
    public string ImageUrl { get;private set; }
    public int BrandId { get;private set; }
    public Brand? Brand { get;private set; }
    public string BarCode { get;private set; }
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }


    private Product()
    {

    }

    public Product(string name, string code, string description, ProductUnitEnum unitId, decimal weight, string imageUrl,
                    int brandId,string barCode, string createdBy, decimal price)
    {
        Name = name;
        Code = code;
        Description = description;
        UnitId = unitId;
        Weight = weight;
        ImageUrl = imageUrl;
        BrandId = brandId;
        Status = StatusEnum.Initiate;
        BarCode = barCode;
        SetCreated(createdBy);
        Price = price;
        StockQuantity = 0;
    }

    public void Update(string name, string code, string description, ProductUnitEnum unitId, decimal weight, string imageUrl,
                    int brandId, string barCode, StatusEnum status, string modifiedBy,decimal price, int stockQuantity)
    {
        Name = name;
        Code = code;
        Description = description;
        UnitId = unitId;
        Weight = weight;
        ImageUrl = imageUrl;
        BrandId = brandId;
        BarCode = barCode;
        Status = status;
        SetModified(modifiedBy);
        Price = price;
        StockQuantity = stockQuantity;
    }
}
