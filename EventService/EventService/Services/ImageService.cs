namespace EventService.Services
{
    /// <summary>
    /// Сервис изображений
    /// </summary>
    public class ImageService : IImageService
    {
        /// <summary>
        /// Проверка существования изображения
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns></returns>
        public bool IsImageExists(Guid? imageId)
        {
            return true;
        }
    }
}
