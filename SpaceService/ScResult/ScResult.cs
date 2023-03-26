namespace SpaceService;

/// <summary>
/// Базовый класс для результата возвращаемого из методов контроллеров
/// </summary>
public class ScResult
{
    /// <summary>
    /// Информация об ошибке для случая если возникла ошибка при вызове метода
    /// </summary>
    public ScError? Error { get; set; }

    public ScResult()
    {
    }

    public ScResult(ScError? error)
    {
        Error = error;
    }
}