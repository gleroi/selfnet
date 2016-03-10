namespace Selfwin.Core
{
    public interface ISettingsViewModel
    {
        string Url { get; set; }
        int Port { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}