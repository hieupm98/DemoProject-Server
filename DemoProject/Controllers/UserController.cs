using DemoProject.Entites;
using DemoProject.EntityFrameworkCore;
using DemoProject.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace DemoProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly TextHelper _textHelper;

        public UserController(AppDbContext context, TextHelper textHelper)
        {
            _context = context;
            _textHelper = textHelper;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<User>> Get()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<User>> GetById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<User>> AddNewUser(UserDto inputUser)
        {
            if (string.IsNullOrWhiteSpace(inputUser.FirstName))
                return BadRequest("Must input FirstName");
            if (string.IsNullOrWhiteSpace(inputUser.LastName))
                return BadRequest("Must input LastName");
            var newUser = new User();
            newUser.Id = Guid.NewGuid();
            newUser.FirstName = _textHelper.NormalizeText(inputUser.FirstName);
            newUser.LastName = _textHelper.NormalizeText(inputUser.LastName);
            newUser.PhoneNumber = inputUser.PhoneNumber;
            newUser.Email = inputUser.Email;
            newUser.ZipCode = inputUser.ZipCode;
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult<User>> UpdateUser(string id, User inputUser)
        {
            if (!string.Equals(id, inputUser.Id.ToString()))
                return NotFound();
            var user = await _context.Users.FindAsync(inputUser.Id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(inputUser.FirstName))
                    return BadRequest("Must input FirstName");
                if (string.IsNullOrWhiteSpace(inputUser.LastName))
                    return BadRequest("Must input LastName");
                user.FirstName = _textHelper.NormalizeText(inputUser.FirstName);
                user.LastName = _textHelper.NormalizeText(inputUser.LastName);
                user.PhoneNumber = inputUser.PhoneNumber;
                user.Email = inputUser.Email;
                user.ZipCode = inputUser.ZipCode;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            else
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
        }
    }
}
