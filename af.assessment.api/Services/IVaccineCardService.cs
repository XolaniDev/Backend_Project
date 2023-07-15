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
    public interface IVaccineCardService
    {
        /// <summary>
        ///     Provides a service method to get a user by ID.
        /// </summary>
        /// <param name="guid"></param>
        public Task<Member> GetUsersFamilyMembers(Guid guid);
    }
}