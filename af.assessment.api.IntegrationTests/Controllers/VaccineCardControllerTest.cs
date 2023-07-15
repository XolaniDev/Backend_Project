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

using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace af.assessment.api.IntegrationTests.Controllers
{
    [Collection("Database")]
    public class VaccineCardControllerTest : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly ApiWebApplicationFactory _factory;
        private readonly string _url = "/api/VaccineCard";

        public VaccineCardControllerTest(ApiWebApplicationFactory factory) => _factory = factory;

        [Fact]
        public async Task Get_GetUserFamilyMembers_ReturnSuccessStatusCodeAsync()
        {
            // arrange
            var id = "2c2b6b15-3530-4a93-9a17-5f0bcd7423f9";
            var client = _factory.CreateClient();
        
            // act
            var response = await client.GetAsync(_url + '/' + id);
        
            // assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("00000000-0000-0000-0000-000000000000", HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f8", HttpStatusCode.NotFound)]
        public async Task Get_GetUserFamilyMembers_ReturnFailureStatusCodeAsync(string guid, HttpStatusCode code)
        {
            // arrange
            var id = guid;
            var client = _factory.CreateClient();
        
            // act
            var response = await client.GetAsync(_url + '/' + id);
        
            // assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(code, response.StatusCode);

        }

        
        
    }
}