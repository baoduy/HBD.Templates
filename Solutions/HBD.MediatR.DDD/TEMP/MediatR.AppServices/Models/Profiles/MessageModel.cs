using System.ComponentModel.DataAnnotations;

namespace MediatR.AppServices.Models.Profiles;

public class MessageModel
{
    [Required]
    public string Message { get; set; }
}