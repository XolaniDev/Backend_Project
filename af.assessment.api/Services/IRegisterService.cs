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

using System.Threading.Tasks;
using af.assessment.api.Data.Models;

namespace af.assessment.api.Services
{
    /// <summary>
    ///     Represents a register service in the system.
    /// </summary>
    public interface IRegisterService
    {
        /// <summary>
        ///     Creates a new user with the details provided by the user and a id of a new user is returned.
        /// </summary>
        /// <param name="member">
        ///     A <see cref="Member"/> representing the new memebrs details.
        /// </param>
        /// <returns>
        ///     A <see cref="bool"/> indicating if the operations succeeded or not.
        /// </returns>
        public Task<bool> RegisterMember(Member member);
    }
}