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

using FluentValidation.TestHelper;
using af.assessment.api.Validators;
using Xunit;
using af.assessment.api.Data.Dtos;

namespace af.assessment.api.test.Validators
{
    /// <summary>
    ///     Testing the Validator class.
    /// </summary>
    public class LoginDtoValidatorsTest
    {
        /// <summary>
        ///    Instantiating the <see cref="LoginDtoValidator"/>.
        /// </summary>
        private readonly LoginDtoValidator _validator;

        /// <summary>
        ///     Instantiating the <see cref="_validator"/> dependency injection.
        /// </summary>
        public LoginDtoValidatorsTest()
        {
            _validator = new LoginDtoValidator();
        }

        /// <summary>
        ///      Tests that MemberDtoValidator has an invalid GUID that returns a null.
        /// </summary>
        [Fact]
        public void MemberDtoValidator_Invalid_Guid_Should_Return_Null()
        {
            //Arrange
            var memberDTO = new LoginDto
            {
                IdentificationNumber = string.Empty
            };

            //Act
            var result = _validator.TestValidate(memberDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.IdentificationNumber);
        }

        /// <summary>
        ///      Tests that MemberDtoValidator has an invalid password that returns a null.
        /// </summary>
        [Fact]
        public void MemberDtoValidator_Invalid_Password_Should_Return_Null()
        {
            //Arrange
            var memberDTO = new LoginDto
            {
                Password = null
            };

            //Act
            var result = _validator.TestValidate(memberDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        /// <summary>
        ///     Tests that MemberDtoValidator has a valid GUID and shows no validation error.
        /// </summary>
        [Fact]
        public void MemberDtoValidator_Vaild_Guid_Returns_No_Validation_Error()
        {
            //Arrange
            var memberDTO = new LoginDto
            {
                IdentificationNumber = "0001010000006"
            };

            //Act
            var result = _validator.TestValidate(memberDTO);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.IdentificationNumber);
        }

        /// <summary>
        ///     Tests that MemberDtoValidator has an invalid password and shows validation error.
        /// </summary>
        /// <param name="password">
        ///      A <see cref="String"/> representing the password value.
        /// </param>
        [Theory]
        [InlineData("")]
        [InlineData("aaaaaaa*1")]
        [InlineData("AAAAAAAA*1")]
        [InlineData("noNumbers*")]
        [InlineData("1234568Aa")]
        [InlineData("Aa*1234")]
        [InlineData("Aa*123456aaaaaaa")]
        [InlineData(null)]
        public void MemberDtoValidator_Vaild_Password_Returns_Validation_Error(string password)
        {
            //Arrange
            var memberDTO = new LoginDto
            {
                Password = password
            };

            //Act
            var result = _validator.TestValidate(memberDTO);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        /// <summary>
        ///     Tests that the MemberDtoValidator has a valid password and shows no validation error.
        /// </summary>
        /// <param name="password">
        ///     A <see cref="String"/> representing the password value.
        /// </param>
        [Theory]
        [InlineData("Password!134")]
        public void MemberDtoValidator_Vaild_Password_Returns_No_Validation_Error(string password)
        {
            //Arrange
            var memberDTO = new LoginDto
            {
                Password = password
            };

            //Act
            var result = _validator.TestValidate(memberDTO);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }

        /// <summary>
        ///     Test case for the validator <see cref="MemberDTOvalidator"/> showing a positive response.
        /// </summary>
        /// <param name="id">
        ///     A <see cref="String"/> representing the id number value.
        /// </param>
        [Theory]
        [InlineData("0001010000006")]
        [InlineData("0001010000105")]
        public void MemberDtoValidator_ValidIdentificationNumber_ReturnsNo_Validation_Errors_For_IdentificationNumber(string id)
        {
            // arrange
            var memberdto = new LoginDto { IdentificationNumber = id };

            // act
            var result = _validator.TestValidate(memberdto);

            // assert
            result.ShouldNotHaveValidationErrorFor(x => x.IdentificationNumber);

        }

        /// <summary>
        ///     Test that there are no validation errors for the given valid identification number values.
        /// </summary>
        /// <param name="id">
        ///     A <see cref="string"/> representing the identification number value.
        /// </param>
        [Theory]
        [InlineData("")]
        [InlineData("0")]
        [InlineData("00000000000000")]
        [InlineData("0000000000000")]
        [InlineData("000909zzzz1zz")]
        [InlineData("000909!!!!1@@")]
        [InlineData("0013990000100")]
        [InlineData("0009090000500")]
        [InlineData(null)]
        public void MemberDtoValidator_InvalidIdentificationNumber_Returns_Validation_Errors_For_IdentificationNumber(string id)
        {
            // arrange
            var memberdto = new LoginDto { IdentificationNumber = id };

            // act
            var result = _validator.TestValidate(memberdto);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.IdentificationNumber);

        }


    }
}
