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
    ///     Represents the a register data transfer object for registering a new member.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RegisterDto
    {
       /// <summary>
       ///     A <see cref="string"/> reperesenting the full name in the register dto.
       /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     A <see cref="string"/>  representing the email address in the register dto.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        ///     A <see cref="string"/> representing the identification number in the register dto.
        /// </summary>
        public string IdentificationNumber { get; set; }
        /// <summary>
        ///     A <see cref="string"/> reperesenting the mobile number in the register dto.
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        ///     A <see cref="string"/> representing the password in the register dto.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        ///     A <see cref="int"/> representing the otp preference in the register dto.
        /// </summary>
        public int OtpPreference { get; set; }
    }
}