using System.Text.Json.Serialization;
using HBD.Web.Models;

namespace MediatR.AppServices.Abstracts;

public abstract class ModelBase : Model
{
    [JsonIgnore] public string UserId { get; internal set; }
}