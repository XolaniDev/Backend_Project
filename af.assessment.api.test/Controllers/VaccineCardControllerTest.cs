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
using af.assessment.api.Controllers;
using af.assessment.api.Data.Models;
using af.assessment.api.Services;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using af.assessment.api.Converters;
using af.assessment.api.Data.Dtos;
using System.Collections.Generic;

namespace af.assessment.api.test.Controllers
{
    /// <summary>
    ///     Provides tests for the OnboardingController.
    /// </summary>
    public class VaccineCardControllerTest
    {

        /// <summary>
        ///     Creating an instance of _controller for VaccineCardController.
        /// </summary>
        private readonly VaccineCardController _controller;

        /// <summary>
        ///     Instantiating a mock for the <see cref="IVaccineCardService"/>.
        /// </summary>
        private readonly Mock<IVaccineCardService> _serviceMock;

        /// <summary>
        ///     Creating an instance of Mock LoginDtoConverter for_converterMock.
        /// </summary>
        private readonly Mock<IVaccineCardDtoConverter> _converterMock;

        /// <summary>
        ///     Instantiating the mock for Logger and password hasher.
        /// </summary>
        private readonly Mock<ILogger<VaccineCardController>> _logger;

        /// <summary>
        ///     Creates instances of Mock for onboardingService and craetes an intsance of the OnboardingController.
        /// </summary>
        public VaccineCardControllerTest()
        {
            _serviceMock = new Mock<IVaccineCardService>();
            _logger = new Mock<ILogger<VaccineCardController>>();
            _converterMock = new Mock<IVaccineCardDtoConverter>();
            _controller = new VaccineCardController( _serviceMock.Object, _converterMock.Object, _logger.Object);
        }

        /// <summary>
        ///     Test case for retrieving a user successfully.
        /// </summary>
        /// <returns>
        ///     Existing User.
        /// </returns>
        [Fact]
        public async Task GetUsersFamilyMembers_ValidExistingGuid_ReturnsOk()
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

            var memberDto = new VaccineCardDto{ 
                FamilyMember = new List<FamilyMemberDto>(){
                    new FamilyMemberDto(){
                        FirstName = "Jonathon",
                        LastName = "Player",
                        IdentificationNumber = "2345678901234",
                        MemberType = Enums.MemberType.Son,
                        Vaccines = new List<VaccineDto>(){
                            new VaccineDto(){
                                Name = "Flu Vaccine",
                                Dose = 2,
                                ClinicName = "Flora",
                                VaccineStatus = true,
                                BatchNumber = 4000,
                                DateAdministered = new DateTime(),
                                Site = "Arm"
                            }
                        }
                    }
                }
            };

            _serviceMock.Setup(s => s.GetUsersFamilyMembers(id)).ReturnsAsync(member);
            _converterMock.Setup(c => c.ConvertToListOfFamilyMembers(member)).Returns(memberDto);

            //Act

            var actionResult = await _controller.GetUsersFamilyMembers(id) as OkObjectResult;
            var DTOResult = actionResult.Value as VaccineCardDto;

            //Assert

            Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsType<VaccineCardDto>(DTOResult);
        }

        /// <summary>
        ///     Test case to get a Non-Existing User.
        /// </summary>
        /// <returns>
        ///     Null when user does not exist.
        /// </returns>
        [Fact]
        public async Task GetUsersFamilyMembers_ValidNonExistingGuid_ReturnsNotFound() 
        {
            //Arrange
            Member member = null;
            _serviceMock.Setup(s => s.GetUsersFamilyMembers(It.IsAny<Guid>())).ReturnsAsync(member);

            //Act
            var actionResult = await _controller.GetUsersFamilyMembers(Guid.NewGuid()) as NotFoundResult;

            //Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        /// <summary>
        ///     Test case to retrieve an existing user.
        /// <returns> 
        ///    A BadRequest when GUID is empty.
        /// </returns>
        [Fact]
        public async Task GetUsersFamilyMembers_InvalidGuid_ReturnsBadRequest() 
        {
            //Arrange
            var id = Guid.Empty;

            //Act
            var actionResult = await _controller.GetUsersFamilyMembers(id) as BadRequestResult;

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }
    }
}
