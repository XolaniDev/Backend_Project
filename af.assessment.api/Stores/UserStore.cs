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
using Microsoft.EntityFrameworkCore;
using af.assessment.api.Data;
using af.assessment.api.Data.Models;
using Microsoft.Extensions.Logging;

namespace af.assessment.api.Stores
{
    ///  <inheritdoc/>
    public class UserStore : IUserStore
    {
        /// <summary>
        ///     A <see cref="VaccineDbContext"/> representing the database context.
        /// </summary>
        private readonly VaccineDbContext _dbContext;

        /// <summary>
        ///     An <see cref="ILogger"/> implementation of <see cref="UserStore"/> representing the data store logger.
        /// </summary>
        private readonly ILogger<UserStore> _logger;

        /// <summary>
        ///      Initializes a new instance of the <see cref="UserStore"/> class with the database context and logger to be used.
        /// </summary>
        public UserStore(VaccineDbContext dbContext, ILogger<UserStore> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }        

        /// <inheritdoc/>
        public async Task<Member> GetProfileById(Guid memberId)
        {
            _logger.LogInformation("Member is successfully retrieved from the Database");
            return await _dbContext.Members.Include(m => m.Locations)
                .Include(m => m.FamilyMember)
                .Include(m => m.MedicalDetails)
                .FirstOrDefaultAsync(m => m.Id == memberId);
        }

        /// <inheritdoc/>
        public async Task<Location> GetLocationByMemberId(Guid memberId)
        {
            return await _dbContext.Locations.FirstOrDefaultAsync(x => x.MemberId == memberId);
        }

        /// <inheritdoc/>
        public async Task<MedicalDetails> GetMedicalDetailsByMemberId(Guid memberId)
        {
            return await _dbContext.MedicalDetails.FirstOrDefaultAsync(x => x.MemberId == memberId);
        }

        /// <inheritdoc/>
        public async Task<int> Save()
        {
            return await _dbContext.SaveChangesAsync();
        }       
    }
}