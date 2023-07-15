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

using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using af.assessment.api.Data;
using af.assessment.api.Stores;
using af.assessment.api.Data.Models;
using System.Threading.Tasks;

namespace af.assessment.api.test.Stores
{
    /// <summary>
    ///     Provides unit tests for the <see cref="UserStore"/> class.
    /// </summary>
    public class UserStoreTest
    {
        /// <summary>
        ///       A <see cref="UserStore"/> representing the user store to be called.
        /// </summary>
        private readonly UserStore _userStore;

        /// <summary>
        ///    Instantiating properties for the <see cref="DbContextOptions"/>.
        /// </summary>
        private readonly DbContextOptions<VaccineDbContext> _options;

        /// <summary>
        ///     A <see cref="Mock"/> implementation of <see cref="ILogger"/> representing the user to be mocked.
        /// </summary>
        private readonly Mock<ILogger<UserStore>> _loggerMock = new Mock<ILogger<UserStore>>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserStoreTest"/> class.
        /// </summary>
        public UserStoreTest()
        {
            var builder = new DbContextOptionsBuilder<VaccineDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _options = builder.Options;

            _userStore = new UserStore(new VaccineDbContext(_options), _loggerMock.Object);
            SeedData();
        }

        /// <summary>
        ///     Seeds the database context.
        /// </summary>
        private void SeedData()
        {
            using (var context = new VaccineDbContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var members = new List<Member>
                {
                    new Member
                    {
                        Id = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb55395"),
                        Name = ("Sarah Mallon"),
                        Email = ("sarah.mallon@gmail.com"),
                        IdentificationNumber = "92030067091",
                        MobileNumber = "0867789965",
                        Locations = new Location()
                            {
                                City = "Joburg",
                                StreetName = "Moosa Hassen",
                                PostalCode = 1813,
                                MemberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb55395"),
                                Id = Guid.NewGuid()
                            },
                        
                        FamilyMember = new List<FamilyMember>()
                        {
                            new FamilyMember()
                            {
                            }
                        },

                        MedicalDetails = new MedicalDetails()
                        {
                            MainMemberName = "Sarah Mallon",
                            MainMemberNumber = "0867789965",
                            MedicalAidName = "Bonnitas",
                            MedicalAidNumber = "123456"
                        }
                    },

                    new Member
                    {
                        Id = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb55385"),
                        Name = ("Mary Jane"),
                        Email = ("mary@gmail.com"),
                        IdentificationNumber = "0001010000006",
                        MobileNumber = "0712345601",                                    
                        FamilyMember = new List<FamilyMember>()
                        {
                            new FamilyMember()
                            {
                            }
                        }                     
                    },
                };

                context.AddRange(members);
                context.SaveChanges();
            }
        }

        /// <summary>
        ///     Tests that <see cref="UserStore.GetProfileById(Guid)"/> returns a member.
        /// </summary>
        [Fact]
        public async void Should_Get_User_Profile_If_User_Exists_Successfully()
        {
            // Arrange 
            var memberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb55395");

            using (var context = new VaccineDbContext(_options))
            {
                // Act
                var result = await _userStore.GetProfileById(memberId);

                // Assert
                Assert.Equal(result.Id, memberId);
            }
        }

        /// <summary>
        ///     Tests that <see cref="UserStore.GetProfileById(Guid)"/> does not returns a member if they do not exist.
        /// </summary>
        [Fact]
        public async void Should_Not_Get_User_Profile_If_User_Does_Not_Exists_Successfully()
        {
            // Arrange 
            var memberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb55565");
            var newMember = new Member
            {
                Id = memberId,
                Name = ("Betty Boop"),
                Email = ("betty@gmail.com"),
                IdentificationNumber = "0012345689102",
                MobileNumber = "07123458101",
                Locations = new Location(),
                FamilyMember = new List<FamilyMember>()
                {
                    new FamilyMember()
                },
                MedicalDetails = new MedicalDetails()
            };

            using (var context = new VaccineDbContext(_options))
            {
                // Act
                var result = await _userStore.GetProfileById(memberId);

                // Assert
                Assert.Null(result);
            }
        }

        /// <summary>
        ///     Test case to successfully return a location.
        /// </summary>
        [Fact]
        public async void GetLocationByMemberId_Should_Return_Location()
        {
            // Arrange
            var memberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb55395");

            using (var context = new VaccineDbContext(_options))
            {
                // Act
                var result = await _userStore.GetLocationByMemberId(memberId);

                // Assert
                Assert.Equal("Joburg", result.City);
                Assert.Equal("Moosa Hassen", result.StreetName);
                Assert.Equal(1813, result.PostalCode);
            }            
        }

        /// <summary>
        ///     Test case to return null if location does not exist.
        /// </summary>
        [Fact]
        public async void GetLocationByMemberId_Should_Return_Null()
        {
            // Arrange
            var memberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb55385");

            using (var context = new VaccineDbContext(_options))
            {
                // Act
                var result = await _userStore.GetLocationByMemberId(memberId);

                // Assert
                Assert.Null(result);
            }
        }

        /// <summary>
        ///     Test case to successfully return a medical details.
        /// </summary>
        [Fact]
        public async void GetMedicalAidDetailsByMemberId_Should_Return_MedicalDetails()
        {
            // Arrange
            var memberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb55395");

            using (var context = new VaccineDbContext(_options))
            {
                // Act
                var result = await _userStore.GetMedicalDetailsByMemberId(memberId);

                // Assert
                Assert.Equal("Sarah Mallon", result.MainMemberName);
                Assert.Equal("0867789965", result.MainMemberNumber);
                Assert.Equal("Bonnitas", result.MedicalAidName);
                Assert.Equal("123456", result.MedicalAidNumber);             
            }
        }

        /// <summary>
        ///     Test case to return null if medical details does not exist.
        /// </summary>
        [Fact]
        public async void GetMedicalAidDetailsByMemberId_Should_Return_Null()
        {
            // Arrange
            var memberId = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb55385");

            using (var context = new VaccineDbContext(_options))
            {
                // Act
                var result = await _userStore.GetMedicalDetailsByMemberId(memberId);

                // Assert
                Assert.Null(result);
            }
        }

    }
}