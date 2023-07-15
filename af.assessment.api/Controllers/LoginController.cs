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
using af.assessment.api.Converters;
using af.assessment.api.Data.Dtos;
using af.assessment.api.Services;
using af.assessment.api.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace af.assessment.api.Controllers
{
    /// <summary>
    ///     This class contains the API action request response methods.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        /// <summary>
        ///     A <see cref="ILoginService"/> representing the service to be called.
        /// </summary>
        private readonly ILoginService _LoginService;
        /// <summary>
        ///     A <see cref="ILogger"/> for the <see cref="LoginController"/> class representing the logger to be called.
        /// </summary>
        private readonly ILogger<LoginController> _logger;

        private readonly IUserToken _userToken;
        /// <summary>
        ///     A <see cref="IPasswordHasher"/> representing the password hasher utility to be used.
        /// </summary>
        private readonly IPasswordHasher _passwordHasher;

        /// <summary>
        ///      Initialise a new instance of the <see cref="LoginController"/> class.
        /// </summary>
        /// <param name="loginService">
        ///       A <see cref="ILoginService"/> representing the login service to be called.
        /// </param>
        /// <param name="logger">
        ///     A <see cref="ILogger"/> representing the logger to be used.
        /// </param>
        /// <param name="userToken">
        ///     A <see cref="IUserToken"/> representing the token to be used.
        /// </param>
        /// <param name="passwordHasher">
        ///     A <see cref="IPasswordHasher"/> representing the password hashing utility class to be used.
        /// </param>
        public LoginController(ILoginService loginService, ILogger<LoginController> logger, IUserToken userToken, IPasswordHasher passwordHasher)
        {
            _LoginService = loginService;
            _logger = logger;
            _userToken = userToken;
            _passwordHasher = passwordHasher;
        }

        /// <summary>
        ///     Login request that will log in a user if the user exists.
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns>
        ///     The user is logged in.
        /// </returns>
        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation("Login-Member")]
        [SwaggerResponse(200, type: typeof(LoginDto), description: "Member logged in")]
        [SwaggerResponse(404, description: "Member not found in database")]
        [SwaggerResponse(400, description: "Invaild Member Id entered")]
        [SwaggerResponse(401, description: "User unauthorised to login")]
        public async Task<IActionResult> LogUserIn(LoginDto loginDto)
        {

            if (loginDto.IdentificationNumber == null || loginDto.Password == null || loginDto.IdentificationNumber == string.Empty || loginDto.Password == string.Empty)
            {
                return BadRequest();
            }

            string idNumber = loginDto.IdentificationNumber;
            string password = loginDto.Password;

            var memberVerificationModel = await _LoginService.VerifyUser(idNumber);

            if (memberVerificationModel == null)
            {
                return NotFound();
            }

            string passwordSalt = memberVerificationModel.Salt;
            var saltDTO = _passwordHasher.GenerateHashPasswordWithSalt(password, passwordSalt);

            var memberModel = await _LoginService.LogUserIn(idNumber, saltDTO.HashResult.ToString());

            if (memberModel == null)
            {
                return Unauthorized("Invalid login Details");
            }
            else
            {
                return Ok(_userToken.BuildUserToken(memberModel));
            }
        }
     
    }
}