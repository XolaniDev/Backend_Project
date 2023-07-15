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
using System.Threading.Tasks;
using af.assessment.api.Controllers;
using af.assessment.api.Data.Models;
using af.assessment.api.Services;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using af.assessment.api.Converters;
using af.assessment.api.Utilities;
using af.assessment.api.Data.Dtos;

namespace af.assessment.api.test.Controllers
{
    /// <summary>
    ///     Provides tests for the OnboardingController.
    /// </summary>
    public class LoginControllerTest
    {

        /// <summary>
        ///     Creating an instance of _controller for OnboardingController.
        /// </summary>
        private readonly LoginController _controller;

        /// <summary>
        ///     Instantiating a mock for the <see cref="ILoginService"/>.
        /// </summary>
        private readonly Mock<ILoginService> _serviceMock;

        /// <summary>
        ///    Creating an instance of Mock IUserToken for _userTokenMock.
        /// </summary>
        private readonly Mock<IUserToken> _userTokenMock;


        /// <summary>
        ///     Instantiating the mock for Logger and password hasher.
        /// </summary>
        private readonly Mock<ILogger<LoginController>> _logger;

        private readonly Mock<IPasswordHasher> _passwordHasher;

        /// <summary>
        ///     Creates instances of Mock for onboardingService and craetes an intsance of the OnboardingController.
        /// </summary>
        public LoginControllerTest()
        {
            _serviceMock = new Mock<ILoginService>();
            _logger = new Mock<ILogger<LoginController>>();
            _userTokenMock = new Mock<IUserToken>();
            _passwordHasher = new Mock<IPasswordHasher>();
            _controller = new LoginController( _serviceMock.Object, _logger.Object, _userTokenMock.Object, _passwordHasher.Object);
        }

        [Fact]
        public async Task LogUserIn_Should_Return_BadRequest_If_ID_Is_Empty()
        {
            //Arrange
            var member = new LoginDto
            {
                IdentificationNumber = string.Empty
            };

            //Act
            var result = await _controller.LogUserIn(member) as BadRequestResult;

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        /// <summary>
        ///     Test case for LogUserIn.
        /// </summary>
        /// <returns>
        ///     A BadRequest <see cref="string"/> when pasword is empty.
        /// </returns>
        [Fact]
        public async Task LogUserIn_Should_Return_BadRequest_If_Password_Is_Empty()
        {
            //Arrange
            var model = new LoginDto
            {
                Password = string.Empty
            };

            //Act
            var result = await _controller.LogUserIn(model) as BadRequestResult;

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        /// <summary>
        ///     Test case method for the LogUserIn.
        /// </summary>
        /// <returns>A BadRequest <see cref="string"/> when IdentificationNumber is null.</returns>
        [Fact]
        public async Task LogUserIn_Should_Return_BadRequestResult_If_IdentificationNumber_Is_Null()
        {
            //Arrange
            var member = new LoginDto
            {
                IdentificationNumber = null
            };

            //Act
            var result = await _controller.LogUserIn(member) as BadRequestResult;

            //Assert
            Assert.IsType<BadRequestResult>(result);

        }

        /// <summary>
        ///     Test case to check if method for the log in user.
        /// </summary>
        /// <returns> 
        ///     NotFound <see cref="string"/> when password is null.
        /// </returns>
        [Fact]
        public async Task LogUserIn_Should_Return_BadRequestResult_If_Password_Is_Null()
        {
            //Arrange
            var member = new LoginDto
            {
                Password = null
            };

            //Act
            var result = await _controller.LogUserIn(member) as BadRequestResult;

            //Assert
            Assert.IsType<BadRequestResult>(result);

        }

        /// <summary>
        ///     The following test will NotFound if the memberDTO are vaild but the user model does not exist in the databse for a correct ID but incorret password.
        /// </summary>
        /// <returns>
        ///     NotFound <see cref="string"/> when user does not exist.
        /// </returns>
        [Fact]
        public async Task LogUserIn_With_Valid_Inputs_Should_Return_NotFound_If_User_Does_Not_Exist()
        {
            //Arrange
            string idnumber = "9306195030087";
            string unHashedPassword = "Genn@M12";
            string password = "$2a$11$66mWNajWltzj.iDOJIjFyeg.CIcfSNwROR5zUNUtLDKHQKN1hrUJi";
            string salt = "$2a$11$66mWNajWltzj.iDOJIjFye";
            var id = Guid.NewGuid();

            var model = new Member()
            {
                Id = id,
                Name = "testName",
                Email = "test@email.com",
                IdentificationNumber = idnumber,
                MobileNumber = "0",
                Password = password,
                Salt = salt

            };

            Member badMember = null;

            var memberDTO = new LoginDto
            {
                IdentificationNumber = idnumber,
                Password = unHashedPassword
            };

            var passwordHashResultDTO = new PasswordHashResultDto()
            {
                HashResult = "$2a$11$66mWNajWltzj.iDOJIjFyeg.CIcfSNwROR5zUNUtLDKHQKN1hrUJi",
                Salt = salt
            };

            _serviceMock.Setup(x => x.VerifyUser(idnumber)).ReturnsAsync(model);
            _passwordHasher.Setup(p => p.GenerateHashPasswordWithSalt(unHashedPassword, salt)).Returns(passwordHashResultDTO);
            _serviceMock.Setup(s => s.LogUserIn(idnumber, password)).ReturnsAsync(badMember);
           

            //Act
            var result = await _controller.LogUserIn(memberDTO) as UnauthorizedObjectResult;

            //Assert
            Assert.IsType<UnauthorizedObjectResult>(result);

        }
        /// <summary>
        ///     Testing the <see cref="Task"/> for the logged in user.
        /// </summary>
        /// <returns>
        ///      An Ok <see cref="ObjectResult"/> if a user exists in the db.
        /// </returns>
        [Fact]
        public async Task LogUserIn_With_Valid_Inputs_Should_Return_OK_If_User_Does_Exist()
        {
            //Arrange
            string idnumber = "9306195030087";
            string unHashedPassword = "Genn@M12";
            string password = "$2a$11$66mWNajWltzj.iDOJIjFyeg.CIcfSNwROR5zUNUtLDKHQKN1hrUJi";
            string salt = "$2a$11$66mWNajWltzj.iDOJIjFye";
            var id = Guid.NewGuid();

            var memberDTO = new LoginDto
            {
                IdentificationNumber = idnumber,
                Password = unHashedPassword
            };

            var model = new Member()
            {
                Id = id,
                Name = "testName",
                Email = "test@email.com",
                IdentificationNumber = idnumber,
                MobileNumber = "0",
                Password = password,
                Salt = salt

            };

            var passwordHashResultDTO = new PasswordHashResultDto()
            {
                HashResult = "$2a$11$66mWNajWltzj.iDOJIjFyeg.CIcfSNwROR5zUNUtLDKHQKN1hrUJi",
                Salt = salt
            };

            _serviceMock.Setup(x => x.VerifyUser(idnumber)).ReturnsAsync(model);
            _passwordHasher.Setup(p => p.GenerateHashPasswordWithSalt(unHashedPassword, salt)).Returns(passwordHashResultDTO);
            _serviceMock.Setup(s => s.LogUserIn(idnumber, password)).ReturnsAsync(model);

            //Act
            var result = await _controller.LogUserIn(memberDTO) as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

    }
}
