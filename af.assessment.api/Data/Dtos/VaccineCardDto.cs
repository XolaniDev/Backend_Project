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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace af.assessment.api.Data.Dtos
{
    /// <summary>
    ///     Represents the vaccine card data transfer object for a member.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class VaccineCardDto
    {
        /// <summary>
        ///    Gets or sets a list of <see cref="FamilyMemberDto"/> representing the member's family members information that will be retrieve. 
        /// </summary>
        //[DataMember(Name = "familyMember")]
        public List<FamilyMemberDto> FamilyMember { get; set; }
    }
}