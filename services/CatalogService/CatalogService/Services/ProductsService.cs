using AutoMapper;
using CatalogService.DTOs;
using CatalogService.Helpers;
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

    public async Task<List<ProductDTO.Index>> GetAsync(ProductRequest.Index request)
    {
        var products = await _productsCollection.Find(_ => true)
            .Skip((request.Page - 1) * request.PageSize)
            .Limit(request.PageSize)
            .ToListAsync();
        var productDTOs = _mapper.Map<List<ProductDTO.Index>>(products);
        return productDTOs;
    }

    public async Task<Product> CreateAsync(ProductRequest.Create request)
    {
        var product = _mapper.Map<Product>(request.Product);
        await _productsCollection.InsertOneAsync(product);
        return product;
    }

}
