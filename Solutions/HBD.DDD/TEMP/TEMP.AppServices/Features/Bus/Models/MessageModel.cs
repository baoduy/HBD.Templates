using System.ComponentModel.DataAnnotations;

namespace TEMP.AppServices.Features.Bus.Models;

public class MessageModel
{
    [Required] public string Message { get; set; } = default!;
}