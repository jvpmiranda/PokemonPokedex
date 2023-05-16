﻿using System.Text.Json;

namespace ApiPokedex.Contract;

public class Error
{
    public int Status { get; set; }
    public string Message { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}