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
using af.assessment.api.Data;
using af.assessment.api.Data.Models;
using af.assessment.api.Stores;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace af.assessment.api.test.Stores
{
    /// <summary>
    ///      Provides unit tests for the <see cref="VaccineStoreTest"/> class.
    /// </summary>
    public class VaccineStoreTest
    {
        /// <summary>
        ///     Instantiating the <see cref="VaccineStore"/>.
        /// </summary>
        private VaccineStore _vaccineStore;

        /// <summary>
        ///    Instantiating properties for the <see cref="DbContextOptions"/>.
        /// </summary>
        public DbContextOptions<VaccineDbContext> Options { get; private set; }

        /// <summary>
        ///     Creating VaccineDbContext properties.
        /// </summary>
        public Guid test_guid = Guid.NewGuid();
        /// <summary>
        ///     A <see cref="string"/> representing a test id number. 
        /// </summary>
        public string id = "1000000000000";
        /// <summary>
        ///     A <see cref="string"/> representing a test password.
        /// </summary>
        public string password = "TestPassword";

        /// <summary>
        ///      Constructor for the <see cref="VaccineStoreTest"/>.
        /// </summary>
        public VaccineStoreTest()
        {
            var builder = new DbContextOptionsBuilder<VaccineDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            Options = builder.Options;

            _vaccineStore = new VaccineStore(new VaccineDbContext(Options));
            Seed();
        }

        /// <summary>
        ///     Test case <see cref="Task"/> to retrieve the existing user successfully.
        /// </summary>
        /// <returns>
        ///     Existing user when their record exists.
        /// </returns>
        [Fact]
        public async Task GetExistingUser_Successfully_When_Record_Exists()
        {
            //Arrange
            var newTestGuid = test_guid;
            using (var _context = new VaccineDbContext(Options))
            {
                //Act
                var result = await _vaccineStore.GetExistingUser(newTestGuid);

                //Assert
                Assert.Equal(result.Id, newTestGuid);
            }
        }

        /// <summary>
        ///     Test case <see cref="Task"/> to retrieve the existing user unsuccessfully.
        /// </summary>
        /// <returns> 
        ///   A null value when user record does not exist.
        /// </returns>
        [Fact]
        public async Task GetExistingUser_Unsuccessfully_Should_Return_null_If_User_Does_Not_Exist()
        {
            // Arrange
            var id = Guid.NewGuid();
            using (var _context = new VaccineDbContext(Options))
            {
                // Act
                var result = await _vaccineStore.GetExistingUser(id);

                // Assert
                Assert.Null(result);
            };
        }

        /// <summary>
        ///      Test case <see cref="Task"/> to log user in successfully.
        /// </summary>
        /// <returns> 
        ///      Member object if user exists.
        /// </returns>
        [Fact]
        public async Task LogUserIn_Successfully_Should_Return_Member_Object_If_User_Exists()
        {
            //Arrange
            var testId = "1000000000000";
            var testPassword = "TestPassword";
            using (var _context = new VaccineDbContext(Options))
            {
                //Act
                var result = await _vaccineStore.LogUserIn(testId, testPassword);

                //Assert
                Assert.Equal(result.IdentificationNumber, testId);
                Assert.Equal(result.Password, testPassword);
            };
        }

        /// <summary>
        ///      Test case <see cref="Task"/> to log user in unsuccessfully.
        /// </summary>
        /// <returns>
        ///      A null value if user does not exist.
        /// </returns>
        [Fact]
        public async Task LogUserIn_Unsuccessfully_Should_Return_Null_If_User_Does_Not_Exists()
        {
            //Arrange
            var testId = "100000000000";
            var testPassword = "TestPasswor";
            using (var _context = new VaccineDbContext(Options))
            {
                //Act
                var result = await _vaccineStore.LogUserIn(testId, testPassword);

                //Assert
                Assert.Null(result);
            };
        }

        /// <summary>
        ///      Test case <see cref="Task"/> to verify user in successfully.
        /// </summary>
        /// <returns>
        ///      Member object if the user exists.
        /// </returns>
        [Fact]
        public async Task VerifyUser_Successfully_Should_Return_Member_Object_If_User_Exists()
        {
            //Arrange
            var testId = "100000000000";
            using (var _context = new VaccineDbContext(Options))
            {
                //Act
                var result = await _vaccineStore.VerifyUser(testId);

                //Assert
                Assert.Null(result);
            };
        }

        /// <summary>
        ///     Test case <see cref="Task"/> to verify user in unsuccessfully.
        /// </summary>
        /// <returns> 
        ///     A null if the user does not exist.
        /// </returns>
        [Fact]
        public async Task VerifyUser_Unsuccessfully_Should_Return_Null_If_User_Does_Not_Exists()
        {
            //Arrange
            var testId = id;
            using (var _context = new VaccineDbContext(Options))
            {
                //Act
                var result = await _vaccineStore.VerifyUser(testId);

                //Assert
                Assert.Equal(result.IdentificationNumber, id);
            };
        }

        /// <summary>
        ///      Seeds the database context.
        /// </summary>
        private void Seed()
        {
            using (var _context = new VaccineDbContext(Options))
            {
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();

                var member = new Member()
                {
                    Id = test_guid,
                    Name = "test",
                    Email = "test@email.com",
                    IdentificationNumber = id,
                    MobileNumber = "0",
                    Password = password
                };

                _context.Add(member);
                _context.SaveChanges();
            }
        }
    }
}
