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
public class UsuarioController : ControllerBase
{
    private readonly AutenticacionUsuario _authenticateUser;
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly Application.Services.UsuarioService _userService;

    public UsuarioController(
        AutenticacionUsuario authenticateUser,
        JwtTokenGenerator jwtTokenGenerator,
        Application.Services.UsuarioService userService)
    {
        _authenticateUser = authenticateUser;
        _jwtTokenGenerator = jwtTokenGenerator;
        _userService = userService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("change-rol")]
    public async Task<IActionResult> ChangeRol([FromBody] CambiarRolUsuarioDto dto)
    {
        try
        {
            await _userService.ChangeUsuarioRolAsync(dto);
            return Ok(new { message = "Rol actualizado correctamente." });
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("Usuario") && ex.Message.Contains("no encontrado"))
                return NotFound(new { error = ex.Message });

            if (ex.Message.Contains("Rol") && ex.Message.Contains("no encontrado"))
                return NotFound(new { error = ex.Message });

            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrarUsuarioDto dto)
    {
        try
        {
            await _userService.RegistrarUsuarioAsync(dto);
            return Ok(new { message = "Usuario registrado correctamente." });
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("contraseña") || ex.Message.Contains("password"))
                return BadRequest(new { error = ex.Message });

            if (ex.Message.Contains("email") && ex.Message.Contains("inválido"))
                return BadRequest(new { error = ex.Message });

            if (ex.Message.Contains("usuario") && ex.Message.Contains("ya está registrado"))
                return Conflict(new { error = ex.Message });

            return BadRequest(new { error = ex.Message }); // fallback
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UsuarioLoginDto dto)
    {
        try
        {
            var user = await _authenticateUser.ExecuteAsync(dto.NombreUsuario, dto.Password);
            if (user is null)
                return Unauthorized(new { error = "Credenciales inválidas o usuario deshabilitado." });

            var token = _jwtTokenGenerator.GenerarToken(user);

            var response = new UsuarioLoginResponseDto
            {
                Token = token,
                Usuario = new UsuarioDto
                {
                    Id = user.Id,
                    NombreUsuario = user.NombreUsuario,
                    NombreApellido = $"{user.Nombre} {user.Apellido}",
                    Email = user.Email,
                    RolId = user.RolId,
                    GrupoId = user.GrupoId
                }
            };

            return Ok(AxResponse<UsuarioLoginResponseDto>.Ok(response, "Login successful."));
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
            await _userService.DisableUsuarioAsync(id);
            return Ok(new { message = "Usuario deshabilitado correctamente." });
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("no encontrado"))
                return NotFound(new { error = ex.Message });

            if (ex.Message.Contains("no puede ser deshabilitado")) // ajustá según tu mensaje exacto
                return Conflict(new { error = ex.Message });

            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpPut("enable/{id}")]
    public async Task<IActionResult> Enable(long id)
    {
        try
        {
            await _userService.EnableUsuarioAsync(id);
            return Ok(new { message = "Usuario habilitado correctamente." });
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("no encontrado"))
                return NotFound(new { error = ex.Message });

            if (ex.Message.Contains("no puede ser habilitado"))
                return Conflict(new { error = ex.Message });

            return BadRequest(new { error = ex.Message }); // fallback
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            await _userService.DeleteUsuarioAsync(id);
            return NoContent();
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("no encontrado"))
                return NotFound(new { error = ex.Message });

            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }


    [Authorize(Roles = "Admin")]
    [HttpGet("list")]
    public async Task<IActionResult> List()
    {
        var users = await _userService.GetAllUsuarioAsync();
        return Ok(users);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("change-password/{id}")]
    public async Task<IActionResult> ChangePassword(long id, [FromBody] string newPassword)
    {
        try
        {
            await _userService.ChangePasswordAsync(id, newPassword);
            return Ok(new { message = "Contraseña actualizada correctamente." });
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("no encontrado"))
                return NotFound(new { error = ex.Message });

            if (ex.Message.Contains("contraseña") || ex.Message.Contains("password"))
                return BadRequest(new { error = ex.Message });

            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

}
