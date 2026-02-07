using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Infrastucture;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var records = await _context.DashboardRecords
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();

            return Ok(records);
        }
    }
}
