namespace SpaceService
{
    /// <summary>
    /// Настройки сервера
    /// </summary>
    public class IdentityServerConfig
    {
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

        /// <summary>
        /// 
        /// </summary>
        public string IntrospectionEndpoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
