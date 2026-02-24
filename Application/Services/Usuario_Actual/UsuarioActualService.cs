using Application.Interfaces.Usuario_Actual;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Services.Usuario_Actual
{
    /// <summary>
    /// Obtiene la información del usuario 
    /// actualmente autenticado a través del contexto HTTP de ASP.NET Core.
    /// </summary>
    /// <seealso cref="Application.Interfaces.Usuario_Actual.IUsuarioActualRepository" />
    public class UsuarioActualService(IHttpContextAccessor httpContextAccessor) : IUsuarioActualRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        /// <summary>
        /// Obtiene el identificador único (ID) del usuario autenticado. 
        /// Busca el Claim de tipo NameIdentifier y lo convierte a long.
        /// Devuelve 0 si el usuario no está autenticado o el claim no se encuentra.
        /// </summary>
        public long Id
        {
            get
            {
                // Busca el claim que contiene el ID del usuario (NameIdentifier).
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                // Extrae el valor y, si existe, lo intenta convertir a long; de lo contrario, devuelve 0.
                if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
                    return userId;
                return 0;
            }
        }

        /// <summary>
        /// Obtiene el nombre de usuario (username) del usuario autenticado. 
        /// Busca el Claim de tipo Name.
        /// </summary>
        public string? UserName =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
    }
}
