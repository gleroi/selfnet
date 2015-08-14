using Xunit;

namespace Selfnet.Tests
{
    public class LoginTests
    {
        [Fact]
        public async void Login_ShouldWork()
        {
            Context.Http.GetReturns("{ 'success': true }");



            var api = Context.Api();

            var result = await api.Login();

            Assert.True(result);
        }
    }
}