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
using FluentValidation;

namespace af.assessment.api.Validators.UpdateProfile
{
    /// <summary>
    ///     Provides validation rules to validate the <see cref="LocationDetailsDto"/> class.
    /// </summary>
    public class LocationDetailsDtoValidator: AbstractValidator<LocationDetailsDto>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="LocationDetailsDtoValidator"/> class.
        /// </summary>
        public LocationDetailsDtoValidator()
        {
            RuleFor(x => x.PostalCode).NotEmpty().GreaterThan(-1);
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.StreetName).NotEmpty();
        }
    }
}
