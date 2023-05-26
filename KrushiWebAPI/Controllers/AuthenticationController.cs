using KrushiWebAPI.Context;
using KrushiWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KrushiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
         ProjectDBContext _context;

        public AuthenticationController(ProjectDBContext context)
        {
            _context = context;               
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]Admin admin)
        {
            var admins = _context.Admins.Where(a => a.Username == admin.Username && a.Password == admin.Password).ToList();
            return Ok(admins);
        }

        [HttpPut]
        [Route("changepassword/{id}/{oldpassword}/{newpassword}")]
        public async Task<IActionResult> ChangePassword([FromRoute]int id, [FromRoute]string oldpassword, [FromRoute]string newpassword)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
                return NotFound();
            
                if (admin.Password.Equals(oldpassword))
                {  
                    admin.Password = newpassword;
                    await _context.SaveChangesAsync();
                    return Ok(admin);
                }
            else
            {
                return NotFound();
            }
        }

    }
}
