using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_Do_List.Auth;
using To_Do_List.Dto;
using To_Do_List.Dto.AccountDto;
using To_Do_List.Interface;

namespace To_Do_List.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepositroy _userRepositroy;
        public AccountController(IUserRepositroy userRepositroy)
        {
            _userRepositroy = userRepositroy;
        }

        [HttpPost("SignUp")]

        public async Task<IActionResult>SignUp([FromBody]SignUpDto signUpDto)
        {  
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var staff = signUpDto.ToUser();

            await _userRepositroy.SignUp(staff);

            return Ok("User Created");
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userRepositroy.GetByUsername(loginDto.Username);
            var userDto = user.ToUserDto();

            if (user == null)
                return BadRequest("Invalid Username");

            if (user.Password != loginDto.Password)
                return BadRequest("Invalid Password");

            return Ok(userDto);
        }

        [BasicAuthorization]
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetUser()
        {
            var username = User.Identity.Name;

            var user = await _userRepositroy.GetByUsernameDto(username);
            if (user == null)
                return BadRequest("Invalid Username");
            return Ok(user);
        }

        [BasicAuthorization]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var username = User.Identity.Name;

            var user = await _userRepositroy.GetByUsername(username);

            await _userRepositroy.UpdateUser(userUpdateDto, user);

            return Ok("user Updated");
        }

        [BasicAuthorization]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser()
        {
            var username = User.Identity.Name;

            var user = await _userRepositroy.GetByUsername(username);

            await _userRepositroy.DeleteUser(user);

            return Ok("User Deleted");
        }
    }
}
