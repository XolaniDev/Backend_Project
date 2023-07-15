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

using Xunit;
using System;
using System.Collections.Generic;
using Moq;
using Microsoft.EntityFrameworkCore;
using af.assessment.api.Data;
using af.assessment.api.Data.Models;
using af.assessment.api.Stores;
using Microsoft.Extensions.Logging;

namespace af.assessment.api.test.Stores
{
    /// <summary>
    ///     Provides unit tests for the <see cref="MemberStore"/> class.
    /// </summary>   
    public class MemberStoreTest
    {
        /// <summary>
        ///     A <see cref="MemberStore"/> representing the member store to be called.
        /// </summary>
        private readonly MemberStore _memberStore;
        
        /// <summary>
        ///    Instantiating properties for the <see cref="DbContextOptions"/>.
        /// </summary>
        private readonly DbContextOptions<VaccineDbContext> _options;

        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="ILogger"/> representing the register converter to be mocked.
        /// </summary>
        private readonly Mock<ILogger<MemberStore>> _loggerMock = new Mock<ILogger<MemberStore>>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="MemberStoreTest"/> class.
        /// </summary>
        public MemberStoreTest()
        {
            var builder = new DbContextOptionsBuilder<VaccineDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _options = builder.Options;

            _memberStore = new MemberStore(new VaccineDbContext(_options), _loggerMock.Object);
            SeedData();
        }

        /// <summary>
        ///     Seeds the database context.
        /// </summary>
        private void SeedData()
        {
            using (var context = new VaccineDbContext(_options))
            {
                context.Database.EnsureCreated();
                context.Database.EnsureDeleted();
                var members = new List<Member>
                {
                  new Member
                  {
                      Name = "Gary Player",
                      IdentificationNumber = "1234567890123",
                      MobileNumber = "0896570741",
                      Password = "Password1",
                  },

                  new Member
                  {
                      Name = "Betty Boop",
                      IdentificationNumber = "0012345689102",
                      MobileNumber = "0896570741",
                      Password = "Password1",
                  }
                };
                context.AddRange(members);
                context.SaveChanges();
            }
        }

        /// <summary>
        ///     Tests that <see cref="MemberStore.RegisterMember(Member)"/> returns a the newly created member.
        /// </summary>
        [Fact]
        public async void RegisterMember_ValidInputs_ReturnsTrue()
        {
            // arrange 
            var newMember = new Member
            {
                Name = "Sally May",
                IdentificationNumber = "001234567732",
                MobileNumber = "0175146240",
                Password = "Password1",
            };

            using (var context = new VaccineDbContext(_options))
            {
                // act
                var result = await _memberStore.RegisterMember(newMember);

                // assert
                Assert.True(result);
            }

        }

        /// <summary>
        ///     Tests that <see cref="MemberStore.RegisterMember(Member)"/> returns false if member already exists.
        /// </summary>
        [Fact]
        public async void RegisterMember_ValidInputs_ReturnsFalse()
        {
            // arrange 
            var newMember = new Member
            {
                Name = "Gary Player",
                IdentificationNumber = "1234567890123",
                MobileNumber = "0896570741",
                Password = "Password1",
            };

            using (var context = new VaccineDbContext(_options))
            {
                // act
                var result = await _memberStore.RegisterMember(newMember);

                // assert
                Assert.False(result);
            }
        }
    }
}