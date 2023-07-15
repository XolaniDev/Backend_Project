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
    ///     Provides store methods for saving changes and retrieving details on a member in the system.
    /// </summary>
    public interface IUserStore
    {
        /// <summary>
        ///     Returns member with the provided details.
        /// </summary>
        /// <param name="memberId">
        ///      A <see cref="Guid"/> representing the id of the member that needs to be retrieved from the database.
        /// </param>
        /// <returns>
        ///     A <see cref="Member"/> and their information.
        /// </returns>
        public Task<Member> GetProfileById(Guid memberId);

        /// <summary>
        ///     Returns location for the given member id.
        /// </summary>
        /// <param name="memberId">
        ///     A <see cref="Guid"/> representing the id of the member to which the location belongs.
        /// </param>
        /// <returns>
        ///     The relevant <see cref="Location"/>,
        /// </returns>
        public Task<Location> GetLocationByMemberId(Guid memberId);

        /// <summary>
        ///      Returns medical details for the given member id.
        /// </summary>
        /// <param name="memberId">
        ///      A <see cref="Guid"/> representing the id of the member to which the medical details belongs.
        /// </param>
        /// <returns>
        ///      The relevant <see cref="MedicalDetails"/>,
        /// </returns>
        public Task<MedicalDetails> GetMedicalDetailsByMemberId(Guid memberId);

        /// <summary>
        ///     Saves all model changes to database.      
        /// </summary>
        /// <returns>
        ///     A task that represents the async save operation. The task result contains the number entries written to the database
        /// </returns>
        public Task<int> Save();
    }
}