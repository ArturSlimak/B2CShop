using AutoMapper;
using CatalogService.DTOs;
using CatalogService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CatalogService.Services;

public class ProductsService
{
    private readonly IMongoCollection<Product> _productsCollection;
    private readonly IMapper _mapper;

    public ProductsService(IOptions<CatalogDBSettings> catalogDBSettings, IMapper mapper)
    {
        MongoClient mongoClient = new(catalogDBSettings.Value.ConnectionString);
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(catalogDBSettings.Value.DatabaseName);
        _productsCollection = mongoDatabase.GetCollection<Product>(catalogDBSettings.Value.Collections.Products);
        _mapper = mapper;
    }

    public async Task<List<Product>> GetAsync() => await _productsCollection.Find(_ => true).ToListAsync();

    public async Task<Product> CreateAsync(ProductDTO.Create newProduct)
    {
        var product = _mapper.Map<Product>(newProduct);
        await _productsCollection.InsertOneAsync(product);
        return product;
    }

}
