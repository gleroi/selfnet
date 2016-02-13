using Caliburn.Micro;

namespace Selfwin.Settings
{
    public class SettingsViewModel : Screen
    {
        private string _url;
        public string Url
        {
            get { return _url; }
            set
            {
                if (value == _url) return;
                _url = value;
                NotifyOfPropertyChange();
            }
        }

        private int _port;
        public int Port
        {
            get { return _port; }
            set
            {
                if (value == _port) return;
                _port = value;
                NotifyOfPropertyChange();
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
            }
        }
    }
}