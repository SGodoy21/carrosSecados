using Application.Exceptions;
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
        catch (UserNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (RoleNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
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
        catch (InvalidPasswordException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidEmailException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (UsernameExistsException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        try
        {
            var user = await _authenticateUser.ExecuteAsync(dto.Username, dto.Password);
            if (user is null)
                return Unauthorized(new { error = "Credenciales inválidas o usuario deshabilitado." });

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
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("disable/{id}")]
    public async Task<IActionResult> Disable(long id)
    {
        try
        {
            await _userService.DisableUserAsync(id);
            return Ok(new { message = "Usuario deshabilitado correctamente." });
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("enable/{id}")]
    public async Task<IActionResult> Enable(long id)
    {
        try
        {
            await _userService.EnableUserAsync(id);
            return Ok(new { message = "Usuario habilitado correctamente." });
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}
