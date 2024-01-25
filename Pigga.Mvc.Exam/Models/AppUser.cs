using Microsoft.AspNetCore.Identity;

namespace Pigga.Mvc.Exam.Models
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; } = null!;
        public string SurName { get; set; } = null!;
    }
}
