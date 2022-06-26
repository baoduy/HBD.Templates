using System.Text.Json.Serialization;

namespace MediatR.AppServices.Share;

public class BaseCommand
{
    [JsonIgnore] public string? UserId { get; set; }    
}