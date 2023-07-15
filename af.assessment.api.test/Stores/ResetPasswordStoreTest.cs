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
    ///      Provides unit tests for the <see cref="ResetPasswordStoreTest"/> class.
    /// </summary>
    public class ResetPasswordStoreTest
    {
        /// <summary>
        ///     Declaring <see cref="ResetPasswordStore"/> representing the ResetPasswordStore.
        /// </summary>
        private ResetPasswordStore _ResetPasswordStore;

        /// <summary>
        ///    Declaring <see cref="DbContextOptions"/> representing the DBcontextOptions.
        /// </summary>
        private DbContextOptions<VaccineDbContext> Options { get;  set; }

        /// <summary>
        ///     Creating VaccineDbContext properties.
        /// </summary>
        private Guid test_guid = Guid.NewGuid();
        
        /// <summary>
        ///     A <see cref="string"/> representing a test id number. 
        /// </summary>
        private string id = "1000000000000";
        
        /// <summary>
        ///     A <see cref="string"/> representing a test password.
        /// </summary>
        private string password = "TestPassword";

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResetPasswordStoreTest"/> class.
        /// </summary>
        public ResetPasswordStoreTest()
        {
            var builder = new DbContextOptionsBuilder<VaccineDbContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            Options = builder.Options;

            _ResetPasswordStore = new ResetPasswordStore(new VaccineDbContext(Options));
            Seed();
        }

        /// <summary>
        ///     Test case <see cref="Task"/> to retrieve the existing user successfully with id number.
        /// </summary>
        [Fact]
        public async Task Verify_ExistingUser_Successfully_Using_IdNumber()
        {
            //Arrange
            var newTestGuid = test_guid;
            using (var _context = new VaccineDbContext(Options))
            {
                //Act
                var result = await _ResetPasswordStore.VerifyUserWithIdNumber(id);

                //Assert
                Assert.Equal(result.IdentificationNumber, id);
            }
        }

        /// <summary>
        ///     Test case <see cref="Task"/> to retrieve the existing user successfully with member id.
        /// </summary>
        [Fact]
        public async Task Verify_ExistingUser_Successfully_Using_MemberId()
        {
            //Arrange           
            using (var _context = new VaccineDbContext(Options))
            {
                //Act
                var result = await _ResetPasswordStore.VerifyUserWithIdGuid(test_guid);

                //Assert
                Assert.Equal(result.IdentificationNumber, id);
            }
        }

        /// <summary>
        ///     Test case <see cref="Task"/> to retrieve the existing user unsuccessfully.
        /// </summary>
        [Fact]
        public async Task Verify_ExistingUser_Unsuccessfully_Using_IdNumber()
        {
            // Arrange
            var id = "0001010000105";
            using (var _context = new VaccineDbContext(Options))
            {
                // Act
                var result = await _ResetPasswordStore.VerifyUserWithIdNumber(id);

                // Assert
                Assert.Null(result);
            };
        }

        /// <summary>
        ///     Test case <see cref="Task"/> to retrieve the existing user unsuccessfully.
        /// </summary>
        [Fact]
        public async Task Verify_ExistingUser_Unsuccessfully_Using_MemberId()
        {
            // Arrange            
            using (var _context = new VaccineDbContext(Options))
            {
                // Act
                var result = await _ResetPasswordStore.VerifyUserWithIdGuid(Guid.NewGuid());

                // Assert
                Assert.Null(result);
            };
        }

        /// <summary>
        ///      Test case <see cref="Task"/> to change a users password in successfully.
        /// </summary>
        [Fact]
        public async Task Change_Password_Successfully_Should_Return_Member_Object_If_User_Exists()
        {
            //Arrange
            var testId = test_guid;
            var testPassword = "TestPassword2";
            var testSalt = "somerandomSalt";
            using (var _context = new VaccineDbContext(Options))
            {
                //Act
                var result = await _ResetPasswordStore.ChangePassword(testId, testSalt, testPassword);

                //Assert
                Assert.Equal(result.Id, testId);
                Assert.Equal(result.Password, testPassword);
            };
        }
        
        /// <summary>
        ///      Test case <see cref="Task"/> returns a null for a when password change is unsuccessful.
        /// </summary>
        [Fact]
        public async Task Change_Password_Unsuccessfully_Should_Return_Null_Object_If_User_Does_Not_Exists()
        {
            //Arrange
            var testId = Guid.NewGuid();
            var testPassword = "TestPassword2";
            var testSalt = "somerandomSalt";

            using (var _context = new VaccineDbContext(Options))
            {                
                //Act
                var result = await _ResetPasswordStore.ChangePassword(testId, testSalt, testPassword);

                //Assert
                Assert.Null(result);
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