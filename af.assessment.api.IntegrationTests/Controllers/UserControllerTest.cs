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
using af.assessment.api.Controllers;
using af.assessment.api.Data.Dtos;
using af.assessment.api.Data.DTOs.UpdateProfile;
using Newtonsoft.Json;
using Xunit;

namespace af.assessment.api.IntegrationTests.Controllers
{
    /// <summary>
    ///     Provides integration tests for the class <see cref="UserController"/>.
    /// </summary>
    [Collection("Database")]
    public class UserControllerTest : IClassFixture<ApiWebApplicationFactory>
    {
        /// <summary>
        ///     An <see cref="ApiWebApplicationFactory"/> representing the factory that will be used to boostrap the application in memory for functional tests.
        /// </summary>
        private readonly ApiWebApplicationFactory _factory;

        /// <summary>
        ///     A <see cref="String"/> representing the User API route.
        /// </summary>
        private readonly string url = "/api/User";
        

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserControllerTest"/> class with the factory that will be used.
        /// </summary>
        /// <param name="factory">
        ///     An <see cref="ApiWebApplication"/> representing the factory that will be used to boostrap the application in memory for functional tests.
        /// </param>
        public UserControllerTest(ApiWebApplicationFactory factory) => _factory = factory;

        /// <summary>
        ///     Tests that <see cref="UserController.GetProfileByid(memberId)"/> returns a success status code for valid credentials.
        /// </summary>
        [Fact]
        public async Task Get_GetProfileById_ReturnSuccessStatusCodeAsync()
        {
            // arrange
            var memberId = "2c2b6b15-3530-4a93-9a17-5f0bcd7423f9";
            var client = _factory.CreateClient();

            // act
            var response = await client.GetAsync(url + "/" + memberId);

            // assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseBody = await response.Content.ReadAsStringAsync();
            var userDto = JsonConvert.DeserializeObject<UserDto>(responseBody);

            Assert.Equal("Jamie Dimon", userDto.Name);
            Assert.Equal("jamie@eblocks.co.za", userDto.Email);
            Assert.Equal("0001010000006", userDto.IdentificationNumber);
            Assert.Equal("(087)-999-8765", userDto.MobileNumber);
            Assert.Equal("https://dcvcstorage.blob.core.windows.net/profilepics/ben.jpg", userDto.ProfilePictureUrl);
            Assert.Equal("Discovery Life", userDto.MedicalAidName);
            Assert.Equal("001334761", userDto.MedicalAidNumber);
            Assert.Equal("Jamie Dimon", userDto.MainMemberName);
            Assert.Equal("(087)-999-8765", userDto.MainMemberNumber);
            Assert.Equal(1220, userDto.PostalCode);
            Assert.Equal("Johannesburg", userDto.City);
            Assert.Equal("21st Street", userDto.StreetName);
        }

