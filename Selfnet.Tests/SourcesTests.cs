using System.Linq;
using Xunit;

namespace Selfnet.Tests
{
    public class SourcesTests
    {
        [Fact]
        public async void Sources_Get_ShouldWork()
        {
            Context.Http.GetReturns(@"[{""id"":""33"",""title"":"".NET Curry: Recent Articles"",""tags"":"""",""spout"":""spouts\\rss\\feed"",""params"":{""url"":""http://feeds.feedburner.com/netCurryRecentArticles""},""filter"":null,""error"":"""",""icon"":""a39473e4e64f4ef8cba7ea8b87fea3f8.png""},
                {""id"":""27"",""title"":""Android Developers Blog"",""tags"":"""",""spout"":""spouts\\rss\\feed"",""params"":{""url"":""http://android-developers.blogspot.com/atom.xml""},""filter"":null,""error"":"""",""icon"":""615e9d7f13db96b7bbb57ff8345bdc50.png""},
                {""id"":""42"",""title"":""Architecture @ Microsoft Blog"",""tags"":"""",""spout"":""spouts\\rss\\feed"",""params"":{""url"":""http://blogs.msdn.com/b/architecture/rss.aspx""},""filter"":null,""error"":"""",""icon"":null}]");

            var api = Context.Api();

            var result = await api.Sources.Get();

            Assert.NotNull(result);
            Assert.NotEmpty(result);

            Assert.Equal(3, result.Count());

            var source = result.First();
            Assert.Equal(33, source.Id);
            Assert.Equal("http://feeds.feedburner.com/netCurryRecentArticles", source.Params["url"]);
        }
    }
}
