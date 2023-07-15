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
    ///      Provides services to login a user into the application.
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        ///     Provides a service method to get a user by ID.
        /// </summary>
        /// <param name="guid"></param>
        public Task<Member> GetExistingUser(Guid guid);

        /// <summary>
        ///     Gets a member with the provided details and returns if the member has been logged in.
        /// </summary>
        /// <param name="id">
        ///     A <see cref="string"/> representing the id credential to log in.
        /// </param>
        /// <param name="password">
        ///     A <see cref="string"/> representing the password credential to log in.
        /// </param>
        /// <returns>
        ///     A <see cref="Member"/> indicating the member if the operation was successful or null if the operation was unsuccessful.
        /// </returns>
        public Task<Member> LogUserIn(string id, string password);

        /// <summary>
        ///     Verify a user with the details provided by the id of a user is returned.
        /// </summary>
        /// <param name="id">
        ///     A <see cref="string"/> representing the id number to be verified.
        /// </param>
        /// <returns>
        ///     A <see cref="Member"/> indicating the member if the operation was successful or null if the operation was unsuccessful.
        /// </returns>
        public Task<Member> VerifyUser(string id);
    }
}