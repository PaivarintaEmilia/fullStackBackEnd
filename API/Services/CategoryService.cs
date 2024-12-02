using System;
using API.Data;
using API.Data.Dtos;
using API.Models;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class CategoryService(DataContext context) : ICategoryService
{
    // Uuden kategorian lisääminen
    public async Task<Category> Create(CategoryReqDto req, int userId)
    {
        // Käyttäjän id eli userId tulisi saada tokenin mukana kun on kirjautunut


        // Luodaan uusi category
        var category = new Category
        {
            // Id tulisi lisätä automaattisesti SqLite tietokantaan. Tarvitaanko tätä kohtaa?
            Name = req.Name,
            Description = req.Description,
            UserId = userId
        };

        // Lisätään uudet tiedot tietokantaan
        context.Categories.Add(category);
        await context.SaveChangesAsync();

        return category;
    }


    // Kategorian muokkaaminen
    public async Task<CategoryResDto?> Update(CategoryReqDto req, int id)
    {
        // Haetaan category
        var category = await context.Categories.FirstOrDefaultAsync(category => category.Id == id);

        // Muokataan tarvittavat tiedot, jos category ei ole null
        if (category != null)
        {
            category.Name = req.Name;
            category.Description = req.Description;
            // Tallennetaan muutokset
            await context.SaveChangesAsync();
            // Muunnetaan Category -> CategoryResDto
            // En käyttänyt tähän mapperia, koska olen harjoitellut sitä jo ja halusin käyttää vaihtoehtoista tapaa, jotta sekin jää ylös muistiin jatkoa varten. 
            return new CategoryResDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                UserId = category.UserId
            };
        }

        return null;

    }

    public async Task<string?> Delete(int id)
    {
        // Kategorian etsiminen id:llä
        var category = await context.Categories.FirstOrDefaultAsync(category => category.Id == id);

        // Jos kategoriaa ei löyty
        if (category == null) {
            return "Ei löytynyt kategoriaa tällä id:llä. Poisto epäonnistunut.";
        };

        // Jos löytyy niin poistetaan kategoria
        context.Categories.Remove(category);

        // Tallennetaan muutokset
        await context.SaveChangesAsync();

        // Palautetaan viesti onnistuneesta poistosta
        return "Poisto onnistui.";
    }
}
