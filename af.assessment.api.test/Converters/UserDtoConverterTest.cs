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
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System;

namespace af.assessment.api.test.Converters
{
    /// <summary>
    ///     Provides unit test to test the <see cref="UserDtoConverterTest"/> class.
    /// </summary>
    public class UserDtoConverterTest
    {
        /// <summary>
        ///      A <see cref="UserDtoConverter"/> representing the converter class that contains the methods to be tested.
        /// </summary>
        private readonly UserDtoConverter _userDtoConverter;
        
        /// <summary>
        ///      A <see cref="Mock"/> implementation of <see cref="ILogger"/> representing the register converter to be mocked.
        /// </summary>
        private readonly Mock<ILogger<UserDtoConverter>> _loggerMock = new Mock<ILogger<UserDtoConverter>>();
        
        /// <summary>
        ///     Initialise the new instance of the <see cref="UserDtoConverterTest"/> class.
        /// </summary>
        public UserDtoConverterTest()
        {
            _userDtoConverter = new UserDtoConverter(_loggerMock.Object);
        }

        /// <summary>
        ///     Tests that ConvertToUserDto returns dto when member is valid.
        /// </summary>
        [Fact]
        public void ConvertToUserDto_Should_Convert_From_Member_To_UserDto()
        {
            //Arrange 
            var member = new Member()
            {
                Name = "testName",
                Email = "test@email.com",
                IdentificationNumber = "1000000000000",
                MobileNumber = "0",

                MedicalDetails = new MedicalDetails()
                {
                    MedicalAidName = "",
                    MedicalAidNumber = "",
                    MainMemberName = "",
                    MainMemberNumber = null,

                },

                Locations = new Location()
                {
                    StreetName = "",
                    PostalCode = 0,
                    City = "",
                },
            };
            var member1 = new Member()
            {
                Name = "John Smith",
                Email = null,
                IdentificationNumber = "0001010000105",
                MobileNumber = "(012)-345-6789",
                ProfilePictureUrl = "https://dcvcstorage.blob.core.windows.net/profilepics/ben.jpg",
                MedicalDetails = null,
                Locations = null,
            };

            //Act
            var result = _userDtoConverter.ConvertToUserDto(member);
            var result1 = _userDtoConverter.ConvertToUserDto(member1);

            //Assert
            Assert.IsType<UserDto>(result);
            Assert.Equal(member.Name, result.Name);
            Assert.Equal(member.IdentificationNumber, result.IdentificationNumber);
            Assert.Equal(member.MobileNumber, result.MobileNumber);
            Assert.Equal(member.Email, result.Email);
            Assert.Equal(member.ProfilePictureUrl, result.ProfilePictureUrl);
            Assert.Equal(member.MedicalDetails.MedicalAidName, result.MedicalAidName);
            Assert.Equal(member.MedicalDetails.MedicalAidNumber, result.MedicalAidNumber);
            Assert.Equal(member.MedicalDetails.MainMemberName, result.MainMemberName);
            Assert.Equal(member.MedicalDetails.MainMemberNumber, result.MainMemberNumber);
            Assert.Equal(member.Locations.StreetName, result.StreetName);
            Assert.Equal(member.Locations.PostalCode, result.PostalCode);
            Assert.Equal(member.Locations.City, result.City);

            Assert.IsType<UserDto>(result1);
            Assert.Equal(member1.Name, result1.Name);
            Assert.Equal(member1.IdentificationNumber, result1.IdentificationNumber);
            Assert.Equal(member1.MobileNumber, result1.MobileNumber);
            Assert.Equal("", result1.Email);
            Assert.Null(result1.MedicalAidName);
            Assert.Null(result1.MedicalAidNumber);
            Assert.Null(result1.MainMemberName);
            Assert.Null(result1.MainMemberNumber);
            Assert.Null(result1.StreetName);
            Assert.Equal(-1, result1.PostalCode);
            Assert.Null(result1.City);


        }

        /// <summary>
        ///     Tests that ConvertToUserDto returns null when member is null.
        /// </summary>
        [Fact]
        public void ConvertToUserDto_Should_Unsuccessfully_Convert_From_Member_To_UserDto_And_Return_Null()
        {
            //Arrange
            Member member = null;

            //Act
            var result = _userDtoConverter.ConvertToUserDto(member);

            //Assert
            Assert.Null(result);
        }

        /// <summary>
        ///     Tests that <see cref="UserDtoConverter.ConvertToMember(Guid, UserDto)"> returns null when dto is empty.
        /// </summary>
        [Fact]
        public void ConvertToMember_EmptyNullParameters_ReturnsNull()
        {
            // arrange
            var userDto = new UserDto();

            // act
            var result = _userDtoConverter.ConvertToMember(Guid.Empty, userDto);
            var result2 = _userDtoConverter.ConvertToMember(Guid.NewGuid(), null);

            // assert
            Assert.Null(result);
            Assert.Null(result2);
        }

