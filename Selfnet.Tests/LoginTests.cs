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
            Context.HttpGetReturns("login", "{'success':true}");

            var opts = new ConnectionOptions()
            {
                Host = "nostromo.myds.me",
                Base = "selfoss",
                Username = "gleroi",
                Password = "cXVa2I0L",
            };
            var api = new SelfossApi(opts);
            var result = await api.Login();
        }
    }
}
