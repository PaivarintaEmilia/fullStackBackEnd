using System;
using API.Models;

namespace API.Services.Interfaces;

public interface IProductService
{
    // Tämä sisältää vain tietotyypit, parametrit ja metodien nimet

    // Kaikkien tuotteiden hakua varten
    Task<List<Product>> GetAll();

    // Tuotteiden hakemiseen kategorian perusteella
    Task<List<Product>> GetByCategory(int categoryId);

}
