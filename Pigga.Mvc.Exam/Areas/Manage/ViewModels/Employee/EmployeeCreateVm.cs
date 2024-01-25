using System.ComponentModel.DataAnnotations;

namespace Pigga.Mvc.Exam.Areas.Manage.ViewModels
{
    public class EmployeeCreateVm
    {
        [Required]
        [MinLength(3)]
        [MaxLength(27)]
        public string Name { get; set; } = null!;
        [Required]
        [MinLength(3)]
        [MaxLength(27)]
        public string Surname { get; set; } = null!;
        [Required]
        [MinLength(10)]
        [MaxLength(300)]
        public string About { get; set; } = null!;
        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string Fblink { get; set; } = null!;
        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string GoogleLink { get; set; } = null!;
        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string TwLink { get; set; } = null!;
        [Required]
        [MinLength(10)]
        [MaxLength(100)]
        public string InstagramLink { get; set; } = null!;
        [Required]
        public IFormFile Photo { get; set; } = null!;
    }
}
