namespace EventService.Services
{
    /// <summary>
    /// Сервис пространств
    /// </summary>
    public class SpaceService : ISpaceService
    {
        /// <summary>
        /// Проверка существования пространства
        /// </summary>
        /// <param name="spaceId"></param>
        /// <returns></returns>
        public bool IsSpaceExists(Guid? spaceId)
        {
            return spaceId != null;
        }
    }
}
