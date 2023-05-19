using System.ComponentModel.DataAnnotations;

namespace MrWorldwide.WebApi.Features.Authorization.Domain;

public class TokenRequest
{
    [Required]
    public string IdToken { get; set; }
}