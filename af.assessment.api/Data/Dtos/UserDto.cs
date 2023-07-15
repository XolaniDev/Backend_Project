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
    ///     Represents the a member data transfer object for a member.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class UserDto
    {
        /// <summary>
        ///     A <see cref="string"/> representing the full name in the user dto.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     A <see cref="string"/>  representing the email address in the user dto.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     A <see cref="string"/> representing the identification number in the user dto.
        /// </summary>
        public string IdentificationNumber { get; set; }

        /// <summary>
        ///     A <see cref="string"/> representing the mobile number in the user dto.
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        ///     A <see cref="string"/> representing the user profile picture url
        /// </summary>
        public string ProfilePictureUrl { get; set; }

        /// <summary>
        ///     A <see cref="string"/> representing the medical aid name  in the user dto.
        /// </summary>
        public string MedicalAidName { get; set; }
        
        /// <summary>
        ///      A <see cref="string"/> representing the medical aid number  in the user dto.
        /// </summary>
        public string MedicalAidNumber { get; set; }
        
        /// <summary>
        ///      A <see cref="string"/> representing the main member name  in the user dto.
        /// </summary>
        public string MainMemberName { get; set; }
        
        /// <summary>
        ///      A <see cref="string"/> representing the main member cell number  in the user dto.
        /// </summary>
        public string MainMemberNumber { get; set; }
        
        /// <summary>
        ///    A <see cref="string"/> representing the Street name in the user dto.
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