        /// <summary>
        ///     Tests that <see cref="UserController.GetProfileByid(memberId)"/> returns a <see cref="HttpStatusCode.InternalServerError"/> status code for empty guid string.
        /// </summary>
        [Fact]
        public async Task Get_GetProileById_ReturnInternalServerError()
        {
            // arrange
            var memberId = Guid.Empty.ToString();
            var client = _factory.CreateClient();
        
            // act
            var response = await client.GetAsync(url + '/' + memberId);
        
            // assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        /// <summary>
        ///     Tests that <see cref="UserController.GetProfileByid(memberId)"/> returns a <see cref="HttpStatusCode.BadRequest"/> status code for valid credentials.
        /// </summary>
        [Fact]
        public async Task Get_GetProileById_ReturnBadRequest()
        {
            // arrange
            var memberId = "2c2b6b15-3530-4a93-9a17-5f0bcd7423f0";
            var client = _factory.CreateClient();
        
            // act
            var response = await client.GetAsync(url + '/' + memberId);
        
            // assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
        /// <summary>
        ///     Tests that <see cref="UserController.UpdatePersonalProfileDetails(memberId, personalDetailsDto)"/> returns a <see cref="HttpStatusCode.BadRequest"/>, returns a <see cref="HttpStatusCode.NotFound"/>status code for valid credentials.
        /// </summary>
        /// <param name="memberID">
        ///      A <see cref="String"/> representing the guid field of a member.
        /// </param>
        /// <param name="name">
        ///     A <see cref="String"/> representing the name field of a member.
        /// </param>
        /// <param name="email">
        ///     A <see cref="String"/> representing the email field of a member.
        /// </param>
        /// <param name="identificationNumber">
        ///     A <see cref="String"/> representing the id field of a member.
        /// </param>
        /// <param name="mobileNumber">
        ///      A <see cref="String"/> representing the mobile number field of a member.
        /// </param>
        /// <param name="code">
        ///      A <see cref="HttpStatusCode"/> http response from the controller.
        /// </param>
        [Theory]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f8", "Jamie Dimon","jamie@eblocks.co.za","0001010000006","(087)-999-8765", HttpStatusCode.NotFound)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423", "Jamie Dimon","jamie@eblocks.co.za","0001010000006","(087)-999-8765", HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9", null,null,null,null, HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9", "","jamie@eblocks.co.za","0001010000006","(087)-999-8765", HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9", "Jamie Dimon","","0001010000006","(087)-999-8765", HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9", "Jamie Dimon","jamie@eblocks.co.za","","(087)-999-8765", HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9", "Jamie Dimon","jamie@eblocks.co.za","0001010000006","", HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9", "","","","", HttpStatusCode.BadRequest)]
        public async Task Post_UpdatePersonalProfileDetails_Unsuccessfully(string memberID, string name, string email, string identificationNumber, string mobileNumber, HttpStatusCode code)
        {
            //arrange 
            var dto = new PersonalDetailsDto
            {
                Name = name,
                Email = email,
                IdentificationNumber = identificationNumber,
                MobileNumber = mobileNumber
            };
            var memberUrl = url + "/personalDetails/" + memberID;
            var client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), UTF8Encoding.UTF8, "application/json");
            
            //act
            var response = await client.PostAsync(memberUrl, content);
            
