using System.ComponentModel.DataAnnotations;

namespace Pigga.Mvc.Exam.Areas.Manage.ViewModels
{
    public class LoginVm
    {
        [Required]
        [MinLength(3)]
        [MaxLength(250)]
        public string UserNameOrEmail { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        public bool IsRemember { get; set; }
    }
}
