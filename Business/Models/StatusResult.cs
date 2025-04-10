using Domain.Models;

namespace Business.Models;


public class ServiceResult
{
    public bool Succeeded { get; set; }

    public int statusCode { get; set; }

    public string? Error { get; set; }
}

public class StatusResult : ServiceResult
{
    public IEnumerable<Status>? Result { get; set; }
}

public class StatusResult<T> : ServiceResult
{
    public IEnumerable<Client>? Result { get; set; }
}
