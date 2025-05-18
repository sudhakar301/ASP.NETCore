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
        public string Role { get; set; }

        public LoginModel()
        {
            
        }
        public List<LoginModel> FetchUserDetails()
        {
            return new List<LoginModel>()
            {
                 new LoginModel { UserName = "string", EmailAddress = "sudhakar301@gmail.com",UserID="301472" ,Role="Admin"},
                 new LoginModel { UserName = "Rao", EmailAddress = "rao@gmail.com",UserID="301472" ,Role="Developer"},
                 new LoginModel { UserName = "Tumma", EmailAddress = "tumma@gmail.com",UserID="301472" ,Role="Developer"},
            };
           
        }
    }
}
