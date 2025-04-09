namespace Business.Models;

public class ServiceResult
{
    public bool Succeded { get; set; }

    public int StatusCode { get; set; }

    public string? Error { get; set; }
}

