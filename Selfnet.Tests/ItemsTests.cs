using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Selfnet.Tests
{
    public class ItemsTests
    {
        [Fact]
        public async void Items_ShouldWork()
        {
            //Context.Http.GetReturns("{ 'success': true }");

            var api = Context.Api();

            var result = await api.Items();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
