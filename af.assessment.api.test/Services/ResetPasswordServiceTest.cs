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
using af.assessment.api.Services;
using af.assessment.api.Data.Models;
using af.assessment.api.Stores;
using Microsoft.Extensions.Logging;
using af.assessment.api.Data.Dtos;

namespace af.assessment.api.test.Services
{
    /// <summary>
    ///     Provide unit test to test <see cref="ResetPasswordService"/> class.
    /// </summary>
    public class ResetPasswordServiceTest
    {

        /// <summary>
        ///     A <see cref="ResetPasswordService"/>  represent the Instance of the service.
        /// </summary>
        private readonly ResetPasswordService _resetPasswordService;

        /// <summary>
        ///     A <see cref="ResetPasswordDto"/>  represent the instance of a service DTO.
        /// </summary>
        private readonly ResetPasswordDto _resetPasswordDTO;

        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="IResetPasswordStore"/> representing the store to be mocked.
        /// </summary>
        private readonly Mock<IResetPasswordStore> _resetPasswordStoreMock = new Mock<IResetPasswordStore>();


        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="ILogger"/> representing the reset password converter to be mocked.
        /// </summary>
        private readonly Mock<ILogger<ResetPasswordService>> _loggerMock = new Mock<ILogger<ResetPasswordService>>();

        /// <summary>
        ///     Initialise an  instance of the <see cref="ResetPasswordServiceTest"/>  class.
        /// </summary>
        public ResetPasswordServiceTest()
        {
            _resetPasswordService = new ResetPasswordService(_resetPasswordStoreMock.Object, _loggerMock.Object);
        }

        /// <summary>
        ///      Test that successfully gets <see cref="Task"/> an existing member with id number.
        /// </summary>
        [Fact]
        public async Task ResetPasswordService_Verify_User_Exist_With_IdNumber_Successfull()
        {
            //Arrange
            var memberId = Guid.NewGuid();
            var idNumber = "0001010000006";

            var member = new Member()
            {
                Id = memberId,
                IdentificationNumber = idNumber
            };                      
            _resetPasswordStoreMock.Setup(x => x.VerifyUserWithIdNumber(idNumber)).ReturnsAsync(member);

            //Act
            var result = await _resetPasswordService.VerifyUserWithIdNumber(idNumber);
          
            //Assert
            Assert.Equal(member.Id, result.Id);
        }

        /// <summary>
        ///      Test that successfully gets <see cref="Task"/> an existing member with member id.
        /// </summary>
        [Fact]
        public async Task ResetPasswordService_Verify_User_Exist_With_MemberId_Successfull()
        {
            //Arrange
            var memberId = Guid.NewGuid();           

            var member = new Member()
            {
                Id = memberId,                
            };
            _resetPasswordStoreMock.Setup(x => x.VerifyUserWithIdGuid(memberId)).ReturnsAsync(member);

            //Act
            var result = await _resetPasswordService.VerifyUserwithIdGuid(memberId);

            //Assert
            Assert.Equal(member.Id, result.Id);
        }

        /// <summary>
        ///      Test that unsuccessfully gets <see cref="Task"/> an existing member with id number.
        /// </summary>
        [Fact]
        public async Task ResetPasswordService_Verify_User_Exist_With_IdNumber_Unsuccessfull()
        {
            //Arrange
            var memberId = Guid.NewGuid();

            _resetPasswordStoreMock.Setup(x => x.VerifyUserWithIdGuid(memberId)).ReturnsAsync(() => null);

            //Act
            var result = await _resetPasswordService.VerifyUserwithIdGuid(memberId);

            //Assert
            Assert.Null(result);
        }

        /// <summary>
        ///      Test that unsuccessfully gets <see cref="Task"/> an existing member with member id.
        /// </summary>
        [Fact]
        public async Task ResetPasswordService_Verify_User_Exist_With_MemberId_Unsuccessfull()
        {
            //Arrange
            var idNumber = "0001010000006";

            _resetPasswordStoreMock.Setup(x => x.VerifyUserWithIdNumber(idNumber)).ReturnsAsync(() => null);

            //Act
            var result = await _resetPasswordService.VerifyUserWithIdNumber(idNumber);

            //Assert
            Assert.Null(result);
        }

        /// <summary>
        ///     Tests that password was successfully updated.
        /// </summary>        
        [Fact]
        public async Task ResetPasswordService_Update_Password_Successfully()
        {
            // Arrange
            var memberId = Guid.Parse("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9");
            var password = "$2a$11$66mWNajWltzj.iDOJIjFyeg.CIcfSNwROR5zUNUtLDKHQKN1hrUJi";
            var salt = "$2a$11$66mWNajWltzj.iDOJIjFye";

            var model = new Member()
            {
                Id = memberId
            };

            _resetPasswordStoreMock.Setup(x => x.ChangePassword(memberId, salt, password)).ReturnsAsync(() => model);

            // Act
            var result = await _resetPasswordService.ChangePassword(memberId, salt, password);

            // Assert
            Assert.Equal(model, result);
        }

        /// <summary>
        ///     Test that  <see cref="Task"/> returns null if password was update was unsuccessful.
        /// </summary>        
        [Fact]
        public async Task ResetPasswordService_Update_Password_Unsuccessfully()
        {
            // Arrange
            var memberId = Guid.Parse("2c2b6b15-3530-4a93-9a17-5f0bcd7423f9");
            var password = "$2a$11$66mWNajWltzj.iDOJIjFyeg.CIcfSNwROR5zUNUtLDKHQKN1hrUJi";
            var salt = "$2a$11$66mWNajWltzj.iDOJIjFye";
          
            _resetPasswordStoreMock.Setup(x => x.ChangePassword(memberId, salt, password)).ReturnsAsync(() => null);

            // Act
            var result = await _resetPasswordService.ChangePassword(memberId, salt, password);

            // Assert
            Assert.Null(result);
        }
    }
}
