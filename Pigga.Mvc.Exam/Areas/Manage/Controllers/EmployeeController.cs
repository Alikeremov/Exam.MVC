using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pigga.Mvc.Exam.Areas.Manage.ViewModels;
using Pigga.Mvc.Exam.DAL;
using Pigga.Mvc.Exam.Models;
using Pigga.Mvc.Exam.Utilites.Extensions;

namespace Pigga.Mvc.Exam.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateVm createVm)
        {
            if(!ModelState.IsValid) return View(createVm);
            if (!createVm.Photo.ValidateSize(5))
            {
                ModelState.AddModelError("Photo", "Photo size max must be 5 mb");
                return View(createVm);
            }
            if (!createVm.Photo.ValidateType("image/"))
            {
                ModelState.AddModelError("Photo", "Photo type must be only photo");
                return View(createVm);
            }
            Employee employee = new Employee
            {
                Name = createVm.Name,
                Surname = createVm.Surname, 
                About = createVm.About,
                GoogleLink = createVm.GoogleLink,
                Fblink = createVm.Fblink,
                InstagramLink = createVm.InstagramLink,
                TwLink=createVm.TwLink
            };
            employee.Image = await createVm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "imgs");
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id < 1) return BadRequest();
            Employee existed=await _context.Employees.FirstOrDefaultAsync(x=>x.Id==id);
            if(existed == null) return NotFound();
            EmployeeUpdateVm vm = new EmployeeUpdateVm
            {
                Name= existed.Name,
                Surname= existed.Surname,
                About = existed.About,  
                GoogleLink = existed.GoogleLink,
                Fblink = existed.Fblink,
                TwLink=existed.TwLink,
                InstagramLink=existed.InstagramLink,
                Image = existed.Image,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id , EmployeeUpdateVm updateVm)
        {
            if (id < 1) return BadRequest();
            Employee existed = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (existed == null) return NotFound();
            if(!ModelState.IsValid) return View(updateVm);
            if (updateVm.Photo != null)
            {
                if (!updateVm.Photo.ValidateSize(5))
                {
                    ModelState.AddModelError("Photo", "Photo size max must be 5 mb");
                    return View(updateVm);
                }
                if (!updateVm.Photo.ValidateType("image/"))
                {
                    ModelState.AddModelError("Photo", "Photo type must be only photo");
                    return View(updateVm);
                }
                string fileName= await updateVm.Photo.CreateFileAsync(_env.WebRootPath, "assets", "imgs");
                existed.Image.DeleteFile(_env.WebRootPath, "assets", "imgs");
                existed.Image = fileName;
            }
            existed.Name = updateVm.Name;
            existed.Surname = updateVm.Surname;
            existed.About = updateVm.About;
            existed.Fblink = updateVm.Fblink;
            existed.TwLink  = updateVm.TwLink;
            existed.InstagramLink = updateVm.InstagramLink;
            existed.GoogleLink = updateVm.GoogleLink;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();
            Employee existed = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (existed == null) return NotFound();
            existed.Image.DeleteFile(_env.WebRootPath, "assets", "imgs");
            _context.Employees.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
