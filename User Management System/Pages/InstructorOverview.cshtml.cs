using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Lightaplusplus.Models;
using System.ComponentModel.DataAnnotations;

namespace Lightaplusplus.Pages
{
    public class InstructorOverviewModel : PageModel
    {
        private readonly Lightaplusplus.Data.Lightaplusplus_SystemContext _context;

        public InstructorOverviewModel(Lightaplusplus.Data.Lightaplusplus_SystemContext context)
        {
            _context = context;
        }

        public Users Users { get; set; }

        [BindProperty]
        public int id { get; set; }

        /// <summary>
        /// Sections that this particular instructor teaches
        /// </summary>
        /// 
        [BindProperty]
        public Sections[] sectionsTaught { get; set; }
        
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Users = await _context.Users.FirstOrDefaultAsync(m => m.ID == id);

            if (Users == null)
            {
                return NotFound();
            }
            var sections = _context.Sections.Where(i => i.InstructorId == Users.ID);

            sectionsTaught = new Sections[sections.Count()];
            int iter = 0;
            foreach (var section in sections)
            {
                sectionsTaught[iter] = section;
                iter++;
            }

            foreach (var section in sectionsTaught)
            {
                var courses = _context.Courses.Where(c => c.CourseId == section.CourseId);
                foreach (var course in courses)
                {
                    section.Course = course;
                }
            }
            return Page();
        }
    }
}
