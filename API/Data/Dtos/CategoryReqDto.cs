using System;

namespace API.Data.Dtos;

public class CategoryReqDto
{

    public required string Name { get; set; }
    public string? Description { get; set; } = null;

}
