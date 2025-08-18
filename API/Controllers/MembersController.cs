
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // localhost:5001/api/members
    public class MembersController(AppDbContext context) : ControllerBase
    {
        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync();
            return Ok(members);
        }

        [HttpGet("{id}")] // localhost:5001/api/members/bob-id
        public async Task<ActionResult<AppUser>> GetMember(int id)
        {
            var member = await context.Users.FindAsync(id);
            if (member == null)
                return NotFound();
            return Ok(member);
        }
    }
}