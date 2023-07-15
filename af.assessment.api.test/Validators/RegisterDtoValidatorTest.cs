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

namespace af.assessment.api.test.Validators
{
    /// <summary>
    ///     Provides test methods for the <see cref="RegisterDtoValidator"/> rules.
    /// </summary>
    public class RegisterDtoValidatorTest
    {
        /// <summary>
        ///     A <see cref="RegisterDtoValidator"/> representing the validator to be tested.
        /// </summary>
        private RegisterDtoValidator _validator;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RegisterValidatorTest"/> class.
        /// </summary>
        public RegisterDtoValidatorTest()
        {
            _validator = new RegisterDtoValidator();
        }

        /// <summary>
        ///     Tests that there are no validation errors for a valid name.
        /// </summary>
        [Fact]
        public void RegisterDtoValidator_ValidName_ReturnsNoValidationErrorForName()
        {
            // arrange
            var dto = new RegisterDto { Name = "Charlie Brown" };
            
            // act
            var result = _validator.TestValidate(dto);
            
            // assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            
        }

        /// <summary>
        ///     Tests that there are validation errors for an invalid name.
        /// </summary>
        [Fact]
        public void RegisterDtoValidator_InvalidName_ReturnsValidationErrorsForName()
        {
            // arrange
            var dto = new RegisterDto { Name = null };
            
            // act
            var result = _validator.TestValidate(dto);
            
            // assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            
        }

        /// <summary>
        ///     Tests that there are no validation errors for the given valid otp preference values.
        /// </summary>
        /// <param name="otp">
        ///     An <see cref="int"/> representing the otp inputs for the test.
        /// </param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void RegisterDtoValidator_ValidInt_ReturnsNoValidationErrorForOtpPreference(int otp)
        {
            // arrange
            var dto = new RegisterDto { OtpPreference = otp };
            
            // act
            var result = _validator.TestValidate(dto);
            
            // assert
            result.ShouldNotHaveValidationErrorFor(x => x.OtpPreference);
            
        }

        /// <summary>
        ///     Tests that there are validation errors for an invalid otp preference value.
        /// </summary>
        [Fact]
        public void RegisterDtoValidator_InvalidInt_ReturnsValidationErrorsForOtpPreference()
        {
            // arrange
            var dto = new RegisterDto { OtpPreference =  -1};
            
            // act
            var result = _validator.TestValidate(dto);
            
            // assert
            result.ShouldHaveValidationErrorFor(x => x.OtpPreference);
            
        }

        /// <summary>
        ///     Tests that there are no validation errors for the given valid email value.
        /// </summary>
        /// <param name="email">
        ///      A <see cref="string"/> representing the email input values.
        /// </param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a@a.org")]
        public void RegisterDtoValidator_ValidEmail_ReturnsNoValidationErrorForEmail(string email)
        {
            // arrange
            var dto = new RegisterDto { Email = email };
            
            // act
            var result = _validator.TestValidate(dto);
            
            // assert
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
            
        }

        /// <summary>
        ///     Tests that there are validation errors for the given invalid email values.
        /// </summary>
        /// <param name="email">
        ///     A <see cref="string"/> representing the email input values.
        /// </param>
        [Theory]
        [InlineData("hello")]
        [InlineData("world.com")]
        public void RegisterDtoValidator_InvalidEmail_ReturnsValidationErrorsForEmail(string email)
        {
            // arrange
            var dto = new RegisterDto { Email = email };
            
            // act
            var result = _validator.TestValidate(dto);
            
            // assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
            
        }

        /// <summary>
        ///     Test that there are no validation errors for the given valid identification number values.
        /// </summary>
        /// <param name="id">
        ///     A <see cref="string"/> representing the identification number value.
        /// </param>
        [Theory]
        [InlineData("0001010000006")]
        [InlineData("0001010000105")]
        [InlineData("9912310000085")]
        public void RegisterDtoValidator_ValidIdentificationNumber_ReturnsNoValidationErrorsForIdentificationNumber(string id)
        {
            // arrange
            var dto = new RegisterDto { IdentificationNumber = id};

            // act
            var result = _validator.TestValidate(dto);
            
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
        public void RegisterDtoValidator_InvalidIdentificationNumber_ReturnsValidationErrorsForIdentificationNumber(string id)
        {
            // arrange
            var dto = new RegisterDto {IdentificationNumber = id };
            
            // act
            var result = _validator.TestValidate(dto);
            
            // assert
            result.ShouldHaveValidationErrorFor(x => x.IdentificationNumber);
            
        }

        /// <summary>
        ///     Test that there are no validation errors for the given mobile number value.
        /// </summary>
        [Fact]
        public void RegisterDtoValidator_ValidMobileNumber_ReturnsNoValidationErrorsForMobileNumber()
        {
            // arrange
            var dto = new RegisterDto { MobileNumber = "(011)-111-1111" };
            
            // act
            var result = _validator.TestValidate(dto);
            
            // assert
            result.ShouldNotHaveValidationErrorFor(x => x.MobileNumber);
            
        }

        /// <summary>
        ///     Test that there are validation errors for the given invalid mobile number values.
        /// </summary>
        /// <param name="number">
        ///      A <see cref="string"/> representing the mobile number value.
        /// </param>
        [Theory]
        [InlineData("(xxx)-xxx-xxx")]
        [InlineData("(!!!)-xxx-xxx")]
        public void RegisterDtoValidator_InvalidMobileNumber_ReturnsValidationErrorsForMobileNumber(string number)
        {
            // arrange
            var dto = new RegisterDto { MobileNumber = number };
            
            // act
            var result = _validator.TestValidate(dto);
            
            // assert
            result.ShouldHaveValidationErrorFor(x => x.MobileNumber);
            
        }
        
        /// <summary>
        ///     Test that <see cref="RegisterDtoValidator.RegisterDto"/> returns null when given an invalid password input.
        /// </summary>
        [Fact]
        public void RegisterDtoValidator_Invalid_Password_Should_Return_Null()
        {
            //Arrange
            var registerDto = new RegisterDto
            {
                Password = null
            };

            //Act
            var result = _validator.TestValidate(registerDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
        
        /// <summary>
        ///     Test that <see cref="RegisterDtoValidator.RegisterDto"/> returns a error when a given password does not meet requirements.
        /// </summary>
        /// <param name="password">
        ///      A <see cref="string"/> representing the password value.
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
        public void RegisterDtoValidator_Vaild_Password_Returns_Validation_Error(string password)
        {
            //Arrange
            var registerDto = new RegisterDto
            {
                Password = password
            };

            //Act
            var result = _validator.TestValidate(registerDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        /// <summary>
        ///     Test that <see cref="RegisterDtoValidator.RegisterDto"/> returns no error when a given password met requirements.
        /// </summary>
        /// <param name="password">
        ///      A <see cref="string"/> representing the password value.
        /// </param>
        [Theory]
        [InlineData("Password!134")]
        [InlineData("Password!133")]
        public void RegisterDtoValidator_Vaild_Password_Returns_No_Validation_Error(string password)
        {
            //Arrange
            var registerDto = new RegisterDto
            {
                Password = password
            };

            //Act
            var result = _validator.TestValidate(registerDto);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }
    }
}