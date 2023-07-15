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
using af.assessment.api.Converters;
using af.assessment.api.Data.Dtos;
using af.assessment.api.Data.Models;
using Microsoft.Extensions.Logging;
using System;

namespace af.assessment.api.test.Converters
{
    /// <summary>
    ///     Provides unit test to test the <see cref="RegisterDtoConverter"/> class.
    /// </summary>
    public class RegisterDtoConverterTest
    {
        /// <summary>
        ///   A <see cref="RegisterDtoConverter"/> representing the converter class that contains the methods to be tested.
        /// </summary>          
        private readonly RegisterDtoConverter _converter;

        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="ILogger"/> representing the register converter to be mocked.
        /// </summary>
        private readonly Mock<ILogger<RegisterDtoConverter>> _loggerMock = new Mock<ILogger<RegisterDtoConverter>>();

        /// <summary>
        ///     Initialise the new instance of the <see cref="RegisterDtoConverterTest"/>  class.
        /// </summary>
        public RegisterDtoConverterTest()
        {
            _converter = new RegisterDtoConverter(_loggerMock.Object);
        }

        /// <summary>
        ///      Test that <see cref="RegisterDtoConverter.ConvertToMember(af.assessment.api.Data.Dtos.RegisterDto)"/> returns a Member when given an valid register dto input.
        /// </summary>
        [Theory]
        [InlineData("Gary Player", null, "1234567890123", "0896570741", "Password1", 1)]
        [InlineData("Player Gary", null, "1234567890124", "0896570742", "Password1@", 2)]
        public void ConvertToMember_ValidRegisterDto_ReturnsRegisterDto(String name, String email,String id_num, String mobile, String password, int pref)
        {
            // arrange 
            var registerDto = new RegisterDto
            {
                Name = name,
                Email = email,
                IdentificationNumber = id_num,
                MobileNumber = mobile,
                Password = password,
                OtpPreference = pref
            };
            
            var registerDto1 = new RegisterDto
            {
                Name = name,
                Email = "",
                IdentificationNumber = id_num,
                MobileNumber = mobile,
                Password = password,
                OtpPreference = pref
            };

            var expected = new Member
            {
                Name = name,
                Email = "",
                IdentificationNumber = id_num,
                MobileNumber = mobile,
                Password = "Aasfdgka@r34qf",
                Salt = "1234",
                OtpPreference = pref
            };

            var passwordHashResult = new PasswordHashResultDto { HashResult = "Aasfdgka@r34qf", Salt = "1234" };

            // act 
            var result = _converter.ConvertToMember(registerDto, passwordHashResult);
            var result1 = _converter.ConvertToMember(registerDto1, passwordHashResult);

            // assert
            Assert.Equal(expected.Name, result.Name);
            Assert.Equal(expected.MobileNumber, result.MobileNumber);
            Assert.Equal(expected.IdentificationNumber, result.IdentificationNumber);
            Assert.Equal(expected.Password, passwordHashResult.HashResult);
            Assert.Equal(expected.Salt, passwordHashResult.Salt);
            Assert.Equal(expected.OtpPreference, result.OtpPreference);
            Assert.Equal(expected.Email, result1.Email);
        }
    }
}