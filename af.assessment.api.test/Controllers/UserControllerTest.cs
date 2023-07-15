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

using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using af.assessment.api.Controllers;
using af.assessment.api.Services;
using af.assessment.api.Data.Models;
using af.assessment.api.Converters;
using af.assessment.api.Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using af.assessment.api.Data.DTOs.UpdateProfile;

namespace af.assessment.api.test.Controllers
{
    /// <summary>
    ///     Provide unit test to test <see cref="UserController"/> class.
    /// </summary>
    public class UserControllerTest
    {
        /// <summary>
        ///      A <see cref="UserController"/>  represent the controller that contains the method to be tested.
        /// </summary>
        private readonly UserController _userController;

        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="IUserService"/> representing the service to be mocked.  
        /// </summary>
        private readonly Mock<IUserService> _serviceMock = new Mock<IUserService>();

        private readonly Mock<IUserDtoConverter> _converterMock = new Mock<IUserDtoConverter>();

        /// <summary>
        ///     A <see cref="Logger"/> implementation of <see cref="_logger"/>  representing the logger that is being mocked.
        /// </summary>
        private readonly Mock<ILogger<UserController>> _logger;
        
        /// <summary>
        ///     Initialise an  instance of the <see cref="UserControllerTest"/> class.
        /// </summary>
        public UserControllerTest()
        {
            var logger = new Mock<ILogger<UserController>>();
            _userController = new UserController(_serviceMock.Object, logger.Object, _converterMock.Object);
        }

        /// <summary>
        ///      Test case for retrieving a user successfully.
        /// </summary>
        [Fact]
        public async Task GetProfileById_Should_Get_Profile_By_Id()
        {
            //Arrange
            var memberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb5539f");
            var newMember = new Member
            {
                Id = memberId,
                Name = ("Sarah Mallon"),
                Email = ("sarah.mallon@gmail.com"),
                IdentificationNumber = "92030067091",
                MobileNumber = "0867789965",              
            };
            var dto = new UserDto
            {
                Name = "Sarah Mallon",
                Email = "sarah.mallon@gmail.com",
                IdentificationNumber = "92030067091",
                MobileNumber = "0867789965",
                MedicalAidName = "",
                MedicalAidNumber = "",
                MainMemberName = "",
                MainMemberNumber = "",
                StreetName = "",
                PostalCode = 0,
                City = ""
            };

            _serviceMock.Setup(s => s.GetProfileById(It.IsAny<Guid>()))
                .Returns(Task.FromResult(newMember));
            _converterMock.Setup(x => x.ConvertToUserDto(It.IsAny<Member>())).Returns(dto);

            //Act
            var actionResult = await _userController.GetProfileById(memberId) as OkObjectResult;
            var result = actionResult.Value;

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsType<UserDto>(result);
        }

        /// <summary>
        ///    Should return <see cref="BadRequestResult"/> when memberId is empty. 
        /// </summary>
        [Fact]
        public async Task GetProfileById_Should_Return_BadRequest()
        {
            //Arrange
            var memberId = Guid.Empty;

            _serviceMock.Setup(s => s.GetProfileById(It.IsAny<Guid>()))
                .Returns(Task.FromResult<Member>(null));

            //Act
            var actionResult = await _userController.GetProfileById(memberId) as BadRequestResult;            

            //Assert            
            Assert.IsType<BadRequestResult>(actionResult);            
        }

        /// <summary>
        ///     Should return <see cref="NotFoundResult"/> when the member doesnt exist.
        /// </summary>       
        [Fact]
        public async Task GetProfileById_Should_Return_NotFound()
        {
            // Arrange
            var memberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb5539f");
            var emptyUser = new PersonalDetailsDto();

            _serviceMock.Setup(x => x.GetProfileById(memberId)).ReturnsAsync(() => null);

            // Act
            var actionResult = await _userController.UpdateProfilePersonalDetails(memberId, emptyUser) as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }


