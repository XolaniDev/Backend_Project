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
using Microsoft.EntityFrameworkCore;
using af.assessment.api.Data;
using af.assessment.api.Data.Models;
using af.assessment.api.Stores;

namespace af.assessment.api.test.Stores
{
    /// <summary>
    ///     Provides unit tests for the <see cref="FamilyMemberStore"/> class.
    /// </summary>   
    public class FamilyMemberStoreTest
    {
        /// <summary>
        ///    Instantiating properties for the <see cref="DbContextOptions"/>.
        /// </summary>
        private readonly DbContextOptions<VaccineDbContext> _options;

        /// <summary>
        ///     A <see cref="FamilyMemberStore"/> representing the family member store to be called.
        /// </summary>
        private readonly FamilyMemberStore _familyMemberStore;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MemberStoreTest"/> class.
        /// </summary>
        public FamilyMemberStoreTest()
        {
            var builder = new DbContextOptionsBuilder<VaccineDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _options = builder.Options;

            _familyMemberStore = new FamilyMemberStore(new VaccineDbContext(_options));
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

                var familyMember = new List<FamilyMember>
                {
                    new FamilyMember()
                    {
                        Id = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb55123"),
                        FirstName = "Jonathon",
                        LastName = "Player",
                        IdentificationNumber = "2345678901234",
                        Relationship = Enums.MemberType.Son,
                        Appointments = new List<Appointment>(){ new Appointment(){}},
                        Vaccines = new List<Vaccine>(){
                            new Vaccine() {
                                Id = Guid.Parse("123415b1-af8a-4505-b795-12707bb55543"),
                                Name = "Flu Vaccine",
                                Dose = 2,
                                Clinic = new ClinicLocation(){
                                    Id = Guid.Parse("345415b1-af8a-4505-b795-12707bb55543"),
                                    ClinicName = "Flora",
                                    Location = new Location(){
                                        Id = Guid.Parse("789415b1-af8a-4505-b795-12707bb55543")
                                    }
                                },
                                VaccineStatus = true,
                                BatchNumber = 4000,
                                AdministeredDate = new DateTime(),
                                Site = "Arm"
                            }
                        },
                        MemberId = Guid.Parse("56cd15b1-af8a-4505-b795-12707bb55543"),
                        Member = new Member
                        {
                            Id = Guid.Parse("56cd15b1-af8a-4505-b795-12707bb55543"),
                            Name = "Gary Player",
                            IdentificationNumber = "1234567890123",
                            MobileNumber = "(089)-657-0741",
                            Password = "Password1",
                        },
                    },
                };
                
                var members = new List<Member>
                {
                    new Member
                    {
                        Id = Guid.Parse("123d15b1-af8a-4505-b795-45607bb55638"),
                        Name = "Betty Boop",
                        IdentificationNumber = "0012345689102",
                        MobileNumber = "(089)-657-0741",
                        Password = "Password1",
                    }
                };
                
                context.AddRange(familyMember);
                context.AddRange(members);
                context.SaveChanges();
            }
        }

        /// <summary>
        ///     Tests that <see cref="FamilyMemberStore.GetUsersFamilyMembers(Guid)"/> returns a members family members.
        /// </summary>
        [Fact]
        public async void GetFamilyMembers_ValidExistingGuid_ReturnsFamilyMembers()
        {
            // arrange 
            var guid = Guid.Parse("56cd15b1-af8a-4505-b795-12707bb55543");
            var member = new Member
            {
                Id = Guid.Parse("56cd15b1-af8a-4505-b795-12707bb55543"),
                Name = "Gary Player",
                IdentificationNumber = "1234567890123",
                MobileNumber = "(089)-657-0741",
                Password = "Password1",
                FamilyMember = new List<FamilyMember>()
                {
                    new FamilyMember()
                    {
                        Id = Guid.Parse("48ed15b1-af8a-4505-b795-12707bb55123"),
                        FirstName = "Jonathon",
                        LastName = "Player",
                        IdentificationNumber = "2345678901234",
                        Relationship = Enums.MemberType.Son,
                        Appointments = new List<Appointment>(){ new Appointment(){}},
                        Vaccines = new List<Vaccine>(){
                            new Vaccine() {
                                Id = Guid.Parse("123415b1-af8a-4505-b795-12707bb55543"),
                                Name = "Flu Vaccine",
                                Dose = 2,
                                Clinic = new ClinicLocation(){
                                    Id = Guid.Parse("345415b1-af8a-4505-b795-12707bb55543"),
                                    ClinicName = "Flora",
                                    Location = new Location(){
                                        Id = Guid.Parse("789415b1-af8a-4505-b795-12707bb55543")
                                    }
                                },
                                VaccineStatus = true,
                                BatchNumber = 4000,
                                AdministeredDate = new DateTime(),
                                Site = "Arm"
                            }
                        }
                    }
                },
            };

            using (var context = new VaccineDbContext(_options))
            {
                // act
                var result = await _familyMemberStore.GetUsersFamilyMembers(guid);

                // assert
                Assert.Equal(member.Id, result.Id);
                Assert.Equal(member.Email, result.Email);
                Assert.Equal(member.IdentificationNumber, result.IdentificationNumber);
                Assert.Equal(member.MobileNumber, result.MobileNumber);
                Assert.Equal(member.Password, result.Password);
                Assert.Equal(member.FamilyMember.Count, result.FamilyMember.Count);
            }
        }

        /// <summary>
        ///     Tests that <see cref="FamilyMemberStore.GetUsersFamilyMembers(Guid)"/> returns a null when the member doesn't exist.
        /// </summary>
        [Fact]
        public async void GetFamilyMembers_ValidNonExistingGuid_ReturnsNull()
        {
            // arrange 
            var guid = Guid.Parse("120d15b1-af8a-4505-b095-45607bb55038");

            using (var context = new VaccineDbContext(_options))
            {
                // act
                var result = await _familyMemberStore.GetUsersFamilyMembers(guid);

                // assert
                Assert.Null(result);
            }
        }
    }
}