using System;
using Selfnet;

namespace Selfwin.Selfoss
{
    public class SelfWinSettings
    {
        internal SelfWinSettings()
        {
            this.SelfossOptions = new ConnectionOptions();
        }

        public SelfWinSettings(string url, int port, string username, string password)
            : this()
        {
            this.SelfossOptions.Port = port;
            this.SelfossOptions.Username = username;
            this.SelfossOptions.Password = password;
            this.InitializeUrl(this.SelfossOptions, url);
        }

        private void InitializeUrl(ConnectionOptions selfossOptions, string url)
        {
            Uri uri = new Uri(url);
            selfossOptions.Scheme = uri.Scheme;
            selfossOptions.Host = uri.Host;
            selfossOptions.Base = uri.AbsolutePath;
        }

        public ConnectionOptions SelfossOptions { get; private set; }
    }
}