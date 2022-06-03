using Microsoft.AspNetCore.Http;

namespace Rest.Api.Models;

public class ProblemDetail
{
    public string Token { get; set; }
    public int StatusCode { get; set; }
    public PathString RequestPath { get; set; }
    public string Message { get; set; }
}
