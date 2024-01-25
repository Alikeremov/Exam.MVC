using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Pigga.Mvc.Exam.Areas.Manage.ViewModels;
using Pigga.Mvc.Exam.DAL;
using Pigga.Mvc.Exam.Models;

namespace Pigga.Mvc.Exam.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Settings.ToListAsync());
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id < 1) return BadRequest();
            Setting setting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            if (setting == null) return NotFound();
            SettingUpdateVm updateVm = new SettingUpdateVm
            {
                Key = setting.Key,
                Value = setting.Value,
            };
            return View(updateVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,SettingUpdateVm updateVm)
        {
            if (id < 1) return BadRequest();
            Setting setting = await _context.Settings.FirstOrDefaultAsync(x => x.Id == id);
            if (setting == null) return NotFound();
            if(!ModelState.IsValid)
            {
                return View(updateVm);
            }
            setting.Value = updateVm.Value;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
