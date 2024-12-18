using System;
using API.Data.Dtos.Categories;
using API.Models;

namespace API.Services.Interfaces;

public interface ICategoriesService
{
    // Get categories
    Task<List<Categories>> GetCategories(int userId);

    // Create a cateogry

    // Update a category

    // Delete a category

}
