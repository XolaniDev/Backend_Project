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

using af.assessment.api.Data.DTOs.UpdateProfile;
using af.assessment.api.Validators.UpdateProfile;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace af.assessment.api.test.Validators
{
    /// <summary>
    ///     Provides test methods for <see cref="MedicalAidDetailsDtoValidator"/> rules.
    /// </summary>    
    public class MedicalAidDetailsDtoValidatorTest
    {
        /// <summary>
        ///      A <see cref="MedicalAidDetailsDtoValidator"/> representing the validator to be tested.
        /// </summary>
        private readonly MedicalAidDetailsDtoValidator _validator;

        /// <summary>
        ///      Initializes a new instance of the <see cref="MedicalAidDetailsDtoValidatorTest"/> class.
        /// </summary>
        public MedicalAidDetailsDtoValidatorTest()
        {
            _validator = new MedicalAidDetailsDtoValidator();
        }
        
        /// <summary>
        ///     Tests that there are no validation errors for a valid medical aid name.
        /// </summary>
        /// <param name="name">
        ///     A <see cref="string"/> representing the medical aid name value.
        /// </param>
        [Theory]
        [InlineData("Discovery Ltd")]
        [InlineData("Bonitas")]
        public void MedicalAidDetailsDtoValidator_ValidName_Returns_NoValidationErrors(String name)
        {
            // Arrange
            var dto = new MedicalAidDetailsDto { MedicalAidName = name };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.MedicalAidName);
        }
        
        /// <summary>
        ///     Tests that there are validation errors for an invalid medical aid name.
        /// </summary>
        /// <param name="name">
        ///      A <see cref="string"/> representing the medical aid name value.
        /// </param>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void MedicalAidDetailsDtoValidator_InvalidName_Returns_ValidationErrors(String name)
        {
            // Arrange
            var dto = new MedicalAidDetailsDto { MedicalAidName = name };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.MedicalAidName);
        }
        
        /// <summary>
        ///     Tests that there are no validation errors for a valid medical aid number.
        /// </summary>
        /// <param name="number">
        ///      A <see cref="string"/> representing the medical aid number value.
        /// </param>
        [Theory]
        [InlineData("01233364")]
        [InlineData("012644456")]
        public void MedicalAidDetailsDtoValidator_ValidNumber_Returns_NoValidationErrors(String number)
        {
            // Arrange
            var dto = new MedicalAidDetailsDto { MedicalAidNumber = number };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.MedicalAidNumber);
        }
        
        /// <summary>
        ///     Tests that there are validation errors for an invalid medical aid number.
        /// </summary>
        /// <param name="number">
        ///     A <see cref="string"/> representing the medical aid number value.
        /// </param>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("0123 d654")]
        public void MedicalAidDetailsDtoValidator_InvalidNumber_Returns_ValidationErrors(String number)
        {
            // Arrange
            var dto = new MedicalAidDetailsDto { MedicalAidNumber = number };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.MedicalAidNumber);
        }
        
        /// <summary>
        ///     Tests that there are no validation errors for a valid main member name
        /// </summary>
        /// <param name="name">
        ///     A <see cref="string"/> representing the medical aid main member name value.
        /// </param>
        [Theory]
        [InlineData("Jamie Dimon")]
        [InlineData("Lloyd Blankfien")]
        public void MedicalAidDetailsDtoValidator_ValidMainMemberName_Returns_NoValidationErrors(String name)
        {
            // Arrange
            var dto = new MedicalAidDetailsDto { MainMemberName = name };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.MainMemberName);
        }
        
        /// <summary>
        ///     Tests that there are validation errors for an invalid main member name.
        /// </summary>
        /// <param name="name">
        ///     A <see cref="string"/> representing the medical aid main member name value.
        /// </param>
        [Theory]
        [InlineData("")]
        [InlineData(null)]        
        public void MedicalAidDetailsDtoValidator_InvalidMainMemberName_Returns_ValidationErrors(String name)
        {
            // Arrange
            var dto = new MedicalAidDetailsDto { MainMemberName = name };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.MainMemberName);
        }
        
        /// <summary>
        ///     Tests that there are no validation errors for a valid main member number.
        /// </summary>
        /// <param name="number">
        ///     A <see cref="string"/> representing the medical aid main member mobile number value.
        /// </param>
        [Theory]
        [InlineData("(081)-755-8869")] 
        [InlineData("(081)-755-8860")] 
        public void MedicalAidDetailsDtoValidator_ValidMainMemberNumber_Returns_NoValidationErrors(String number)
        {
            // Arrange
            var dto = new MedicalAidDetailsDto { MainMemberNumber = number };

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.MainMemberNumber);
        }
        
        /// <summary>
        ///     Tests that there are validation errors for an invalid main member number.
        /// </summary>
        /// <param name="number">
        ///     A <see cref="string"/> representing the medical aid main member mobile number value.
        /// </param>
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("312443")]
        public void MedicalAidDetailsDtoValidator_InvalidMainMember_Returns_ValidationErrors(String number)
        {
            // Arrange
            var dto = new MedicalAidDetailsDto { MainMemberNumber = number};

            // Act
            var result = _validator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.MainMemberNumber);
        }
    }
}
