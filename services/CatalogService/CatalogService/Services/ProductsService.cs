using CatalogService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CatalogService.Services;

public class ProductsService
{
    private readonly IMongoCollection<Product> _productsCollection;

    public ProductsService(IOptions<CatalogDBSettings> catalogDBSettings)
    {
        MongoClient mongoClient = new(catalogDBSettings.Value.ConnectionString);
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(catalogDBSettings.Value.DatabaseName);
        _productsCollection = mongoDatabase.GetCollection<Product>(catalogDBSettings.Value.Collections.Products);
    }

    public async Task<List<Product>> GetAsync() => await _productsCollection.Find(_ => true).ToListAsync();

}
