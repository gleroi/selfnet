namespace Selfnet
{
    public class ConnectionOptions
    {
        /// <summary>
        /// http or https, default to http
        /// </summary>
        public string Scheme { get; set; } = "http";

        public string Host { get; set; }

        public string Base { get; set; }

        /// <summary>
        /// default to 80
        /// </summary>
        public int Port { get; set; } = 80;

        public string Username { get; set; }
        public string Password { get; set; }
    }
}