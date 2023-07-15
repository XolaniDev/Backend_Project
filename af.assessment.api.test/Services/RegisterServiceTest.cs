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

namespace af.assessment.api.test.Services
{
    /// <summary>
    ///     Provide unit test to test <see cref="RegisterService"/> class.
    /// </summary>
    public class RegisterServiceTest
    {
        /// <summary>
        ///     A <see cref="RegisterService"/>  represent the service that contains the method to be tested.
        /// </summary>
        private readonly RegisterService _onBoardingService;
        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="IRegisterService"/> representing the store to be mocked.
        /// </summary>
        private readonly Mock<IMemberStore> _storeMock = new Mock<IMemberStore>();

        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="ILogger"/> representing the register converter to be mocked.
        /// </summary>
        private readonly Mock<ILogger<RegisterService>> _loggerMock = new Mock<ILogger<RegisterService>>();

        /// <summary>
        ///     Initialise an  instance of the <see cref="RegisterServiceTest"/>  class.
        /// </summary>
        public RegisterServiceTest()
        {
            _onBoardingService = new RegisterService(_storeMock.Object, _loggerMock.Object);
        }

        /// <summary>
        ///      Test that <see cref="RegisterService.RegisterMember(Member)"/>   returns true for the valid input user.
        /// </summary>        
        [Theory]
        [InlineData("Gary Player","1234567890123", "0896570741", "Password1")]
        [InlineData("Player Gary","1234567890124", "0896570742", "Password1@")]
        public async Task RegisterUser_ValidInputs_ReturnsCreatedResult(String name, String id_num, String mobile, String password )
        {
            // arrange
            var id = Guid.NewGuid();
           
            var newMember = new Member
            {
                Name = name,
                IdentificationNumber = id_num,
                MobileNumber = mobile,
                Password = password,
            };

            _storeMock.Setup(x => x.RegisterMember(It.IsAny<Member>()))
                .Returns(Task.FromResult(true));

            //act
            var result = await _onBoardingService.RegisterMember(newMember);

            // assert
            Assert.True(result);
        }

        /// <summary>
        ///     Test that <see cref="RegisterService.RegisterMember(Member)"/> returns bad request response some feilds are missing.
        /// </summary>
        [Fact]
        public async Task RegisterUser_MissingFields_ReturnsNull()
        {
            // arrange
            var id = Guid.Empty;
            var newMember = new Member
            {
                Name = "Gary Player",
                IdentificationNumber = "1234567890123",
                MobileNumber = "",
                Password = "Password1",
            };

            _storeMock.Setup(x => x.RegisterMember(It.IsAny<Member>())).Returns(Task.FromResult(false));

            // act
            var result = await _onBoardingService.RegisterMember(newMember);

            // assert
           Assert.False(result);

        }
    }
}