using System;

namespace API.Data.Dtos;

public class CategoryResDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; } = null;
    public required int? UserId { get; set; }

}
