using System.Text.Json.Serialization;

namespace TEMP.AppServices.Share;

public abstract class ModelBase
{
    [JsonIgnore]public string? UserId { get; set; }
}