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
    ///     Provides service methods for retrieving and saving changes to a member in the system.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        ///     <see cref="GetProfileById"/> Retrieves the user profile by their Id. 
        /// </summary>
        /// <param name="memberId">
        ///      A <see cref="Guid"/> representing the members details.
        /// </param>
        /// <returns>
        ///       A <see cref="Member"/> indicating the member if the operation was successful or null if the operation was unsuccessful.
        /// </returns>
        public Task<Member> GetProfileById(Guid memberId);

       /// <summary>
       ///      Saves all model changes.
       /// </summary>
        public Task<int> Save();

        /// <summary>
        ///     Returns a <see cref="Location"/> using the given <see cref="Guid"/> member id.
        /// </summary>
        /// <param name="memberId">
        ///     A <see cref="Guid"/> representing member id.
        /// </param>
        /// <returns>
        ///     A <see cref="Location"/> representing the location linked to the member.
        /// </returns>
        public Task<Location> GetLocationByMemberId(Guid memberId);

        /// <summary>
        ///      Returns a <see cref="MedicalDetails"/> using the given <see cref="Guid"/> member id.
        /// </summary>
        /// <param name="memberId">
        ///     A <see cref="Guid"/> representing member id.
        /// </param>
        /// <returns>
        ///      A <see cref="MedicalDetails"/> representing the medical aid linked to the member.
        /// </returns>
        public Task<MedicalDetails> GetMedicalDetailsByMemberId(Guid memberId);
    }
}