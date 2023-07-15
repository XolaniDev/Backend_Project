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
using af.assessment.api.Stores;
using Microsoft.Extensions.Logging;

namespace af.assessment.api.Services
{
   
    /// <inheritdoc/>
    public class RegisterService : IRegisterService
    {
        /// <summary>
        ///     A <see cref="IMemberStore"/> representing the member store to be called.
        /// </summary>
        private readonly IMemberStore _memberStore;
        /// <summary>
        ///     A <see cref="ILogger"/> implementation of type <see cref="RegisterService"/> represemting the logger to be used.
        /// </summary>
        private readonly ILogger<RegisterService> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RegisterService"/> class with the member store to be used.
        /// </summary>
        /// <param name="MemberStore"></param>
        /// <param name="logger"></param>
        public RegisterService(IMemberStore MemberStore , ILogger<RegisterService> logger)
        {
            _memberStore = MemberStore;
            _logger = logger;
        }

        /// <inheritdoc/>
        public Task<bool> RegisterMember(Member newMember)
        {
            _logger.LogInformation("Member has been successfully registered in a service.");
            return _memberStore.RegisterMember(newMember);
        }
    }

    
}