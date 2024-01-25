using System.ComponentModel.DataAnnotations;

namespace Pigga.Mvc.Exam.Areas.Manage.ViewModels
{
    public class RegisterVm
    {
        [Required]
        [MinLength(3)]
        [MaxLength(27)]
        public string Name { get; set; } = null!;
        [Required]
        [MinLength(3)]
        [MaxLength(27)]
        public string SurName { get; set; } = null!;
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;
        [Required]
        [MinLength(6)]
        [MaxLength(250)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [DataType(DataType.Password)]

        public string Password { get; set; } = null!;
        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string ComfirmPassord { get; set; } = null!;

    }
}
