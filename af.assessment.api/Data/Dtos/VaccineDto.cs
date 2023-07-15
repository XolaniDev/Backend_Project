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

namespace af.assessment.api.Data.Dtos
{
    /// <summary>
    ///     The following class contains the properties for the VaccineDto.
    /// </summary>
    public class VaccineDto
    {
        /// <summary>
        ///      A <see cref="string"/> representing the Name of the Vaccine.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///      A <see cref="string"/> representing the Dose of the Vaccine.
        /// </summary>
        public double Dose { get; set; }

        /// <summary>
        ///      A <see cref="bool"/> representing whether the vaccine has been administered or not.
        /// </summary>
        public bool VaccineStatus { get; set; }

        /// <summary>
        ///      A <see cref="double"/> representing the Batch Number of the Vaccine.
        /// </summary>
        public double BatchNumber { get; set; }

        /// <summary>
        ///      A <see cref="DateTime"/> representing the date that the vaccine was administered.
        /// </summary>
        public DateTime DateAdministered { get; set; }

        /// <summary>
        ///      A <see cref="string"/> representing the name of the clinic where the vaccine was administered.
        /// </summary>
        public string ClinicName { get; set; }

        /// <summary>
        ///      A <see cref="string"/> representing the site at which the vaccine was administered on the family members body.
        /// </summary>
        public string Site { get; set; }

        /// <summary>
        ///      A <see cref="string"/> representing the healthcare professional that administered the vaccine.
        /// </summary>
        public string HealthcareProfessional { get; set; }
    }
}