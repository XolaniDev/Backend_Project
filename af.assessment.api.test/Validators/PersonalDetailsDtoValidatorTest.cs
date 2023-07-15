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
using af.assessment.api.Data.DTOs.UpdateProfile;
using af.assessment.api.Validators.UpdateProfile;
using FluentValidation.TestHelper;
using Xunit;

namespace af.assessment.api.test.Validators
{
    /// <summary>
    ///     Provides test methods for <see cref="PersonalDetailsDtoValidator"/> rules.
    /// </summary>
    /// 
    public class PersonalDetailsDtoValidatorTest
    {
        /// <summary>
        ///      A <see cref="PersonalDetailsDtoValidator"/> representing the validator to be tested.
        /// </summary>
        private readonly PersonalDetailsDtoValidator _validator;


        /// <summary>
        ///      Initializes a new instance of the <see cref="PersonalDetailsDtoValidatorTest"/> class.
        /// </summary>
        public PersonalDetailsDtoValidatorTest()
        {
            _validator = new PersonalDetailsDtoValidator();
        }

        /// <summary>
        ///     Tests that there are no validation errors for a valid name.
        /// </summary>
        /// <param name="name">
        ///     A <see cref="string"/> representing the first name and last name value.
        /// </param>
        [Theory]
        [InlineData("Jamie Dimon")]
        [InlineData("Jessy Dimon")]
        public void PersonalDetailsDtoValidator_ValidName_Returns_NoValidationErrors(String name)
        {
            // Arrange
            var dto = new PersonalDetailsDto { Name = name };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        ///      Tests that there are validation errors for a invalid name.
        /// </summary>
        /// <param name="name">
        ///     A <see cref="string"/> representing the first name and last name value.
        /// </param>
        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void PersonalDetailsDtoValidator_InvalidName_Returns_ValidationErrors(String name)
        {
            // Arrange
            var dto = new PersonalDetailsDto { Name = name };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        /// <summary>
        ///     ests that there are no validation errors for the given valid email value.
        /// </summary>
        /// <param name="email">
        ///     A <see cref="string"/> representing the email value.
        /// </param>
        [Theory]       
        [InlineData("a@a.org")]
        [InlineData("lyle@eblocks.co.za")]
        public void PersonalDetailsDtoValidator_ValidEmail_ReturnsNoValidationErrors(string email)
        {
            // arrange
            var dto = new PersonalDetailsDto { Email = email };

            // act
            var result = _validator.TestValidate(dto);

            // assert
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
        }
        
        /// <summary>
        ///     Tests that there are validation errors for the given invalid email values.
        /// </summary>
        /// <param name="email">
        ///     A <see cref="string"/> representing the email value.
        /// </param>
        [Theory]
        [InlineData("")]
        [InlineData("hello")]
        [InlineData("world.com")]
        public void PersonalDetailsDtoValidator_InvalidEmail_Returns_ValidationErrors(string email)
        {
            // arrange
            var dto = new PersonalDetailsDto { Email = email };

            // act
            var result = _validator.TestValidate(dto);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }
        
        /// <summary>
        ///     Test that there are no validation errors for the given valid identification number values.
        /// </summary>
        /// <param name="id">
        ///     A <see cref="string"/> representing the email value.
        /// </param>
        [Theory]
        [InlineData("0001010000006")]
        [InlineData("0001010000105")]
        [InlineData("9912310000085")]
        public void PersonalDetailsDtoValidator_Valid_IdentificationNumber_Returns_NoValidationErrors(string id)
        {
            // arrange
            var dto = new PersonalDetailsDto { IdentificationNumber = id };

            // act
            var result = _validator.TestValidate(dto);

            // assert
            result.ShouldNotHaveValidationErrorFor(x => x.IdentificationNumber);
        }
        
        /// <summary>
        ///     Test that there are validation errors for the given invalid identification number values.
        /// </summary>
        /// <param name="id">
        ///     A <see cref="string"/> representing the Id number value.
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
        public void PersonalDetailsDtoValidator_Invalid_IdentificationNumber_Returns_ValidationErrors(string id)
        {
            // arrange
            var dto = new PersonalDetailsDto { IdentificationNumber = id };

            // act
            var result = _validator.TestValidate(dto);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.IdentificationNumber);
        }
        
        /// <summary>
        ///     Test that there are no validation errors for the given valid mobile number value.
        /// </summary>
        /// <param name="mobileNumber">
        ///     A <see cref="string"/> representing the mobile number value.
        /// </param>
        [Theory]
        [InlineData("(079)-309-7768")]
        [InlineData("(081)-365-8532")]
        public void PersonalDetailsDtoValidator_Valid_MobileNumber_Returns_NoValidationErrors(String mobileNumber)
        {
            // arrange
            var dto = new PersonalDetailsDto { MobileNumber = mobileNumber };

            // act
            var result = _validator.TestValidate(dto);

            // assert
            result.ShouldNotHaveValidationErrorFor(x => x.MobileNumber);
        }
        
        /// <summary>
        ///     Test that there are validation errors for the given invalid mobile number value.
        /// </summary>
        /// <param name="number">
        ///     A <see cref="string"/> representing the mobile number value.
        /// </param>
        [Theory]
        [InlineData("")]
        [InlineData("(08a)-a04-4212")]
        [InlineData("(08)-236-4587")]
        [InlineData("080 236 4587")]
        [InlineData("0802364587")]
        public void PersonalDetailsDtoValidator_Invalid_MobileNumber_ReturnsValidationErrors(string number)
        {
            // arrange
            var dto = new PersonalDetailsDto { MobileNumber = number };

            // act
            var result = _validator.TestValidate(dto);

            // assert
            result.ShouldHaveValidationErrorFor(x => x.MobileNumber);
        }
    }
}
