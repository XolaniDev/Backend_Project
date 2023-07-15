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
using System.Threading.Tasks;
using af.assessment.api.Data.Models;

namespace af.assessment.api.Stores
{
    /// <summary>
    ///     Provides methods for manipulating and accessing the data in the password table.
    /// </summary>
    public interface IResetPasswordStore
    {
        /// <summary>
        ///     Verifies a member with the provided identification number.
        /// </summary>
        /// <param name="id">
        ///     The member identification number.
        /// </param>
        /// <returns>
        ///     A <see cref="Member"/> representing the verified member.
        /// </returns>
        public Task<Member> VerifyUserWithIdNumber(string id);

        /// <summary>
        ///     Verifies a member with the provided member id.
        /// </summary>
        /// <param name="id">
        ///     The member id.
        /// </param>
        /// <returns>A <see cref="Member"/> representing the verified member.</returns>
        public Task<Member> VerifyUserWithIdGuid(Guid id);

        /// <summary>
        ///      Changes the member's password.
        /// </summary>
        /// <param name="id">
        ///     The member id.    
        /// </param>
        /// <param name="salt">
        ///     The salt.
        /// </param>
        /// <param name="password">
        ///     The new password.
        /// </param>
        /// <returns>
        ///     The <see cref="Member"/> password that was reset.
        /// </returns>
        public Task<Member> ChangePassword(Guid id, string salt, string password);


    }
}
