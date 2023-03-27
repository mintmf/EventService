namespace EventService.Infrastracture
{
    /// <summary>
    /// Конфигурация сервиса изображений
    /// </summary>
    public class ImageServiceConfig
    {
        /// <summary>
        /// Адрес
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Адрес метода проверки изображения
        /// </summary>
        public string? IsImageExistsEndpoint { get; set; }
    }
}
