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

namespace af.assessment.api.Converters
{
    /// <inheritdoc/>
    public class RegisterDtoConverter : IRegisterDtoConverter
    {
        /// <summary>
        ///     A <see cref="ILogger"/> for the <see cref="RegisterDtoConverter"/> class representing the logger.
        /// </summary>
        private readonly ILogger<RegisterDtoConverter> _logger;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RegisterDtoConverter"/> class with the logger that will be used.
        /// </summary>
        /// <param name="logger">
        ///     A <see cref="ILogger"/> for <see cref="RegisterDtoConverter"/> representing the logger.
        /// </param>
        public RegisterDtoConverter(ILogger<RegisterDtoConverter> logger)
        {
            _logger = logger;
        }

        ///  <inheritdoc/>
        public Member ConvertToMember(RegisterDto registerDto, PasswordHashResultDto passwordHashResult)
        {
            Member member = new Member
            {
                Name = registerDto.Name,
                Email = registerDto.Email is null ? "" : registerDto.Email,
                IdentificationNumber = registerDto.IdentificationNumber,
                MobileNumber = registerDto.MobileNumber,
                Password = passwordHashResult.HashResult,
                Salt = passwordHashResult.Salt,
                OtpPreference = registerDto.OtpPreference
            };

            _logger.LogInformation("successfully convert to userDto");

            return member;
        }

        
    }
}
