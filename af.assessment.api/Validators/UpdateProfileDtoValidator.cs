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
    ///     Provides validation rules to validate the <see cref="UserDto"/> class.
    /// </summary>
    public class UpdateProfileDtoValidator : AbstractValidator<UserDto>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UpdateProfileDtoValidator"/> class.
        /// </summary>
        public UpdateProfileDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.IdentificationNumber).NotEmpty()
               .Must(ValidatorUtilityMethods.BeAValidIdentificationNumber);
            RuleFor(x => x.MobileNumber).NotEmpty().Matches(@"\([0-9]{3}\)\-[0-9]{3}\-[0-9]{4}");
            
            When(x => x.PostalCode > 0 || !string.IsNullOrEmpty(x.StreetName) || !string.IsNullOrEmpty(x.City), 
            () => {
                RuleFor(x => x.PostalCode).GreaterThan(-1);
                RuleFor(x => x.City).NotEmpty();
                RuleFor(x => x.StreetName).NotEmpty();
            });            

            When(x => !string.IsNullOrEmpty(x.MainMemberName) || !string.IsNullOrEmpty(x.MainMemberNumber) ||
                 !string.IsNullOrEmpty(x.MedicalAidName) || !string.IsNullOrEmpty(x.MedicalAidNumber),
            () => {
                RuleFor(x => x.MedicalAidName).NotEmpty();
                RuleFor(x => x.MedicalAidNumber).NotEmpty();
                RuleFor(x => x.MainMemberName).NotEmpty();
                RuleFor(x => x.MainMemberNumber).NotEmpty().Matches(@"\([0-9]{3}\)\-[0-9]{3}\-[0-9]{4}");
            });
        }             
    }
}
