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
using System;
using System.Globalization;
using System.Linq;
using af.assessment.api.Data.Dtos;


namespace af.assessment.api.Validators
{
    /// <summary>
    ///    Provides validation rules to validate the <see cref="ResetPasswordDto"/> class.
    /// </summary>
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ResetPasswordDtoValidator"/> class.
        /// </summary>
        public ResetPasswordDtoValidator()
        {

            RuleFor(x => x.MemberId).NotEmpty().Equals(Guid.Empty);

            RuleFor(x => x.Password).NotEmpty()
                .Matches(@"[a-z]+")
                .Matches(@"[A-Z]+")
                .Matches(@"[0-9]+")
                .Must(IsValidPassword).WithMessage("Invalid Password Format");

            RuleFor(x => x.ConfirmPassword).NotEmpty()
                .Matches(@"[a-z]+")
                .Matches(@"[A-Z]+")
                .Matches(@"[0-9]+")
                .Must(IsValidPassword).WithMessage("Invalid Confirm Password");

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match");
        }

         /// <summary>
        ///     Validates the password and returns a bool of the operation. 
        /// </summary>
        /// <param name="password">
        ///     A <see cref="string"/> representing the members
        /// </param>
        /// <returns>
        ///     A <see cref="bool"/> if the password is valid.
        /// </returns>
        public bool IsValidPassword(string password)
        {
            if (password is null || password.Trim().Length < 8 || password.Trim().Length > 14 || !hasSpecialChar(password) || password == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        ///     Method for the special characters.
        /// </summary>
        /// <param name="input"></param>
        /// <returns> 
        ///      A <see cref="bool"/> of false.
        /// </returns>
        public static bool hasSpecialChar(string input)
        {
            string specialChar = @"|!#$%&/()=?»«@£§€{}.-;~`'<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }
    }
}
