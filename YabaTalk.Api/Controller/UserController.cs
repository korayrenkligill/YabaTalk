using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YabaTalk.Application.Dtos.Auth;
using YabaTalk.Application.Interfaces.Service;

namespace YabaTalk.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _us;
        public UserController(IUserService us)
        {
            _us = us;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] UserReqDto user)
        {
            var response = await _us.AddAsync(user);
            if (response.Success)
                return Ok(response);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _us.GetAllAsync();
            if (response.Success)
                return Ok(response);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("by-phone")]
        public async Task<IActionResult> GetByPhone([FromQuery] string phoneNumber)
        {
            var response = await _us.GetByPhoneAsync(phoneNumber);
            if (response.Success)
                return Ok(response);

            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("by-id")]
        public async Task<IActionResult> GetById([FromQuery] string id)
        {
            var response = await _us.GetByIdAsync(id);
            if (response.Success)
                return Ok(response);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPatch("by-phone")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
        {
            var response = await _us.UpdateAsync(dto);
            if (response.Success)
                return Ok(response);

            return StatusCode(response.StatusCode, response);
        }
    }
}
