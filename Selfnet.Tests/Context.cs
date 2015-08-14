namespace Selfnet.Tests
{
    internal static class Context
    {
        private static SelfossApi api;

        public static ConnectionOptions Options = new ConnectionOptions
        {
            Host = "",
            Base = "selfoss",
            Username = "",
            Password = ""
        };

        public static readonly HttpGatewayFake Http = new HttpGatewayFake();

        public static SelfossApi Api()
        {
            if (api == null)
            {
                api = new SelfossApi(Options, Http);
                //api = new SelfossApi(Options);
            }
            return api;
        }
    }
}