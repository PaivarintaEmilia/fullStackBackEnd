using System;
using API.Data.Dtos.Categories;
using API.Models;

namespace API.Services.Interfaces;

public interface ICategoriesService
{
    // Get categories
    Task<List<Categories>> GetCategories(int userId);

    // Create a cateogry
    Task<Categories> CreateCategory(CategoriesReqDto req, int userId);

    // Update a category
    Task<CategoriesResDto?> UpdateCategory(CategoriesReqDto req, int id);

    // Delete a category
    Task<string> DeleteCategory(int id);

}
