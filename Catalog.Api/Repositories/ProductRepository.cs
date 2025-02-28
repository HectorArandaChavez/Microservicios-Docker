﻿using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogService catalogService;

        public ProductRepository(ICatalogService catalogService)
        {
            this.catalogService = catalogService;
        }

        // Expresion body because is only one line
        // public async Task CreateProduct(Product product) => await catalogService.Products.InsertOneAsync(product);
        public async Task CreateProduct(Product product)
        {
            await catalogService.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await catalogService.Products.DeleteOneAsync(filter); 

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            return await catalogService.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await catalogService.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await catalogService.Products.ReplaceOneAsync(filter: p => p.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
