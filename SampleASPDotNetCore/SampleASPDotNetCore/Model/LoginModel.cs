using System.ComponentModel.DataAnnotations;

namespace SampleASPDotNetCore.Model
{
    public class LoginModel
    {
       [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string UserID { get; set; }
    }
}
