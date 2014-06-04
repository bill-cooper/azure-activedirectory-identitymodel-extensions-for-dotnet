//-----------------------------------------------------------------------
// Copyright (c) Microsoft Open Technologies, Inc.
// All Rights Reserved
// Apache License 2.0
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Xml;
using Saml2SecurityTokenHandler = Microsoft.IdentityModel.Tokens.Saml2SecurityTokenHandler;

namespace Microsoft.IdentityModel.Test
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class Saml2SecurityTokenHandlerTests
    {
        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        [TestProperty("TestCaseID", "f0b4edf5-e1bd-448f-9c0f-50b2a47bfd24")]
        [Description("Tests: Constructors")]
        public void Saml2SecurityTokenHandler_Constructors()
        {
            Saml2SecurityTokenHandler saml2SecurityTokenHandler = new Saml2SecurityTokenHandler();
        }

        [TestMethod]
        [TestProperty("TestCaseID", "1832c430-b491-48db-86bb-59faf72304bd")]
        [Description("Tests: Defaults")]
        public void Saml2SecurityTokenHandler_Defaults()
        {
            Saml2SecurityTokenHandler samlSecurityTokenHandler = new Saml2SecurityTokenHandler();
            Assert.IsTrue(samlSecurityTokenHandler.MaximumTokenSizeInBytes == TokenValidationParameters.DefaultMaximumTokenSizeInBytes, "MaximumTokenSizeInBytes");
        }

        [TestMethod]
        [TestProperty("TestCaseID", "e40d2758-e36c-4b52-9ac9-31bcfc27c308")]
        [Description("Tests: GetSets")]
        public void Saml2SecurityTokenHandler_GetSets()
        {
            Saml2SecurityTokenHandler samlSecurityTokenHandler = new Saml2SecurityTokenHandler();
            TestUtilities.GetSet(samlSecurityTokenHandler, "MaximumTokenSizeInBytes", (object)0, ExpectedException.ArgumentOutOfRangeException(substringExpected: "IDX10101"));
            TestUtilities.GetSet(samlSecurityTokenHandler, "MaximumTokenSizeInBytes", (object)1, ExpectedException.NoExceptionExpected);
        }

        [TestMethod]
        [TestProperty("TestCaseID", "617fb57a-9b95-40a3-8cf4-652b33450a54")]
        [Description("Tests: Publics")]
        public void Saml2SecurityTokenHandler_Publics()
        {
            //CanReadToken();
            ValidateAudience();
            ValidateIssuer();
            ValidateToken();
        }

        private void CanReadToken()
        {
            // CanReadToken
            Saml2SecurityTokenHandler samlSecurityTokenHandler = new Saml2SecurityTokenHandler();
            Assert.IsFalse(CanReadToken(securityToken: null, samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: ExpectedException.NoExceptionExpected));

            string samlString = new string('S', TokenValidationParameters.DefaultMaximumTokenSizeInBytes + 1);
            Assert.IsFalse(CanReadToken(samlString, samlSecurityTokenHandler, ExpectedException.NoExceptionExpected));

            samlString = new string('S', TokenValidationParameters.DefaultMaximumTokenSizeInBytes);
            CanReadToken(securityToken: samlString, samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: ExpectedException.NoExceptionExpected);

            samlString = IdentityUtilities.CreateSamlToken();
            Assert.IsFalse(CanReadToken(securityToken: samlString, samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: ExpectedException.NoExceptionExpected));

            samlString = IdentityUtilities.CreateSaml2Token();
            Assert.IsTrue(CanReadToken(securityToken: samlString, samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: ExpectedException.NoExceptionExpected));
        }

        private bool CanReadToken(string securityToken, Saml2SecurityTokenHandler samlSecurityTokenHandler, ExpectedException expectedException)
        {
            bool canReadToken = false;
            try
            {
                canReadToken = samlSecurityTokenHandler.CanReadToken(securityToken);
                expectedException.ProcessNoException();
            }
            catch (Exception exception)
            {
                expectedException.ProcessException(exception);
            }

            return canReadToken;
        }

        private void ValidateIssuer()
        {
            DerivedSamlSecurityTokenHandler samlSecurityTokenHandler = new DerivedSamlSecurityTokenHandler();

            ExpectedException expectedException = ExpectedException.NoExceptionExpected;
            ValidateIssuer(null, new TokenValidationParameters { ValidateIssuer = false }, samlSecurityTokenHandler, expectedException);

            expectedException = ExpectedException.ArgumentNullException( substringExpected: "Parameter name: validationParameters");
            ValidateIssuer("bob", null, samlSecurityTokenHandler, expectedException);

            expectedException = ExpectedException.SecurityTokenInvalidIssuerException(substringExpected: "IDX10204");
            ValidateIssuer("bob", new TokenValidationParameters { }, samlSecurityTokenHandler, expectedException);

            expectedException = ExpectedException.NoExceptionExpected;
            string issuer = ValidateIssuer("bob", new TokenValidationParameters { ValidIssuer = "bob" }, samlSecurityTokenHandler, expectedException);
            Assert.IsTrue(issuer == "bob", "issuer mismatch");

            expectedException = ExpectedException.SecurityTokenInvalidIssuerException(substringExpected: "IDX10205");
            ValidateIssuer("bob", new TokenValidationParameters { ValidIssuer = "frank" }, samlSecurityTokenHandler, expectedException);

            List<string> validIssuers = new List<string> { "john", "paul", "george", "ringo" };
            expectedException = ExpectedException.SecurityTokenInvalidIssuerException(substringExpected: "IDX10205");
            ValidateIssuer("bob", new TokenValidationParameters { ValidIssuers = validIssuers }, samlSecurityTokenHandler, expectedException);

            expectedException = ExpectedException.NoExceptionExpected;
            ValidateIssuer("bob", new TokenValidationParameters { ValidateIssuer = false }, samlSecurityTokenHandler, expectedException);

            validIssuers.Add("bob");
            expectedException = ExpectedException.NoExceptionExpected;
            issuer = ValidateIssuer("bob", new TokenValidationParameters { ValidIssuers = validIssuers }, samlSecurityTokenHandler, expectedException);
            Assert.IsTrue(issuer == "bob", "issuer mismatch");

            expectedException =  ExpectedException.SecurityTokenInvalidIssuerException(substringExpected: "IDX10204");
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                IssuerValidator = IdentityUtilities.IssuerValidatorEcho,
            };

            ValidateIssuer("bob", validationParameters, samlSecurityTokenHandler, expectedException);
                        
            // no delegate secondary should still succeed
            expectedException = ExpectedException.NoExceptionExpected;
            validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidIssuers = validIssuers,
            };

            issuer = ValidateIssuer("bob", validationParameters, samlSecurityTokenHandler, expectedException);
            Assert.IsTrue(issuer == "bob", "issuer mismatch");

            // no delegate, secondary should fail
            validIssuers = new List<string> { "john", "paul", "george", "ringo" };
            expectedException = ExpectedException.SecurityTokenInvalidIssuerException(substringExpected: "IDX10205");
            validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new X509SecurityKey(KeyingMaterial.DefaultCert_2048),
                ValidateAudience = false,
                ValidIssuer = "http://Bob",
            };
            ValidateIssuer("bob", validationParameters, samlSecurityTokenHandler, expectedException);
        }

        private string ValidateIssuer(string issuer, TokenValidationParameters validationParameters, DerivedSamlSecurityTokenHandler samlSecurityTokenHandler, ExpectedException expectedException)
        {
            string returnVal = string.Empty;
            try
            {
                returnVal = samlSecurityTokenHandler.ValidateIssuerPublic(issuer, new DerivedSaml2SecurityToken(), validationParameters);
                expectedException.ProcessNoException();
            }
            catch (Exception exception)
            {
                expectedException.ProcessException(exception);
            }

            return returnVal;
        }

        private void ValidateToken()
        {
            // parameter validation
            Saml2SecurityTokenHandler samlSecurityTokenHandler = new Saml2SecurityTokenHandler();
            ExpectedException expectedException = ExpectedException.ArgumentNullException(substringExpected: "name: securityToken");
            ValidateToken(securityToken: null, validationParameters: new TokenValidationParameters(), samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: expectedException);

            expectedException = ExpectedException.ArgumentNullException(substringExpected: "name: validationParameters");
            ValidateToken(securityToken: "s", validationParameters: null, samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: expectedException);

            expectedException = ExpectedException.ArgumentException(substringExpected: "IDX10209");
            samlSecurityTokenHandler.MaximumTokenSizeInBytes = 1;
            ValidateToken(securityToken: "ss", validationParameters: new TokenValidationParameters(), samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: expectedException);

            samlSecurityTokenHandler.MaximumTokenSizeInBytes = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;
            string samlToken = IdentityUtilities.CreateSaml2Token();
            expectedException = ExpectedException.NoExceptionExpected;
            ValidateToken(samlToken, IdentityUtilities.DefaultAsymmetricTokenValidationParameters, samlSecurityTokenHandler, expectedException);
        }

        private void ValidateAudience()
        {
            Saml2SecurityTokenHandler samlSecurityTokenHandler = new Saml2SecurityTokenHandler();
            ExpectedException expectedException;
            string samlString = IdentityUtilities.CreateSaml2Token();

            TokenValidationParameters tokenValidationParameters =
                new TokenValidationParameters
                {
                    IssuerSigningToken = IdentityUtilities.DefaultAsymmetricSigningToken,
                    RequireExpirationTime = false,
                    RequireSignedTokens = false,
                    ValidIssuer = IdentityUtilities.DefaultIssuer,
                };

            // Do not validate audience
            tokenValidationParameters.ValidateAudience = false;
            expectedException = ExpectedException.NoExceptionExpected;
            ValidateToken(securityToken: samlString, validationParameters: tokenValidationParameters, samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: expectedException);

            // no valid audiences
            tokenValidationParameters.ValidateAudience = true;
            expectedException = ExpectedException.SecurityTokenInvalidAudienceException("IDX10208");
            ValidateToken(securityToken: samlString, validationParameters: tokenValidationParameters, samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: expectedException);

            tokenValidationParameters.ValidateAudience = true;
            tokenValidationParameters.ValidAudience = "John";
            expectedException = new ExpectedException(typeExpected: typeof(SecurityTokenInvalidAudienceException), substringExpected: "IDX10214");
            ValidateToken(securityToken: samlString, validationParameters: tokenValidationParameters, samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: expectedException);

            // UriKind.Absolute, no match.
            tokenValidationParameters.ValidateAudience = true;
            tokenValidationParameters.ValidAudience = IdentityUtilities.NotDefaultAudience;
            expectedException = new ExpectedException(typeExpected: typeof(SecurityTokenInvalidAudienceException), substringExpected: "IDX10214");
            ValidateToken(securityToken: samlString, validationParameters: tokenValidationParameters, samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: expectedException);

            expectedException = ExpectedException.NoExceptionExpected;
            tokenValidationParameters.ValidAudience = IdentityUtilities.DefaultAudience;
            tokenValidationParameters.ValidAudiences = null;
            ValidateToken(securityToken: samlString, validationParameters: tokenValidationParameters, samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: expectedException);

            // !UriKind.Absolute
            List<string> audiences = new List<string> { "John", "Paul", "George", "Ringo" };
            tokenValidationParameters.ValidAudience = null;
            tokenValidationParameters.ValidAudiences = audiences;
            tokenValidationParameters.ValidateAudience = false;
            expectedException = ExpectedException.NoExceptionExpected;
            ValidateToken(securityToken: samlString, validationParameters: tokenValidationParameters, samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: expectedException);

            // UriKind.Absolute, no match
            audiences = new List<string> { "http://www.John.com", "http://www.Paul.com", "http://www.George.com", "http://www.Ringo.com", "    " };
            tokenValidationParameters.ValidAudience = null;
            tokenValidationParameters.ValidAudiences = audiences;
            tokenValidationParameters.ValidateAudience = false;
            expectedException = ExpectedException.NoExceptionExpected;
            ValidateToken(securityToken: samlString, validationParameters: tokenValidationParameters, samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: expectedException);

            tokenValidationParameters.ValidateAudience = true;
            expectedException = new ExpectedException(typeExpected: typeof(SecurityTokenInvalidAudienceException), substringExpected: "IDX10214");
            ValidateToken(securityToken: samlString, validationParameters: tokenValidationParameters, samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: expectedException);

            tokenValidationParameters.ValidateAudience = true;
            expectedException = ExpectedException.NoExceptionExpected;
            audiences.Add(IdentityUtilities.DefaultAudience);
            ValidateToken(securityToken: samlString, validationParameters: tokenValidationParameters, samlSecurityTokenHandler: samlSecurityTokenHandler, expectedException: expectedException);
        }

        private ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, Saml2SecurityTokenHandler samlSecurityTokenHandler, ExpectedException expectedException)
        {
            ClaimsPrincipal princiapl = null;
            try
            {
                SecurityToken validatedToken;
                princiapl = samlSecurityTokenHandler.ValidateToken(securityToken, validationParameters, out validatedToken);
                expectedException.ProcessNoException();
            }
            catch (Exception exception)
            {
                expectedException.ProcessException(exception);
            }

            return princiapl;
        }

        private class DerivedSamlSecurityTokenHandler : Saml2SecurityTokenHandler
        {
            public ClaimsIdentity CreateClaimsPublic(Saml2SecurityToken samlToken, string issuer, TokenValidationParameters validationParameters)
            {
                return base.CreateClaimsIdentity(samlToken, issuer, validationParameters);
            }

            public string ValidateIssuerPublic(string issuer, SecurityToken securityToken, TokenValidationParameters validationParameters)
            {
                return base.ValidateIssuer(issuer, securityToken, validationParameters);
            }
        }

        private class DerivedSaml2SecurityToken : Saml2SecurityToken
        {
            public Saml2Assertion SamlAssertion { get; set; }

            public DerivedSaml2SecurityToken()
                : base(new DerivedSaml2Assertion(IdentityUtilities.DefaultIssuer))
            { }

            public DerivedSaml2SecurityToken(Saml2Assertion samlAssertion)

                : base(samlAssertion)
            { }
        }

        private class DerivedSaml2Assertion : Saml2Assertion
        {
            public DerivedSaml2Assertion(string issuer)
                : base(new Saml2NameIdentifier(issuer))
            {
                DerivedIssuer = issuer;
            }

            public string DerivedIssuer { get; set; }
        }
    }
}