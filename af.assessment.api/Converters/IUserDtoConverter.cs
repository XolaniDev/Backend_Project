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
using System;

namespace af.assessment.api.Converters
{
    /// <summary>
    ///     Represents a UserDtoConverter in the system.
    /// </summary>
    public interface IUserDtoConverter
    {
        /// <summary>
        ///     Converts a <see cref="UserDto"/> then returns the resulting <see cref="Member"/> class.
        /// </summary>
        /// <param name="member">
        ///     A <see cref="Member"/> representing the member to be converted.
        /// </param>
        /// <returns>
        ///     A <see cref="UserDto"/> representing the resulting user data transfer object of the operation.
        /// </returns>
        public UserDto ConvertToUserDto(Member member);

        /// <summary>
        ///      Converts a <see cref="UserDto"/> then returns the resulting <see cref="Member"/> class.        
        /// </summary>
        /// <param name="memberId">
        ///     A <see cref="Guid"/> representing the guid of the member to convert.
        /// </param>        
        /// <param name="dto">
        ///     A <see cref="UserDto"/> representing the user data transfer object to convert.
        /// </param>        
        /// <returns>
        ///     A <see cref="Member"/> representing the resulting model of the operation.
        /// </returns>
        public Member ConvertToMember(Guid memberId, UserDto dto);
    }
}

