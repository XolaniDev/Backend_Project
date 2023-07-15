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
using Moq;
using Xunit;
using af.assessment.api.Data.Models;
using af.assessment.api.Services;
using af.assessment.api.Stores;

namespace af.assessment.api.test.Services
{
    /// <summary>
    ///     Provide unit test to test <see cref="UserService"/> class.
    /// </summary>
    public class VaccineCardServiceTest
    {
        /// <summary>
        ///       A <see cref="VaccineCardService"/> representing the service that contains the method to be tested.
        /// </summary>
        private readonly VaccineCardService _vaccineCardService;

        /// <summary>
        ///      A <see cref="Mock"/> implementation of <see cref="IVaccineCardService"/> representing the store to be mocked.
        /// </summary>
        private readonly Mock<IFamilyMemberStore> _storeMock = new Mock<IFamilyMemberStore>();

        /// <summary>
        ///      Initialise an instance of the <see cref="VaccineCardServiceTest"/> class.
        /// </summary>
        public VaccineCardServiceTest()
        {
            _vaccineCardService = new VaccineCardService(_storeMock.Object);
        }

        /// <summary>
        ///      Test that successfully gets an existing user and its family members.
        /// </summary>
        /// <returns> 
        ///      A member object.
        /// </returns>
        [Fact]
        public async Task GetUsersFamilyMembers_ValidExistingGuid_ReturnsFamilyMembers()
        {
            //Arrange
            var member = new Member
            {
                Id = Guid.Parse("56cd15b1-af8a-4505-b795-12707bb55543"),
                Name = "Gary Player",
                IdentificationNumber = "1234567890123",
                MobileNumber = "0896570741",
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
                            new Vaccine(){
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

            _storeMock.Setup(s => s.GetUsersFamilyMembers(It.IsAny<Guid>())).ReturnsAsync(member);
            //Act

            var result = await _vaccineCardService.GetUsersFamilyMembers(It.IsAny<Guid>());
            //Assert

            Assert.Equal(member.Id, result.Id);
            Assert.Equal(member.Name, result.Name);
            Assert.Equal(member.Email, result.Email);
            Assert.Equal(member.IdentificationNumber, result.IdentificationNumber);
            Assert.Equal(member.MobileNumber, result.MobileNumber);
            Assert.Equal(member.Password, result.Password);
            Assert.Equal(member.FamilyMember.Count, result.FamilyMember.Count);
        }

        /// <summary>
        ///      Test that unsuccessfully gets an existing user and its family members.
        /// </summary>
        /// <returns> 
        ///      A member object.
        /// </returns>
        [Fact]
        public async Task GetUsersFamilyMembers_ValidNonExistingGuid_ReturnsNull()
        {
            //Arrange
            Member nullMember = null;

            _storeMock.Setup(s => s.GetUsersFamilyMembers(It.IsAny<Guid>())).ReturnsAsync(nullMember);
            //Act

            var result = await _vaccineCardService.GetUsersFamilyMembers(It.IsAny<Guid>());
            //Assert

            Assert.Null(result);
        }
    }
}