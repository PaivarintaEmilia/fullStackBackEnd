using System;

namespace API.Data.Dtos;

public class DateTimeReqDto
{
    public required DateTime StartingDate { get; set; }
    public required DateTime EndingDate { get; set; }

}
