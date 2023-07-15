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
    ///     Provides test methods for <see cref="UpdateProfileDtoValidator"/> rules.
    /// </summary>
    public class UpdateProfileDtoValidatorTest
    {
        /// <summary>
        ///      A <see cref="UpdateProfileDtoValidator"/> representing the validator to be tested.
        /// </summary>
        private readonly UpdateProfileDtoValidator _validator;

        /// <summary>
        ///      Initializes a new instance of the <see cref="UpdateProfileDtoValidatorTest"/> class.
        /// </summary>
        public UpdateProfileDtoValidatorTest()
        {
            _validator = new UpdateProfileDtoValidator();
        }

        /// <summary>
        ///     Tests that there are no validation errors for a valid name.
        /// </summary>
        [Fact]
        public void UpdateProfileDtoValidator_ValidName_Returns_NoValidationErrorForName()
        {
            // Arrange
            var dto = new UserDto{ Name = "Jamie Dimon"  };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        ///     Tests that there are validation errors for an invalid name.
        /// </summary>
        [Fact]
        public void UpdateProfileDtoValidator_InvalidName_Returns_ValidationErrors()
        {
            // Arrange
            var dto = new UserDto { Name = "" };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        ///      Tests that there are no validation errors for the given valid email value.
        /// </summary>
        /// <param name="email">
        ///     A <see cref="string"/> representing the email input values.
        /// </param>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("a@a.org")]
        public void UpdateProfileDtoValidator_ValidEmail_ReturnsNoValidationErrorForEmail(string email)
        {
            // arrange
            var dto = new UserDto { Email = email };

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
        public void UpdateProfileDtoValidator_InvalidEmail_ReturnsValidationErrorsForEmail(string email)
        {
            // arrange
            var dto = new UserDto { Email = email };

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
        public void UpdateProfileDtoValidator_ValidIdentificationNumber_ReturnsNoValidationErrorsForIdentificationNumber(string id)
        {
            // arrange
            var dto = new UserDto { IdentificationNumber = id };

            // act
            var result = _validator.TestValidate(dto);

            // assert
            result.ShouldNotHaveValidationErrorFor(x => x.IdentificationNumber);
        }

        /// <summary>
        ///     Test that there are validation errors for the given valid identification number values.
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
        public void UpdateProfileDtoValidator_InvalidIdentificationNumber_ReturnsValidationErrorsForIdentificationNumber(string id)
        {
            // arrange
            var dto = new UserDto { IdentificationNumber = id };

            // act
            var result = _validator.TestValidate(dto);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.IdentificationNumber);
        }

        /// <summary>
        ///     Test that there are no validation errors for the given mobile number value.
        /// </summary>
        [Fact]
        public void UpdateProfileDtoValidator_ValidMobileNumber_ReturnsNoValidationErrorsForMobileNumber()
        {
            // arrange
            var dto = new UserDto { MobileNumber = "(011)-111-1111" };

            // act
            var result = _validator.TestValidate(dto);

            // assert
            result.ShouldNotHaveValidationErrorFor(x => x.MobileNumber);
        }

         /// <summary>
         ///    Test that there are validation errors for the given invalid mobile number value.
         /// </summary>
         /// <param name="number">
         ///     A <see cref="string"/> representing the mobile number value.
         /// </param>
        [Theory]
        [InlineData("(xxx)-xxx-xxx")]
        [InlineData("(!!!)-xxx-xxx")]
        public void UpdateProfileDtoValidator_InvalidMobileNumber_ReturnsValidationErrorsForMobilNumber(string number)
        {
            // arrange
            var dto = new UserDto { MobileNumber = number };

            // act
            var result = _validator.TestValidate(dto);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.MobileNumber);
        }

        /// <summary>
        ///     Test that there are no validation errors for the given Location Details.
        /// </summary>
        [Fact]
        public void UpdateProfileDtoValidator_ValidLocationDetails_ReturnsNoValidationErrors()
        {
            // Arrange
            var dto = new UserDto 
            { 
                Name = "Lyle D",
                IdentificationNumber = "8704215255083",
                MobileNumber = "(011)-111-1111",
                Email = "lyle@eblocks", 
                StreetName = "11 Moosa Hassen",
                City = "Johannesburg",
                PostalCode = 2020
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.PostalCode);
            result.ShouldNotHaveValidationErrorFor(x => x.StreetName);
            result.ShouldNotHaveValidationErrorFor(x => x.City);
        }

        /// <summary>
        ///     Test that there are validation errors for the given Location Details.
        /// </summary>
        /// <param name="city">
        ///      A <see cref="string"/> representing the city name value.
        /// </param>
        /// <param name="streetName">
        ///      A <see cref="string"/> representing the street name value.
        /// </param>
        /// <param name="postalCode">
        ///      A <see cref="string"/> representing the postal code value.
        /// </param>
        [Theory]
        [InlineData("Joburg", "", 2020)]
        [InlineData("Joburg", null, 2020)]
        [InlineData("Joburg", "21 comos road", -1)]
        [InlineData("", "21 cosmos road", 2020)]
        [InlineData(null, "21 cosmos road", 2020)]        
        public void UpdateProfileDtoValidator_InvalidLocationDetails_ReturnsValidationErrors(
            string city, string streetName, int postalCode)
        {
            // Arrange
            var dto = new UserDto
            {
                Name = "Lyle D",
                IdentificationNumber = "8704215255083",
                MobileNumber = "(011)-111-1111",
                Email = "lyle@eblocks",
                StreetName = streetName,
                City = city,
                PostalCode = postalCode
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        ///     Test that there are no validation errors for the given Medical Aid Details.
        /// </summary>
        [Fact]
        public void UpdateProfileDtoValidator_ValidMedicalDetails_ReturnsNoValidationErrors()
        {
            // Arrange
            var dto = new UserDto() 
            {
                Name = "Lyle D",
                IdentificationNumber = "8704215255083",
                MobileNumber = "0793097768",
                Email = "lyle@eblocks",
                MedicalAidName = "Discovery",
                MedicalAidNumber = "00129323",
                MainMemberName = "Jamie Dimon",
                MainMemberNumber = "(011)-111-1111"
            };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.MedicalAidName);
            result.ShouldNotHaveValidationErrorFor(x => x.MedicalAidNumber);
            result.ShouldNotHaveValidationErrorFor(x => x.MainMemberName);
            result.ShouldNotHaveValidationErrorFor(x => x.MainMemberNumber);
        }
    }
}
