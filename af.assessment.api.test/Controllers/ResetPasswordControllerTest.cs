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
    public class ResetPasswordControllerTest
    {
        /// <summary>
        ///     A <see cref="ResetPasswordController"/>  represent the instance of ResetPassword controller.
        /// </summary>
        private readonly ResetPasswordController _controller;

        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="IResetPasswordService"/> representing the service to be mocked.
        /// </summary>
        private readonly Mock<IResetPasswordService> _serviceMock = new Mock<IResetPasswordService>();

        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="ILogger"/> representing the reset password converter to be mocked.
        /// </summary>
        private readonly Mock<ILogger<ResetPasswordController>> _loggerMock = new Mock<ILogger<ResetPasswordController>>();

        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="IPasswordHasher"/> representing the password hasher to be mocked.
        /// </summary>
        private readonly Mock<IPasswordHasher> _passwordHasherMock = new Mock<IPasswordHasher>();

        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="IUserToken"/> representing the store to be mocked.
        /// </summary>
        private readonly Mock<IUserToken> _userTokenMock = new Mock<IUserToken>();

        /// <summary>
        ///     Initialise an  instance of the <see cref="ResetPasswordControllerTest"/> class.
        /// </summary>
        public ResetPasswordControllerTest()
        {
            _controller = new ResetPasswordController(_serviceMock.Object, _userTokenMock.Object, _passwordHasherMock.Object, _loggerMock.Object);
        }

        /// <summary>
        ///     The following test checks if a user exists successfully.
        /// </summary>
        [Fact]
        public async Task ResetPassword_Check_User_Exists_Using_IdNumber_Successfully()
        {
            //Arrange

            var idnumber = "0001010000106";
            var id = Guid.NewGuid();

            var model = new Member()
            {
                Id = id,
                Name = "testName",
                Email = "test@email.com",
                IdentificationNumber = idnumber,
                MobileNumber = "(077)-111-1111",
            };

            _serviceMock.Setup(x => x.VerifyUserWithIdNumber(idnumber)).ReturnsAsync(model);

            //Act
            var actionResult = await _controller.VerifyUserExists(idnumber) as OkObjectResult;
            var result = actionResult.Value;

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(result, model.Id);
        }

        /// <summary>
        ///     The following test an unsuccessful response checks if a user exists with a registered account.
        /// </summary>
        [Fact]
        public async Task ResetPassword_Check_User_Exists_Using_IdNumber_Unsuccessfully()
        {
            //Arrange

            var idnumber = "0001010000106";
            var id = Guid.NewGuid();

            _serviceMock.Setup(x => x.VerifyUserWithIdNumber(idnumber)).ReturnsAsync(() => null);

            //Act
            var actionResult = await _controller.VerifyUserExists(idnumber) as NotFoundResult;

            //Assert
            Assert.IsType<NotFoundResult>(actionResult);

        }

        /// <summary>
        ///     The following test will check if a user tires to reset a password with Guid that does not exist in database.
        /// </summary>
        [Fact]
        public async Task ResetPassword_With_Invalid_Guid()
        {
            //Arrange
            var resetPasswordDto = new ResetPasswordDto
            {
                Password = "",
                ConfirmPassword = "",
                MemberId = Guid.Empty
            };

            _serviceMock.Setup(x => x.VerifyUserwithIdGuid(resetPasswordDto.MemberId)).ReturnsAsync(() => null);

            //Act
            var result = await _controller.ChangePassword(resetPasswordDto) as NotFoundResult;

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }


        /// <summary>
        ///     The following test will check if a user enters a password with Guid that does exist in database.
        /// </summary>
        [Fact]
        public async Task ResetPassword_With_Valid_Guid()
        {
            //Arrange

            var idnumber = "0001010000105";
            var unHashedPassword = "Genn@M12";
            var password = "$2a$11$66mWNajWltzj.iDOJIjFyeg.CIcfSNwROR5zUNUtLDKHQKN1hrUJi";
            var salt = "$2a$11$66mWNajWltzj.iDOJIjFye";
            var id = Guid.NewGuid();

            var resetPasswordDto = new ResetPasswordDto
            {
                MemberId = id,
                Password = unHashedPassword,
                ConfirmPassword = unHashedPassword
            };

            var model = new Member()
            {
                Id = id,
                Name = "testName",
                Email = "test@email.com",
                IdentificationNumber = idnumber,
                MobileNumber = "(077)-111-1111",
                Password = password,
                Salt = salt

            };

            var passwordHashResultDTO = new PasswordHashResultDto()
            {
                HashResult = "$2a$11$66mWNajWltzj.iDOJIjFyeg.CIcfSNwROR5zUNUtLDKHQKN1hrUJi",
                Salt = salt
            };

            _serviceMock.Setup(x => x.VerifyUserwithIdGuid(resetPasswordDto.MemberId)).ReturnsAsync(model);
            _passwordHasherMock.Setup(p => p.GenerateHashPasswordWithSalt(unHashedPassword, salt)).Returns(passwordHashResultDTO);
            _serviceMock.Setup(s => s.ChangePassword(resetPasswordDto.MemberId, salt, password)).ReturnsAsync(model);

            //Act
            var result = await _controller.ChangePassword(resetPasswordDto) as OkResult;

            //Assert
            Assert.IsType<OkResult>(result);
        }

    }
}
