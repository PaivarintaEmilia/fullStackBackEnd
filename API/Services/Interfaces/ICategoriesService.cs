using System;
using API.Data.Dtos.Categories;
using API.Models;

namespace API.Services.Interfaces;

public interface ICategoriesService
{
    // Get categories
    Task<List<Categories>> GetCategories(int userId);

    // Create a cateogry
    Task<Categories> CreateCategory(CategoriesResDto req, int userId);

    // Update a category
    Task<CategoriesResDto?> UpdateCategory(CategoriesResDto req, int id);

    // Delete a category
    Task<string> DeleteCategory(int id);

}
