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
    /// <inheritdoc/>
    public class PasswordHasher : IPasswordHasher
    {
        
        /// <inheritdoc/>
        public PasswordHashResultDto GenerateHashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
            
            return new PasswordHashResultDto { HashResult = hash, Salt = salt };
        }

        /// <inheritdoc/>
        public PasswordHashResultDto GenerateHashPasswordWithSalt(string password, string salt)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return new PasswordHashResultDto { HashResult = hash, Salt = salt };
        }
    }
}
