using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Selfnet.Tests
{
    public class TagsTests
    {
        [Fact]
        public async void Tags_ShouldWork()
        {
            var api = Context.Api();
            var tags = await api.Tags.Get();

            Assert.NotNull(tags);
        }
    }
}
