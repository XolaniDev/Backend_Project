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
using af.assessment.api.Data.Models;

namespace af.assessment.api.Converters
{
    /// <summary>
    ///     Represents a RegisterDtoConverter in the sytem.
    /// </summary>
    public interface IRegisterDtoConverter
    {
        /// <summary>
        ///     Converts a <see cref="RegisterDto"/> with a <see cref="PasswordHashResultDto"/> and then returns the resulting <see cref="Member"/> class.
        /// </summary>
        /// <param name="registerDto">
        ///     A <see cref="RegisterDto"/> representing the data trasfer object to be converted.
        /// </param>
        /// <param name="passwordHashResult">
        ///     A <see cref="PasswordHashResultDto"/> representing the hashed password and salt for the member.
        /// </param>
        /// <returns>
        ///     A <see cref="Member"/> representing the resulting member of the operation.
        /// </returns>
        public Member ConvertToMember(RegisterDto registerDto, PasswordHashResultDto passwordHashResult);
    }
}