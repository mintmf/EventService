namespace EventService.Models
{
    /// <summary>
    /// Класс пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ник пользователя
        /// </summary>
        public string? Nickname { get; set; }
    }
}
