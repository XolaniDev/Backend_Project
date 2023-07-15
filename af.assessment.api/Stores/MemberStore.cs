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

using System.Linq;
using System.Threading.Tasks;
using af.assessment.api.Data;
using af.assessment.api.Data.Models;
using Microsoft.Extensions.Logging;

namespace af.assessment.api.Stores
{
    /// <inheritdoc/>
    public class MemberStore : IMemberStore
    {
        /// <summary>
        ///     A <see cref="VaccineDbContext"/> representing the database context.
        /// </summary>
        public readonly VaccineDbContext _dbContext;

        /// <summary>
        ///     A <see cref="ILogger"/> implementation of <see cref="MemberStore"/> representing the data store logger.
        /// </summary>
        private readonly ILogger<MemberStore> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MemberStore"/> class with the database context and the logger for the instance.
        /// </summary>
        /// <param name="vaccineDbContext">
        ///     A <see cref="VaccineDbContext"/> representing the database context to be used.
        /// </param>
        /// <param name="logger">
        ///     A <see cref="ILogger"/> of <see cref="MemberStore"/> logger to be used.
        /// </param>
        public MemberStore(VaccineDbContext vaccineDbContext , ILogger<MemberStore> logger  )
        {
            _dbContext = vaccineDbContext;
            _logger = logger;
        }
        
        /// <inheritdoc/>
        public async Task<bool> RegisterMember(Member newMember)
        {
            bool exist = _dbContext.Members
                .Where( m => m.IdentificationNumber == newMember.IdentificationNumber)
                .ToList().Count > 0;

            if (exist)
            {
                _logger.LogWarning("Member is already exist to the Database");
                return false;
            }

            _dbContext.Add(newMember);
            var result = await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Member is successfully added to the Database");
            return result >0;
        }
    }

}