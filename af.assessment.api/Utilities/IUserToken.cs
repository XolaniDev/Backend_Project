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

namespace af.assessment.api.Utilities
{
    /// <summary>
    ///     Provides a user token generation service.
    /// </summary>
    public interface IUserToken
    {
        /// <summary>
        ///      Creates an <see cref="UserTokenDto"/> for the given member.
        /// </summary>
        /// <param name="memberModel">
        ///     A <see cref="Member"/> representing the member to generate a token for.
        /// </param>
        /// <returns> 
        ///     An <see cref="UserTokenDto"/> representing the user token result of the operation.  
        /// </returns>
        public UserTokenDto BuildUserToken(Member memberModel);

    }
}