        /// <summary>
        ///     Test case for returning <see cref="BadRequestResult"/> when memberId guid is empty.
        /// </summary>        
        [Fact]
        public async Task UpdateProfilePersonalDetails_Should_Return_Bad_Request_When_Guid_Is_Empty()
        {
            // arrange
            var memberId = Guid.Empty;
            _serviceMock.Setup(s => s.GetProfileById(memberId)).Returns(Task.FromResult<Member>(null));

            // Act
            var actionResult = await _userController.UpdateProfilePersonalDetails(memberId, new PersonalDetailsDto()) as BadRequestResult;

            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        /// <summary>
        ///     Test case for returning <see cref="NotFoundResult"/> when member does not exist.
        /// </summary>       
        [Fact] 
        public async Task UpdateProfilePersonalDetails_Should_Return_Not_Found_When_Member_Does_Not_Exist()
        {
            // Arrange
            var memberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb5539f");            

            _serviceMock.Setup(s => s.GetProfileById(memberId)).Returns(Task.FromResult<Member>(null));

            // Act
            var result = await _userController.UpdateProfilePersonalDetails(memberId, new PersonalDetailsDto()) as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }        

        /// <summary>
        ///     Test case for succesfully updating member personal details.
        /// </summary>        
        [Fact]
        public async Task UpdateProfilePersonalDetails_Should_Return_Member_With_Updated_Personal_Details()
        {
            // Arrange
            var memberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb5539f");

            var dto = new PersonalDetailsDto()
            {
                Name = "Sarah Mallon",
                IdentificationNumber = "9203070911083",
                MobileNumber = "0867789965",
                Email = "sarah.mallon@gmail.com"
            };

            var userDto = new UserDto() 
            {
                Name = "Sarah Mallon",
                IdentificationNumber = "9203070911083",
                MobileNumber = "0867789965",
                Email = "sarah.mallon@gmail.com"
            };
            var updatedMember = new Member
            {
                Id = memberId,
                Name = ("Sarah Mallon"),
                Email = ("sarah.mallon@gmail.com"),
                IdentificationNumber = "9203070911083",
                MobileNumber = "0867789965",
            };

            _serviceMock.Setup(x => x.GetProfileById(It.IsAny<Guid>())).ReturnsAsync(() => updatedMember);
            _converterMock.Setup(x => x.ConvertToUserDto(It.IsAny<Member>())).Returns(userDto);

            // Act
            var actionResult = await _userController.UpdateProfilePersonalDetails(memberId, dto) as OkObjectResult;
            var result = actionResult.Value as UserDto;

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsType<UserDto>(result);            
            Assert.Equal(result.Name, dto.Name);
            Assert.Equal(result.MobileNumber, dto.MobileNumber);
            Assert.Equal(result.Email, dto.Email);
            Assert.Equal(result.IdentificationNumber, dto.IdentificationNumber);
            _serviceMock.Verify(x => x.Save(), Times.Once);
        }

        /// <summary>
        ///     Test case for returning <see cref="BadRequestResult"/> when memberId guid is empty.        
        /// </summary>        
        [Fact]
        public async Task UpdateProfileLocationDetails_Should_Return_Bad_Request_When_MenmberId_Guid_Is_Empty()
        {
            // Arrange
            var memberId = Guid.Empty;

            // Act 
            var actionResult = await  _userController.UpdateProfileLocationDetails(memberId, new LocationDetailsDto()) as BadRequestResult;

            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        /// <summary>
        ///     Test case for returning <see cref="NotFoundResult"/> when member doesnt exist.
        /// </summary>        
        [Fact]
        public async Task UpdateProfileLocationDetails_Should_Return_Not_Found_When_Member_Location_Doesnt_Exist()
        {
            // Arrange
            var memberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb5539f");
            _serviceMock.Setup(x => x.GetProfileById(It.IsAny<Guid>())).ReturnsAsync(() => null);

            // Act
            var actionResult = await _userController.UpdateProfileLocationDetails(memberId, new LocationDetailsDto()) as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }


        /// <summary>
        ///     Test case for succesfully updating member location details.
        /// </summary>        
        [Fact]
        public async Task UpdateProfileLocationDetails_Should_Return_Member_With_Updated_Location_Details()
        {
            // Arrange
            var memberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb5539f");
            var locationId = Guid.Parse("35c1a665-7bb5-43f2-9423-beb9afdda515");

            var userDto = new UserDto()
            {
                City = "Joburg",
                StreetName = "11 Moosa Hassen",
                PostalCode = 1813
            };

            var updatedLocation = new Location()
            {
                City = "Joburg",
                StreetName = "11 Moosa Hassen",
                PostalCode = 1813
            };

            var dto = new LocationDetailsDto()
            {
                City = "Joburg",
                StreetName = "11 Moosa Hassen",
                PostalCode = 1813
            };

            var updatedMember = new Member
            {
                Id = memberId,
                Name = ("Sarah Mallon"),
                Email = ("sarah.mallon@gmail.com"),
                IdentificationNumber = "9203070911083",
                MobileNumber = "0867789965",
            };

            _serviceMock.Setup(x => x.GetLocationByMemberId(It.IsAny<Guid>())).ReturnsAsync(() => updatedLocation);
            _serviceMock.Setup(x => x.GetProfileById(It.IsAny<Guid>())).ReturnsAsync(() => updatedMember);
            _converterMock.Setup(x => x.ConvertToUserDto(It.IsAny<Member>())).Returns(userDto);

            // Act
            var actionResult = await _userController.UpdateProfileLocationDetails(memberId, dto) as OkObjectResult;
            var result = actionResult.Value as UserDto;

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsType<UserDto>(result);
            Assert.Equal(result.City, dto.City);
            Assert.Equal(result.PostalCode, dto.PostalCode);
            Assert.Equal(result.StreetName, dto.StreetName);
            _serviceMock.Verify(x => x.Save(), Times.Once);
        }

        /// <summary>
        ///     Test case for returning <see cref="BadRequestResult"/> when memberId guid is empty.
        /// </summary>        
        [Fact]
        public async Task UpdateProfileMedicalAidDetails_Should_Return_BadRequest_When_MemberId_Is_Empty()
        {
            // Arrange
            var memberId = Guid.Empty;

            // Act
            var actionResult = await _userController.UpdateProfileMedicalAidDetails(memberId, new MedicalAidDetailsDto()) as BadRequestResult;

            // Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        /// <summary>
        ///     Test case for returning <see cref="NotFoundResult"/> when member does not exist.            
        /// </summary>        
        [Fact]
        public async Task UpdateProfileMedicalAidDetails_Should_Return_NotFound_When_Member_Medical_Details_Does_Not_Exist()
        {
            // Arrange
            var memberId = Guid.NewGuid();
            _serviceMock.Setup(x => x.GetProfileById(It.IsAny<Guid>())).ReturnsAsync(() => null);

            // Act
            var actionResult = await _userController.UpdateProfileMedicalAidDetails(memberId, new MedicalAidDetailsDto()) as NotFoundResult;

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        /// <summary>
        ///      Test case for successfully updating member medical aid details.
        /// </summary>        
        [Fact]
        public async Task UpdateProfileMedicalAidDetails_Should_Return_Member_With_Updated_MedicalAid_Details()
        {
            // Arrange
            var memberId = Guid.NewGuid();

            var medicalDetails = new MedicalDetails()
            {
                Id = Guid.NewGuid(),
                MedicalAidName = "Discovery",
                MainMemberName = "Jamie Dimon",
                MedicalAidNumber = "123456",
                MainMemberNumber = "0813658532",
                MemberId = memberId
            };

            var dto = new MedicalAidDetailsDto() 
            {
                MainMemberName = "Jamie Mark Dimon",
                MedicalAidNumber = "12345678",
                MainMemberNumber = "0731494119",
                MedicalAidName = "Bonnitas"

            };
            var updatedMember = new Member()
            {
                Id = memberId,                
            };

            var userDto = new UserDto() 
            {
                MainMemberName = "Jamie Mark Dimon",
                MainMemberNumber = "0731494119",
                MedicalAidNumber = "12345678",
                MedicalAidName = "Bonnitas"
            };

            _serviceMock.Setup(x => x.GetMedicalDetailsByMemberId(It.IsAny<Guid>())).ReturnsAsync(() => medicalDetails);
            _serviceMock.Setup(x => x.GetProfileById(It.IsAny<Guid>())).ReturnsAsync(() => updatedMember);
            _converterMock.Setup(x => x.ConvertToUserDto(It.IsAny<Member>())).Returns(userDto);

            // Act
            var actionResult = await _userController.UpdateProfileMedicalAidDetails(memberId, dto) as OkObjectResult;
            var result = actionResult.Value as UserDto;

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsType<UserDto>(result);
            Assert.Equal(result.MedicalAidName, dto.MedicalAidName);
            Assert.Equal(result.MedicalAidNumber, dto.MedicalAidNumber);
            Assert.Equal(result.MainMemberName, dto.MainMemberName);
            Assert.Equal(result.MainMemberNumber, dto.MainMemberNumber);
            _serviceMock.Verify(x => x.Save(), Times.Once);
        }
    }
}