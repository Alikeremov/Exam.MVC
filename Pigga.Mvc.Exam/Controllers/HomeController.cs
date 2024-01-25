using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pigga.Mvc.Exam.DAL;
using Pigga.Mvc.Exam.Models;
using Pigga.Mvc.Exam.ViewModels;
using System.Diagnostics;

namespace Pigga.Mvc.Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            HomeVm vm = new HomeVm
            {
                Employees= await _context.Employees.ToListAsync()
            };
            return View(vm);
        }

        
    }
}