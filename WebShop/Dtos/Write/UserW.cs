

using System.ComponentModel.DataAnnotations;

namespace WebShop.Dtos.Write
{
    public class UserW
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public string password { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }

        public IFormFile? avatar { get; set; }
    }
}
