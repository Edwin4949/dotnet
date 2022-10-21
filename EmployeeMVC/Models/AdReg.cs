using System.ComponentModel.DataAnnotations;

namespace EmployeeMVC.Models
{
    public class AdReg
    {
        
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required, MinLength(10)]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

