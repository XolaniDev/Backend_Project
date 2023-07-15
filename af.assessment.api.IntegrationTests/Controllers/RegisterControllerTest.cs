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

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using af.assessment.api.Data.Dtos;
using Newtonsoft.Json;
using Xunit;

namespace af.assessment.api.IntegrationTests.Controllers
{
    /// <summary>
    ///     Provides integration tests for the class <see cref="RegisterController"/>.
    /// </summary>
    [Collection("Database")]
    public class RegisterControllerTest : IClassFixture<ApiWebApplicationFactory>
    {
        /// <summary>
        ///     An <see cref="ApiWebApplicationFactory"/> representing the factory that will be used to boostrap the application in memory for functional tests.
        /// </summary>
        private readonly ApiWebApplicationFactory _factory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RegisterControllerTest"/> class with the factory that will be used.
        /// </summary>
        /// <param name="factory">
        ///     An <see cref="ApiWebApplication"/> representing the factory that will be used to boostrap the application in memory for functional tests.
        /// </param>
        public RegisterControllerTest(ApiWebApplicationFactory factory)
        {
            _factory = factory;
        }
        
        /// <summary>
        ///     Tests that <see cref="RegisterController.RegisterMember(newRegisterdto)"/> returns a success code for valid user details.
        /// </summary>
        [Fact]
        public async Task Post_RegisterMember_ReturnSuccessCodeAsync()
        {
            // arrange
            var registerDto = new RegisterDto 
            {
                Name = "John Smith",
                Email = "john.smith@email.com",
                IdentificationNumber = "0001010000105",
                MobileNumber = "(012)-345-6789",
                Password = "Password1@",
                OtpPreference = 1
            };
            var content = new StringContent(JsonConvert.SerializeObject(registerDto), UTF8Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();

            // act
            var response = await client.PostAsync("/api/Register", content);

            // assert
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        ///     Tests that <see cref="RegisterController.RegisterMember(newRegisterdto)"/> returns a bad request status code for valid user details with an id that already exists in the database.
        /// </summary>
        [Fact]
        public async Task Post_RegisterMember_ReturnBadRequestIdExistsAsync()
        {
            // arrange
            var registerDto = new RegisterDto 
            {
                Name = "John Smith",
                Email = "john.smith@email.com",
                IdentificationNumber = "0001010000006",
                MobileNumber = "(012)-345-6789",
                Password = "Password1@",
                OtpPreference = 1
            };
            var content = new StringContent(JsonConvert.SerializeObject(registerDto), UTF8Encoding.UTF8, "application/json");
            var client = _factory.CreateClient(); 
        
            // act
            var response = await client.PostAsync("/api/Register", content);

            // assert
            Assert.False(response.IsSuccessStatusCode);
        }

        /// <summary>
        ///     Tests that <see cref="RegisterController.RegisterMember(newRegisterdto)"/> returns a bad request status code for invalid user details.
        /// </summary>
        [Fact]
        public async Task Post_RegisterMember_ReturnBadRequestValidationFailAsync()
        {
            // arrange
            var registerDto = new RegisterDto 
            {
                Name = "John Smith",
                Email = "john.smith",
                IdentificationNumber = "0001010000009",
                MobileNumber = "(012) 345-6789",
                Password = "Password1",
                OtpPreference = 0
            };
            var content = new StringContent(JsonConvert.SerializeObject(registerDto), UTF8Encoding.UTF8, "application/json");
            var client = _factory.CreateClient(); 
        
            // act
            var response = await client.PostAsync("/api/Register", content);

            // assert
            Assert.False(response.IsSuccessStatusCode);
        }
    }
}
