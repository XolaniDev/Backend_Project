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
using af.assessment.api.Validators.Utils;
using FluentValidation;

namespace af.assessment.api.Validators.UpdateProfile
{
    /// <summary>
    ///     Provides validation rules to validate the <see cref="PersonalDetailsDto"/> class.
    /// </summary>
    public class PersonalDetailsDtoValidator: AbstractValidator<PersonalDetailsDto>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PersonalDetailsDtoValidator"/> class.
        /// </summary>
        public PersonalDetailsDtoValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.IdentificationNumber).NotEmpty()
               .Must(ValidatorUtilityMethods.BeAValidIdentificationNumber);
            RuleFor(x => x.MobileNumber).NotEmpty().Matches(@"\([0-9]{3}\)\-[0-9]{3}\-[0-9]{4}");
        }   
    }
}
