namespace EventService.Models.Configs
{
    /// <summary>
    /// Настройки сервера
    /// </summary>
    public class IdentityServerConfig
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="address"></param>
        /// <param name="authority"></param>
        /// <param name="audience"></param>
        public IdentityServerConfig(string address, string authority, string audience)
        {
            Address = address;
            Authority = authority;
            Audience = audience;
        }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Authority
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// Audince
        /// </summary>
        public string Audience { get; set; }
    }
}
