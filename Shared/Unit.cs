namespace Shared;

/// <summary>
/// Igual a MediatR.Unit: representa “no hay contenido” en respuestas o métodos.
/// </summary>
public readonly record struct Unit
{
    public static readonly Unit Value = new();
}