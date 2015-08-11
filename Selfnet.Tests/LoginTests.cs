using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Selfnet.Tests
{
    public class LoginTests
    {
        [Fact]
        public async void Login_ShouldWork()
        {
            Context.Http.GetReturns("{ 'success': true }");

            Context.Options = new ConnectionOptions()
            {
                Host = "nostromo.myds.me",
                Base = "selfoss",
                Username = "paul",
                Password = "mlkpoim",
            };

            var api = Context.Api();

            var result = await api.Login();

            Assert.True(result);
        }
    }
}