            //assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(code, response.StatusCode);
        }
        
        /// <summary>
        ///     Tests that <see cref="UserController.UpdatePersonalProfileDetails(memberId, personalDetailsDto)"/> returns a <see cref="HttpStatusCode.BadRequest"/> status code for invalid credentials.
        /// </summary>
        [Fact]
        public async Task Post_UpdatePersonalProfileDetails_Unsuccessfully_Empty_Guid()
        {
            //arrange 
            var memberId =Guid.Empty;
            var dto = new PersonalDetailsDto
            {
                Name = "Jamie Dimon",
                Email = "jamie@eblocks.co.za",
                IdentificationNumber = "0001010000006",
                MobileNumber = "(087)-999-8765"
            };
            var memberUrl = url + "/personalDetails/" + memberId;
            var client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), UTF8Encoding.UTF8, "application/json");
            
            //act
            var response = await client.PostAsync(memberUrl, content);
            
            //assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        
        /// <summary>
        ///     Tests that <see cref="UserController.UpdatePersonalProfileDetails(memberId, personalDetailsDto)"/> returns a <see cref="HttpStatusCode.Ok"/> status code for valid credentials.
        /// </summary>
        [Fact]
        public async Task Post_UpdatePersonalProfileDetails_Successfully()
        {
            //arrange 
            var memberId =Guid.Parse("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9");
            var dto = new PersonalDetailsDto
            {
                Name = "Jamie Dimon",
                Email = "jamie@eblocks.co.za",
                IdentificationNumber = "0001010000006",
                MobileNumber = "(087)-999-8765"
            };
            var memberUrl = url + "/personalDetails/" + memberId;
            var client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), UTF8Encoding.UTF8, "application/json");
            
            //act
            var response = await client.PostAsync(memberUrl, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var userDto = JsonConvert.DeserializeObject<UserDto>(responseBody);
            
           
            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Jamie Dimon", userDto.Name);
            Assert.Equal("jamie@eblocks.co.za", userDto.Email);
            Assert.Equal("0001010000006", userDto.IdentificationNumber);
            Assert.Equal("(087)-999-8765", userDto.MobileNumber);
        }
        
        /// <summary>
        ///      Tests that <see cref="UserController.UpdateLocationDetails(memberId, locationDetailsDto)"/> returns a <see cref="HttpStatusCode.Ok"/> status code for valid credentials.
        /// </summary>
        [Fact]
        public async Task Post_UpdateProfileLocationDetails_Successfully()
        {
           //arrange
           var memberId =Guid.Parse("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9");
           var dto = new LocationDetailsDto
           {
                StreetName = "21st Street",
                PostalCode = 1220,
                City = "Johannesburg"
           };
           
           var memberUrl = url + "/locationDetails/" + memberId;
           var client = _factory.CreateClient();
           var content = new StringContent(JsonConvert.SerializeObject(dto), UTF8Encoding.UTF8, "application/json");
           
           //act
           var response = await client.PostAsync(memberUrl, content);
           var responseBody = await response.Content.ReadAsStringAsync();
           var userDto = JsonConvert.DeserializeObject<UserDto>(responseBody);
           
           //assert
           response.EnsureSuccessStatusCode();
           Assert.Equal(HttpStatusCode.OK, response.StatusCode);
           Assert.Equal("21st Street", userDto.StreetName );
           Assert.Equal(1220, userDto.PostalCode);
           Assert.Equal("Johannesburg",userDto.City);
        }
        
        /// <summary>
        ///     Tests that <see cref="UserController.UpdateProfileLocationDetails(memberId, locationDetailsDto)"/> returns a <see cref="HttpStatusCode.BadRequest"/> status code for invalid credentials.
        /// </summary>
        [Fact]
        public async Task Post_UpdateProfileLocationDetails_Unsuccessfully_Empty_Guid()
        {
            //arrange 
            var memberId =Guid.Empty;
            var dto = new LocationDetailsDto
            {
                StreetName = "21st Street",
                PostalCode = 1220,
                City = "Johannesburg"
            };
            var memberUrl = url + "/locationDetails/" + memberId;
            var client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), UTF8Encoding.UTF8, "application/json");
            
            //act
            var response = await client.PostAsync(memberUrl, content);
            
            //assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        
        /// <summary>
        ///     Tests that <see cref="UserController.UpdateProfileLocationDetails(memberId, locationDetailsDto)"/> returns a <see cref="HttpStatusCode.BadRequest"/> , returns a <see cref="HttpStatusCode.Notfound"/> or status code for invalid credentials.
        /// </summary>
        /// <param name="id">
        ///     A <see cref="String"/> representing the id number field of a member.
        /// </param>
        /// <param name="streetName">
        ///     A <see cref="String"/> representing the street name field of a member.
        /// </param>
        /// <param name="postalCode">
        ///     A <see cref="int"/> representing the postal code field of a member.
        /// </param>
        /// <param name="city">
        ///     A <see cref="String"/> representing the city name field of a member.
        /// </param>
        /// <param name="code">
        ///     A <see cref="HttpStatusCode"/> representing the http response to be expected.
        /// </param>
        [Theory]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f8", "21st Street",1220,"Johannesburg", HttpStatusCode.NotFound)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9", "",1220,"Johannesburg", HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9", "21st Street",null,"Johannesburg", HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9", "21st Street",1220,"", HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9", null,null,null, HttpStatusCode.BadRequest)]
         public async Task Post_UpdateProfileLocationDetails_Unsuccessfully(string id, string streetName, int postalCode, string city, HttpStatusCode code)
        {
            //arrange
            var memberId = id;
            var dto = new LocationDetailsDto
            {
                StreetName = streetName,
                PostalCode = postalCode,
                City = city
            };
            var memberUrl = url + "/locationDetails/" + memberId;
            var client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), UTF8Encoding.UTF8, "application/json");

            
            //act
            var response = await client.PostAsync(memberUrl, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            
            //assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(code, response.StatusCode);
        }
        
        /// <summary>
        ///     Tests that <see cref="UserController.UpdatemedicalAidDetails(memberId, medicalAidDetailsDto)"/> returns a <see cref="HttpStatusCode.Ok"/> status code for valid credentials.
        /// </summary>
        [Fact]
        public async Task Post_UpdateProfileMedicalAidDetails_successfully()
        {
            //arrange
            var memberId =Guid.Parse("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9");
            var dto = new MedicalAidDetailsDto
            {
                MedicalAidNumber = "001334761",
                MedicalAidName = "Discovery Life",
                MainMemberName = "Jamie Dimon",
                MainMemberNumber = "(087)-999-8765"
            };
            
            var memberUrl = url + "/medicalAidDetails/" + memberId;
            var client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), UTF8Encoding.UTF8, "application/json");
            
            //act
            var response = await client.PostAsync(memberUrl, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            var userDto = JsonConvert.DeserializeObject<UserDto>(responseBody);
            
            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("001334761", userDto.MedicalAidNumber);
            Assert.Equal("Discovery Life", userDto.MedicalAidName);
            Assert.Equal("Jamie Dimon", userDto.MainMemberName);
            Assert.Equal("(087)-999-8765", userDto.MainMemberNumber);
        }
        
        /// <summary>
        ///     Tests that <see cref="UserController.UpdateMedicalAidDetails(memberId, medicalAidDetailsDto)"/> returns a <see cref="HttpStatusCode.BadRequest"/> , returns a <see cref="HttpStatusCode.Notfound"/> or status code for invalid credentials.
        /// </summary>
        /// <param name="id">
        ///     A <see cref="String"/> representing the id field of a member.
        /// </param>
        /// <param name="medicalAidNumber">
        ///     A <see cref="String"/> representing the medical aid number field of a member.
        /// </param>
        /// <param name="medicalAidName">
        ///     A <see cref="String"/> representing the medical aid number field of a member.
        /// </param>
        /// <param name="mainMemberName">
        ///     A <see cref="String"/> representing the main member name field of a member.
        /// </param>
        /// <param name="mainMemeberNumber">
        ///     A <see cref="String"/> representing the main members' phone  field of a member.
        /// </param>
        /// <param name="code">
        ///     A <see cref="HttpStatusCode"/> representing the http expected response field from the controller.
        /// </param>
        [Theory]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f8", "001334761","Discovery Life","Jamie Dimon","(087)-999-8765", HttpStatusCode.NotFound)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f", "001334761","Discovery Life","Jamie Dimon","(087)-999-8765", HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f8", "","Discovery Life","Jamie Dimon","(087)-999-8765", HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f8", "001334761","","Jamie Dimon","(087)-999-8765", HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f8", "001334761","Discovery Life","","(087)-999-8765", HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f8", "001334761","Discovery Life","Jamie Dimon","", HttpStatusCode.BadRequest)]
        [InlineData("2c2b6b15-3530-4a93-9a17-5f0bcd7423f", null,null,null,null, HttpStatusCode.BadRequest)]
        public async Task Post_UpdateProfileMedicalAidDetails_Unsuccessfully(string id, string medicalAidNumber, string medicalAidName, string mainMemberName, string mainMemeberNumber, HttpStatusCode code)
        {
            //arrange
            var memberId = id;
            var dto = new MedicalAidDetailsDto
            {
                MedicalAidNumber = medicalAidNumber,
                MedicalAidName = medicalAidName,
                MainMemberName = mainMemberName,
                MainMemberNumber = mainMemeberNumber
            };
            
            var memberUrl = url + "/medicalAidDetails/" + memberId;
            var client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), UTF8Encoding.UTF8, "application/json");
            
            //act
            var response = await client.PostAsync(memberUrl, content);
            var responseBody = await response.Content.ReadAsStringAsync();
            
            //assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(code, response.StatusCode);
        }
        
        /// <summary>
        ///     Tests that <see cref="UserController.UpdateMedicalAidDetails(memberId, medicalAidDetailsDto)"/> returns a <see cref="HttpStatusCode.BadRequest"/> status code for invalid credentials.
        /// </summary>
        [Fact]
        public async Task Post_UpdateProfileMedicalAidDetails_Unsuccessfully_Empty_Guid()
        {
            //arrange 
            var memberId =Guid.Empty;
            var dto = new MedicalAidDetailsDto
            {
                MedicalAidNumber = "001334761",
                MedicalAidName = "Discovery Life",
                MainMemberName = "Jamie Dimon",
                MainMemberNumber = "(087)-999-8765"
            };
            var memberUrl = url + "/medicalAidDetails/" + memberId;
            var client = _factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(dto), UTF8Encoding.UTF8, "application/json");
            
            //act
            var response = await client.PostAsync(memberUrl, content);
            
            //assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}