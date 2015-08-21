using System.Linq;
using System.Security.Claims;
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

        [Fact]
        public async void Sources_Stats_ShouldWork()
        {
            Context.Http.GetReturns(@"[{""id"":""33"",""title"":"".NET Curry: Recent Articles"",""unread"":""0""},
                {""id"":""27"",""title"":""Android Developers Blog"",""unread"":""42""},
                {""id"":""42"",""title"":""Architecture @ Microsoft Blog"",""unread"":""0""}]");

            var api = Context.Api();

            var result = await api.Sources.Stats();

            Assert.NotNull(result);

            var stats = result.ToList();
            Assert.Equal(3, stats.Count);

            var stat = stats[1];
            Assert.Equal(27, stat.Id);
            Assert.Equal(42, stat.Unread);
        }

        [Fact]
        public async void Sources_Spouts_ShouldWork()
        {
            Context.Http.GetReturns(@"{""spouts\\deviantart\\dailydeviations"":{""name"":""deviantART - daily deviations"",""description"":""daily deviations of deviantART"",""params"":false},
                ""spouts\\deviantart\\usersfavs"":{""name"":""deviantART - favs of a user"",""description"":""favorites of a user on deviantART"",""params"":{""username"":{""title"":""Username"",""type"":""text"",""default"":"""",""required"":true,""validation"":[""notempty""]}}},
                ""spouts\\deviantart\\user"":{""name"":""deviantART - user"",""description"":""deviations of a deviantART user"",""params"":{""username"":{""title"":""Username"",""type"":""text"",""default"":"""",""required"":true,""validation"":[""notempty""]}}}}");

            var api = Context.Api();

            var result = await api.Sources.Spouts();

            Assert.NotNull(result);
            var spouts = result.ToList();

            Assert.Equal(3, spouts.Count);

            var spout1 = spouts[0];
            Assert.Equal(@"spouts\deviantart\dailydeviations", spout1.Id);
            Assert.Empty(spout1.Params);

            var spout2 = spouts[1];
            Assert.Equal(@"spouts\deviantart\usersfavs", spout2.Id);
            Assert.Equal(1, spout2.Params.Count);
        }
    }
}
