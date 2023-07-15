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
using af.assessment.api.Enums;

namespace af.assessment.api.Data.Dtos
{
    /// <summary>
    ///     The following class contains the properties for the MemberDto.
    /// </summary>
    public class FamilyMemberDto
    {   
        /// <summary>
        ///      A <see cref="string"/> representing the First Name in the FamilyMemberDto.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///      A <see cref="string"/> representing the Last Name in the FamilyMemberDto.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///      A <see cref="string"/> representing the Identification Number in the FamilyMemberDto.
        /// </summary>
        public string IdentificationNumber { get; set; }

        /// <summary>
        ///      A <see cref="MemberType"/> representing the Member Type in the FamilyMemberDto.
        /// </summary>
        public MemberType MemberType { get; set; }

        /// <summary>
        ///     Gets or sets a list of <see cref="VaccineDto"/> representing the vaccines of a family member.
        /// </summary>
        public List<VaccineDto> Vaccines { get; set; }
    }
}