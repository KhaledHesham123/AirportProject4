using System.ComponentModel.DataAnnotations;

namespace AirportProject4.Project.core.DTOS.ViewModles
{
    public class RigsterviewModler
    {
        [Required(ErrorMessage = "Email Is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName Is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password doesn't match Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Required To Agree")]
        public bool IsAgree { get; set; }
    }
}
