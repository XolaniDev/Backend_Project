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

using af.assessment.api.Data.Dtos;
using af.assessment.api.Data.Models;
using Microsoft.Extensions.Logging;
using System;

namespace af.assessment.api.Converters
{
    /// <inheritdoc />
    public class UserDtoConverter : IUserDtoConverter
    {
        /// <summary>
        ///     A <see cref="ILogger"/> for the <see cref="UserDtoConverter"/> class representing the logger.
        /// </summary>
        private readonly ILogger<UserDtoConverter> _logger;
        /// <summary>
        ///      Initializes a instance of the <see cref="UserDtoConverter"/> class with the logger that will be used.
        /// </summary>
        /// <param name="logger">
        ///     A <see cref="ILogger"/> for <see cref="UserDtoConverter"/> representing the logger.
        /// </param>
        public UserDtoConverter(ILogger<UserDtoConverter> logger)
        {
            _logger = logger;
        }

        ///  <inheritdoc/>
        public UserDto ConvertToUserDto(Member member)
        {
            if (member is null)
            {
                _logger.LogInformation("Member paramater is null");
                return null;
            }

            var userDto = new UserDto
            {
                Name = member.Name,
                Email = member.Email is null ? "" : member.Email,
                IdentificationNumber = member.IdentificationNumber,
                MobileNumber = member.MobileNumber,
                ProfilePictureUrl = member.ProfilePictureUrl,

                MedicalAidName = member.MedicalDetails != null ? member.MedicalDetails.MedicalAidName : null,
                MedicalAidNumber = member.MedicalDetails != null ? member.MedicalDetails.MedicalAidNumber : null,
                MainMemberName = member.MedicalDetails != null ? member.MedicalDetails.MainMemberName : null,
                MainMemberNumber = member.MedicalDetails != null ? member.MedicalDetails.MainMemberNumber : null,

                StreetName = member.Locations != null ? member.Locations.StreetName : null,
                PostalCode = member.Locations != null ? member.Locations.PostalCode : -1,
                City = member.Locations != null ? member.Locations.City : null,
            };

            _logger.LogInformation("successfully convert to userDto");

            return userDto;
        }

        ///  <inheritdoc/>
        public Member ConvertToMember(Guid memberId, UserDto dto)
        {
            if (memberId == Guid.Empty || memberId == null)
            {
                _logger.LogInformation("Member id paramater is invalid");
                return null;
            }

            if (dto is null)
            {
                _logger.LogInformation("Dto paramater is null");
                return null;
            }

            var member = new Member
            {
                Id = memberId,
                Name = dto.Name,
                IdentificationNumber = dto.IdentificationNumber,
                Email = dto.Email,
                MobileNumber = dto.MobileNumber,
                MedicalDetails = string.IsNullOrEmpty(dto.MedicalAidName) || string.IsNullOrEmpty(dto.MedicalAidNumber) || string.IsNullOrEmpty(dto.MainMemberName) || string.IsNullOrEmpty(dto.MainMemberNumber) ? null : new MedicalDetails()
                {
                    MedicalAidName = dto.MedicalAidName,
                    MedicalAidNumber = dto.MedicalAidNumber,
                    MainMemberName = dto.MainMemberName,
                    MainMemberNumber = dto.MainMemberNumber
                },
                Locations = string.IsNullOrEmpty(dto.StreetName) || string.IsNullOrEmpty(dto.City) || dto.PostalCode < 0 ? null : new Location()
                {
                    StreetName = dto.StreetName,
                    City = dto.City,
                    PostalCode = dto.PostalCode,
                }
            };

            return member;
        }
    }
}