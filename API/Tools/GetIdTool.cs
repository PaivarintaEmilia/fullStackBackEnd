using System;
using System.Security.Claims;
using API.Tools.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Tools;

public class GetIdTool : IGetIdTool
{
    public Task<int?> GetIdFromToken(HttpContext httpContext)
    {
        var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier) ?? httpContext.User.FindFirst("sub");

        if (userIdClaim == null)
        {
            return Task.FromResult<int?>(null);
        }

        // Muutetaan id kokonaisluvuksi
        return Task.FromResult<int?>(int.Parse(userIdClaim.Value));
    }
}
