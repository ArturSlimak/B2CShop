namespace CatalogService.DTOs;

public class ProductDTO
{

    public class Index
    {
        public string? Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

    }

    public class Mutate
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
