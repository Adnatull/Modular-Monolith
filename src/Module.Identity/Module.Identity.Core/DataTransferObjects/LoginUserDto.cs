using System.ComponentModel.DataAnnotations;

namespace Module.Identity.Core.DataTransferObjects {
    public class LoginUserDto {
        [Required]
        [Display(Name = "UserName")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
