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
using af.assessment.api.Converters;
using af.assessment.api.Data.Dtos;
using af.assessment.api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace af.assessment.api.Controllers
{
    /// <summary>
    ///     This class contains the API action request response methods.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class VaccineCardController : ControllerBase
    {
        /// <summary>
        ///     A <see cref="IVaccineCardService"/> representing the service to be called.
        /// </summary>
        private readonly IVaccineCardService _VaccineCardService;

        /// <summary>
        ///     Creating an instance of _converter for <see cref="IVaccineCardDtoConverter"/> interface.
        /// </summary>
        private readonly IVaccineCardDtoConverter _converter;

        /// <summary>
        ///     A <see cref="ILogger"/> for the <see cref="VaccineCardController"/> class representing the logger to be called.
        /// </summary>
        private readonly ILogger<VaccineCardController> _logger;

        /// <summary>
        ///     Initialise a new instance of the <see cref="VaccineCardController"/> class.
        /// </summary>
        /// <param name="vaccineCardService">
        ///     A <see cref="IVaccineCardService"/> representing the service to be called. 
        /// </param>
        /// <param name="converter">
        ///     Creating an instance of _converter for <see cref="IVaccineCardDtoConverter"/> interface.
        /// </param>
        /// <param name="logger">
        ///     A <see cref="ILogger"/> for the <see cref="VaccineCardController"/> class representing the logger to be called.
        /// </param>
        public VaccineCardController(IVaccineCardService vaccineCardService, IVaccineCardDtoConverter converter, ILogger<VaccineCardController> logger)
        {
            _VaccineCardService = vaccineCardService;
            _converter = converter;
            _logger = logger;
        }

        /// <summary>
        ///     Retrieves a user's family members successfully.
        /// </summary>
        /// <param name="Id">
        ///     A <see cref="Guid"/> representing a member's unique identifier.
        /// </param>
        /// <returns>
        ///     An <see cref="IActionResult"/> representing whether the request is successfully made or not.
        /// </returns>
        [HttpGet("{Id}", Name = "GetFamilyMembers")]
        [Produces("application/json")]
        [SwaggerOperation("get-FamilyMember")]
        [SwaggerResponse(200, type: typeof(VaccineCardDto), description: "Family Members retrieved")]
        [SwaggerResponse(404, description: "Member not found in database")]
        [SwaggerResponse(400, description: "Invalid Member Id entered")]
        public async Task<IActionResult> GetUsersFamilyMembers(Guid Id)
        {
            if (Id == Guid.Empty)
                return BadRequest();

            var model = await _VaccineCardService.GetUsersFamilyMembers(Id);

            if (model == null)
            {
                return NotFound();
            }

            var dto = _converter.ConvertToListOfFamilyMembers(model);
            _logger.LogInformation("{item} has been fetched at {time}", model.Id, DateTime.Now);

            return Ok(dto);
        }
    }
}