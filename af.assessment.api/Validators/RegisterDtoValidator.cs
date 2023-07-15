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

using FluentValidation;
using af.assessment.api.Data.Dtos;
using af.assessment.api.Validators.Utils;

namespace af.assessment.api.Validators
{
    /// <summary>
    ///     Provides validation rules to validate the <see cref="RegisterDto"/> class.
    /// </summary>
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RegisterDtoValidator"/> class.
        /// </summary>
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.OtpPreference).NotEmpty().InclusiveBetween(1,3);
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.IdentificationNumber).NotEmpty()
                .Must(ValidatorUtilityMethods.BeAValidIdentificationNumber);
            RuleFor(x => x.MobileNumber).NotEmpty().Matches(@"\([0-9]{3}\)\-[0-9]{3}\-[0-9]{4}");
            RuleFor(x => x.Password).NotEmpty()
                .Matches(@"[a-z]+")
                .Matches(@"[A-Z]+")
                .Matches(@"[0-9]+")
                .Must(ValidatorUtilityMethods.IsValidPassword);
        }

    }
}