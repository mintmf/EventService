namespace EventService.Services
{
    /// <summary>
    /// Интерфейс сервиса пространств
    /// </summary>
    public interface ISpaceService
    {
        /// <summary>
        /// Проверка существования пространства
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        public Task<bool> IsSpaceExists(Guid? spaceId);
    }
}
