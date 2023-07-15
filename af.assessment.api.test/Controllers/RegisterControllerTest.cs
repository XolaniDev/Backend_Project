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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using af.assessment.api.Controllers;
using af.assessment.api.Services;
using af.assessment.api.Data.Models;
using af.assessment.api.Data.Dtos;
using af.assessment.api.Converters;
using af.assessment.api.Utilities;

namespace af.assessment.api.test.Controllers
{
    /// <summary>
    ///     Provide unit test to test <see cref="RegisterController"/> class.
    /// </summary>
    public class RegisterControllerTest
    {
        /// <summary>
        ///     A <see cref="RegisterController"/>  represent the controller that contains the method to be tested.
        /// </summary>
        private readonly RegisterController _controller;

        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="IRegisterService"/> representing the service to be mocked.
        /// </summary>
        private readonly Mock<IRegisterService> _serviceMock = new Mock<IRegisterService>();
        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="IRegisterDtoConverter"/> representing the register converter to be mocked.
        /// </summary>
        private readonly Mock<IRegisterDtoConverter> _converterMock = new Mock<IRegisterDtoConverter>();
        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="ILogger"/> representing the register converter to be mocked.
        /// </summary>
        private readonly Mock<ILogger<RegisterController>> _loggerMock = new Mock<ILogger<RegisterController>>();
        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="IPasswordHasher"/> representing the password hasher to be mocked.
        /// </summary>
        private readonly Mock<IPasswordHasher> _passwordHasherMock = new Mock<IPasswordHasher>();

        /// <summary>
        ///     Initialise an  instance of the <see cref="RegisterControllerTest"/> class.
        /// </summary>
        public RegisterControllerTest() {
            _controller = new RegisterController(_serviceMock.Object, _converterMock.Object, _loggerMock.Object, _passwordHasherMock.Object);
        }

        /// <summary>
        ///      Test that <see cref="RegisterController.RegisterMember(newRegisterDto)"/>   returns true for the valid input user.
        /// </summary>        
        [Fact]
        public async Task RegisterUser_ValidInputs_ReturnsCreatedResult()
        {
            // arrange
            var id = Guid.NewGuid();
            var newRegisterDto = new RegisterDto
            {
                Name = "Gary Player",
                IdentificationNumber = "1234567890123",
                MobileNumber = "0896570741",
                Password = "Password1",
            };

            var newMember = new Member
            {
                Name = "Gary Player",
                IdentificationNumber = "1234567890123",
                MobileNumber = "0896570741",
                Password = "Password1",
            };
           
            _serviceMock.Setup(x=> x.RegisterMember(It.IsAny<Member>())).Returns(Task.FromResult(true));
            _converterMock.Setup(x => x.ConvertToMember(It.IsAny<RegisterDto>(), It.IsAny<PasswordHashResultDto>())).Returns(newMember);

            //act
            var result = await _controller.RegisterMember(newRegisterDto);
            
            // assert
            Assert.IsType<CreatedResult>(result);
        }

        /// <summary>
        ///     Test that <see cref="RegisterController.RegisterMember(newRegisterDto)"/>   returns user exists in the database.
        /// </summary>
        [Fact]
        public async void RegisterUser_AlreadyExists_ReturnsBadRequest()
        {
            //arrange
            var id = Guid.NewGuid();
            var newRegisterDto = new RegisterDto
            {
                Name = "Gary Player",
                IdentificationNumber = "1234567890123",
                MobileNumber = "0896570741",
                Password = "Password1",
            };
            var newMember = new Member
            {
                Id= id,
                Name = "Gary Player",
                IdentificationNumber = "1234567890123",
                MobileNumber = "0896570741",
                Password = "Password1",
            };
            
            _serviceMock.Setup(s => s.RegisterMember(It.IsAny<Member>()))
                .Returns(Task.FromResult(false));

            _converterMock.Setup(x => x.ConvertToMember(It.IsAny<RegisterDto>(), It.IsAny<PasswordHashResultDto>())).Returns(newMember);
            //act
            var result = await _controller.RegisterMember(newRegisterDto);
            //assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
        
        /// <summary>
        ///     Test that <see cref="RegisterController.RegisterMember(newRegisterDto)"/> returns bad request response some fields are missing.
        /// </summary>
        [Fact]
        public async Task RegisterUser_MissingFields_ReturnsNull()
        {
            // arrange
            var id = Guid.Empty;
            var newRegisterDto = new RegisterDto
            {
                Name = "Gary Player",
                IdentificationNumber = "1234567890123",
                MobileNumber = "0896570741",
                Password = "Password1",
            };

            var newMember = new Member
            {
                Name = "Gary Player",
                IdentificationNumber = "1234567890123",
                MobileNumber = "",
                Password = "Password1",
            };

            _converterMock.Setup(x => x.ConvertToMember(It.IsAny<RegisterDto>(), It.IsAny<PasswordHashResultDto>())).Returns<Member>(null);
            
            // act
            var result = await _controller.RegisterMember(newRegisterDto);

            // assert
            Assert.IsType<BadRequestResult>(result);

        }

    }
}