/*
 * [2019] - [2021] Eblocks Software (Pty) Ltd, All Rights Reserved.
 * NOTICE: All information contained herein is, and remains the property of Eblocks
 * Software (Pty) Ltd.
 * and its suppliers (if any). The intellectual and technical concepts contained herein
 * are proprietary
 * to Eblocks Software (Pty) Ltd. and its suppliers (if any) and may be covered by South 
 * African, U.S.
 * and Foreign patents, patents in process, and are protected by trade secret and / or 
 * copyright law.
 * Dissemination of this information or reproduction of this material is forbidden unless
 * prior written
 * permission is obtained from Eblocks Software (Pty) Ltd.
*/

using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using af.assessment.api.Data.Dtos;
using Newtonsoft.Json;
using Xunit;

namespace af.assessment.api.IntegrationTests.Controllers
{
    /// <summary>
    ///     Provides integration tests for the class <see cref="LoginController"/>.
    /// </summary>
    [Collection("Database")]
    public class LoginControllerTest : IClassFixture<ApiWebApplicationFactory>
    {
        /// <summary>
        ///     An <see cref="ApiWebApplicationFactory"/> representing the factory that will be used to boostrap the application in memory for functional tests.
        /// </summary>
        private readonly ApiWebApplicationFactory _factory;

        /// <summary>
        ///     A <see cref="String"/> representing the Login API route.
        /// </summary>
        private readonly String url = "/api/Login";

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginControllerTest"/> class with the factory that will be used.
        /// </summary>
        /// <param name="factory">
        ///     An <see cref="ApiWebApplication"/> representing the factory that will be used to boostrap the application in memory for functional tests.
        /// </param>
        public LoginControllerTest(ApiWebApplicationFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        ///     Tests that <see cref="LoginController.LogUserIn(memberDto)"/> returns a success status code for valid credentials.
        /// </summary>
        [Fact]
        public async Task Post_LogUserIn_ReturnSuccessCodeAsync()
        {
            // arrange
            var memberDto = new LoginDto 
            {
                IdentificationNumber = "0001010000006",
                Password = "Atleti987@"
            };
            var content = new StringContent(JsonConvert.SerializeObject(memberDto), UTF8Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();
        
            // act
            var response = await client.PostAsync(url, content); 
        
            // assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /// <summary>
        ///     Tests that <see cref="LoginController.LogUserIn(memberDto)"/> returns the correct failure code for different failure scenarios.
        /// </summary>
        /// <param name="id">
        ///     A <see cref="String"/> representing the id field for login credential.
        /// </param>
        /// <param name="pwd">
        ///     A <see cref="String"/> representing the password for login credential.
        /// </param>
        /// <param name="code">
        ///     A <see cref="HttpStatusCode"/> representing the expected status code for the given login credentials.
        /// </param>
        [Theory]
        [InlineData("", "", HttpStatusCode.BadRequest)]
        [InlineData("0001010000006", "", HttpStatusCode.BadRequest)]
        [InlineData(null, "Password1@", HttpStatusCode.BadRequest)]
        [InlineData("0001010000006", null, HttpStatusCode.BadRequest)]
        [InlineData("1010101010104", "Password1@", HttpStatusCode.NotFound)]
        [InlineData("0001010000006", "Password1!", HttpStatusCode.Unauthorized)]
        public async Task Post_LogInUser_ReturnFailureCodeAsync(String id, String pwd, HttpStatusCode code)
        {
            // arrange
            var memberDto = new LoginDto
            {
                IdentificationNumber = id,
                Password = pwd
            };
            var content = new StringContent(JsonConvert.SerializeObject(memberDto), UTF8Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();
        
            // act
            var response = await client.PostAsync(url, content);
        
            // assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(code, response.StatusCode);
        }

    }
}