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
    ///  <inheritdoc />
    public class UserService : IUserService
    {
        /// <summary>
        ///      A <see cref="IUserService"/> representing the member store to be called.
        /// </summary>
        private readonly IUserStore _userStore;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserService"/> class with the member store to be used.
        /// </summary>
        public UserService(IUserStore UserStore)
        {
            _userStore = UserStore;
        }        

        /// <inheritdoc/>
        public async Task<Member> GetProfileById(Guid memberId)
        {
            if (memberId == null)
            {
                throw new AggregateException();
            }

            return await _userStore.GetProfileById(memberId);
        }               

        /// <inheritdoc/>
        public Task<int> Save()
        {
            return _userStore.Save();
        }

        /// <inheritdoc/>
        public async Task<Location> GetLocationByMemberId(Guid memberId)
        {
            return await _userStore.GetLocationByMemberId(memberId);
        }

        /// <inheritdoc/>
        public async Task<MedicalDetails> GetMedicalDetailsByMemberId(Guid memberId)
        {
            return await _userStore.GetMedicalDetailsByMemberId(memberId);
        }
    }
}