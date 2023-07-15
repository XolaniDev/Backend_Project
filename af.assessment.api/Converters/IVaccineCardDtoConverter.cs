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

namespace af.assessment.api.Converters
{
    /// <summary>
    ///     Represents the interface of the converter class.
    /// </summary>
    public interface IVaccineCardDtoConverter
    {
        /// <summary>
        ///     Converts a <see cref="VaccineCardDto"/> then returns the resulting <see cref="FamilyMember"/> class.
        /// </summary>
        /// <param name="member">
        ///     A <see cref="Member"/> representing the member to be converted.
        /// </param>
        /// <returns>
        ///     A <see cref="VaccineCardDto"/> representing the resulting family member transfer object of the operation.
        /// </returns>
        public VaccineCardDto ConvertToListOfFamilyMembers(Member member);
    }
}