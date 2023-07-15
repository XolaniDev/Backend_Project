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
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using af.assessment.api.Converters;
using af.assessment.api.Data.DTOs.UpdateProfile;
using af.assessment.api.Data.Dtos;
using af.assessment.api.Services;

namespace af.assessment.api.Controllers
{
    /// <summary>
    ///     This class contains a API action request response methods.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        ///      A <see cref="IUserService"/> representing the service to be called.
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        ///     A <see cref="ILogger"/> for the <see cref="UserController"/> class representing the logger to be called.
        /// </summary>
        private readonly ILogger<UserController> _logger;

        /// <summary>
        ///     A <see cref="IUserDtoConverter"/> representing the user data transfer object converter.
        /// </summary>
        private readonly IUserDtoConverter _userDtoConverter;

        /// <summary>
        ///      Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">
        ///        A <see cref="IUserService"/> representing the user service to be called.
        /// </param>
        /// <param name="logger">
        ///     A <see cref="ILogger"/> representing the logger to be used.
        /// </param>
        /// <param name="userDtoConverter">
        ///     A <see cref="IUserDtoConverter"/> representing the user data transfer object converter to be used.
        /// </param>
        public UserController(IUserService userService, ILogger<UserController> logger, IUserDtoConverter userDtoConverter)
        {
            _userService = userService;
            _logger = logger;
            _userDtoConverter = userDtoConverter;
        }

        /// <summary>
        ///      Retrieves and then returns the user's information successfully.
        /// </summary>
        /// <param name="memberId">
        ///     A <see cref="Guid"/> representing a member's guid.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> with a type <see cref="IActionResult"/> of instance <see cref="UserDto"/> representing the user dto of the user's personal information.
        /// </returns>
        [HttpGet("{memberId}")]
        [Produces("application/json")]
        [SwaggerOperation("get-profile-by-id")]
        [SwaggerResponse(200, type: typeof(UserDto), description: "Profile is successfully retrieved")]
        [SwaggerResponse(400, description: "Profile not found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> GetProfileById([FromRoute] Guid memberId)
        {
            if(memberId == Guid.Empty) return BadRequest();            

            var result = await _userService.GetProfileById(memberId);

            if (result == null) return NotFound();            

            var dto = _userDtoConverter.ConvertToUserDto(result);
            return Ok(dto);
        }

        /// <summary>
        ///     Updates the member personal details and then returns the updated member.
        /// </summary>
        /// <param name="memberId">
        ///     A <see cref="Guid"/> representing the member's unique identifier to query the database.
        /// </param>
        /// <param name="dto">
        ///     A <see cref="PersonalDetailsDto"/> representing the values to be updated.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> with a type <see cref="IActionResult"/> of instance <see cref="UserDto"/> representing the user dto  of the user's personal information.
        /// </returns>
        [HttpPost("personalDetails/{memberId}")]
        [Produces("application/json")]
        [SwaggerOperation("update-profile-personal-details")]
        [SwaggerResponse(200, description: "Ok")]
        [SwaggerResponse(400, description: "Bad Request")]
        [SwaggerResponse(404, description: "Not Found")]
        public async Task<IActionResult> UpdateProfilePersonalDetails([FromRoute]Guid memberId, [FromBody] PersonalDetailsDto dto)
        {
            if(memberId == Guid.Empty) return BadRequest();
            
            var member = await _userService.GetProfileById(memberId);

            if(member == null) return NotFound();

            member.Email = dto.Email;
            member.Name = dto.Name;
            member.IdentificationNumber = dto.IdentificationNumber;
            member.MobileNumber = dto.MobileNumber;

            await _userService.Save();

            var result = await _userService.GetProfileById(member.Id);
            var resultDto = _userDtoConverter.ConvertToUserDto(result);

            return Ok(resultDto);
        }

        /// <summary>
        ///     Updates the member location details and then returns the updated member.
        /// </summary>
        /// <param name="memberId">
        ///     A <see cref="Guid"/> representing the member's unique identifier to query the database.
        /// </param>
        /// <param name="dto">
        ///     A <see cref="LocationDetailsDto"/> representing the values to be updated.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> with a type <see cref="IActionResult"/> of instance <see cref="UserDto"/> representing the user.
        /// </returns>
        [HttpPost("locationDetails/{memberId}")]
        [Produces("application/json")]
        [SwaggerOperation("update-profile-location-details")]
        [SwaggerResponse(200, description: "Ok")]
        [SwaggerResponse(400, description: "Bad Request")]
        [SwaggerResponse(404, description: "Not Found")]
        public async Task<IActionResult> UpdateProfileLocationDetails([FromRoute] Guid memberId, [FromBody] LocationDetailsDto dto)
        {
            if (memberId == Guid.Empty) return BadRequest();

            var location = await _userService.GetLocationByMemberId(memberId);

            if (location == null) return NotFound();

            location.City = dto.City;
            location.StreetName = dto.StreetName;
            location.PostalCode = dto.PostalCode;

            await _userService.Save();

            var result = await _userService.GetProfileById(memberId);
            var resultDto = _userDtoConverter.ConvertToUserDto(result);

            return Ok(resultDto);
        }

        /// <summary>
        ///      Updates the member medical aid details and then returns the updated member.
        /// </summary>
        /// <param name="memberId">
        ///     A <see cref="Guid"/> representing the member's unique identifier to query the database.
        /// </param>
        /// <param name="dto">
        ///     A <see cref="MedicalAidDetailsDto"/> representing the values to be updated.
        /// </param>
        /// <returns>
        ///     A <see cref="Task"/> with a type <see cref="IActionResult"/> of instance <see cref="UserDto"/> representing the user.
        /// </returns>
        [HttpPost("medicalAidDetails/{memberId}")]
        [Produces("application/json")]
        [SwaggerOperation("update-profile-medical-aid-details")]
        [SwaggerResponse(200, description: "Ok")]
        [SwaggerResponse(400, description: "Bad Request")]
        [SwaggerResponse(404, description: "Not Found")]
        public async Task<IActionResult> UpdateProfileMedicalAidDetails([FromRoute] Guid memberId, [FromBody] MedicalAidDetailsDto dto)
        {
            if (memberId == Guid.Empty) return BadRequest();

            var medicalDetails = await _userService.GetMedicalDetailsByMemberId(memberId);

            if (medicalDetails == null) return NotFound();

            medicalDetails.MainMemberName = dto.MainMemberName;
            medicalDetails.MainMemberNumber = dto.MainMemberNumber;
            medicalDetails.MedicalAidName = dto.MedicalAidName;
            medicalDetails.MedicalAidNumber = dto.MedicalAidNumber;

            await _userService.Save();

            var result = await _userService.GetProfileById(memberId);
            var resultDto = _userDtoConverter.ConvertToUserDto(result);

            return Ok(resultDto);            
        }
    }
}