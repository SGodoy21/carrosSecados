using Application.Exceptions;
using Application.Helpers;
using Application.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.DTOs.Auth;
using Shared.Responses;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Tags("Usuario")]
public class UsuarioController(
    AutenticacionUsuario authenticateUser,
    JwtTokenGenerator jwtTokenGenerator,
    UsuarioService userService) : ControllerBase
{
    private readonly AutenticacionUsuario _authenticateUser = authenticateUser;
    private readonly JwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
    private readonly UsuarioService _userService = userService;

    [Authorize(Roles = "Admin")]
    [HttpPut("change-rol")]
    [SwaggerOperation(Summary = "ChangeRol", Description = "Cambia el rol asignado a un usuario.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AxResponse<Unit>>> ChangeRol([FromBody] CambiarRolUsuarioDto dto)
    {
        try
        {
            await _userService.ChangeUsuarioRolAsync(dto);
            return Ok(AxResponse<Unit>.Ok(Unit.Value, "Rol actualizado correctamente."));
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("Usuario") && ex.Message.Contains("no encontrado"))
                return NotFound(AxResponse<Unit>.Fail(ex.Message));

            if (ex.Message.Contains("Rol") && ex.Message.Contains("no encontrado"))
                return NotFound(AxResponse<Unit>.Fail(ex.Message));

            return BadRequest(AxResponse<Unit>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(AxResponse<Unit>.Fail(ex.Message));
        }
    }

    [HttpPost("register")]
    [SwaggerOperation(Summary = "Register", Description = "Registra un nuevo usuario en el sistema.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AxResponse<Unit>>> Register([FromBody] RegistrarUsuarioDto dto)
    {
        try
        {
            await _userService.RegistrarUsuarioAsync(dto);
            return Ok(AxResponse<Unit>.Ok(Unit.Value, "Usuario registrado correctamente."));
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("usuario") && ex.Message.Contains("ya está registrado"))
                return Conflict(AxResponse<Unit>.Fail(ex.Message));

            if (ex.Message.Contains("contraseña") || ex.Message.Contains("password"))
                return BadRequest(AxResponse<Unit>.Fail(ex.Message));

            if (ex.Message.Contains("email") && ex.Message.Contains("inválido"))
                return BadRequest(AxResponse<Unit>.Fail(ex.Message));

            return BadRequest(AxResponse<Unit>.Fail(ex.Message)); // fallback
        }
        catch (Exception ex)
        {
            return BadRequest(AxResponse<Unit>.Fail(ex.Message));
        }
    }

    [HttpPost("login")]
    [SwaggerOperation(Summary = "Login", Description = "Autentica un usuario y devuelve un token JWT.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AxResponse<UsuarioLoginResponseDto>>> Login([FromBody] UsuarioLoginDto dto)
    {
        try
        {
            var user = await _authenticateUser.ExecuteAsync(dto.NombreUsuario, dto.Password);
            if (user is null)
                return Unauthorized(AxResponse<UsuarioLoginResponseDto>.Fail("Credenciales inválidas o usuario deshabilitado."));

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
            return BadRequest(AxResponse<UsuarioLoginResponseDto>.Fail(ex.Message));
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("disable/{id}")]
    [SwaggerOperation(Summary = "Disable", Description = "Deshabilita un usuario por ID.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AxResponse<Unit>>> Disable([FromRoute] long id)
    {
        try
        {
            await _userService.DisableUsuarioAsync(id);
            return Ok(AxResponse<Unit>.Ok(Unit.Value, "Usuario deshabilitado correctamente."));
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("no encontrado"))
                return NotFound(AxResponse<Unit>.Fail(ex.Message));

            if (ex.Message.Contains("no puede ser deshabilitado"))
                return Conflict(AxResponse<Unit>.Fail(ex.Message));

            return BadRequest(AxResponse<Unit>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(AxResponse<Unit>.Fail(ex.Message));
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("enable/{id}")]
    [SwaggerOperation(Summary = "Enable", Description = "Habilita un usuario por ID.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AxResponse<Unit>>> Enable([FromRoute] long id)
    {
        try
        {
            await _userService.EnableUsuarioAsync(id);
            return Ok(AxResponse<Unit>.Ok(Unit.Value, "Usuario habilitado correctamente."));
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("no encontrado"))
                return NotFound(AxResponse<Unit>.Fail(ex.Message));

            if (ex.Message.Contains("no puede ser habilitado"))
                return Conflict(AxResponse<Unit>.Fail(ex.Message));

            return BadRequest(AxResponse<Unit>.Fail(ex.Message)); // fallback
        }
        catch (Exception ex)
        {
            return BadRequest(AxResponse<Unit>.Fail(ex.Message));
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete/{id}")]
    [SwaggerOperation(Summary = "Delete", Description = "Elimina un usuario por ID.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AxResponse<Unit>>> Delete([FromRoute] long id)
    {
        try
        {
            await _userService.DeleteUsuarioAsync(id);
            // Para mantener consistencia de wrapper, devolvemos 200 con mensaje.
            return Ok(AxResponse<Unit>.Ok(Unit.Value, "Usuario eliminado correctamente."));
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("no encontrado"))
                return NotFound(AxResponse<Unit>.Fail(ex.Message));

            return BadRequest(AxResponse<Unit>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(AxResponse<Unit>.Fail(ex.Message));
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("list")]
    [SwaggerOperation(Summary = "List", Description = "Devuelve la lista de todos los usuarios registrados.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<AxResponse<List<UsuarioDto>>>> List()
    {
        var users = await _userService.GetAllUsuarioAsync();
        return Ok(AxResponse<List<UsuarioDto>>.Ok(users, "Usuarios obtenidos correctamente."));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("change-password/{id}")]
    [SwaggerOperation(Summary = "ChangePassword", Description = "Cambia la contraseña de un usuario.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AxResponse<Unit>>> ChangePassword([FromRoute] long id, [FromBody] string newPassword)
    {
        try
        {
            await _userService.ChangePasswordAsync(id, newPassword);
            return Ok(AxResponse<Unit>.Ok(Unit.Value, "Contraseña actualizada correctamente."));
        }
        catch (AppException ex)
        {
            if (ex.Message.Contains("no encontrado"))
                return NotFound(AxResponse<Unit>.Fail(ex.Message));

            if (ex.Message.Contains("contraseña") || ex.Message.Contains("password"))
                return BadRequest(AxResponse<Unit>.Fail(ex.Message));

            return BadRequest(AxResponse<Unit>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return BadRequest(AxResponse<Unit>.Fail(ex.Message));
        }
    }
}