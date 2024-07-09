
using System.ComponentModel.DataAnnotations;


namespace WebLibrary.Core.Models
{
    public class UserS
    {
        [Required]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }

    }
}
