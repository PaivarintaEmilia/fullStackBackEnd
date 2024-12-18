using System;

namespace API.Tools.Interfaces;

public interface IGetIdTool
{
    Task<int?> GetIdFromToken(HttpContext httpContext);
}
