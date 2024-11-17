using CatalogService.DTOs;

namespace CatalogService.Helpers;

public class ProductRequest
{
    public class Index
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }

    public class Create
    {
        public required ProductDTO.Mutate Product { get; set; }

    }
}
