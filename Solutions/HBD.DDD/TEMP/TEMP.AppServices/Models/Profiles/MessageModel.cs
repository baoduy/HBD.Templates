using System.ComponentModel.DataAnnotations;

namespace TEMP.AppServices.Models.Profiles;

public class MessageModel
{
    [Required]
    public string Message { get; set; }
}