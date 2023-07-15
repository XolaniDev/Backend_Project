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

using af.assessment.api.Converters;
using af.assessment.api.Data.Dtos;
using af.assessment.api.Data.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace af.assessment.api.test.Converters
{
    /// <summary>
    ///     Provides unit test to test the <see cref="VaccineCardDtoConverter"/> class.
    /// </summary>
    public class VaccineCardDtoConverterTest
    {
        /// <summary>
        ///   A <see cref="VaccineCardDtoConverter"/> representing the converter class that contains the methods to be tested.
        /// </summary>          
        private readonly VaccineCardDtoConverter _converter;

        /// <summary>
        ///     Initialise the new instance of the <see cref="VaccineCardDtoConverterTest"/> class.
        /// </summary>
        public VaccineCardDtoConverterTest()
        {
            _converter = new VaccineCardDtoConverter();
        }

        /// <summary>
        ///     Test case method to convert from Member To VaccineCardDto successfully.
        /// </summary>
        [Fact]
        public void ConvertToListOfFamilyMembers_ValidMemberInput_ReturnsVaccineCardDto()
        {
            //Arrange 
            var id = Guid.NewGuid();
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

            //Act
            var result = _converter.ConvertToListOfFamilyMembers(member);

            //Assert
            Assert.IsType<VaccineCardDto>(result);
            Assert.Equal(member.FamilyMember[0].FirstName,result.FamilyMember[0].FirstName);
            Assert.Equal(member.FamilyMember[0].LastName,result.FamilyMember[0].LastName);
            Assert.Equal(member.FamilyMember[0].IdentificationNumber,result.FamilyMember[0].IdentificationNumber);
            Assert.Equal(member.FamilyMember[0].Relationship,result.FamilyMember[0].MemberType);
            Assert.IsType<List<VaccineDto>>(result.FamilyMember[0].Vaccines);
            Assert.Equal(member.FamilyMember[0].Vaccines[0].Name,result.FamilyMember[0].Vaccines[0].Name);
            Assert.Equal(member.FamilyMember[0].Vaccines[0].Dose,result.FamilyMember[0].Vaccines[0].Dose);
            Assert.Equal(member.FamilyMember[0].Vaccines[0].Clinic.ClinicName,result.FamilyMember[0].Vaccines[0].ClinicName);
            Assert.Equal(member.FamilyMember[0].Vaccines[0].VaccineStatus,result.FamilyMember[0].Vaccines[0].VaccineStatus);
            Assert.Equal(member.FamilyMember[0].Vaccines[0].BatchNumber,result.FamilyMember[0].Vaccines[0].BatchNumber);
            Assert.Equal(member.FamilyMember[0].Vaccines[0].AdministeredDate,result.FamilyMember[0].Vaccines[0].DateAdministered);
            Assert.Equal(member.FamilyMember[0].Vaccines[0].Site,result.FamilyMember[0].Vaccines[0].Site);
        }

        /// <summary>
        ///     Test case method to convert from Member To VaccineCardDto unsuccessfully.
        /// </summary>
        [Fact]
        public void ConvertToListOfFamilyMembers_InvalidMemberInput_ReturnsNull()
        {
            //Arrange
            Member member = null;

            //Act
            var result = _converter.ConvertToListOfFamilyMembers(member);

            //Assert
            Assert.Null(result);

        }
    }
}
