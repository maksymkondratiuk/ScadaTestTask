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
    public class AuthorTest : BaseTest
    {
        private int authorId = -1;

        [Fact]
        public async Task CreateNewAuthorAndVerifyIt() 
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("api/v1/Authors", Method.Post);
            var random = new Random();
            int randomId = random.Next(1, 11);

            var requestBody = new AuthorModel
            {
                Id = randomId,
                IdBook = 1,
                FirstName = "Maks",
                LastName = "Kondratiuk"
            };

            request.AddJsonBody(requestBody);

            var response = await client.ExecuteAsync<AuthorModel>(request);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Data);
            Assert.Equal(requestBody.Id, response.Data.Id);
            Assert.Equal(requestBody.IdBook, response.Data.IdBook);
            Assert.Equal(requestBody.FirstName, response.Data.FirstName);
            Assert.Equal(requestBody.LastName, response.Data.LastName);
            authorId = response.Data.Id;
        }

        [Fact]
        public async Task DeleteCreatedAuthor()
        {
            if (authorId == -1)
            {
                await CreateNewAuthorAndVerifyIt();
            } 

            var client = new RestClient(BaseUrl);
            var request = new RestRequest($"api/v1/Authors/{authorId}", Method.Delete);

            var deleteResponse = await client.ExecuteAsync(request);

            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
        }
    }
}
