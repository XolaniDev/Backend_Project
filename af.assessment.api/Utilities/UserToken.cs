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
using af.assessment.api.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace af.assessment.api.Token
{
    /// <inheritdoc/>
    public class UserToken : IUserToken
    {
        /// <summary>
        ///      An <see cref="IConfiguration"/> representing the key value application configuration properties.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserToken"/> class with the configuration properties to be used.
        /// </summary>
        /// <param name="configuration"></param>
        public UserToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc/>
        public UserTokenDto BuildUserToken(Member memberModel )
        { 
            if ( memberModel is null || memberModel.IdentificationNumber == string.Empty || memberModel.MobileNumber == string.Empty || memberModel.Name == string.Empty || memberModel.Password == string.Empty)
            {
                return null;
               
            }
            else {

                var configKey = _configuration["jwt:key"];

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configKey));

                var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenExpiration = DateTime.UtcNow.AddMinutes(20);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, memberModel.MobileNumber),
                };

                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: tokenExpiration,
                    signingCredentials: credential
                    );

                var userTokenDto = new UserTokenDto()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpirationTime = tokenExpiration,
                    Name = memberModel.Name,
                    Guid = memberModel.Id,
                    ProfilePictureUrl = memberModel.ProfilePictureUrl
                };

                return userTokenDto;
            }

        }
    }
}
