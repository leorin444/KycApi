using System.ComponentModel.DataAnnotations;

namespace KycApi.Models
{
    public class KycApplication
    {
        public int Id { get; set; }  // Auto-generated ID

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string CitizenshipNumber { get; set; }

        public string CitizenshipIssuedDate { get; set; }
    }
}
