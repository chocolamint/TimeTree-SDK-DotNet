namespace TimeTreeSdkDotNet.Tests
{
    public class TimeTreeClientTest
    {
        [Fact]
        public void CanCreateInstance()
        {
            var ex = Record.Exception(() => new TimeTreeClient());
            Assert.Null(ex);
        }
    }
}