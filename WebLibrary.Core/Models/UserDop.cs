using System.ComponentModel.DataAnnotations;


namespace WebLibrary.Core.Models
{
    

    public class UserDop : UserS
    {
        public string UserName { get; set; }

        [Required]
        public enumRole Role { get; set; }
    }
}
