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

using System.Diagnostics.CodeAnalysis;

namespace af.assessment.api.Data.DTOs.UpdateProfile
{
    /// <summary>
    ///     Represents the location details of a member when updating the member.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class LocationDetailsDto
    {
        /// <summary>
        ///    A <see cref="string"/> representing the street name in the user dto.
        /// </summary>
        public string StreetName { get; set; }

        /// <summary>
        ///     A <see cref="int"/> representing the postal code in the user dto.
        /// </summary>
        public int PostalCode { get; set; }

        /// <summary>
        ///     A <see cref="string"/> representing the city in the user dto.
        /// </summary>
        public string City { get; set; }
    }
}
