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
using af.assessment.api.Converters;
using af.assessment.api.Data.Dtos;
using af.assessment.api.Data.Models;
using af.assessment.api.Enums;
using af.assessment.api.Services;
using af.assessment.api.Token;
using af.assessment.api.Utilities;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
namespace af.assessment.api.Controllers
{
    /// <summary>
    ///     This class contains the reset password API action request response methods.
    /// </summary>
    
    [ApiController]
    [Route("api/[controller]")]
    public class ResetPasswordController : ControllerBase
    {
        /// <summary>
        ///     A <see cref="IResetPasswordService"/> representing the service to be called.
        /// </summary>
        private readonly IResetPasswordService _resetPasswordService;
        
        /// <summary>
        ///     A <see cref="IUserToken"/> representing the token utility to be used.
        /// </summary>
        private readonly IUserToken _userToken;
        
        /// <summary>
        ///     A <see cref="IPasswordHasher"/> representing the password hasher utility to be used.
        /// </summary>
        private readonly IPasswordHasher _passwordHasher;
        
        /// <summary>
        ///     A <see cref="ILogger"/> for the <see cref="ResetPasswordController"/> class representing the logger to be called.
        /// </summary>
        private readonly ILogger<ResetPasswordController> _logger;
        
        /// <summary>
        ///     Initialise a new instance of the <see cref="ResetPasswordController"/> class.
        /// </summary>
        /// <param name="resetPasswordService">
        ///     A <see cref="IResetPasswordService"/> representing the resetPassword service to be called.
        /// </param>
        /// <param name="userToken">
        ///     A <see cref="IUserToken"/> representing the UserToken utilities to be called.
        /// </param>
        /// <param name="passwordHasher">
        ///     A <see cref="IPasswordHasher"/> representing the PasswordHasher utilities to be called.
        /// </param>
        /// <param name="logger">
        ///     A <see cref="ILogger"/> representing the logger to be used.
        /// </param>
        public ResetPasswordController(IResetPasswordService resetPasswordService, IUserToken userToken, IPasswordHasher passwordHasher, ILogger<ResetPasswordController> logger)
        {
            _resetPasswordService = resetPasswordService;
            _userToken = userToken;
            _passwordHasher = passwordHasher;
            _logger = logger;
        }
        
        /// <summary>
        ///     The following controller resets the password for a registered user.
        /// </summary>
        /// <param name="resetPasswordDto">
        ///     A <see cref="resetPasswordDto"/> that represents the resetPassword Dto.
        /// </param>
        /// <returns>
        ///      A <see cref="HttpStatusCode"/> that returns an Ok response.
        /// </returns>
        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation("Reset-Password")]
        [SwaggerResponse(200, type: typeof(ResetPasswordDto), description: "Password reset")]
        [SwaggerResponse(404, description: "Member not found in database")]       
        public async Task<IActionResult> ChangePassword(ResetPasswordDto resetPasswordDto)
        {
            var member = await _resetPasswordService.VerifyUserwithIdGuid(resetPasswordDto.MemberId);
            if (member == null) return NotFound();

            var passwordSalt = member.Salt;
            var saltDTO = _passwordHasher.GenerateHashPasswordWithSalt(resetPasswordDto.Password, passwordSalt);
            var model = await _resetPasswordService.
                ChangePassword(resetPasswordDto.MemberId, saltDTO.Salt.ToString(), saltDTO.HashResult.ToString());

            return Ok();         
        }

        /// <summary>
        ///     Verifies that a member exists.
        /// </summary>
        /// <param name="idNumber">
        ///     A <see cref="String"/> that represents the id number of the member.
        /// </param>
        /// <returns>
        ///     A <see cref="Guid"/> that represents the memberId of the member.
        /// </returns>
        [HttpGet("{idNumber}")]
        [Produces("application/json")]
        [SwaggerOperation("Reset-Password")]
        [SwaggerResponse(200, type: typeof(Guid), description: "Verify User")]
        [SwaggerResponse(404, description: "Member not found in database")]
        public async Task<IActionResult> VerifyUserExists([FromRoute] String idNumber)
        {
            var member = await _resetPasswordService.VerifyUserWithIdNumber(idNumber);
            if (member == null) return NotFound();

            return Ok(member.Id);
        }
    }

    
}
