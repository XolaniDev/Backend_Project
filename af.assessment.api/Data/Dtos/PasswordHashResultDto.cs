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
    ///     Represents a password hash result.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PasswordHashResultDto
    {
        /// <summary>
        ///     A <see cref="string"/> representing the hashed password and salt result.
        /// </summary>
        public string HashResult { get; set; }
        /// <summary>
        ///     An array of <see cref="byte"/> representing the salt used for the hashed password.
        /// </summary>
        /// <value></value>
        public string Salt { get; set; }
    }
}