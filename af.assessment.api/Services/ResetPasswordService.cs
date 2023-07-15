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
using System.Collections.Generic;
using System.Threading.Tasks;
using af.assessment.api.Data.Models;
using af.assessment.api.Enums;
using af.assessment.api.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace af.assessment.api.Services
{

    /// <inheritdoc/>
    public class ResetPasswordService : IResetPasswordService
    {
        /// <summary>
        ///     A <see cref="IResetPasswordStore"/> representing the reset password store to be called.
        /// </summary>
        private readonly IResetPasswordStore _resetPasswordStore;

        /// <summary>
        ///     A <see cref="ILogger"/> implementation of type <see cref="ResetPasswordService"/> representing the logger to be used.
        /// </summary>
        private readonly ILogger<ResetPasswordService> _logger;

        /// <summary>
        ///      Initializes a new instance of the <see cref="ResetPasswordService"/> class with the member store to be used.
        /// </summary>
        /// <param name="resetPasswordStore"></param>
        public ResetPasswordService(IResetPasswordStore resetPasswordStore, ILogger<ResetPasswordService> logger)
        {
            _resetPasswordStore = resetPasswordStore;
            _logger = logger;
        }
        
        /// <inheritdoc/>
        public async Task<Member> VerifyUserWithIdNumber(string id)
        {           
            return await _resetPasswordStore.VerifyUserWithIdNumber(id);
        }
        
        /// <inheritdoc/>
        public async Task<Member> ChangePassword(Guid id, string salt, string password)
        {         
            return await _resetPasswordStore.ChangePassword(id, salt, password);
        }


        /// <inheritdoc/>
        public async Task<Member> VerifyUserwithIdGuid(Guid id)
        {
            return await _resetPasswordStore.VerifyUserWithIdGuid(id);
        }
    }
}