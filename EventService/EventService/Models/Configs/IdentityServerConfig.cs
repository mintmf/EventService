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
        /// <param name="issuer"></param>
        /// <param name="authority"></param>
        /// <param name="audience"></param>
        public IdentityServerConfig(string issuer, string authority, string audience)
        {
            Issuer = issuer;
            Authority = authority;
            Audience = audience;
        }

        /// <summary>
        /// Адрес
        /// </summary>
        public string Issuer { get; set; }

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
