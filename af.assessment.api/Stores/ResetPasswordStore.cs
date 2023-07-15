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
using System.Linq;
using System.Threading.Tasks;
using af.assessment.api.Data;
using af.assessment.api.Data.Models;
using af.assessment.api.Enums;
using Microsoft.EntityFrameworkCore;


namespace af.assessment.api.Stores
{
    /// <inheritdoc/>
    public class ResetPasswordStore : IResetPasswordStore
    {
        /// <summary>
        ///     A <see cref="VaccineDbContext"/> representing the database context.
        /// </summary>
        private readonly VaccineDbContext _dbContext;
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ResetPasswordStore"/> class with the database context and the logger for the instance.
        /// </summary>
        /// <param name="dbContext">
        ///     A <see cref="VaccineDbContext"/> representing the database context to be used.
        /// </param>
        public ResetPasswordStore(VaccineDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        /// <inheritdoc/>
        public async Task<Member> ChangePassword(Guid id, string salt, string password)
        {
            var member = _dbContext.Members.FirstOrDefault(a => a.Id == id);
            if (member == null) return null;
            
            member.Password = password;
            member.Salt = salt;

            await _dbContext.SaveChangesAsync();

            return member;
        }       

        /// <inheritdoc/>
        public async Task<Member> VerifyUserWithIdNumber(string id)
        {
            return await _dbContext.Members
                .Where(x => x.IdentificationNumber == id)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<Member> VerifyUserWithIdGuid(Guid id)
        {
            return await _dbContext.Members
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
