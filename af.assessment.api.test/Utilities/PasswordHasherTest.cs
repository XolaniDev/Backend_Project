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
using af.assessment.api.Utilities;
using af.assessment.api.Data.Dtos;

namespace af.assessment.api.test.Utilities
{
    /// <summary>
    ///     Provides unit tests for the <see cref="PasswordHasher"/> class.
    /// </summary>
    public class PasswordHasherTest
    {
        /// <summary>
        ///     A <see cref="PasswordHasher"/> representing the password hasher class to be tested.
        /// </summary>
        private readonly PasswordHasher _passwordHasher;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PasswordHasherTest"/> class.
        /// </summary>
        public PasswordHasherTest()
        {
            _passwordHasher = new PasswordHasher();
        }

        /// <summary>
        ///     Tests that the <see cref="PasswordHasher.GenerateHashPassword(string)" returns a new salt and a password hash using the generated salt successfully.
        /// </summary>
        [Fact]
        public void GenerateHashPassword_ValidPassword_ReturnPasswordHashResult()
        {
            // arrange
            var password = "Abcd1234@";
            
            // act
            var result = _passwordHasher.GenerateHashPassword(password);
            var expected = BCrypt.Net.BCrypt.HashPassword(password, result.Salt);
            
            // assert
            Assert.IsType<PasswordHashResultDto>(result);
            Assert.NotNull(result.HashResult);
            Assert.NotNull(result.Salt);
            Assert.Equal(expected, result.HashResult);
        }

        /// <summary>
        ///     Tests that the <see cref="PasswordHasher.GenerateHashPasswordWithSalt(string, string)" returns a new salt and a password hash using the generated salt successfully.
        /// </summary>
        [Fact]
        public void GenerateHashPasswordWithSalt_ValidPasswordAndString_ReturnPasswordHashResult()
        {
            // arrange
            var password = "Abcd1234@";
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            
            // act
            var result = _passwordHasher.GenerateHashPasswordWithSalt(password, salt);
            var expected = BCrypt.Net.BCrypt.HashPassword(password, salt);
            
            // assert
            Assert.IsType<PasswordHashResultDto>(result);
            Assert.NotNull(result.HashResult);
            Assert.Equal(salt, result.Salt);
            Assert.Equal(expected, result.HashResult);
        }

    }
}