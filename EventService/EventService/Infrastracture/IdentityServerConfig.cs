namespace EventService.Infrastracture
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
        /// <param name="introspectionEndpoint"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        public IdentityServerConfig(string issuer, string authority, string audience, string introspectionEndpoint, string clientId, string clientSecret)
        {
            Issuer = issuer;
            Authority = authority;
            Audience = audience;
            IntrospectionEndpoint = introspectionEndpoint;
            ClientId = clientId;
            ClientSecret = clientSecret;
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