        /// <summary>
        ///      Tests that <see cref="UserDtoConverter.ConvertToMember(Guid, UserDto)"> returns null when dto is empty.
        /// </summary>
        /// <param name="name">
        ///      A <see cref="String"/> representing the name value.
        /// </param>
        /// <param name="email">
        ///      A <see cref="String"/> representing the email value.
        /// </param>
        /// <param name="id">
        ///      A <see cref="String"/> representing the Id value.
        /// </param>
        /// <param name="mobileNumber">
        ///     A <see cref="String"/> representing the mobile number value.
        /// </param>
        /// <param name="url">
        ///     A <see cref="String"/> representing profile picture url.
        /// </param>
        /// <param name="medicalAidName">
        ///     A <see cref="String"/> representing the medical aid name value.
        /// </param>
        /// <param name="medicalAidNumber">
        ///     A <see cref="String"/> representing the medical aid number value.
        /// </param>
        /// <param name="mainMemberName">
        ///     A <see cref="String"/> representing the medical aid main member name value.
        /// </param>
        /// <param name="mainMemberNumber">
        ///     A <see cref="String"/> representing the medical aid main member number value.
        /// </param>
        /// <param name="street">
        ///     A <see cref="String"/> representing the street name.
        /// </param>
        /// <param name="city">
        ///     A <see cref="String"/> representing the city name.
        /// </param>
        /// <param name="code">
        ///     A <see cref="int"/> representing the city code value.
        /// </param>
        /// <param name="medDetails">
        ///     A <see cref="String"/> representing the medical details values.
        /// </param>
        /// <param name="location">
        ///     A <see cref="bool"/> representing location value.
        /// </param>
        [Theory]
        [InlineData("John Smith", "email@mail.com", "0001010000105", "(012)-345-6789", "https://randomprofilepictureurl.com", "", "", "", "", "", "", -1, false, false)]
        [InlineData("John Smith", "email@mail.com", "0001010000105", "(012)-345-6789", "https://randomprofilepictureurl.com", "Middle Earth Life", "", "", "", "Bag End, Underhill", "", -1, false, false)]
        [InlineData("John Smith", "email@mail.com", "0001010000105", "(012)-345-6789", "https://randomprofilepictureurl.com", "Middle Earth Life", "123467890", "", "", "Bag End, Underhill", "Hobbiton", -1, false, false)]
        [InlineData("John Smith", "email@mail.com", "0001010000105", "(012)-345-6789", "https://randomprofilepictureurl.com", "Middle Earth Life", "123467890", "Frodo Baggins", "", "Bag End, Underhill", "Hobbiton", 13, false, true)]
        [InlineData("John Smith", "email@mail.com", "0001010000105", "(012)-345-6789", "https://randomprofilepictureurl.com", "Middle Earth Life", "123467890", "Frodo Baggins", "(098)-765-4321", "Bag End, Underhill", "Hobbiton", 13, true, true)]
        public void ConvertToMember_ValidParamaters_ReturnsConvertedMember(String name, String email, String id, String mobileNumber, String url, String medicalAidName, String medicalAidNumber, String mainMemberName, String mainMemberNumber, String street, String city, int code, bool medDetails, bool location)
        {
            // arrange
            var memberId = Guid.NewGuid();
            var userDto = new UserDto()
            {
                Name = name,
                Email = email,
                IdentificationNumber = id,
                MobileNumber = mobileNumber,
                ProfilePictureUrl = url,
                MedicalAidName = medicalAidName,
                MedicalAidNumber = medicalAidNumber,
                MainMemberName = mainMemberName,
                MainMemberNumber = mainMemberNumber,
                StreetName = street,
                City = city,
                PostalCode = code
            };
            var member = new Member()
            {
                Id = memberId,
                Name = name,
                Email = email,
                IdentificationNumber = id,
                MobileNumber = mobileNumber,
                ProfilePictureUrl = url,
                MedicalDetails = medDetails ? new MedicalDetails()
                {
                    MedicalAidName = medicalAidName,
                    MedicalAidNumber = medicalAidNumber,
                    MainMemberName = mainMemberName,
                    MainMemberNumber = mainMemberNumber,
                } : null,
                Locations = location ? new Location()
                {
                    StreetName = street,
                    City = city,
                    PostalCode = code
                } : null
            };

            // act
            var result = _userDtoConverter.ConvertToMember(memberId, userDto);

            // assert
            Assert.IsType<Member>(result);
            Assert.Equal(result.Id, member.Id);
            Assert.Equal(result.Name, member.Name);
            Assert.Equal(result.Email, member.Email);
            Assert.Equal(result.MobileNumber, member.MobileNumber);
            Assert.Equal(result.IdentificationNumber, member.IdentificationNumber);
            if (medDetails) {
                Assert.Equal(result.MedicalDetails.MainMemberName, member.MedicalDetails.MainMemberName);
                Assert.Equal(result.MedicalDetails.MainMemberNumber, member.MedicalDetails.MainMemberNumber);
                Assert.Equal(result.MedicalDetails.MedicalAidName, member.MedicalDetails.MedicalAidName);
                Assert.Equal(result.MedicalDetails.MedicalAidNumber, member.MedicalDetails.MedicalAidNumber);
            } else {
                Assert.Null(result.MedicalDetails);
            }

            if (location) {
                Assert.Equal(result.Locations.City, member.Locations.City);
                Assert.Equal(result.Locations.StreetName, member.Locations.StreetName);
                Assert.Equal(result.Locations.PostalCode, member.Locations.PostalCode);
            } else {
                Assert.Null(result.Locations);
            }
        }
    }
}
