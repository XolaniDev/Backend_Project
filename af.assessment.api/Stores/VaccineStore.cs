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
using System.Linq;
using System.Threading.Tasks;
using af.assessment.api.Data;
using af.assessment.api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace af.assessment.api.Stores
{
    /// <inheritdoc/>
    public class VaccineStore : IVaccineStore
    {
        /// <summary>
        ///     A <see cref="VaccineDbContext"/> representing the database context.
        /// </summary>
        private readonly VaccineDbContext _dbContext;
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="MemberStore"/> class with the database context and the logger for the instance.
        /// </summary>
        /// <param name="dbContext">
        ///     A <see cref="VaccineDbContext"/> representing the database context to be used.
        /// </param>
        public VaccineStore(VaccineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<Member> GetExistingUser(Guid id)
        {
            return  await _dbContext.Members
                   .Where(x => x.Id == id)
                   .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<Member> LogUserIn(string id , string password)
        {

            return await _dbContext.Members
                   .Where(x => x.IdentificationNumber == id)
                   .Where(y => y.Password == password)
                   .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<Member> VerifyUser(string id)
        {
            return await _dbContext.Members
                .Where(x => x.IdentificationNumber == id)
                .FirstOrDefaultAsync();
        }
    }
}