using RestSharp;
using scadaTestTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace scadaTestTask.Tests
{
    public class BookTest : BaseTest
    {
        [Fact]
        public async Task UpdateBookAndVerifySuccess()
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("api/v1/Books/1", Method.Put);

            var requestBody = new BookModel
            {
                Id = 1,
                Title = "Kobzar",
                Description = "Taras Shevchenko book",
                PageCount = 190,
                Excerpt = "Yak ynry to pohovayte",
                PublishDate = DateTime.Now,
            };

            request.AddJsonBody(requestBody);

            var response = await client.ExecuteAsync<BookModel>(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Data);
            Assert.Equal(requestBody.Id, response.Data.Id);
            Assert.Equal(requestBody.Title, response.Data.Title);
            Assert.Equal(requestBody.Description, response.Data.Description);
            Assert.Equal(requestBody.PageCount, response.Data.PageCount);
            Assert.Equal(requestBody.Excerpt, response.Data.Excerpt);
            Assert.InRange(response.Data.PublishDate,
            requestBody.PublishDate.AddMilliseconds(-1),
            requestBody.PublishDate.AddMilliseconds(1));
        }

        [Fact]
        public async Task GetRandomBookAndVerifySuccess()
        {
            var random = new Random();
            int randomId = random.Next(1, 11);
            int expectedPageCount = randomId * 100;

            var client = new RestClient(BaseUrl);
            var request = new RestRequest($"api/v1/Books/{randomId}", Method.Get);

            var response = await client.ExecuteAsync<BookModel>(request);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Assert.NotNull(response.Data);
                Assert.Equal(expectedPageCount, response.Data.PageCount);
            }
        }
    }
}
