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
            Context.Http.GetReturns("[{\"tag\":\".net\",\"color\":\"#3166ff\",\"unread\":\"0\"}]");

            var api = Context.Api();
            var tags = await api.Tags.Get();

            Assert.NotNull(tags);
            Assert.Equal(1, tags.Count());

            var tag = tags.First();

            Assert.Equal(".net", tag.Tag);
        }
    }
}
