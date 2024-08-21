using RestSharp;
using scadaTestTask.Models;
using System.Net;

namespace scadaTestTask.Tests
{
    public class ActivitiesTest : BaseTest
    {

        [Fact]
        public async Task GetAndCountActivitiesWithSpecificDate()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("api/v1/Activities", Method.Get);

            var response = await client.ExecuteAsync<List<ActivitiesModel>>(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Data);
            Assert.Equal(30, response.Data.Count);

            var yesterday = DateTime.UtcNow.AddDays(-1).Date;

            Assert.All(response.Data, activity =>
            {
                DateTime activityDueDate = DateTime.Parse(activity.DueDate).Date;
                Assert.NotEqual(yesterday, activityDueDate);
            });

        }
    }
}