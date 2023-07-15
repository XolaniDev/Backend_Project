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

namespace af.assessment.api.Data.Dtos
{
    /// <summary>
    ///     The following class contains the properties for the MemberDto.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class LoginDto
    {
        /// <summary>
        ///      A <see cref="string"/> reperesenting the Id number in the MemberDto.
        /// </summary>
        public string IdentificationNumber { get; set; }
        /// <summary>
        ///      A <see cref="string"/> reperesenting the password in the MemberDto.
        /// </summary>
        public string Password { get; set; }

    }
}
