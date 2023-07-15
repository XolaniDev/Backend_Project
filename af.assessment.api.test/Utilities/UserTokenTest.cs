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
using af.assessment.api.Data.Models;
using af.assessment.api.Token;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using Xunit;

namespace af.assessment.api.test.Token
{
    /// <summary>
    ///     Test cases for the user token.
    /// </summary>
    public class UserTokenTest
    {
        /// <summary>
        ///     Intsance of the UserToken class.
        /// </summary>
        private readonly UserToken _userToken;

        /// <summary>
        ///     Instance of the IConfiguration.
        /// </summary>
        private readonly Mock<IConfiguration> _mockconfiguration;

        /// <summary>
        ///     Injecting dependencies for the UserTokenTest class.
        /// </summary>
        public UserTokenTest()
        {
            _mockconfiguration = new Mock<IConfiguration>();

            _userToken = new UserToken(_mockconfiguration.Object);
        }

        /// <summary>
        ///     Tests that returns null if the member is null.
        /// </summary>
        [Fact]
        public void BuildUserToken_Unsuccessfully_Should_Return_Null_If_MemberDTO_Is_Null()
        {
            //Arrange
            Member memberModel = null;

            //Act
            var result = _userToken.BuildUserToken(memberModel);

            //Assert
            Assert.Null(result);
        }

        /// <summary>
        ///      Tests that token object is not null if the member is not null and of the correct structure.
        /// </summary>
        [Fact]
        public void BuildUserToken__Should_build_Successfully()
        {

            //Arrange
            string idnumber = "9306195030087";
            string password = "$2a$11$66mWNajWltzj.iDOJIjFyeg.CIcfSNwROR5zUNUtLDKHQKN1hrUJi";
            string salt = "$2a$11$66mWNajWltzj.iDOJIjFye";
            string secretKey = "asajkdsajaskjfhvdvusdjVuvvjsdVuhdivjSDyqytquldu";
            var id = Guid.NewGuid();

            Member memberModel = new Member()
            {
                Id = id,
                Name = "testName",
                Email = "test@email.com",
                IdentificationNumber = idnumber,
                MobileNumber = "0837778888",
                Password = password,
                Salt = salt
            };

            var userTokenDto = new UserTokenDto()
            {
                Token = "eyJheXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTYzNjQ1MjY4NSwiZXhwIjoxNjM2NDU2Mjg1fQ.K9MAtZw8ZOUg4oq - LxpPfe8I61KdGRNoAD9JHgDYDZI",
                ExpirationTime = DateTime.UtcNow.AddMinutes(20),
                Name = "testName",
                Guid  = id
            };

            _mockconfiguration.Setup(_configuration => _configuration["jwt:key"]).Returns(secretKey);
            //Act

            var result = _userToken.BuildUserToken(memberModel);
            //Assert
            Assert.NotNull(result.Token); 
        }

        /// <summary>
        ///     Test for a null if the member model is missing parameters are empty.
        /// </summary>
        [Fact]
        public void BuildUserToken__Should_Throw_ArgumentException_If_MemberModel_Is_Empty()
        {

            //Arrange
            string idnumber = "";
            string password = "";
            string salt = "";
            var id = Guid.Empty;

            Member memberModel = new Member()
            {
                Id = id,
                Name = "",
                Email = "",
                IdentificationNumber = idnumber,
                MobileNumber = "",
                Password = password,
                Salt = salt
            };

            //Act
            var result = _userToken.BuildUserToken(memberModel);

            //Assert
            Assert.Null(result);
        }
    }
}
