using System.Linq;
using Xunit;

namespace Selfnet.Tests
{
    public class ItemsTests
    {
        [Fact]
        public async void Items_ShouldWork()
        {
            Context.Http.GetReturns(@"[" +
                                    @"{""id"":""1825"",""datetime"":""2015-08-14 11:00:00"",""title"":""Production postmortem: The case of the man in the middle"",""content"":""<p>to make our life harder.</p> "",""unread"":""0"",""starred"":""0"",""source"":""25"",""thumbnail"":"""",""icon"":""8541a93ee20c97ed37ba9ca966e10579.png"",""uid"":""http://ayende.com/blog/171683/production-postmortem-the-case-of-the-man-in-the-middle?Key=508ba398-8457-4ff4-8f4f-f964b0283dde"",""link"":""http://feedproxy.google.com/~r/AyendeRahien/~3/lZ8GQQ2T-ko/production-postmortem-the-case-of-the-man-in-the-middle"",""updatetime"":""2015-08-14 11:27:40"",""author"":"""",""sourcetitle"":""Ayende @ Rahien"",""tags"":""""}" 
                                    + @",{""id"":""1824"",""datetime"":""2015-08-14 10:27:45"",""title"":""The Morning Brew #1925"",""content"":""<h3>Information</h3>\n"",""unread"":""0"",""starred"":""0"",""source"":""66"",""thumbnail"":"""",""icon"":""f5bace712844995f47284050b1051c5c.png"",""uid"":""http://blog.cwa.me.uk/?p=4572"",""link"":""http://feedproxy.google.com/~r/ReflectivePerspective/~3/w-TFWha-1HQ/"",""updatetime"":""2015-08-14 11:27:52"",""author"":""Chris Alcock"",""sourcetitle"":""The Morning Brew"",""tags"":""""}]");


            var api = Context.Api();

            var result = (await api.Items()).ToList();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(1825, result[0].Id);
            Assert.Equal(1824, result[1].Id);
        }

        [Fact]
        public async void Items_GetLast2_ShouldWork()
        {
            Context.Http.GetReturns(@"[" +
                                    @"{""id"":""1825"",""datetime"":""2015-08-14 11:00:00"",""title"":""Production postmortem: The case of the man in the middle"",""content"":""<p>to make our life harder.</p> "",""unread"":""0"",""starred"":""0"",""source"":""25"",""thumbnail"":"""",""icon"":""8541a93ee20c97ed37ba9ca966e10579.png"",""uid"":""http://ayende.com/blog/171683/production-postmortem-the-case-of-the-man-in-the-middle?Key=508ba398-8457-4ff4-8f4f-f964b0283dde"",""link"":""http://feedproxy.google.com/~r/AyendeRahien/~3/lZ8GQQ2T-ko/production-postmortem-the-case-of-the-man-in-the-middle"",""updatetime"":""2015-08-14 11:27:40"",""author"":"""",""sourcetitle"":""Ayende @ Rahien"",""tags"":""""}"
                                    + @",{""id"":""1824"",""datetime"":""2015-08-14 10:27:45"",""title"":""The Morning Brew #1925"",""content"":""<h3>Information</h3>\n"",""unread"":""0"",""starred"":""0"",""source"":""66"",""thumbnail"":"""",""icon"":""f5bace712844995f47284050b1051c5c.png"",""uid"":""http://blog.cwa.me.uk/?p=4572"",""link"":""http://feedproxy.google.com/~r/ReflectivePerspective/~3/w-TFWha-1HQ/"",""updatetime"":""2015-08-14 11:27:52"",""author"":""Chris Alcock"",""sourcetitle"":""The Morning Brew"",""tags"":""""}]");


            var api = Context.Api();

            var result = (await api.Items(new ItemsFilter { ItemsCount = 2})).ToList();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async void Items_GetUnread_ShouldWork()
        {
            Context.Http.GetReturns(@"[" +
                                    @"{""id"":""1825"",""datetime"":""2015-08-14 11:00:00"",""title"":""Production postmortem: The case of the man in the middle"",""content"":""<p>to make our life harder.</p> "",""unread"":""1"",""starred"":""1"",""source"":""25"",""thumbnail"":"""",""icon"":""8541a93ee20c97ed37ba9ca966e10579.png"",""uid"":""http://ayende.com/blog/171683/production-postmortem-the-case-of-the-man-in-the-middle?Key=508ba398-8457-4ff4-8f4f-f964b0283dde"",""link"":""http://feedproxy.google.com/~r/AyendeRahien/~3/lZ8GQQ2T-ko/production-postmortem-the-case-of-the-man-in-the-middle"",""updatetime"":""2015-08-14 11:27:40"",""author"":"""",""sourcetitle"":""Ayende @ Rahien"",""tags"":""""}"
                                    + @",{""id"":""1824"",""datetime"":""2015-08-14 10:27:45"",""title"":""The Morning Brew #1925"",""content"":""<h3>Information</h3>\n"",""unread"":""1"",""starred"":""1"",""source"":""66"",""thumbnail"":"""",""icon"":""f5bace712844995f47284050b1051c5c.png"",""uid"":""http://blog.cwa.me.uk/?p=4572"",""link"":""http://feedproxy.google.com/~r/ReflectivePerspective/~3/w-TFWha-1HQ/"",""updatetime"":""2015-08-14 11:27:52"",""author"":""Chris Alcock"",""sourcetitle"":""The Morning Brew"",""tags"":""""}]");
            var api = Context.Api();
            var result = (await api.Items(new ItemsFilter
            {
                ItemStatus = Status.Unread
            })).ToList();

            Assert.NotNull(result);
            Assert.True(result.All(item => item.Unread));
        }
    }
}
