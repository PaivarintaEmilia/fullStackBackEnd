using System;
using API.Data;
using API.Data.Dtos.Categories;
using API.Models;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class CategoriesService(DataContext context) : ICategoriesService
{
    // Get categories
    public async Task<List<Categories>> GetCategories(int userId)
    {
        var categories = await context.Categories
            .Where(i => i.UserId == userId && i.UserDefined == false)
            .ToListAsync();

        return categories; // Palautetaan modelit, koska controllerissa tapahtuu mappaus
    }
}
