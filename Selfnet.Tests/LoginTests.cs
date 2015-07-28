using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Selfnet.Tests
{
    public class LoginTests
    {
        [Fact]
        public async void Login_ShouldWork()
        {
            var opts = new ConnectionOptions()
            {
                Host = "",
                Base = "selfoss",
                Username = "",
                Password = "",
            };
            var api = new SelfossApi(opts);
            var result = await api.Login();
        }
    }
}
