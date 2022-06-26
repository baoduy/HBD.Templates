using System.ComponentModel.DataAnnotations;

namespace MediatR.AppServices.Features.Bus.Models;

public class BusMessageModel
{
    [Required]
    public string Message { get; set; }
}