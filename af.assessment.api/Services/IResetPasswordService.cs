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


namespace af.assessment.api.Services
{
    /// <summary>
    ///     Represents a reset password service in the system.
    /// </summary>
    public interface IResetPasswordService
    {
        
        /// <summary>
        ///     Provides a method that verifies an existing user based on ID number.
        /// </summary>
        /// <param name="id">
        ///     Represents the id number of the member.
        /// </param>
        /// <returns>
        ///     A requested <see cref="Member"/>.
        /// </returns>
        public Task<Member> VerifyUserWithIdNumber(string id);

        /// <summary>
        ///     Provides a method that verifies an existing user based on id Guid.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        ///     A requested <see cref="Member"/>.
        /// </returns>
        public Task<Member> VerifyUserwithIdGuid(Guid id);

        /// <summary>
        ///     Provides a service method to change/reset user password.
        /// </summary>
        /// <param name="id">
        ///     A <see cref="string"/> representing a  ID.
        /// </param>
        /// <param name="salt">
        ///     A <see cref="string"/> representing a salt.
        /// </param>
        /// <param name="password">
        ///     A <see cref="string"/> representing a password.
        /// </param>
        /// <returns>
        ///     A changed/reset password.
        /// </returns>
        public Task<Member> ChangePassword(Guid id, string salt, string password);
    }
}