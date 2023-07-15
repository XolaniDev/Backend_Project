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
using System.Collections.Generic;
using System.Text;
using af.assessment.api.Data.DTOs.UpdateProfile;
using af.assessment.api.Validators.UpdateProfile;
using FluentValidation.TestHelper;
using Xunit;

namespace af.assessment.api.test.Validators
{
    /// <summary>
    ///     Provides test methods for <see cref="LocationDetailsDtoValidator"/> rules.
    /// </summary>
    /// 
    public class LocationDetailsDtoValidatorTest
    {
        /// <summary>
        ///      A <see cref="LocationDetailsDtoValidator"/> representing the validator to be tested.
        /// </summary>
        private readonly LocationDetailsDtoValidator _validator;

        /// <summary>
        ///      Initializes a new instance of the <see cref="LocationDetailsDtoValidatorTest"/> class.
        /// </summary>
        public LocationDetailsDtoValidatorTest()
        {
            _validator = new LocationDetailsDtoValidator();
        }
        
        /// <summary>
        ///     Tests that there are no validation errors for a valid postal code.
        /// </summary>
        /// <param name="postalCode">
        ///     A <see cref="int"/> representing the postal code number value.
        /// </param>
        [Theory]
        [InlineData(1234)]
        [InlineData(1813)]
        [InlineData(1)]
        public void LocationDetailsDtoValidator_ValidPostalCode_Returns_NoValidationErrors(int postalCode)
        {
            // Arrange
            var dto = new LocationDetailsDto { PostalCode = postalCode };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.PostalCode);
        }
        
        /// <summary>
        ///     Tests that there are validation errors for an invalid postal code.
        /// </summary>
        /// <param name="postalCode">
        ///      A <see cref="int"/> representing the postal code number value.
        /// </param>
        [Theory]
        [InlineData(-1)]
        [InlineData(-2)]
        public void LocationDetailsDtoValidator_InvalidPostalCode_Returns_ValidationErrors(int postalCode)
        {
            // Arrange
            var dto = new LocationDetailsDto { PostalCode = postalCode };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        }
        
        /// <summary>
        ///     Tests that there are no validation errors for a valid street name.
        /// </summary>
        /// <param name="street">
        ///     A <see cref="String"/> representing the street name value.
        /// </param>
        [Theory]
        [InlineData("11 Moosa Hassen")]
        [InlineData("10 Downing")]
        public void LocationDetailsDtoValidator_ValidStreet_Returns_NoValidationErrors(String street)
        {
            // Arrange
            var dto = new LocationDetailsDto { StreetName = street };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.StreetName);
        }
        
        /// <summary>
        ///     Tests that there are validation errors for an invalid street name.
        /// </summary>
        /// <param name="street">
        ///     A <see cref="String"/> representing the street name value.
        /// </param>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void LocationDetailsDtoValidator_InvalidStreet_Returns_ValidationErrors(String street)
        {
            // Arrange
            var dto = new LocationDetailsDto { StreetName = street };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.StreetName);
        }
        
        /// <summary>
        ///      Tests that there are no validation errors for a valid city.
        /// </summary>
        /// <param name="city">
        ///      A <see cref="String"/> representing the city name value.
        /// </param>
        [Theory]
        [InlineData("Joburg")]
        [InlineData("Cape Town")]
        public void LocationDetailsDtoValidator_ValidCity_Returns_NoValidationErrors(String city)
        {
            // Arrange
            var dto = new LocationDetailsDto { City = city };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.City);
        }
        
        /// <summary>
        ///     Tests that there are validation errors for an invalid city
        /// </summary>
        /// <param name="city">
        ///     A <see cref="String"/> representing the city name value.
        /// </param>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void LocationDetailsDtoValidator_InvalidCity_Returns_ValidationErrors(String city)
        {
            // Arrange
            var dto = new LocationDetailsDto { City = city };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.City);
        }
    }
}
