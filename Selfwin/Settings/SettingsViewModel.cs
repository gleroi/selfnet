using System;
using Caliburn.Micro;
using Selfnet;
using Selfwin.Selfoss;

namespace Selfwin.Settings
{
    public class SettingsViewModel : Screen
    {
        public SettingsViewModel(SelfwinApp app)
        {
            this.App = app;
        }

        private readonly SelfwinApp App;

        protected override void OnActivate()
        {
            base.OnActivate();
            var settings = this.App.Settings();
            this.InitializeConnection(settings.SelfossOptions);
        }

        private void InitializeConnection(ConnectionOptions conn)
        {
            if (!String.IsNullOrEmpty(conn.Scheme) && !String.IsNullOrEmpty(conn.Host) 
                && !String.IsNullOrEmpty(conn.Base))
            {
                var bld = new UriBuilder(conn.Scheme, conn.Host);
                bld.Path = conn.Base;
                this.Url = bld.Uri.ToString();
            }

            this.Port = conn.Port;
            this.Username = conn.Username;
            this.Password = conn.Password;
        }

        private string _url;
        public string Url
        {
            get { return _url; }
            set
            {
                if (value == _url) return;
                _url = value;
                NotifyOfPropertyChange();
                this.Save();
            }
        }

        private int _port = 80;
        public int Port
        {
            get { return _port; }
            set
            {
                if (value == _port) return;
                _port = value;
                NotifyOfPropertyChange();
                this.Save();
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (value == _username) return;
                _username = value;
                NotifyOfPropertyChange(() => Username);
                this.Save();
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password) return;
                _password = value;
                NotifyOfPropertyChange();
                this.Save();
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if (value == _errorMessage) return;
                _errorMessage = value;
                NotifyOfPropertyChange();
            }
        }

        public async void Save()
        {
            try
            {
                await this.App.SaveSettings(this);
                this.ErrorMessage = null;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }
        }
    }
}