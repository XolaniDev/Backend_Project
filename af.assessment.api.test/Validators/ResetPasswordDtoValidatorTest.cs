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
using FluentValidation.TestHelper;
using af.assessment.api.Validators;
using af.assessment.api.Data.Dtos;
using System;

namespace af.assessment.api.test.Validators
{

    /// <summary>
    ///     Provides test methods for the <see cref="ResetPasswordDtoValidator"/> rules.
    /// </summary>
    public class ResetPasswordDtoValidatorTest
    {
        /// <summary>
        ///     A <see cref="ResetPasswordDtoValidator"/> representing the validator to be tested.
        /// </summary>
        private ResetPasswordDtoValidator _validator;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResetPasswordDtoValidatorTest"/> class.
        /// </summary>
        public ResetPasswordDtoValidatorTest()
        {
            _validator = new ResetPasswordDtoValidator();
        }
        
        /// <summary>
        ///     Test that there are no validation errors for the given valid member id.
        /// </summary>
        [Fact]
        public void ResetPasswordDtoValidator_ValidId_ReturnsNoValidationErrorsForIdentificationNumber()
        {
            // arrange
            var dto = new ResetPasswordDto { MemberId = Guid.Parse("4e33612c-9a6a-4e89-aa75-995f24e9d6d5") };

            // act
            var result = _validator.TestValidate(dto);

            // assert
            result.ShouldNotHaveValidationErrorFor(x => x.MemberId);

        }



        /// <summary>
        ///     Test that there are validation errors for the given invalid member id values.
        /// </summary>
        [Fact]       
        public void ResetPasswordDtoValidator_EmptyGuid_ReturnsValidationErrorsForMemberId()
        {
            // arrange
            var dto = new ResetPasswordDto { MemberId = Guid.Empty };

            // act
            var result = _validator.TestValidate(dto);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.MemberId);

        }
        
        /// <summary>
        ///     Test that <see cref=" ResetPasswordDtoValidator"/> returns null when given an invalid password and confirm password input.
        /// </summary>
        [Fact]
        public void ResetPasswordDtoValidator_Invalid_Password_Should_Return_Null()
        {
            //Arrange
            var ResetPasswordDto = new ResetPasswordDto
            {
                Password = null,
                ConfirmPassword = null
            };

            //Act
            var result = _validator.TestValidate(ResetPasswordDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
            result.ShouldHaveValidationErrorFor(y => y.ConfirmPassword);
        }

        /// <summary>
        ///     Test that <see cref="ResetPasswordDtoValidator"/> returns a error when a given password and confirm password that do not meet requirements.
        /// </summary>
        /// <param name="password">
        ///      A <see cref="string"/> representing the password value.
        /// </param>
        /// <param name="confirmPassword">
        ///     A <see cref="string"/> representing the confirm password value.
        /// </param>
        [Theory]
        [InlineData("", "")]
        [InlineData("aaaaaaa*1", "aaaaaaa*1")]
        [InlineData("AAAAAAAA*1", "AAAAAAAA*1")]
        [InlineData("noNumbers*", "noNumbers*")]
        [InlineData("1234568Aa", "1234568Aa")]
        [InlineData("Aa*1234", "Aa*1234")]
        [InlineData("Aa*123456aaaaaaa", "Aa*123456aaaaaaa")]
        [InlineData(null, null)]
        public void ResetPasswordDtoValidator_Vaild_Password_Returns_Validation_Error(string password, string confirmPassword)
        {
            //Arrange
            var ResetPasswordDto = new ResetPasswordDto
            {
                Password = password,
                ConfirmPassword = confirmPassword
            };

            //Act
            var result = _validator.TestValidate(ResetPasswordDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
            result.ShouldHaveValidationErrorFor(y => y.ConfirmPassword);
        }

        /// <summary>
        ///     Test on <see cref="ResetPasswordDtoValidator"/> that returns a successful response for a valid password and confirm password.
        /// </summary>
        /// <param name="password">
        ///      A <see cref="string"/> representing the password value.
        /// </param>
        /// <param name="confirmPassword">
        ///     A <see cref="string"/> representing the confirm password value.
        /// </param>
        [Theory]
        [InlineData("Password!134", "Password!134")]
        [InlineData("Password!133", "Password!133")]
        public void ResetPasswordDtoValidator_Valid_Password_Returns_No_Validation_Error(string password, string confirmPassword)
        {
            //Arrange
            var ResetPasswordDto = new ResetPasswordDto
            {
                Password = password,
                ConfirmPassword = confirmPassword
            };

            //Act
            var result = _validator.TestValidate(ResetPasswordDto);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
            result.ShouldNotHaveValidationErrorFor(y => y.ConfirmPassword);
        }
    }  
}
