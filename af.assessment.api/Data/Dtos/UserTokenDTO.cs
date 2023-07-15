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
using System.Diagnostics.CodeAnalysis;

namespace af.assessment.api.Data.Dtos
{
    /// <summary>
    ///     Represents a Usertoken DTO for creating a JWT.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UserTokenDto
    {
        /// <summary>
        ///      A <see cref="string"/> reperesenting the token content in the UserTokenDto.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///     A <see cref="DateTime"/> reperesenting the token time of creation in the UserTokenDto.
        /// </summary>
        public DateTime ExpirationTime { get; set; }

        /// <summary>
        ///     A <see cref="string"/> representing the name of the user in the UserTokenDto.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }

        /// <summary>
        ///     A <see cref="Guid"/> representing the Guid of the user in the UserTokenDTO.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        ///     A <see cref="String"/> representing the Url to the user profile picture.
        /// </summary>
        public string ProfilePictureUrl { get; set; }
    }
}
