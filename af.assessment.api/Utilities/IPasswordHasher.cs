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

namespace af.assessment.api.Utilities
{
    /// <summary>
    ///     Provides methods to generate hashed passwords.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        ///     Generates a hashed password and then returns the hashed password with it's salt.
        /// </summary>
        /// <param name="password">
        ///     A <see cref="string"/> representing the plaintext password.
        /// </param>
        /// <returns>
        ///     A <see cref="PasswordHashResultDto"/> containing the hashed password and it's salt component.
        /// </returns>
        public PasswordHashResultDto GenerateHashPassword(string password);
        /// <summary>
        ///     Generates a hashed password with the given salt and returns the hashed result.
        /// </summary>
        /// <param name="password">
        ///     A <see cref="string"/> representing the plaintext password.
        /// </param>
        /// <param name="salt">
        ///     A <see cref="string"/> representing the salt for password hashing.
        /// </param>
        /// <returns>
        ///     A <see cref="PasswordHashResultDto"/> containing the hashed password and it's salt component.
        /// </returns>
        public PasswordHashResultDto GenerateHashPasswordWithSalt(string password, string salt);
    }
}
