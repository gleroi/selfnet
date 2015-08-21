using Xunit;

namespace Selfnet.Tests
{
    public class ChangeStatusTests
    {
        [Fact]
        public async void MarkRead_ShouldWork()
        {
            Context.Http.PostReturns("{ 'success': true }");

            var api = Context.Api();
            var result = await api.Items.MarkRead(1846);

            Assert.True(result);
        }

        [Fact]
        public async void MarkUnread_ShouldWork()
        {
            Context.Http.PostReturns("{ 'success': true }");

            var api = Context.Api();
            var result = await api.Items.MarkUnread(1846);

            Assert.True(result);
        }

        [Fact]
        public async void MarkStarred_ShouldWork()
        {
            Context.Http.PostReturns("{ 'success': true }");

            var api = Context.Api();
            var result = await api.Items.MarkStarred(1846);

            Assert.True(result);
        }

        [Fact]
        public async void MarkUnstarred_ShouldWork()
        {
            Context.Http.PostReturns("{ 'success': true }");

            var api = Context.Api();
            var result = await api.Items.MarkUnstarred(1846);

            Assert.True(result);
        }

        [Fact]
        public async void MarAllkRead_ShouldWork()
        {
            Context.Http.PostReturns("{ 'success': true }");

            var api = Context.Api();
            var result = await api.Items.MarkAllRead(1846, 1845, 1844, 1843);

            Assert.True(result);
        }
    }
}
