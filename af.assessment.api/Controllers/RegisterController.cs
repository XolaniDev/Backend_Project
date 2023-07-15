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

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using af.assessment.api.Data.Dtos;
using af.assessment.api.Data.Models;
using af.assessment.api.Services;
using af.assessment.api.Converters;
using Microsoft.Extensions.Logging;
using af.assessment.api.Utilities;

namespace af.assessment.api.Controllers
{
    /// <summary>
    ///     Provides api methods to query an OnBoarding api.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        /// <summary>
        ///     A <see cref="IRegisterService"/> representing the service to be called.
        /// </summary>
        private readonly IRegisterService _registerService;
        private readonly IRegisterDtoConverter _registerDtoConverter;
        /// <summary>
        ///     A <see cref="ILogger"/> for the <see cref="RegisterController"/> class representing the logger to be called.
        /// </summary>
        private readonly ILogger<RegisterController> _logger;
        /// <summary>
        ///     A <see cref="IPasswordHasher"/> representing the password hasher utility to be used.
        /// </summary>
        private readonly IPasswordHasher _passwordHasher;

        /// <summary>
        ///     Initialise a new instance of the <see cref="RegisterController"/> class.
        /// </summary>
        /// <param name="registerService">
        ///     A <see cref="IRegisterService"/> representing the register service to be called.
        /// </param>
        /// <param name="registerDtoConverter">
        ///     A <see cref="IRegisterDtoConverter"/> representing the converter to be used.
        /// </param>
        /// <param name="logger">
        ///     A <see cref="ILogger"/> representing the logger to be used.
        /// </param>
        /// <param name="passwordHasher">
        ///     A <see cref="IPasswordHasher"/> representing the password hashing utility class to be used.
        /// </param>
        public RegisterController(IRegisterService registerService, IRegisterDtoConverter registerDtoConverter, Microsoft.Extensions.Logging.ILogger<RegisterController> logger, Utilities.IPasswordHasher passwordHasher)
        {
            _registerService = registerService;
            _registerDtoConverter = registerDtoConverter;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        ///     Creates a new member and returns the action result of the operation.
        /// </summary>
        /// <param name="newRegisterDto">
        ///     A <see cref="RegisterDto"/> representing the details of the new member.
        /// </param>
        /// <returns>
        ///     A <see cref="bool"/> with the result of type <see cref="IActionResult"/> representing the result of the operation.
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse (201, type: typeof(Member), description: "Created a user")]
        [SwaggerResponse(400, type: typeof(Member), description: "Created a user was unsuccessful")]
        public async Task<IActionResult> RegisterMember([FromBody] RegisterDto newRegisterDto)
        {
            var passwordHashResult = _passwordHasher.GenerateHashPassword(newRegisterDto.Password);
            var newMember = _registerDtoConverter.ConvertToMember(newRegisterDto, passwordHashResult);

            if (newMember is null) return BadRequest();

            var result = await _registerService.RegisterMember(newMember);

            if (result)
            {
                _logger.LogInformation("Member has been successfully registered.");
                return Created(nameof(RegisterMember), true);
            }
            else
            {
                _logger.LogWarning("Member is unsuccessfully registered in a controller layer.");
                return BadRequest("ID already exists."); 
            }
            
        }
    }
}