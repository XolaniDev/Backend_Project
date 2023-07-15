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

using af.assessment.api.Data.Dtos;
using af.assessment.api.Validators.Utils;
using FluentValidation;

namespace af.assessment.api.Validators
{
    /// <summary>
    ///     Provides validation rules to validate the <see cref="LoginDto"/> class.
    /// </summary>
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginDtoValidator"/> class.
        /// </summary>
        public LoginDtoValidator()
        {
            
            RuleFor(x => x.Password).NotEmpty()
                .Matches(@"[a-z]+")
                .Matches(@"[A-Z]+")
                .Matches(@"[0-9]+")
                .Must(ValidatorUtilityMethods.IsValidPassword);
            RuleFor(x => x.IdentificationNumber).NotEmpty()
             .Must(ValidatorUtilityMethods.BeAValidIdentificationNumber);
        }      
    }
}
