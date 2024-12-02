using System;
using API.Data;
using API.Models;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class ProductService(DataContext context) : IProductService
{
    // Täällä hoidetaann tietokantayhteydet

    // Kaikkien tuotteiden hakua varten
    public async Task<List<Product>> GetAll()
    {
        var products = await context.Products.ToListAsync();
        return products;
    }

    // Tuotteiden hakuun kategorian mukaan
    public async Task<List<Product>> GetByCategory(int categoryId)
    {
        var products = await context.Products
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
        return products;
    }


}
