using Application.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Responses;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly AuthenticateUser _authenticateUser;
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly Application.Services.UserService _userService;

    public UserController(
        AuthenticateUser authenticateUser,
        JwtTokenGenerator jwtTokenGenerator,
        Application.Services.UserService userService)
    {
        _authenticateUser = authenticateUser;
        _jwtTokenGenerator = jwtTokenGenerator;
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("change-role")]
    public async Task<IActionResult> ChangeRole([FromBody] Shared.DTOs.ChangeUserRoleDto dto)
    {
        try
        {
            await _userService.ChangeUserRoleAsync(dto);
            return Ok(new { message = "Rol actualizado correctamente." });
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        try
        {
            await _userService.RegistrarUsuarioAsync(dto);
            return Ok(new { message = "Usuario registrado correctamente." });
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var user = await _authenticateUser.ExecuteAsync(dto.Username, dto.Password);

        if (user is null)
            return Unauthorized(AxResponse<UserLoginResponseDto>.Fail("Invalid credentials or user disabled."));

        var token = _jwtTokenGenerator.GenerateToken(user);

        var response = new UserLoginResponseDto
        {
            Token = token,
            User = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                RoleId = user.RoleId,
                GroupId = user.GroupId
            }
        };

        return Ok(AxResponse<UserLoginResponseDto>.Ok(response, "Login successful."));
    }
}
