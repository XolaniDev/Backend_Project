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
using af.assessment.api.Stores;

namespace af.assessment.api.Services
{
    /// <inheritdoc/>
    public class LoginService : ILoginService
    {
        /// <summary>
        ///     A <see cref="IVaccineStore"/> representing the vaccine store to be called.
        /// </summary>
        private readonly IVaccineStore _vaccineStore;
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="LoginService"/> class with the member store to be used.
        /// </summary>
        /// <param name="vaccineStore"></param>
       public LoginService(IVaccineStore vaccineStore)
        {
            _vaccineStore = vaccineStore;
        }
        /// <inheritdoc/>
        public async Task<Member> LogUserIn(string id , string password)
        {
            return await _vaccineStore.LogUserIn(id, password);
        }

        /// <inheritdoc/>
        public async Task<Member> GetExistingUser(Guid guid)
        {
            return await _vaccineStore.GetExistingUser(guid);
        }

        /// <inheritdoc/>
        public async Task<Member> VerifyUser(string id)
        {
            return await _vaccineStore.VerifyUser(id);
        }


    }
}