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
using System.Globalization;
using System.Linq;

namespace af.assessment.api.Validators.Utils
{
    /// <summary>
    ///     Provides Utility methods that is used accross validators.
    /// </summary>
    public class ValidatorUtilityMethods
    {

        /// <summary>
        ///     Validates the password and returns a bool of the operation. 
        /// </summary>
        /// <param name="password">
        ///     A <see cref="string"/> representing the member's password.
        /// </param>
        /// <returns>
        ///     A <see cref="bool"/> if the password is valid.
        /// </returns>
        public static bool IsValidPassword(string password)
        {

            if (password is null || password.Trim().Length < 8 || password.Trim().Length > 14 || !hasSpecialChar(password) || password == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        ///     Method for the special characters.
        /// </summary>
        /// <param name="input"></param>
        /// <returns> 
        ///      A <see cref="bool"/> of false.
        /// </returns>
        public static bool hasSpecialChar(string input)
        {
            string specialChar = @"|!#$%&/()=?»«@£§€{}.-;~`'<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }
        /// <summary>
        ///      Method for a valid <see cref="string"/> ID number.
        /// </summary>
        /// <param name="identificationNumber"></param>
        /// <returns>
        ///      A <see cref="bool"/> of true.
        /// </returns>
        public static bool BeAValidIdentificationNumber(string identificationNumber)
        {
            if (identificationNumber is null ||
                identificationNumber.Trim().Length != 13 ||
                identificationNumber.Any(c => c < '0' || c > '9')) return false;

            string dob = identificationNumber.Substring(0, 6);
            if (!DateTime.TryParseExact(dob, "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result)) return false;

            if (identificationNumber[10] != '0' && identificationNumber[10] != '1' || !IsValidCheckSum(identificationNumber)) return false;

            return true;

        }
        /// <summary>
        ///      Method for validating the valid sum in an <see cref="string"/> ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> 
        ///      The correct number of digits in an  <see cref="string"/> ID.
        /// </returns>
        public static bool IsValidCheckSum(string id)
        {
            int sum = 0, counter = 0;
            string check = id.Substring(0, id.Length - 1);
            int givenCheckDigit = id[id.Length - 1] - '0';

            for (int i = check.Length - 1; i >= 0; i--)
            {
                int digit = check[i] - '0';
                if (counter % 2 == 0) digit = digit * 2;
                if (digit > 9) digit -= 9;
                sum += digit;
                counter++;
            }

            int minusValue = sum % 10;
            int calculatedCheckDigit = 10 - (minusValue == 0 ? 10 : minusValue);
            return givenCheckDigit == calculatedCheckDigit;
        }
    }
}
