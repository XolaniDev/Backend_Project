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

namespace af.assessment.api.Stores
{
    /// <summary>
    ///     Provides methods for manipulating and accessing the data in the member's table.
    /// </summary>
    public interface IMemberStore
    {
        /// <summary>
        ///     Creates a new member with the provided details and returns if the member has been added.
        /// </summary>
        /// <param name="newMember">
        ///     A <see cref="Member"/> representing the member details that needs to be saved in the database.
        /// </param>
        /// <returns>
        ///     A <see cref="bool"/> representing the member has been created. 
        /// </returns>
        public Task<bool> RegisterMember(Member newMember);
    }
}