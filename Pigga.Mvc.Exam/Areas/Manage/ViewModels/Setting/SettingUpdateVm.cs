using System.ComponentModel.DataAnnotations;

namespace Pigga.Mvc.Exam.Areas.Manage.ViewModels
{
    public class SettingUpdateVm
    {
        public string? Key { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string Value { get; set; } = null!;
    }
}
