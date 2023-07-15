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
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using af.assessment.api.Data.Models;
using af.assessment.api.Services;
using af.assessment.api.Stores;

namespace af.assessment.api.test.Services
{
    /// <summary>
    ///     Provide unit test to test <see cref="UserService"/> class.
    /// </summary>
    public class UserServiceTest
    {
        /// <summary>
        ///       A <see cref="UserService"/>  represent the service that contains the method to be tested.
        /// </summary>
        private readonly UserService _userService;

        /// <summary>
        ///      A <see cref="Mock"/> implementation of <see cref="IUserService"/> representing the store to be mocked.
        /// </summary>
        private readonly Mock<IUserStore> _storeMock = new Mock<IUserStore>();

        /// <summary>
        ///      Initialise an  instance of the <see cref="UserServiceTest"/>  class.
        /// </summary>
        public UserServiceTest()
        {
            _userService = new UserService(_storeMock.Object);
        }

        /// <summary>
        ///     Test that <see cref="UserService.GetProfileById(Guid)"/>   returns the user information.
        /// </summary>
        [Fact]
        public async void Should_View_Profile_By_Id()
        {
            //Arrange
            var memberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb5539f");
            var newMember = new Member
            {
                Id = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb5539f"),
                Name = ("Sarah Mallon"),
                Email = ("sarah.mallon@gmail.com"),
                IdentificationNumber = "92030067091",
                MobileNumber = "0867789965",
                Locations = new Location(),
                FamilyMember = new List<FamilyMember>()
                {
                    new FamilyMember()
                },
                MedicalDetails = new MedicalDetails()
            };

            _storeMock.Setup(s => s.GetProfileById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(newMember));
            //Act
            var result = await _userService.GetProfileById(memberId);
            //Assert
            Assert.Equal(memberId, result.Id);
        }

        /// <summary>
        ///      Test that <see cref="UserService.GetProfileById(Guid)"/>   returns the user does not exists.
        /// </summary>
        [Fact]
        public async void Should_Not_Get_Profile_If_Id_Does_Not_Exist()
        {
            //Arrange
            var memberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb5539f");
            var newMember = new Member
            {
                Id = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb55395"),
                Name = ("Sarah Mallon"),
                Email = ("sarah.mallon@gmail.com"),
                IdentificationNumber = "92030067091",
                MobileNumber = "0867789965",
                Locations = new Location(),
                FamilyMember = new List<FamilyMember>()
                {
                    new FamilyMember()
                },
                MedicalDetails = new MedicalDetails()
            };

            _storeMock.Setup(s => s.GetProfileById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(newMember));
            //Act
            var result = await _userService.GetProfileById(memberId);
            //Assert
            Assert.NotEqual(memberId, result.Id);
        }
        
        /// <summary>
        ///     Test case to return <see cref="null"/> when location does not exist in database.
        /// </summary>        
        [Fact]
        public async Task GetLocationByMemberId_Should_Return_Null_When_Location_Does_not_Exist()
        {
            // Arrange
            var memberId = Guid.Parse("dca21998-eeab-4291-aae2-44a7b5e8ff03");
            _storeMock.Setup(x => x.GetLocationByMemberId(It.IsAny<Guid>())).ReturnsAsync(() => null);

            // Act
            var result = await _userService.GetLocationByMemberId(memberId);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        ///     Test case to return <see cref="Location"/> that belongs to member.
        /// </summary>        
        [Fact]
        public async Task GetLocationByMemberId_Should_Return_Location()
        {
            // Arrange
            var memberId = Guid.Parse("dca21998-eeab-4291-aae2-44a7b5e8ff03");
            var location = new Location()
            {
                City = "Joburg",
                StreetName = "11 Moosa Hassen",
                PostalCode = 1813
            };

            _storeMock.Setup(x => x.GetLocationByMemberId(It.IsAny<Guid>())).ReturnsAsync(() => location);

            // Act
            var result = await _userService.GetLocationByMemberId(memberId);

            // Assert
            Assert.Equal(result.City, location.City);
            Assert.Equal(result.PostalCode, location.PostalCode);
            Assert.Equal(result.StreetName, location.StreetName);
        }

        /// <summary>
        ///     Test case to return <see cref="null"/> when medical details does not exist in database.
        /// </summary>        
        [Fact]
        public async Task GetMedicalDetailsByMemberId_Should_Return_Null_When_Medica_Details_Does_not_Exist()
        {
            // Arrange
            var memberId = Guid.Parse("dca21998-eeab-4291-aae2-44a7b5e8ff03");
            _storeMock.Setup(x => x.GetMedicalDetailsByMemberId(It.IsAny<Guid>())).ReturnsAsync(() => null);

            // Act
            var result = await _userService.GetMedicalDetailsByMemberId(memberId);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        ///     Test case to successfully return a <see cref="MedicalDetails"/> that belongs to member.
        /// </summary>        
        [Fact]
        public async Task GetMedicalDetailsByMemberId_Should_Return_MedicalDetails()
        {
            // Arrange
            var memberId = Guid.Parse("dca21998-eeab-4291-aae2-44a7b5e8ff03");
            var medicalDetails = new MedicalDetails()
            {
                MedicalAidName = "Discovery",
                MainMemberName = "Jamie Dimon",
                MedicalAidNumber = "123456",
                MainMemberNumber = "0813658532",
            };

            _storeMock.Setup(x => x.GetMedicalDetailsByMemberId(It.IsAny<Guid>())).ReturnsAsync(() => medicalDetails);

            // Act
            var result = await _userService.GetMedicalDetailsByMemberId(memberId);

            // Assert
            Assert.Equal(result.MedicalAidNumber, medicalDetails.MedicalAidNumber);
            Assert.Equal(result.MedicalAidName, medicalDetails.MedicalAidName);
            Assert.Equal(result.MainMemberName, medicalDetails.MainMemberName);
            Assert.Equal(result.MainMemberNumber, medicalDetails.MainMemberNumber);
        }

        /// <summary>
        ///     Test that the appropriate save methods are called.
        /// </summary>
        [Fact]
        public async void Save()
        {
            // Arrange 
            _storeMock.Setup(x => x.Save()).ReturnsAsync(() => 1);

            // Act
            var result = await _userService.Save();

            // Assert
            Assert.True(result > 0);
            _storeMock.Verify(x => x.Save(), Times.Once);
        }
    }
}