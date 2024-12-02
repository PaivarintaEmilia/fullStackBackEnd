using System;
using API.Data.Dtos;
using API.Models;

namespace API.Services.Interfaces;

public interface ICategoryService
{
    // Categorian luominen
    Task<Category> Create(CategoryReqDto req, int userId);
    // Categorian muokkaaminen
    Task<CategoryResDto?> Update(CategoryReqDto req, int id);
    // Categorian poistamiseen
    Task<string?> Delete(int id);

}
