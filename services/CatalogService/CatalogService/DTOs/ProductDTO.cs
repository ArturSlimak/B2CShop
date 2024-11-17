namespace CatalogService.DTOs;

public class ProductDTO
{

    public class Index
    {
        public string? Id { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }

    }

    public class Mutate
    {
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
