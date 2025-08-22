using System.ComponentModel.DataAnnotations;

namespace DemoProject.Entites
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }
    }
}
