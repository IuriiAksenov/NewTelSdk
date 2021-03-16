using System.Threading.Tasks;
using Xunit;

namespace NewTelSdk.UnitTests
{
    public class CallPasswordApiClientTests
    {
        private readonly CallPasswordApiClient _client;

        public CallPasswordApiClientTests()
        {
            // CallPasswordApiClient.IsDeveloperMode = true;
            _client = new CallPasswordApiClient(TestApiData.AccessKey, TestApiData.SignatureKey);
        }

        [Fact]
        public async Task CallPasswordCall_ValidData_ReturnSuccess()
        {
            var response = await _client.StartPasswordCallAsync(TestApiData.DestinationNumber, TestApiData.Pin);

            Assert.True(response.IsSuccess && response.Data!.IsSuccess);
        }
    }
}