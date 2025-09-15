using Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Responses;
using System;
using System.Threading.Tasks;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            var response = AxResponse<string>.Fail(ex.Message);
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "SQL Server connection error");
            context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            context.Response.ContentType = "application/json";
            var response = AxResponse<string>.Fail("No se pudo conectar a la base de datos.");
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "EF Core database update error");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var response = AxResponse<string>.Fail("Ocurrió un error al guardar los datos.");
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var response = AxResponse<string>.Fail("Error interno del servidor.");
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}