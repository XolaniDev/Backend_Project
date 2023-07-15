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
    ///      Provides integration tests for the class <see cref="ResetPasswordController"/>.
    /// </summary>
    [Collection("Database")]
    public class ResetPasswordControllerTest : IClassFixture<ApiWebApplicationFactory>
    {
        /// <summary>
        ///     An <see cref="ApiWebApplicationFactory"/> representing the factory that will be used to boostrap the application in memory for functional tests.
        /// </summary>
        private readonly ApiWebApplicationFactory _factory;
        
        /// <summary>
        ///     A <see cref="string"/> representing the Reset Password API route.
        /// </summary>
        private readonly String url = "/api/ResetPassword";

        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginControllerTest"/> class with the factory that will be used.
        /// </summary>
        /// <param name="factory">
        ///     An <see cref="ApiWebApplication"/> representing the factory that will be used to boostrap the application in memory for functional tests.
        /// </param>
        public ResetPasswordControllerTest(ApiWebApplicationFactory factory)
        {
            _factory = factory;
        }
        
        /// <summary>
        ///     Tests that <see cref="ResetPasswordController.VerifyUserExists(idNumber)"/> returns a success status code for valid credentials.
        /// </summary>
        [Fact]
        public async Task Get_VerifyUser_ReturnSuccessCodeAsync()
        {
            //Arrange 
            var IdentificationNumber = "0001010000006";
            var client = _factory.CreateClient();
            
            //Act
            var response = await client.GetAsync(url + "/" + IdentificationNumber);
            var responseBody = await response.Content.ReadAsStringAsync();
            var userResponse = JsonConvert.DeserializeObject<string>(responseBody);
            
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9",userResponse);
        }

        /// <summary>
        ///     Tests that <see cref="ResetPasswordController.VerifyUserExists(idNumber)"/> returns a unsuccessful status code for valid credentials for a non-existing ID.
        /// </summary>
        [Fact]
        public async Task Get_VerifyUser_ReturnUnsuccessfulCodeAsync()
        {
            //Arrange 
            var IdentificationNumber = "1010101010104";
            var client = _factory.CreateClient();
            var responseCode = HttpStatusCode.NotFound;
            
            //Act
            var response = await client.GetAsync(url + "/" + IdentificationNumber);
            
            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(responseCode, response.StatusCode);
        }
        
        /// <summary>
        ///     Tests that <see cref="ResetPasswordController.ChangePassword(resetPasswordDtO.MemberId, saltDTO.Salt.ToString(), saltDTO.HashResult.ToString())"/> returns a successful status code for valid credentials for a existing ID.
        /// </summary>
        [Fact]
        public async Task Post_ChangePassword_ReturnSuccessCodeAsync()
        {
            //Arrange
            var memberGuid = Guid.Parse("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9");
            var resetPasswordDto = new ResetPasswordDto() 
            {
                MemberId = memberGuid,
                Password = "Atleti987!",
                ConfirmPassword = "Atleti987!"
            };
            var content = new StringContent(JsonConvert.SerializeObject(resetPasswordDto), UTF8Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();
            
            //Act
            var response = await client.PostAsync(url, content); 
            
            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        /// <summary>
        ///     Tests that <see cref="ResetPasswordController.ChangePassword(resetPasswordDtO.MemberId, saltDTO.Salt.ToString(), saltDTO.HashResult.ToString())"/> returns a Unsuccessful status code for valid credentials for non-existing ID.
        /// </summary>
        [Fact]
        public async Task Post_ChangePassword_ReturnUnsuccessfulCodeAsync()
        {
            //Arrange
            var memberGuid = Guid.NewGuid();
            var responseCode = HttpStatusCode.NotFound;
            var resetPasswordDto = new ResetPasswordDto() 
            {
                MemberId = memberGuid,
                Password = "Atleti987!",
                ConfirmPassword = "Atleti987!"
            };
            var content = new StringContent(JsonConvert.SerializeObject(resetPasswordDto), UTF8Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();
            
            //Act
            var response = await client.PostAsync(url, content); 
            
            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(responseCode, response.StatusCode);
        }
    }
}