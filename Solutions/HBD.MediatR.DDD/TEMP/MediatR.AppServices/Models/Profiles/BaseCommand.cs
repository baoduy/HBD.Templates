using System.Text.Json.Serialization;

namespace MediatR.AppServices.Models.Profiles;

public class BaseCommand
{
    [JsonIgnore] public string UserId { get; internal set; }    
}