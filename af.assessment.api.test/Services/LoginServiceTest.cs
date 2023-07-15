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
using af.assessment.api.Data.Models;
using af.assessment.api.Services;
using af.assessment.api.Stores;
using Moq;
using Xunit;

namespace af.assessment.api.test.Services
{
    /// <summary>
    ///     Test cases for the <see cref="LoginService"/> class.
    /// </summary>
    public class LoginServiceTest
    {
        /// <summary>
        ///      Instantiating the <see cref="LoginService"/>.
        /// </summary>
        private readonly LoginService _loginService;

        /// <summary>
        ///      Instantiating the mock for the <see cref="IVaccineStore"/>.
        /// </summary>
        private readonly Mock<IVaccineStore> _storeMock;

        /// <summary>
        ///      Constructor for the mock <see cref="LoginServiceTest"/>.
        /// </summary>
        public LoginServiceTest()
        {
            _storeMock = new Mock<IVaccineStore>();
            _loginService = new LoginService(_storeMock.Object);
        }

        /// <summary>
        ///      Test that successfully gets <see cref="Task"/> an existing user.
        /// </summary>
        /// <returns> 
        ///      A member object.
        /// </returns>
        [Fact]
        public async Task GetExistingUser_Successfully_Should_Return_A_Member_Object()
        {
            //Arrange
            var id = Guid.NewGuid();
            var member = new Member()
            {
                Id = id,
                Name = "testName",
                Email = "test@email.com",
                IdentificationNumber = "1000000000000",
                MobileNumber = "1",
                Password = "TestPassword",
                Salt = "$2a$11$66mWNajWltzj.iDOJIjFye"
            };

            _storeMock.Setup(s => s.GetExistingUser(id)).ReturnsAsync(member);
            //Act

            var result = await _loginService.GetExistingUser(id);
            //Assert

            Assert.Equal(member.Id, result.Id);
            Assert.Equal(member.Name, result.Name);
            Assert.Equal(member.Email, result.Email);
            Assert.Equal(member.IdentificationNumber, result.IdentificationNumber);
            Assert.Equal(member.MobileNumber, result.MobileNumber);
            Assert.Equal(member.Password, result.Password);
        }

        /// <summary>
        ///       Test that unsuccessfully gets <see cref="Task"/> an existing user.
        /// </summary>
        /// <returns>  
        ///       A null if member does not exist.
        /// </returns>
        [Fact]
        public async Task GetExistingUser_Unsuccessfully_Should_Retrun_null_If_Member_Not_Exist()
        {
            //Arrange

            Member nullMember = null;
            _storeMock.Setup(s => s.GetExistingUser(It.IsAny<Guid>())).ReturnsAsync(nullMember);

            //Act

            var result = await _loginService.GetExistingUser(It.IsAny<Guid>());

            //Assert
            Assert.Null(result);
        }

        /// <summary>
        ///      Test that successfully gets <see cref="Task"/> a logged in user.
        /// </summary>
        /// <returns>
        ///      A member if the user details provided a match for a existing member.
        /// </returns>
        [Fact]
        public async Task LogUserIn_Sucessfully_Should_Return_A_Member_Object()
        {
            //Arrange
            var idNumber = "1000000000000";
            var password = "Password1*";
            var id = Guid.NewGuid();

            var member = new Member()
            {
                Id = id,
                Name = "testName",
                Email = "test@email.com",
                IdentificationNumber = idNumber,
                MobileNumber = "1",
                Password = password,
                Salt = "$2a$11$66mWNajWltzj.iDOJIjFye"
            };

            _storeMock.Setup(s => s.LogUserIn(idNumber, password)).ReturnsAsync(member);

            //Act
            var result = await _loginService.LogUserIn(idNumber, password);

            //Assert
            Assert.Equal(member.Id, result.Id);
            Assert.Equal(member.Name, result.Name);
            Assert.Equal(member.Email, result.Email);
            Assert.Equal(member.IdentificationNumber, result.IdentificationNumber);
            Assert.Equal(member.MobileNumber, result.MobileNumber);
            Assert.Equal(member.Password, result.Password);
        }

        /// <summary>
        ///      Test that unsuccessfully gets <see cref="Task"/> a logged in user.
        /// </summary>
        /// <returns>
        ///      A null if the user details provided do not match an existing user.
        /// /returns>
        [Fact]
        public async Task LogUserIn_Unsucessfully_Should_Return_A_Null_If_Memeber_Object_Does_Not_Exist()
        {
            //Arrange
            var idNumber = "1000000000000";
            var password = "Password1*";
            Member member = null;

            _storeMock.Setup(s => s.LogUserIn(idNumber, password)).ReturnsAsync(member);

            //Act
            var result = await _loginService.LogUserIn(idNumber, password);

            //Assert
            Assert.Null(result);
        }

        /// <summary>
        ///     Test that unsuccessfully gets <see cref="Task"/> to verify a user.
        /// </summary>
        /// <returns> 
        ///     A memeber object when the user exists.
        /// </returns>
        [Fact]
        public async Task VerifyUser_Sucessfully_Should_Return_A_Memeber_Object_If_User_Exist()
        {
            //Arrange
            var idNumber = "1000000000000";
            var password = "Password1*";
            var id = Guid.NewGuid();

            var member = new Member()
            {
                Id = id,
                Name = "testName",
                Email = "test@email.com",
                IdentificationNumber = idNumber,
                MobileNumber = "1",
                Password = password,
                Salt = "$2a$11$66mWNajWltzj.iDOJIjFye"
            };

            _storeMock.Setup(s => s.VerifyUser(idNumber)).ReturnsAsync(member);

            //Act
            var result = await _loginService.VerifyUser(idNumber);

            //Assert
            Assert.Equal(result.IdentificationNumber, member.IdentificationNumber);
        }

        /// <summary>
        ///     Test that unsuccessfully gets <see cref="Task"/> to verify a user.
        /// </summary>
        /// <returns>
        ///     A null if a memeber does not exist based for an non-existant idNumber.
        /// </returns>
        [Fact]
        public async Task VerifyUser_Unsucessfully_Should_Return_A_Null_If_User_Does_Not_Exist()
        {
            //Arrange
            Member member = null;
            var idNumber = "1000000000000";

            _storeMock.Setup(s => s.VerifyUser(idNumber)).ReturnsAsync(member);

            //Act
            var result = await _loginService.VerifyUser(idNumber);

            //Assert
            Assert.Null(result);
        }

    }
}
