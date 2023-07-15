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
using System.Diagnostics.CodeAnalysis;

namespace af.assessment.api.Data.Dtos
{
    /// <summary>
    ///     Represents the a ResetPassword data transfer object for a existing member.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ResetPasswordDto
    {
        /// <summary>
        ///     A <see cref="Guid"/> representing the Id in the ResetPasswordDTO dto.
        /// </summary>
        public Guid MemberId { get; set; }
        
        /// <summary>
        ///     A <see cref="string"/> representing the password in the ResetPasswordDTO dto.
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        ///     A <see cref="string"/> representing the Confirm password in the ResetPasswordDTO dto.
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}