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

    // Create category
    public async Task<Categories> CreateCategory(CategoriesResDto req, int userId)
    {
        var newCategory = new Categories
        {
            Name = req.Name,
            UserDefined = true,
            UserId = userId
        };

        context.Categories.Add(newCategory);

        await context.SaveChangesAsync();

        return newCategory;


    }

    // Update category
    public async Task<CategoriesResDto?> UpdateCategory(CategoriesResDto req, int id)
    {
        var category = await context.Categories.FirstOrDefaultAsync(category => category.Id == id);

        if (category != null)
        {
            category.Name = req.Name;

            await context.SaveChangesAsync();

            return new CategoriesResDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        return null;
    }

    // Delete Category
    public async Task<string> DeleteCategory(int id)
    {
        var category = await context.Categories.FirstOrDefaultAsync(category => category.Id == id);

        if (category == null)
        {
            return "No category found with this id.";
        }

        context.Categories.Remove(category);

        await context.SaveChangesAsync();

        return "Category deleted.";
    }
}
