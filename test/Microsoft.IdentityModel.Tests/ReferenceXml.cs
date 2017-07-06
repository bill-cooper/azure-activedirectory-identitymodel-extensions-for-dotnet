//------------------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Protocols.WsFederation;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Saml;
using Microsoft.IdentityModel.Xml;

namespace Microsoft.IdentityModel.Tests
{
    public class ReferenceXml
    {
        // TODO move this
        public static SecurityKey DefaultAADSigningKey
        {
            get
            {
                var certData = "MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD";
                var aadCert = new X509Certificate2(Convert.FromBase64String(certData));
                return new X509SecurityKey(aadCert);
            }
        }

        #region EnvelopedSignatureReader / Writer

        public static string Saml2Token_TwoSignatures
        {
            get
            {
                return @"<Assertion ID = ""_d60bd9ed-8aab-40c8-ba5f-f548c3401ae2"" IssueInstant=""2017-03-20T15:52:31.957Z"" Version=""2.0"" xmlns=""urn:oasis:names:tc:SAML:2.0:assertion"">
                        <Issuer>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</Issuer>
                        <Signature xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                            <SignedInfo>
                                <CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" />
                                <SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"" />
                                    <Reference URI=""#_d60bd9ed-8aab-40c8-ba5f-f548c3401ae2"">
                                        <Transforms>
                                            <Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" />
                                            <Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" />
                                        </Transforms>
                                        <DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"" />
                                        <DigestValue>Ytfkc60mLe1Zgu7TBQpMv8nJ1SVxT0ZjsFHaFqSB2VI=</DigestValue>
                                    </Reference>
                            </SignedInfo>
                            <SignatureValue>NRV7REVbDRflg616G6gYg0fAGTEw8BhtyPzqaU+kPQI35S1vpgt12VlQ57PkY7Rs0Jucx9npno+bQVMKN2DNhhnzs9qoNY2V3TcdJCcwaMexinHoFXHA0+J6+vR3RWTXhX+iAnfudtKThqbh/mECRLrjyTdy6L+qNkP7sALCWrSVwJVRmzkTOUF8zG4AKY9dQziec94Zv4S7G3cFgj/i7ok2DfBi7AEMCu1lh3dsQAMDeCvt7binhIH2D2ad3iCfYyifDGJ2ncn9hIyxrEiBdS8hZzWijcLs6+HQhVaz9yhZL9u/ZxSRaisXClMdqrLFjUghJ82sVfgQdp7SF165+Q==</SignatureValue>
                                <KeyInfo>
                                    <X509Data>
                                        <X509Certificate>MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</X509Certificate>
                                    </X509Data>
                                </KeyInfo>
                        </Signature>
                        <Signature xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                            <SignedInfo>
                                <CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" />
                                <SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"" />
                                    <Reference URI=""#_d60bd9ed-8aab-40c8-ba5f-f548c3401ae2"">
                                        <Transforms>
                                            <Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" />
                                            <Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" />
                                        </Transforms>
                                        <DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"" />
                                        <DigestValue>Ytfkc60mLe1Zgu7TBQpMv8nJ1SVxT0ZjsFHaFqSB2VI=</DigestValue>
                                    </Reference>
                            </SignedInfo>
                            <SignatureValue>NRV7REVbDRflg616G6gYg0fAGTEw8BhtyPzqaU+kPQI35S1vpgt12VlQ57PkY7Rs0Jucx9npno+bQVMKN2DNhhnzs9qoNY2V3TcdJCcwaMexinHoFXHA0+J6+vR3RWTXhX+iAnfudtKThqbh/mECRLrjyTdy6L+qNkP7sALCWrSVwJVRmzkTOUF8zG4AKY9dQziec94Zv4S7G3cFgj/i7ok2DfBi7AEMCu1lh3dsQAMDeCvt7binhIH2D2ad3iCfYyifDGJ2ncn9hIyxrEiBdS8hZzWijcLs6+HQhVaz9yhZL9u/ZxSRaisXClMdqrLFjUghJ82sVfgQdp7SF165+Q==</SignatureValue>
                                <KeyInfo>
                                    <X509Data>
                                        <X509Certificate>MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</X509Certificate>
                                    </X509Data>
                                </KeyInfo>
                        </Signature>
                        <Subject>
                            <NameID Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:persistent"">RrX3SPSxDw6z4KHaKB2V_mnv0G-LbRZdYvo1RQa1L7s</NameID>
                            <SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer"" />
                        </Subject>
                        <Conditions NotBefore=""2017-03-20T15:47:31.957Z"" NotOnOrAfter=""2017-03-20T16:47:31.957Z"">
                            <AudienceRestriction>
                                <Audience>spn:fe78e0b4-6fe7-47e6-812c-fb75cee266a4</Audience>
                            </AudienceRestriction>
                        </Conditions>
                        <AttributeStatement>
                            <Attribute Name=""http://schemas.microsoft.com/identity/claims/tenantid"">
                                <AttributeValue>add29489-7269-41f4-8841-b63c95564420</AttributeValue>
                            </Attribute>
                            <Attribute Name=""http://schemas.microsoft.com/identity/claims/objectidentifier"">
                                <AttributeValue>d1ad9ce7-b322-4221-ab74-1e1011e1bbcb</AttributeValue>
                            </Attribute>
                            <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"">
                                <AttributeValue>User1@Cyrano.onmicrosoft.com</AttributeValue>
                            </Attribute>
                            <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"">
                                <AttributeValue>1</AttributeValue>
                            </Attribute>
                            <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"">
                                <AttributeValue>User</AttributeValue>
                            </Attribute>
                            <Attribute Name=""http://schemas.microsoft.com/identity/claims/displayname"">
                                <AttributeValue>User1</AttributeValue>
                            </Attribute>
                            <Attribute Name=""http://schemas.microsoft.com/identity/claims/identityprovider"">
                                <AttributeValue>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</AttributeValue>
                            </Attribute>
                        </AttributeStatement>
                        <AuthnStatement AuthnInstant=""2017-03-20T15:52:31.551Z"">
                            <AuthnContext>
                                <AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</AuthnContextClassRef>
                            </AuthnContext>
                        </AuthnStatement>
                    </Assertion>";
            }
        }

        public static string Saml2Token_Valid_Formated
        {
            get
            {
                return @"<Assertion ID = ""_d60bd9ed-8aab-40c8-ba5f-f548c3401ae2"" IssueInstant=""2017-03-20T15:52:31.957Z"" Version=""2.0"" xmlns=""urn:oasis:names:tc:SAML:2.0:assertion"">
                        <Issuer>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</Issuer>
                        <Signature xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                            <SignedInfo>
                                <CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" />
                                <SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"" />
                                    <Reference URI=""#_d60bd9ed-8aab-40c8-ba5f-f548c3401ae2"">
                                        <Transforms>
                                            <Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" />
                                            <Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" />
                                        </Transforms>
                                        <DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"" />
                                        <DigestValue>Ytfkc60mLe1Zgu7TBQpMv8nJ1SVxT0ZjsFHaFqSB2VI=</DigestValue>
                                    </Reference>
                            </SignedInfo>
                            <SignatureValue>NRV7REVbDRflg616G6gYg0fAGTEw8BhtyPzqaU+kPQI35S1vpgt12VlQ57PkY7Rs0Jucx9npno+bQVMKN2DNhhnzs9qoNY2V3TcdJCcwaMexinHoFXHA0+J6+vR3RWTXhX+iAnfudtKThqbh/mECRLrjyTdy6L+qNkP7sALCWrSVwJVRmzkTOUF8zG4AKY9dQziec94Zv4S7G3cFgj/i7ok2DfBi7AEMCu1lh3dsQAMDeCvt7binhIH2D2ad3iCfYyifDGJ2ncn9hIyxrEiBdS8hZzWijcLs6+HQhVaz9yhZL9u/ZxSRaisXClMdqrLFjUghJ82sVfgQdp7SF165+Q==</SignatureValue>
                                <KeyInfo>
                                    <X509Data>
                                        <X509Certificate>MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</X509Certificate>
                                    </X509Data>
                                </KeyInfo>
                        </Signature>
                        <Subject>
                            <NameID Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:persistent"">RrX3SPSxDw6z4KHaKB2V_mnv0G-LbRZdYvo1RQa1L7s</NameID>
                            <SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer"" />
                        </Subject>
                        <Conditions NotBefore=""2017-03-20T15:47:31.957Z"" NotOnOrAfter=""2017-03-20T16:47:31.957Z"">
                            <AudienceRestriction>
                                <Audience>spn:fe78e0b4-6fe7-47e6-812c-fb75cee266a4</Audience>
                            </AudienceRestriction>
                        </Conditions>
                        <AttributeStatement>
                            <Attribute Name=""http://schemas.microsoft.com/identity/claims/tenantid"">
                                <AttributeValue>add29489-7269-41f4-8841-b63c95564420</AttributeValue>
                            </Attribute>
                            <Attribute Name=""http://schemas.microsoft.com/identity/claims/objectidentifier"">
                                <AttributeValue>d1ad9ce7-b322-4221-ab74-1e1011e1bbcb</AttributeValue>
                            </Attribute>
                            <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"">
                                <AttributeValue>User1@Cyrano.onmicrosoft.com</AttributeValue>
                            </Attribute>
                            <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"">
                                <AttributeValue>1</AttributeValue>
                            </Attribute>
                            <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"">
                                <AttributeValue>User</AttributeValue>
                            </Attribute>
                            <Attribute Name=""http://schemas.microsoft.com/identity/claims/displayname"">
                                <AttributeValue>User1</AttributeValue>
                            </Attribute>
                            <Attribute Name=""http://schemas.microsoft.com/identity/claims/identityprovider"">
                                <AttributeValue>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</AttributeValue>
                            </Attribute>
                        </AttributeStatement>
                        <AuthnStatement AuthnInstant=""2017-03-20T15:52:31.551Z"">
                            <AuthnContext>
                                <AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</AuthnContextClassRef>
                            </AuthnContext>
                        </AuthnStatement>
                    </Assertion>";
            }
        }

        public static string Saml2Token_Valid_SignatureNOTFormated
        {
            get
            {
                return @"<Assertion ID = ""_d60bd9ed-8aab-40c8-ba5f-f548c3401ae2"" IssueInstant=""2017-03-20T15:52:31.957Z"" Version=""2.0"" xmlns=""urn:oasis:names:tc:SAML:2.0:assertion"">
                            <Issuer>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</Issuer>
                            <Signature xmlns=""http://www.w3.org/2000/09/xmldsig#""><SignedInfo><CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" /><SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"" /><Reference URI=""#_d60bd9ed-8aab-40c8-ba5f-f548c3401ae2""><Transforms><Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" /><Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" /></Transforms><DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"" /><DigestValue>Ytfkc60mLe1Zgu7TBQpMv8nJ1SVxT0ZjsFHaFqSB2VI=</DigestValue></Reference></SignedInfo><SignatureValue>NRV7REVbDRflg616G6gYg0fAGTEw8BhtyPzqaU+kPQI35S1vpgt12VlQ57PkY7Rs0Jucx9npno+bQVMKN2DNhhnzs9qoNY2V3TcdJCcwaMexinHoFXHA0+J6+vR3RWTXhX+iAnfudtKThqbh/mECRLrjyTdy6L+qNkP7sALCWrSVwJVRmzkTOUF8zG4AKY9dQziec94Zv4S7G3cFgj/i7ok2DfBi7AEMCu1lh3dsQAMDeCvt7binhIH2D2ad3iCfYyifDGJ2ncn9hIyxrEiBdS8hZzWijcLs6+HQhVaz9yhZL9u/ZxSRaisXClMdqrLFjUghJ82sVfgQdp7SF165+Q==</SignatureValue><KeyInfo><X509Data><X509Certificate>MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</X509Certificate></X509Data></KeyInfo></Signature>
                            <Subject>
                                <NameID Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:persistent"">RrX3SPSxDw6z4KHaKB2V_mnv0G-LbRZdYvo1RQa1L7s</NameID>
                                <SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer"" />
                            </Subject>
                            <Conditions NotBefore=""2017-03-20T15:47:31.957Z"" NotOnOrAfter=""2017-03-20T16:47:31.957Z"">
                                <AudienceRestriction>
                                    <Audience>spn:fe78e0b4-6fe7-47e6-812c-fb75cee266a4</Audience>
                                </AudienceRestriction>
                            </Conditions>
                            <AttributeStatement>
                                <Attribute Name=""http://schemas.microsoft.com/identity/claims/tenantid"">
                                    <AttributeValue>add29489-7269-41f4-8841-b63c95564420</AttributeValue>
                                </Attribute>
                                <Attribute Name=""http://schemas.microsoft.com/identity/claims/objectidentifier"">
                                    <AttributeValue>d1ad9ce7-b322-4221-ab74-1e1011e1bbcb</AttributeValue>
                                </Attribute>
                                <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"">
                                    <AttributeValue>User1@Cyrano.onmicrosoft.com</AttributeValue>
                                </Attribute>
                                <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"">
                                    <AttributeValue>1</AttributeValue>
                                </Attribute>
                                <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"">
                                    <AttributeValue>User</AttributeValue>
                                </Attribute>
                                <Attribute Name=""http://schemas.microsoft.com/identity/claims/displayname"">
                                    <AttributeValue>User1</AttributeValue>
                                </Attribute>
                                <Attribute Name=""http://schemas.microsoft.com/identity/claims/identityprovider"">
                                    <AttributeValue>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</AttributeValue>
                                </Attribute>
                            </AttributeStatement>
                            <AuthnStatement AuthnInstant=""2017-03-20T15:52:31.551Z"">
                                <AuthnContext>
                                    <AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</AuthnContextClassRef>
                                </AuthnContext>
                            </AuthnStatement>
                        </Assertion>";
            }
        }

        public static string Saml2Token_Valid_Signed
        {
            get { return @"<Assertion ID = ""_d60bd9ed-8aab-40c8-ba5f-f548c3401ae2"" IssueInstant=""2017-03-20T15:52:31.957Z"" Version=""2.0"" xmlns=""urn:oasis:names:tc:SAML:2.0:assertion""><Issuer>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</Issuer><Signature xmlns=""http://www.w3.org/2000/09/xmldsig#""><SignedInfo><CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" /><SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"" /><Reference URI=""#_d60bd9ed-8aab-40c8-ba5f-f548c3401ae2""><Transforms><Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" /><Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" /></Transforms><DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"" /><DigestValue>Ytfkc60mLe1Zgu7TBQpMv8nJ1SVxT0ZjsFHaFqSB2VI=</DigestValue></Reference></SignedInfo><SignatureValue>NRV7REVbDRflg616G6gYg0fAGTEw8BhtyPzqaU+kPQI35S1vpgt12VlQ57PkY7Rs0Jucx9npno+bQVMKN2DNhhnzs9qoNY2V3TcdJCcwaMexinHoFXHA0+J6+vR3RWTXhX+iAnfudtKThqbh/mECRLrjyTdy6L+qNkP7sALCWrSVwJVRmzkTOUF8zG4AKY9dQziec94Zv4S7G3cFgj/i7ok2DfBi7AEMCu1lh3dsQAMDeCvt7binhIH2D2ad3iCfYyifDGJ2ncn9hIyxrEiBdS8hZzWijcLs6+HQhVaz9yhZL9u/ZxSRaisXClMdqrLFjUghJ82sVfgQdp7SF165+Q==</SignatureValue><KeyInfo><X509Data><X509Certificate>MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</X509Certificate></X509Data></KeyInfo></Signature><Subject><NameID Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:persistent"">RrX3SPSxDw6z4KHaKB2V_mnv0G-LbRZdYvo1RQa1L7s</NameID><SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer"" /></Subject><Conditions NotBefore=""2017-03-20T15:47:31.957Z"" NotOnOrAfter=""2017-03-20T16:47:31.957Z""><AudienceRestriction><Audience>spn:fe78e0b4-6fe7-47e6-812c-fb75cee266a4</Audience></AudienceRestriction></Conditions><AttributeStatement><Attribute Name=""http://schemas.microsoft.com/identity/claims/tenantid""><AttributeValue>add29489-7269-41f4-8841-b63c95564420</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/identity/claims/objectidentifier""><AttributeValue>d1ad9ce7-b322-4221-ab74-1e1011e1bbcb</AttributeValue></Attribute><Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name""><AttributeValue>User1@Cyrano.onmicrosoft.com</AttributeValue></Attribute><Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname""><AttributeValue>1</AttributeValue></Attribute><Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname""><AttributeValue>User</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/identity/claims/displayname""><AttributeValue>User1</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/identity/claims/identityprovider""><AttributeValue>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</AttributeValue></Attribute></AttributeStatement><AuthnStatement AuthnInstant=""2017-03-20T15:52:31.551Z""><AuthnContext><AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</AuthnContextClassRef></AuthnContext></AuthnStatement></Assertion>"; }
        }

        #endregion

        #region KeyInfo

        public static KeyInfoTestSet KeyInfoWrongElement
        {
            get
            {
                return new KeyInfoTestSet
                {
                    Xml = XmlGenerator.Generate(Default.KeyInfo).Replace("<KeyInfo", "<NotKeyInfo>").Replace("/KeyInfo>", "/NotKeyInfo>"),
                    KeyInfo = Default.KeyInfo
                };
            }
        }

        public static KeyInfoTestSet KeyInfoWrongNameSpace
        {
            get
            {
                return new KeyInfoTestSet
                {
                    Xml = XmlGenerator.Generate(Default.KeyInfo).Replace(XmlSignatureConstants.Namespace, $"_{XmlSignatureConstants.Namespace}_"),
                    KeyInfo = Default.KeyInfo
                };
            }
        }

        public static KeyInfoTestSet KeyInfoSingleCertificate
        {
            get
            {
                return new KeyInfoTestSet
                {
                    Xml = XmlGenerator.Generate(Default.KeyInfo),
                    KeyInfo = Default.KeyInfo
                };
            }
        }

        public static KeyInfoTestSet KeyInfoSingleIssuerSerial
        {
            get
            {
                return new KeyInfoTestSet
                {
                    Xml = @"<KeyInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                                <X509Data>
                                   <X509IssuerSerial>
                                     <X509IssuerName>CN=TAMURA Kent, OU=TRL, O=IBM, L=Yamato-shi, ST=Kanagawa, C=JP</X509IssuerName>
                                     <X509SerialNumber>12345678</X509SerialNumber>
                                   </X509IssuerSerial>
                                </X509Data>
                            </KeyInfo>",
                    KeyInfo = new KeyInfo
                    {
                        IssuerName = "CN=TAMURA Kent, OU=TRL, O=IBM, L=Yamato-shi, ST=Kanagawa, C=JP",
                        Kid = "12345678",
                        SerialNumber = "12345678",
                    }
                };
            }
        }

        public static KeyInfoTestSet KeyInfoSingleSKI
        {
            get
            {
                return new KeyInfoTestSet
                {
                    Xml = @"<KeyInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                                <X509Data>
                                    <X509SKI>31d97bd7</X509SKI>
                                </X509Data>
                            </KeyInfo>",
                    KeyInfo = new KeyInfo
                    {
                        Kid = "31d97bd7",
                        SKI = "31d97bd7"
                    }
                };
            }
        }

        public static KeyInfoTestSet KeyInfoSingleSubjectName
        {
            get
            {
                var keyinfo = new KeyInfo
                {
                    Kid = "X509SubjectName",
                    SubjectName = "X509SubjectName"
                };

                return new KeyInfoTestSet
                {
                    KeyInfo = keyinfo,
                    Xml = XmlGenerator.Generate(keyinfo)
                };
            }
        }

        public static KeyInfoTestSet KeyInfoMultipleCertificates
        {
            get
            {
                return new KeyInfoTestSet
                {
                    KeyInfo = Default.KeyInfo,
                    Xml = XmlGenerator.KeyInfoXml(
                        "http://www.w3.org/2000/09/xmldsig#",
                        new XmlEement("X509Data", new List<XmlEement>
                        {
                           new XmlEement("X509Certificate", Default.CertificateData),
                           new XmlEement("X509Certificate", Default.CertificateData)
                        }))
                };
            }
        }

        public static KeyInfoTestSet KeyInfoMultipleIssuerSerial
        {
            get
            {
                return new KeyInfoTestSet
                {
                    Xml = @"<KeyInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                                <X509Data>
                                   <X509IssuerSerial>
                                     <X509IssuerName>CN=TAMURA Kent, OU=TRL, O=IBM, L=Yamato-shi, ST=Kanagawa, C=JP</X509IssuerName>
                                     <X509SerialNumber>12345678</X509SerialNumber>
                                   </X509IssuerSerial>
                                   <X509IssuerSerial>
                                     <X509IssuerName>CN=TAMURA Kent, OU=TRL, O=IBM, L=Yamato-shi, ST=Kanagawa, C=JP</X509IssuerName>
                                     <X509SerialNumber>12345678</X509SerialNumber>
                                   </X509IssuerSerial>
                                </X509Data>
                            </KeyInfo>"
                };
            }
        }

        public static KeyInfoTestSet KeyInfoMultipleSKI
        {
            get
            {
                return new KeyInfoTestSet
                {
                    Xml = @"<KeyInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                                <X509Data>
                                    <X509SKI>31d97bd7</X509SKI>
                                    <X509SKI>31d97bd7</X509SKI>
                                </X509Data>
                            </KeyInfo>"
                };
            }
        }

        public static KeyInfoTestSet KeyInfoMultipleSubjectName
        {
            get
            {
                return new KeyInfoTestSet
                {
                    Xml = @"<KeyInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                                <X509Data>
                                    <X509SubjectName>X509SubjectName</X509SubjectName>
                                    <X509SubjectName>X509SubjectName</X509SubjectName>
                                </X509Data>
                            </KeyInfo>"
                };
            }
        }

        public static KeyInfoTestSet KeyInfoWithWhitespace
        {
            get
            {
                return new KeyInfoTestSet
                {
                    Xml = @"<KeyInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"">

                                <X509Data>

                                    <X509Certificate>MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</X509Certificate>

                                </X509Data>

                            </KeyInfo>",
                    KeyInfo = new KeyInfo
                    {
                        CertificateData = "MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD",
                        Kid = "6B740DD01652EECE2737E05DAE36C5D18FCB74C3"
                    }
                };
            }
        }

        public static KeyInfoTestSet KeyInfoWithUnknownX509DataElements
        {
            get
            {
                return new KeyInfoTestSet
                {
                    Xml = @"<KeyInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                                <X509Data>
                                    <Unknown>MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</Unknown>
                                    <X509Certificate>MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</X509Certificate>
                                </X509Data>
                            </KeyInfo>",
                    KeyInfo = new KeyInfo
                    {
                        CertificateData = "MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD",
                        Kid = "6B740DD01652EECE2737E05DAE36C5D18FCB74C3"
                    }
                };
            }
        }

        public static KeyInfoTestSet KeyInfoWithAllElements
        {
            get
            {
                return new KeyInfoTestSet
                {
                    Xml = @"<KeyInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                                <RetrievalMethod URI = ""http://RetrievalMethod"" >some info </RetrievalMethod>
                                <X509Data>
                                    <X509Certificate>MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</X509Certificate>
                                    <X509IssuerSerial>
                                        <X509IssuerName>CN=TAMURA Kent, OU=TRL, O=IBM, L=Yamato-shi, ST=Kanagawa, C=JP</X509IssuerName>
                                        <X509SerialNumber>12345678</X509SerialNumber>
                                    </X509IssuerSerial>
                                    <X509SKI>31d97bd7</X509SKI>
                                    <X509SubjectName>X509SubjectName</X509SubjectName>
                                </X509Data>
                            </KeyInfo>",
                    KeyInfo = new KeyInfo
                    {
                        CertificateData = "MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD",
                        IssuerName = "CN=TAMURA Kent, OU=TRL, O=IBM, L=Yamato-shi, ST=Kanagawa, C=JP",
                        Kid = "X509SubjectName",
                        RetrievalMethodUri = "http://RetrievalMethod",
                        SerialNumber = "12345678",
                        SKI = "31d97bd7",
                        SubjectName = "X509SubjectName",
                    }
                };
            }
        }

        public static KeyInfoTestSet KeyInfoWithUnknownElements
        {
            get
            {
                return new KeyInfoTestSet
                {
                    Xml = @"<KeyInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                                <UnknownElement>some data</UnknownElement>
                                <RetrievalMethod URI = ""http://RetrievalMethod"" >some info </RetrievalMethod>
                                <X509Data>
                                    <X509Certificate>MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</X509Certificate>
                                    <X509IssuerSerial>
                                        <X509IssuerName>CN=TAMURA Kent, OU=TRL, O=IBM, L=Yamato-shi, ST=Kanagawa, C=JP</X509IssuerName>
                                        <X509SerialNumber>12345678</X509SerialNumber>
                                    </X509IssuerSerial>
                                    <X509SKI>31d97bd7</X509SKI>
                                    <X509SubjectName>X509SubjectName</X509SubjectName>
                                </X509Data>
                            </KeyInfo>",
                    KeyInfo = new KeyInfo
                    {
                        CertificateData = "MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD",
                        IssuerName = "CN=TAMURA Kent, OU=TRL, O=IBM, L=Yamato-shi, ST=Kanagawa, C=JP",
                        Kid = "X509SubjectName",
                        RetrievalMethodUri = "http://RetrievalMethod",
                        SerialNumber = "12345678",
                        SKI = "31d97bd7",
                        SubjectName = "X509SubjectName",
                    }
                };
            }
        }

        public static KeyInfoTestSet KeyInfoMalformedCertificate
        {
            get
            {
                return new KeyInfoTestSet
                {
                    Xml = @"<KeyInfo xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                                <X509Data>
                                    <X509Certificate>%%MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</X509Certificate>
                                </X509Data>
                            </KeyInfo>",
                    KeyInfo = new KeyInfo
                    {
                        CertificateData = "MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD",
                        Kid = "6B740DD01652EECE2737E05DAE36C5D18FCB74C3"
                    }
                };
            }
        }
        #endregion

        #region Saml
#region SamlAction
        public static SamlActionTestSet SamlActionMissValue
        {
            get
            {
                return new SamlActionTestSet
                {
                    Action = Default.SamlAction,
                    Xml = XmlGenerator.SamlActionXml(SamlConstants.Namespace, null, null)
                };
            }
        }

        public static SamlActionTestSet SamlActionNoNamespace
        {
            get
            {
                return new SamlActionTestSet
                {
                    Action = Default.SamlAction,
                    Xml = XmlGenerator.SamlActionXml(SamlConstants.Namespace, null, Default.SamlAction.Value)
                };
            }
        }

        public static SamlActionTestSet SamlActionInvalidNamespace
        {
            get
            {
                return new SamlActionTestSet
                {
                    Action = Default.SamlAction,
                    Xml = XmlGenerator.SamlActionXml(SamlConstants.Namespace, "namespace", Default.SamlAction.Value)
                };
            }
        }

        public static SamlActionTestSet SamlActionValid
        {
            get
            {
                return new SamlActionTestSet
                {
                    Action = Default.SamlAction,
                    Xml = XmlGenerator.SamlActionXml(SamlConstants.Namespace, Default.SamlAction.Namespace.ToString(), Default.SamlAction.Value)
                };
            }
        }
        #endregion

        #region SamlAudienceRestrictionCondition
        public static SamlAudienceRestrictionConditionTestSet SamlAudienceRestrictionConditionNoAudience
        {
            get
            {
                return new SamlAudienceRestrictionConditionTestSet
                {
                    SamlAudienceRestrictionCondition = Default.SamlAudienceRestrictionConditionSingleAudience,
                    Xml = XmlGenerator.SamlAudienceRestrictionConditionXml(new string[] { })
                };
            }
        }

        public static SamlAudienceRestrictionConditionTestSet SamlAudienceRestrictionConditionEmptyAudience
        {
            get
            {
                string[] audiences = new string[]
                    {
                        XmlGenerator.SamlAudienceXml(string.Empty)
                    };

                return new SamlAudienceRestrictionConditionTestSet
                {
                    SamlAudienceRestrictionCondition = Default.SamlAudienceRestrictionConditionSingleAudience,
                    Xml = XmlGenerator.SamlAudienceRestrictionConditionXml(audiences)
                };
            }
        }

        public static SamlAudienceRestrictionConditionTestSet SamlAudienceRestrictionConditionInvaidElement
        {
            get
            {
                return new SamlAudienceRestrictionConditionTestSet
                {
                    SamlAudienceRestrictionCondition = Default.SamlAudienceRestrictionConditionSingleAudience,
                    Xml = XmlGenerator.SamlAudienceRestrictionConditionXml(new string[] { XmlGenerator.SamlActionXml(null, null, null) })
                };
            }
        }

        public static SamlAudienceRestrictionConditionTestSet SamlAudienceRestrictionConditionSingleAudience
        {
            get
            {
                string[] audiences = new string[]
                    {
                        XmlGenerator.SamlAudienceXml(Default.Audience)
                    };

                return new SamlAudienceRestrictionConditionTestSet
                {
                    SamlAudienceRestrictionCondition = Default.SamlAudienceRestrictionConditionSingleAudience,
                    Xml = XmlGenerator.SamlAudienceRestrictionConditionXml(audiences)
                };
            }
        }

        public static SamlAudienceRestrictionConditionTestSet SamlAudienceRestrictionConditionMultiAudience
        {
            get
            {
                List<string> audiences = new List<string>();
                foreach (var audience in Default.Audiences)
                {
                    audiences.Add(XmlGenerator.SamlAudienceXml(audience));
                }

                return new SamlAudienceRestrictionConditionTestSet
                {
                    SamlAudienceRestrictionCondition = Default.SamlAudienceRestrictionConditionMultiAudience,
                    Xml = XmlGenerator.SamlAudienceRestrictionConditionXml(audiences)
                };
            }
        }
        #endregion

        #region SamlConditions
        #endregion

        #endregion

        #region Signature

        public static SignatureTestSet Signature_UnknownDigestAlgorithm
        {
            get
            {
                return new SignatureTestSet
                {
                    Signature = Default.Signature,
                    Xml = XmlGenerator.Generate(Default.Signature).Replace(SecurityAlgorithms.Sha256Digest, $"_{SecurityAlgorithms.Sha256Digest}" )
                };
            }
        }

        public static SignatureTestSet Signature_UnknownSignatureAlgorithm
        {
            get
            {
                return new SignatureTestSet
                {
                    Signature = Default.Signature,
                    Xml = XmlGenerator.Generate(Default.Signature).Replace(SecurityAlgorithms.RsaSha256Signature, $"_{SecurityAlgorithms.RsaSha256Signature}" )
                };
            }
        }

        #endregion

        #region SignInfo

        public static SignedInfoTestSet SignInfoStartsWithWhiteSpace
        {
            get
            {
                return new SignedInfoTestSet
                {
                    SignedInfo = Default.SignedInfo,
                    Xml = "       " + XmlGenerator.Generate(Default.SignedInfo)
                };
            }
        }

        public static SignedInfoTestSet SignedInfoCanonicalizationMethodMissing
        {
            get
            {
                return new SignedInfoTestSet
                {
                    Xml = XmlGenerator.Generate(Default.SignedInfo).Replace("CanonicalizationMethod", "_CanonicalizationMethod")
                };
            }
        }

        public static SignedInfoTestSet SignedInfoReferenceMissing
        {
            get
            {
                return new SignedInfoTestSet
                {
                    Xml = XmlGenerator.Generate(Default.SignedInfo).Replace("Reference", "_Reference")
                };
            }
        }

        public static SignedInfoTestSet SignedInfoTransformsMissing
        {
            get
            {
                return new SignedInfoTestSet
                {
                    Xml = XmlGenerator.Generate(Default.SignedInfo).Replace("Transforms", "_Transforms")
                };
            }
        }

        public static SignedInfoTestSet SignedInfoNoTransforms
        {
            get
            {
                return new SignedInfoTestSet
                {
                    Xml = XmlGenerator.Generate(Default.SignedInfo).Replace("Transform", "_Transform")
                };
            }
        }

        public static SignedInfoTestSet SignedInfoUnknownCanonicalizationtMethod
        {
            get
            {
                return new SignedInfoTestSet
                {
                    Xml = XmlGenerator.SignedInfoXml(
                            XmlSignatureConstants.Namespace,
                            SecurityAlgorithms.ExclusiveC14n,
                            SecurityAlgorithms.RsaSha256Signature,
                            XmlGenerator.ReferenceXml(
                                Guid.NewGuid().ToString(),
                                SecurityAlgorithms.Aes256KW,
                                SecurityAlgorithms.EnvelopedSignature,
                                SecurityAlgorithms.Sha256Digest,
                                Guid.NewGuid().ToString()))
                };
            }
        }

        public static SignedInfoTestSet SignedInfoUnknownTransform
        {
            get
            {
                return new SignedInfoTestSet
                {
                    Xml = XmlGenerator.SignedInfoXml(
                            XmlSignatureConstants.Namespace,
                            SecurityAlgorithms.ExclusiveC14n,
                            SecurityAlgorithms.RsaSha256Signature,
                            XmlGenerator.ReferenceXml(
                                "#_d60bd9ed-8aab-40c8-ba5f-f548c3401ae2",
                                "_http://www.w3.org/2000/09/xmldsig#enveloped-signature",
                                SecurityAlgorithms.ExclusiveC14n,
                                SecurityAlgorithms.Sha256Digest,
                                "Ytfkc60mLe1Zgu7TBQpMv8nJ1SVxT0ZjsFHaFqSB2VI="))

                };
            }
        }

        public static SignedInfoTestSet SignedInfoMissingDigestMethod
        {
            get
            {
                return new SignedInfoTestSet
                {
                    Xml = XmlGenerator.Generate(Default.SignedInfo).Replace("DigestMethod", "_DigestMethod")
                };
            }
        }

        public static SignedInfoTestSet SignedInfoMissingDigestValue
        {
            get
            {
                return new SignedInfoTestSet
                {
                    Xml = XmlGenerator.Generate(Default.SignedInfo).Replace("DigestValue", "_DigestValue")
                };
            }
        }

        public static SignedInfoTestSet SignedInfoValid
        {
            get
            {
                return new SignedInfoTestSet
                {
                    SignedInfo = Default.SignedInfo,
                    Xml = XmlGenerator.Generate(Default.SignedInfo)
                };
            }
        }

        #endregion

        #region Wresult

        public static string WResult_Saml2_Valid
        {
            get
            {
                return @"<t:RequestSecurityTokenResponse xmlns:t=""http://schemas.xmlsoap.org/ws/2005/02/trust""><t:Lifetime><wsu:Created xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">2017-04-23T16:11:17.348Z</wsu:Created><wsu:Expires xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">2017-04-23T17:11:17.348Z</wsu:Expires></t:Lifetime><wsp:AppliesTo xmlns:wsp=""http://schemas.xmlsoap.org/ws/2004/09/policy""><wsa:EndpointReference xmlns:wsa=""http://www.w3.org/2005/08/addressing""><wsa:Address>spn:fe78e0b4-6fe7-47e6-812c-fb75cee266a4</wsa:Address></wsa:EndpointReference></wsp:AppliesTo><t:RequestedSecurityToken><Assertion ID=""_edc15efd-1117-4bf9-89da-28b1663fb890"" IssueInstant=""2017-04-23T16:16:17.348Z"" Version=""2.0"" xmlns=""urn:oasis:names:tc:SAML:2.0:assertion""><Issuer>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</Issuer><Signature xmlns=""http://www.w3.org/2000/09/xmldsig#""><SignedInfo><CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" /><SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"" /><Reference URI=""#_edc15efd-1117-4bf9-89da-28b1663fb890""><Transforms><Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" /><Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" /></Transforms><DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"" /><DigestValue>DO8QQoO629ApWPV3LiY2epQSv+I82iChybeRrXbhgtw=</DigestValue></Reference></SignedInfo><SignatureValue>O8JNyVKm9I7kMqlsaBgLCNwHA0qdXv34YHBVfg217lgeKkMC5taLU/EH7UeeMtapU6zMafcYoCH+Bp9zoqDpflgs78Hkjgn/dEUtjPFn7211VXClcTNqk+yhqXWtu6SKrabeIhKCKtoMA9lUAB4D6ABesb6MpwbM/ULq7T16tycZ3X//iXHeOiMwNiUAePYF22fmgrqRSDRHyLPtiLskP4UMksWJBrXUV96e9EU9aEciCvYpzMDv/VFUOCLiEkBqCdAtPVwVun+5eRk9zEh6qscWi0kAgFl3W3JhugcTTuGQYHXYVIHxbd5O33MwFIMUOmGrI1EXuk+cHIq2KUtSLg==</SignatureValue><KeyInfo><X509Data><X509Certificate>MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</X509Certificate></X509Data></KeyInfo></Signature><Subject><NameID Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:persistent"">RrX3SPSxDw6z4KHaKB2V_mnv0G-LbRZdYvo1RQa1L7s</NameID><SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer"" /></Subject><Conditions NotBefore=""2017-04-23T16:11:17.348Z"" NotOnOrAfter=""2017-04-23T17:11:17.348Z""><AudienceRestriction><Audience>spn:fe78e0b4-6fe7-47e6-812c-fb75cee266a4</Audience></AudienceRestriction></Conditions><AttributeStatement><Attribute Name=""http://schemas.microsoft.com/identity/claims/tenantid""><AttributeValue>add29489-7269-41f4-8841-b63c95564420</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/identity/claims/objectidentifier""><AttributeValue>d1ad9ce7-b322-4221-ab74-1e1011e1bbcb</AttributeValue></Attribute><Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name""><AttributeValue>User1@Cyrano.onmicrosoft.com</AttributeValue></Attribute><Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname""><AttributeValue>1</AttributeValue></Attribute><Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname""><AttributeValue>User</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/identity/claims/displayname""><AttributeValue>User1</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/identity/claims/identityprovider""><AttributeValue>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/claims/authnmethodsreferences""><AttributeValue>http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/password</AttributeValue></Attribute></AttributeStatement><AuthnStatement AuthnInstant=""2017-04-23T16:16:17.270Z""><AuthnContext><AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</AuthnContextClassRef></AuthnContext></AuthnStatement></Assertion></t:RequestedSecurityToken><t:RequestedAttachedReference><SecurityTokenReference d3p1:TokenType=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0"" xmlns:d3p1=""http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd"" xmlns=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd""><KeyIdentifier ValueType=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID"">_edc15efd-1117-4bf9-89da-28b1663fb890</KeyIdentifier></SecurityTokenReference></t:RequestedAttachedReference><t:RequestedUnattachedReference><SecurityTokenReference d3p1:TokenType=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0"" xmlns:d3p1=""http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd"" xmlns=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd""><KeyIdentifier ValueType=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID"">_edc15efd-1117-4bf9-89da-28b1663fb890</KeyIdentifier></SecurityTokenReference></t:RequestedUnattachedReference><t:TokenType>http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0</t:TokenType><t:RequestType>http://schemas.xmlsoap.org/ws/2005/02/trust/Issue</t:RequestType><t:KeyType>http://schemas.xmlsoap.org/ws/2005/05/identity/NoProofKey</t:KeyType></t:RequestSecurityTokenResponse>";
            }
        }

        public static string WResult_Saml2_Valid_Formated
        {
            get
            {
                return @"<t:RequestSecurityTokenResponse xmlns:t=""http://schemas.xmlsoap.org/ws/2005/02/trust"">
                            <t:Lifetime>
                                <wsu:Created xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">2017-04-23T16:11:17.348Z</wsu:Created>
                                <wsu:Expires xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">2017-04-23T17:11:17.348Z</wsu:Expires>
                            </t:Lifetime>
                            <wsp:AppliesTo xmlns:wsp=""http://schemas.xmlsoap.org/ws/2004/09/policy"">
                                <wsa:EndpointReference xmlns:wsa=""http://www.w3.org/2005/08/addressing"">
                                <wsa:Address>spn:fe78e0b4-6fe7-47e6-812c-fb75cee266a4</wsa:Address></wsa:EndpointReference>
                            </wsp:AppliesTo>
                            <t:RequestedSecurityToken>
                                <Assertion ID=""_edc15efd-1117-4bf9-89da-28b1663fb890"" IssueInstant=""2017-04-23T16:16:17.348Z"" Version=""2.0"" xmlns=""urn:oasis:names:tc:SAML:2.0:assertion"">
                                    <Issuer>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</Issuer>
                                    <Signature xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                                        <SignedInfo>
                                            <CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" />
                                            <SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"" />
                                            <Reference URI=""#_edc15efd-1117-4bf9-89da-28b1663fb890"">
                                                <Transforms>
                                                    <Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" />
                                                    <Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" />
                                                </Transforms><DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"" />
                                                <DigestValue>DO8QQoO629ApWPV3LiY2epQSv+I82iChybeRrXbhgtw=</DigestValue>
                                            </Reference>
                                        </SignedInfo>
                                        <SignatureValue>O8JNyVKm9I7kMqlsaBgLCNwHA0qdXv34YHBVfg217lgeKkMC5taLU/EH7UeeMtapU6zMafcYoCH+Bp9zoqDpflgs78Hkjgn/dEUtjPFn7211VXClcTNqk+yhqXWtu6SKrabeIhKCKtoMA9lUAB4D6ABesb6MpwbM/ULq7T16tycZ3X//iXHeOiMwNiUAePYF22fmgrqRSDRHyLPtiLskP4UMksWJBrXUV96e9EU9aEciCvYpzMDv/VFUOCLiEkBqCdAtPVwVun+5eRk9zEh6qscWi0kAgFl3W3JhugcTTuGQYHXYVIHxbd5O33MwFIMUOmGrI1EXuk+cHIq2KUtSLg==</SignatureValue>
                                        <KeyInfo>
                                            <X509Data>
                                                <X509Certificate>MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</X509Certificate>
                                            </X509Data>
                                        </KeyInfo>
                                    </Signature>
                                    <Subject>
                                        <NameID Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:persistent"">RrX3SPSxDw6z4KHaKB2V_mnv0G-LbRZdYvo1RQa1L7s</NameID>
                                        <SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer"" />
                                    </Subject>
                                    <Conditions NotBefore=""2017-04-23T16:11:17.348Z"" NotOnOrAfter=""2017-04-23T17:11:17.348Z"">
                                        <AudienceRestriction>
                                            <Audience>spn:fe78e0b4-6fe7-47e6-812c-fb75cee266a4</Audience>
                                        </AudienceRestriction>
                                    </Conditions>
                                    <AttributeStatement>
                                        <Attribute Name=""http://schemas.microsoft.com/identity/claims/tenantid""><AttributeValue>add29489-7269-41f4-8841-b63c95564420</AttributeValue></Attribute>
                                        <Attribute Name=""http://schemas.microsoft.com/identity/claims/objectidentifier""><AttributeValue>d1ad9ce7-b322-4221-ab74-1e1011e1bbcb</AttributeValue></Attribute>
                                        <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name""><AttributeValue>User1@Cyrano.onmicrosoft.com</AttributeValue></Attribute>
                                        <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname""><AttributeValue>1</AttributeValue></Attribute>
                                        <Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname""><AttributeValue>User</AttributeValue></Attribute>
                                        <Attribute Name=""http://schemas.microsoft.com/identity/claims/displayname""><AttributeValue>User1</AttributeValue></Attribute>
                                        <Attribute Name=""http://schemas.microsoft.com/identity/claims/identityprovider""><AttributeValue>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</AttributeValue></Attribute>
                                        <Attribute Name=""http://schemas.microsoft.com/claims/authnmethodsreferences""><AttributeValue>http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/password</AttributeValue></Attribute>
                                    </AttributeStatement>
                                    <AuthnStatement AuthnInstant=""2017-04-23T16:16:17.270Z"">
                                        <AuthnContext>
                                            <AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</AuthnContextClassRef>
                                        </AuthnContext>
                                    </AuthnStatement>
                                </Assertion>
                            </t:RequestedSecurityToken>
                            <t:RequestedAttachedReference>
                                <SecurityTokenReference d3p1:TokenType=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0"" xmlns:d3p1=""http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd"" xmlns=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"">
                                    <KeyIdentifier ValueType=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID"">_edc15efd-1117-4bf9-89da-28b1663fb890</KeyIdentifier>
                                </SecurityTokenReference>
                            </t:RequestedAttachedReference>
                            <t:RequestedUnattachedReference>
                                <SecurityTokenReference d3p1:TokenType=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0"" xmlns:d3p1=""http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd"" xmlns=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"">
                                    <KeyIdentifier ValueType=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID"">_edc15efd-1117-4bf9-89da-28b1663fb890</KeyIdentifier>
                                </SecurityTokenReference>
                            </t:RequestedUnattachedReference>
                            <t:TokenType>http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0</t:TokenType>
                            <t:RequestType>http://schemas.xmlsoap.org/ws/2005/02/trust/Issue</t:RequestType>
                            <t:KeyType>http://schemas.xmlsoap.org/ws/2005/05/identity/NoProofKey</t:KeyType>
                        </t:RequestSecurityTokenResponse>";
            }
        }

        public static string WResult_Saml2_Valid_With_Spaces
        {
            get
            {
                return @"<t:RequestSecurityTokenResponse xmlns:t=""http://schemas.xmlsoap.org/ws/2005/02/trust"">
                             
                             <t:Lifetime><wsu:Created xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">2017-04-23T16:11:17.348Z</wsu:Created><wsu:Expires xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd"">2017-04-23T17:11:17.348Z</wsu:Expires></t:Lifetime><wsp:AppliesTo xmlns:wsp=""http://schemas.xmlsoap.org/ws/2004/09/policy""><wsa:EndpointReference xmlns:wsa=""http://www.w3.org/2005/08/addressing""><wsa:Address>spn:fe78e0b4-6fe7-47e6-812c-fb75cee266a4</wsa:Address></wsa:EndpointReference></wsp:AppliesTo>
                             
                             <t:RequestedSecurityToken><Assertion ID=""_edc15efd-1117-4bf9-89da-28b1663fb890"" IssueInstant=""2017-04-23T16:16:17.348Z"" Version=""2.0"" xmlns=""urn:oasis:names:tc:SAML:2.0:assertion""><Issuer>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</Issuer><Signature xmlns=""http://www.w3.org/2000/09/xmldsig#""><SignedInfo><CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" /><SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"" /><Reference URI=""#_edc15efd-1117-4bf9-89da-28b1663fb890""><Transforms><Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" /><Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" /></Transforms><DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"" /><DigestValue>DO8QQoO629ApWPV3LiY2epQSv+I82iChybeRrXbhgtw=</DigestValue></Reference></SignedInfo><SignatureValue>O8JNyVKm9I7kMqlsaBgLCNwHA0qdXv34YHBVfg217lgeKkMC5taLU/EH7UeeMtapU6zMafcYoCH+Bp9zoqDpflgs78Hkjgn/dEUtjPFn7211VXClcTNqk+yhqXWtu6SKrabeIhKCKtoMA9lUAB4D6ABesb6MpwbM/ULq7T16tycZ3X//iXHeOiMwNiUAePYF22fmgrqRSDRHyLPtiLskP4UMksWJBrXUV96e9EU9aEciCvYpzMDv/VFUOCLiEkBqCdAtPVwVun+5eRk9zEh6qscWi0kAgFl3W3JhugcTTuGQYHXYVIHxbd5O33MwFIMUOmGrI1EXuk+cHIq2KUtSLg==</SignatureValue><KeyInfo><X509Data><X509Certificate>MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</X509Certificate></X509Data></KeyInfo></Signature><Subject><NameID Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:persistent"">RrX3SPSxDw6z4KHaKB2V_mnv0G-LbRZdYvo1RQa1L7s</NameID><SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer"" /></Subject><Conditions NotBefore=""2017-04-23T16:11:17.348Z"" NotOnOrAfter=""2017-04-23T17:11:17.348Z""><AudienceRestriction><Audience>spn:fe78e0b4-6fe7-47e6-812c-fb75cee266a4</Audience></AudienceRestriction></Conditions><AttributeStatement><Attribute Name=""http://schemas.microsoft.com/identity/claims/tenantid""><AttributeValue>add29489-7269-41f4-8841-b63c95564420</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/identity/claims/objectidentifier""><AttributeValue>d1ad9ce7-b322-4221-ab74-1e1011e1bbcb</AttributeValue></Attribute><Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name""><AttributeValue>User1@Cyrano.onmicrosoft.com</AttributeValue></Attribute><Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname""><AttributeValue>1</AttributeValue></Attribute><Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname""><AttributeValue>User</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/identity/claims/displayname""><AttributeValue>User1</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/identity/claims/identityprovider""><AttributeValue>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/claims/authnmethodsreferences""><AttributeValue>http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/password</AttributeValue></Attribute></AttributeStatement><AuthnStatement AuthnInstant=""2017-04-23T16:16:17.270Z""><AuthnContext><AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</AuthnContextClassRef></AuthnContext></AuthnStatement></Assertion></t:RequestedSecurityToken><t:RequestedAttachedReference><SecurityTokenReference d3p1:TokenType=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0"" xmlns:d3p1=""http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd"" xmlns=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd""><KeyIdentifier ValueType=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID"">_edc15efd-1117-4bf9-89da-28b1663fb890</KeyIdentifier></SecurityTokenReference></t:RequestedAttachedReference><t:RequestedUnattachedReference><SecurityTokenReference d3p1:TokenType=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0"" xmlns:d3p1=""http://docs.oasis-open.org/wss/oasis-wss-wssecurity-secext-1.1.xsd"" xmlns=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd""><KeyIdentifier ValueType=""http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLID"">_edc15efd-1117-4bf9-89da-28b1663fb890</KeyIdentifier></SecurityTokenReference></t:RequestedUnattachedReference><t:TokenType>http://docs.oasis-open.org/wss/oasis-wss-saml-token-profile-1.1#SAMLV2.0</t:TokenType><t:RequestType>http://schemas.xmlsoap.org/ws/2005/02/trust/Issue</t:RequestType><t:KeyType>http://schemas.xmlsoap.org/ws/2005/05/identity/NoProofKey</t:KeyType>

                         </t:RequestSecurityTokenResponse>";
            }
        }

        public static string WResult_Saml2_Missing_RequestedSecurityTokenResponse
        {
            get
            {
                return @"<t:_RequestSecurityTokenResponse xmlns:t=""http://schemas.xmlsoap.org/ws/2005/02/trust""></t:_RequestSecurityTokenResponse>";
            }
        }

        public static string WResult_Saml2_Missing_RequestedSecurityToken
        {
            get
            {
                return @"<t:RequestSecurityTokenResponse xmlns:t=""http://schemas.xmlsoap.org/ws/2005/02/trust"">
                            <t:_RequestedSecurityToken></t:_RequestedSecurityToken>
                         </t:RequestSecurityTokenResponse>";
            }
        }

        public static string WaSignIn_Valid
        {
            get
            {
                return @"wa=wsignin1.0&wresult=%3Ct%3ARequestSecurityTokenResponse+xmlns%3At%3D%22http%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2005%2F02%2Ftrust%22%3E%3Ct%3ALifetime%3E%3Cwsu%3ACreated+xmlns%3Awsu%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2F2004%2F01%2Foasis-200401-wss-wssecurity-utility-1.0.xsd%22%3E2017-04-23T17%3A40%3A36.882Z%3C%2Fwsu%3ACreated%3E%3Cwsu%3AExpires+xmlns%3Awsu%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2F2004%2F01%2Foasis-200401-wss-wssecurity-utility-1.0.xsd%22%3E2017-04-23T18%3A40%3A36.882Z%3C%2Fwsu%3AExpires%3E%3C%2Ft%3ALifetime%3E%3Cwsp%3AAppliesTo+xmlns%3Awsp%3D%22http%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2004%2F09%2Fpolicy%22%3E%3Cwsa%3AEndpointReference+xmlns%3Awsa%3D%22http%3A%2F%2Fwww.w3.org%2F2005%2F08%2Faddressing%22%3E%3Cwsa%3AAddress%3Espn%3Afe78e0b4-6fe7-47e6-812c-fb75cee266a4%3C%2Fwsa%3AAddress%3E%3C%2Fwsa%3AEndpointReference%3E%3C%2Fwsp%3AAppliesTo%3E%3Ct%3ARequestedSecurityToken%3E%3CAssertion+ID%3D%22_710d4516-27ac-4547-816d-3947aeea6edf%22+IssueInstant%3D%222017-04-23T17%3A45%3A36.882Z%22+Version%3D%222.0%22+xmlns%3D%22urn%3Aoasis%3Anames%3Atc%3ASAML%3A2.0%3Aassertion%22%3E%3CIssuer%3Ehttps%3A%2F%2Fsts.windows.net%2Fadd29489-7269-41f4-8841-b63c95564420%2F%3C%2FIssuer%3E%3CSignature+xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2F09%2Fxmldsig%23%22%3E%3CSignedInfo%3E%3CCanonicalizationMethod+Algorithm%3D%22http%3A%2F%2Fwww.w3.org%2F2001%2F10%2Fxml-exc-c14n%23%22+%2F%3E%3CSignatureMethod+Algorithm%3D%22http%3A%2F%2Fwww.w3.org%2F2001%2F04%2Fxmldsig-more%23rsa-sha256%22+%2F%3E%3CReference+URI%3D%22%23_710d4516-27ac-4547-816d-3947aeea6edf%22%3E%3CTransforms%3E%3CTransform+Algorithm%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2F09%2Fxmldsig%23enveloped-signature%22+%2F%3E%3CTransform+Algorithm%3D%22http%3A%2F%2Fwww.w3.org%2F2001%2F10%2Fxml-exc-c14n%23%22+%2F%3E%3C%2FTransforms%3E%3CDigestMethod+Algorithm%3D%22http%3A%2F%2Fwww.w3.org%2F2001%2F04%2Fxmlenc%23sha256%22+%2F%3E%3CDigestValue%3Ekv1eRd%2BMFFaevhwOeeJa5BLkLRV3tFB5QKVD8JBOgMg%3D%3C%2FDigestValue%3E%3C%2FReference%3E%3C%2FSignedInfo%3E%3CSignatureValue%3EABAzMsMPGlKCA0HcmAAiCFwMZr0gtKMUQpRUTCX8YaLAIxOgIW3ZBMTwPHKa2K2lp1Tk97oQBE3S%2Bfg7TKriP1bZB8bdPpfu4GxeeYQteWT7dD%2FJPy1SYCMiRMSsNO4T3O6Keaci1pzwIxzrH8S5s7gzBFPgljf5mHBtZrCTQIK7Ng%2Fnk%2BSpea3RXew05yAQX%2B14Eq6IGPIBsFJidjLqyoslK4OZMNtHLF443AfLs4Ltwfaf89QuOXDpIkKaaHb98uh9XlhB8IMcqzZ1hi2kSadoO7drtOppmeFgYcyOidEuwqQ5%2BksxrOwRb%2F88AfSYEjqUM6U2ldrE8RoUdSf3gQ%3D%3D%3C%2FSignatureValue%3E%3CKeyInfo%3E%3CX509Data%3E%3CX509Certificate%3EMIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S%2Fry7iav%2FIICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei%2BIP3sKmCcMX7Ibsg%2BubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy%2BSVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd%2Fuctpner6oc335rvdJikNmc1cFKCK%2B2irew1bgUJHuN%2BLJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr%2FHCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R%2FX4visjceUlv5jVzCn%2FSIq6Gm9%2FwCqtSxYvifRXxwNpQTOyvHhrY%2FIJLRUp2g9%2FfDELYd65t9Dp%2BN8SznhfB6%2FCl7P7FRo99rIlj%2Fq7JXa8UB%2FvLJPDlr%2BNREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es%2BjuQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ%2FrWQ5J%2F9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A%2FzMOQtoD%3C%2FX509Certificate%3E%3C%2FX509Data%3E%3C%2FKeyInfo%3E%3C%2FSignature%3E%3CSubject%3E%3CNameID+Format%3D%22urn%3Aoasis%3Anames%3Atc%3ASAML%3A2.0%3Anameid-format%3Apersistent%22%3ERrX3SPSxDw6z4KHaKB2V_mnv0G-LbRZdYvo1RQa1L7s%3C%2FNameID%3E%3CSubjectConfirmation+Method%3D%22urn%3Aoasis%3Anames%3Atc%3ASAML%3A2.0%3Acm%3Abearer%22+%2F%3E%3C%2FSubject%3E%3CConditions+NotBefore%3D%222017-04-23T17%3A40%3A36.882Z%22+NotOnOrAfter%3D%222017-04-23T18%3A40%3A36.882Z%22%3E%3CAudienceRestriction%3E%3CAudience%3Espn%3Afe78e0b4-6fe7-47e6-812c-fb75cee266a4%3C%2FAudience%3E%3C%2FAudienceRestriction%3E%3C%2FConditions%3E%3CAttributeStatement%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.microsoft.com%2Fidentity%2Fclaims%2Ftenantid%22%3E%3CAttributeValue%3Eadd29489-7269-41f4-8841-b63c95564420%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.microsoft.com%2Fidentity%2Fclaims%2Fobjectidentifier%22%3E%3CAttributeValue%3Ed1ad9ce7-b322-4221-ab74-1e1011e1bbcb%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2005%2F05%2Fidentity%2Fclaims%2Fname%22%3E%3CAttributeValue%3EUser1%40Cyrano.onmicrosoft.com%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2005%2F05%2Fidentity%2Fclaims%2Fsurname%22%3E%3CAttributeValue%3E1%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2005%2F05%2Fidentity%2Fclaims%2Fgivenname%22%3E%3CAttributeValue%3EUser%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.microsoft.com%2Fidentity%2Fclaims%2Fdisplayname%22%3E%3CAttributeValue%3EUser1%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.microsoft.com%2Fidentity%2Fclaims%2Fidentityprovider%22%3E%3CAttributeValue%3Ehttps%3A%2F%2Fsts.windows.net%2Fadd29489-7269-41f4-8841-b63c95564420%2F%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.microsoft.com%2Fclaims%2Fauthnmethodsreferences%22%3E%3CAttributeValue%3Ehttp%3A%2F%2Fschemas.microsoft.com%2Fws%2F2008%2F06%2Fidentity%2Fauthenticationmethod%2Fpassword%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3C%2FAttributeStatement%3E%3CAuthnStatement+AuthnInstant%3D%222017-04-23T16%3A16%3A17.270Z%22%3E%3CAuthnContext%3E%3CAuthnContextClassRef%3Eurn%3Aoasis%3Anames%3Atc%3ASAML%3A2.0%3Aac%3Aclasses%3APassword%3C%2FAuthnContextClassRef%3E%3C%2FAuthnContext%3E%3C%2FAuthnStatement%3E%3C%2FAssertion%3E%3C%2Ft%3ARequestedSecurityToken%3E%3Ct%3ARequestedAttachedReference%3E%3CSecurityTokenReference+d3p1%3ATokenType%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2Foasis-wss-saml-token-profile-1.1%23SAMLV2.0%22+xmlns%3Ad3p1%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2Foasis-wss-wssecurity-secext-1.1.xsd%22+xmlns%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2F2004%2F01%2Foasis-200401-wss-wssecurity-secext-1.0.xsd%22%3E%3CKeyIdentifier+ValueType%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2Foasis-wss-saml-token-profile-1.1%23SAMLID%22%3E_710d4516-27ac-4547-816d-3947aeea6edf%3C%2FKeyIdentifier%3E%3C%2FSecurityTokenReference%3E%3C%2Ft%3ARequestedAttachedReference%3E%3Ct%3ARequestedUnattachedReference%3E%3CSecurityTokenReference+d3p1%3ATokenType%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2Foasis-wss-saml-token-profile-1.1%23SAMLV2.0%22+xmlns%3Ad3p1%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2Foasis-wss-wssecurity-secext-1.1.xsd%22+xmlns%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2F2004%2F01%2Foasis-200401-wss-wssecurity-secext-1.0.xsd%22%3E%3CKeyIdentifier+ValueType%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2Foasis-wss-saml-token-profile-1.1%23SAMLID%22%3E_710d4516-27ac-4547-816d-3947aeea6edf%3C%2FKeyIdentifier%3E%3C%2FSecurityTokenReference%3E%3C%2Ft%3ARequestedUnattachedReference%3E%3Ct%3ATokenType%3Ehttp%3A%2F%2Fdocs.oasis-open.org%2Fwss%2Foasis-wss-saml-token-profile-1.1%23SAMLV2.0%3C%2Ft%3ATokenType%3E%3Ct%3ARequestType%3Ehttp%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2005%2F02%2Ftrust%2FIssue%3C%2Ft%3ARequestType%3E%3Ct%3AKeyType%3Ehttp%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2005%2F05%2Fidentity%2FNoProofKey%3C%2Ft%3AKeyType%3E%3C%2Ft%3ARequestSecurityTokenResponse%3E&wctx=WsFedOwinState%3DZfCHQBMGl9Nia9P6tbsUq5AFCEu9fGolLxTkikMW-zGMhRMsZb6ofrdCD9uni2PoEuW_1zfJPtZawNSjiy4vIg5o2TJeGiwmKjqM0y3bi4w";
            }
        }

        public static WsFederationMessageTestSet WsSignInTestSet
        {
            get
            {
                return new WsFederationMessageTestSet
                {
                    WsFederationMessage = new WsFederationMessage
                    {
                        Wa = WsFederationConstants.WsFederationActions.SignIn,
                        Wresult = Uri.UnescapeDataString(@"%3Ct%3ARequestSecurityTokenResponse+xmlns%3At%3D%22http%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2005%2F02%2Ftrust%22%3E%3Ct%3ALifetime%3E%3Cwsu%3ACreated+xmlns%3Awsu%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2F2004%2F01%2Foasis-200401-wss-wssecurity-utility-1.0.xsd%22%3E2017-04-23T17%3A40%3A36.882Z%3C%2Fwsu%3ACreated%3E%3Cwsu%3AExpires+xmlns%3Awsu%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2F2004%2F01%2Foasis-200401-wss-wssecurity-utility-1.0.xsd%22%3E2017-04-23T18%3A40%3A36.882Z%3C%2Fwsu%3AExpires%3E%3C%2Ft%3ALifetime%3E%3Cwsp%3AAppliesTo+xmlns%3Awsp%3D%22http%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2004%2F09%2Fpolicy%22%3E%3Cwsa%3AEndpointReference+xmlns%3Awsa%3D%22http%3A%2F%2Fwww.w3.org%2F2005%2F08%2Faddressing%22%3E%3Cwsa%3AAddress%3Espn%3Afe78e0b4-6fe7-47e6-812c-fb75cee266a4%3C%2Fwsa%3AAddress%3E%3C%2Fwsa%3AEndpointReference%3E%3C%2Fwsp%3AAppliesTo%3E%3Ct%3ARequestedSecurityToken%3E%3CAssertion+ID%3D%22_710d4516-27ac-4547-816d-3947aeea6edf%22+IssueInstant%3D%222017-04-23T17%3A45%3A36.882Z%22+Version%3D%222.0%22+xmlns%3D%22urn%3Aoasis%3Anames%3Atc%3ASAML%3A2.0%3Aassertion%22%3E%3CIssuer%3Ehttps%3A%2F%2Fsts.windows.net%2Fadd29489-7269-41f4-8841-b63c95564420%2F%3C%2FIssuer%3E%3CSignature+xmlns%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2F09%2Fxmldsig%23%22%3E%3CSignedInfo%3E%3CCanonicalizationMethod+Algorithm%3D%22http%3A%2F%2Fwww.w3.org%2F2001%2F10%2Fxml-exc-c14n%23%22+%2F%3E%3CSignatureMethod+Algorithm%3D%22http%3A%2F%2Fwww.w3.org%2F2001%2F04%2Fxmldsig-more%23rsa-sha256%22+%2F%3E%3CReference+URI%3D%22%23_710d4516-27ac-4547-816d-3947aeea6edf%22%3E%3CTransforms%3E%3CTransform+Algorithm%3D%22http%3A%2F%2Fwww.w3.org%2F2000%2F09%2Fxmldsig%23enveloped-signature%22+%2F%3E%3CTransform+Algorithm%3D%22http%3A%2F%2Fwww.w3.org%2F2001%2F10%2Fxml-exc-c14n%23%22+%2F%3E%3C%2FTransforms%3E%3CDigestMethod+Algorithm%3D%22http%3A%2F%2Fwww.w3.org%2F2001%2F04%2Fxmlenc%23sha256%22+%2F%3E%3CDigestValue%3Ekv1eRd%2BMFFaevhwOeeJa5BLkLRV3tFB5QKVD8JBOgMg%3D%3C%2FDigestValue%3E%3C%2FReference%3E%3C%2FSignedInfo%3E%3CSignatureValue%3EABAzMsMPGlKCA0HcmAAiCFwMZr0gtKMUQpRUTCX8YaLAIxOgIW3ZBMTwPHKa2K2lp1Tk97oQBE3S%2Bfg7TKriP1bZB8bdPpfu4GxeeYQteWT7dD%2FJPy1SYCMiRMSsNO4T3O6Keaci1pzwIxzrH8S5s7gzBFPgljf5mHBtZrCTQIK7Ng%2Fnk%2BSpea3RXew05yAQX%2B14Eq6IGPIBsFJidjLqyoslK4OZMNtHLF443AfLs4Ltwfaf89QuOXDpIkKaaHb98uh9XlhB8IMcqzZ1hi2kSadoO7drtOppmeFgYcyOidEuwqQ5%2BksxrOwRb%2F88AfSYEjqUM6U2ldrE8RoUdSf3gQ%3D%3D%3C%2FSignatureValue%3E%3CKeyInfo%3E%3CX509Data%3E%3CX509Certificate%3EMIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S%2Fry7iav%2FIICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei%2BIP3sKmCcMX7Ibsg%2BubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy%2BSVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd%2Fuctpner6oc335rvdJikNmc1cFKCK%2B2irew1bgUJHuN%2BLJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr%2FHCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R%2FX4visjceUlv5jVzCn%2FSIq6Gm9%2FwCqtSxYvifRXxwNpQTOyvHhrY%2FIJLRUp2g9%2FfDELYd65t9Dp%2BN8SznhfB6%2FCl7P7FRo99rIlj%2Fq7JXa8UB%2FvLJPDlr%2BNREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es%2BjuQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ%2FrWQ5J%2F9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A%2FzMOQtoD%3C%2FX509Certificate%3E%3C%2FX509Data%3E%3C%2FKeyInfo%3E%3C%2FSignature%3E%3CSubject%3E%3CNameID+Format%3D%22urn%3Aoasis%3Anames%3Atc%3ASAML%3A2.0%3Anameid-format%3Apersistent%22%3ERrX3SPSxDw6z4KHaKB2V_mnv0G-LbRZdYvo1RQa1L7s%3C%2FNameID%3E%3CSubjectConfirmation+Method%3D%22urn%3Aoasis%3Anames%3Atc%3ASAML%3A2.0%3Acm%3Abearer%22+%2F%3E%3C%2FSubject%3E%3CConditions+NotBefore%3D%222017-04-23T17%3A40%3A36.882Z%22+NotOnOrAfter%3D%222017-04-23T18%3A40%3A36.882Z%22%3E%3CAudienceRestriction%3E%3CAudience%3Espn%3Afe78e0b4-6fe7-47e6-812c-fb75cee266a4%3C%2FAudience%3E%3C%2FAudienceRestriction%3E%3C%2FConditions%3E%3CAttributeStatement%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.microsoft.com%2Fidentity%2Fclaims%2Ftenantid%22%3E%3CAttributeValue%3Eadd29489-7269-41f4-8841-b63c95564420%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.microsoft.com%2Fidentity%2Fclaims%2Fobjectidentifier%22%3E%3CAttributeValue%3Ed1ad9ce7-b322-4221-ab74-1e1011e1bbcb%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2005%2F05%2Fidentity%2Fclaims%2Fname%22%3E%3CAttributeValue%3EUser1%40Cyrano.onmicrosoft.com%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2005%2F05%2Fidentity%2Fclaims%2Fsurname%22%3E%3CAttributeValue%3E1%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2005%2F05%2Fidentity%2Fclaims%2Fgivenname%22%3E%3CAttributeValue%3EUser%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.microsoft.com%2Fidentity%2Fclaims%2Fdisplayname%22%3E%3CAttributeValue%3EUser1%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.microsoft.com%2Fidentity%2Fclaims%2Fidentityprovider%22%3E%3CAttributeValue%3Ehttps%3A%2F%2Fsts.windows.net%2Fadd29489-7269-41f4-8841-b63c95564420%2F%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3CAttribute+Name%3D%22http%3A%2F%2Fschemas.microsoft.com%2Fclaims%2Fauthnmethodsreferences%22%3E%3CAttributeValue%3Ehttp%3A%2F%2Fschemas.microsoft.com%2Fws%2F2008%2F06%2Fidentity%2Fauthenticationmethod%2Fpassword%3C%2FAttributeValue%3E%3C%2FAttribute%3E%3C%2FAttributeStatement%3E%3CAuthnStatement+AuthnInstant%3D%222017-04-23T16%3A16%3A17.270Z%22%3E%3CAuthnContext%3E%3CAuthnContextClassRef%3Eurn%3Aoasis%3Anames%3Atc%3ASAML%3A2.0%3Aac%3Aclasses%3APassword%3C%2FAuthnContextClassRef%3E%3C%2FAuthnContext%3E%3C%2FAuthnStatement%3E%3C%2FAssertion%3E%3C%2Ft%3ARequestedSecurityToken%3E%3Ct%3ARequestedAttachedReference%3E%3CSecurityTokenReference+d3p1%3ATokenType%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2Foasis-wss-saml-token-profile-1.1%23SAMLV2.0%22+xmlns%3Ad3p1%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2Foasis-wss-wssecurity-secext-1.1.xsd%22+xmlns%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2F2004%2F01%2Foasis-200401-wss-wssecurity-secext-1.0.xsd%22%3E%3CKeyIdentifier+ValueType%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2Foasis-wss-saml-token-profile-1.1%23SAMLID%22%3E_710d4516-27ac-4547-816d-3947aeea6edf%3C%2FKeyIdentifier%3E%3C%2FSecurityTokenReference%3E%3C%2Ft%3ARequestedAttachedReference%3E%3Ct%3ARequestedUnattachedReference%3E%3CSecurityTokenReference+d3p1%3ATokenType%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2Foasis-wss-saml-token-profile-1.1%23SAMLV2.0%22+xmlns%3Ad3p1%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2Foasis-wss-wssecurity-secext-1.1.xsd%22+xmlns%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2F2004%2F01%2Foasis-200401-wss-wssecurity-secext-1.0.xsd%22%3E%3CKeyIdentifier+ValueType%3D%22http%3A%2F%2Fdocs.oasis-open.org%2Fwss%2Foasis-wss-saml-token-profile-1.1%23SAMLID%22%3E_710d4516-27ac-4547-816d-3947aeea6edf%3C%2FKeyIdentifier%3E%3C%2FSecurityTokenReference%3E%3C%2Ft%3ARequestedUnattachedReference%3E%3Ct%3ATokenType%3Ehttp%3A%2F%2Fdocs.oasis-open.org%2Fwss%2Foasis-wss-saml-token-profile-1.1%23SAMLV2.0%3C%2Ft%3ATokenType%3E%3Ct%3ARequestType%3Ehttp%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2005%2F02%2Ftrust%2FIssue%3C%2Ft%3ARequestType%3E%3Ct%3AKeyType%3Ehttp%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2005%2F05%2Fidentity%2FNoProofKey%3C%2Ft%3AKeyType%3E%3C%2Ft%3ARequestSecurityTokenResponse%3E".Replace('+', ' ')),
                        Wctx = Uri.UnescapeDataString(@"WsFedOwinState%3DZfCHQBMGl9Nia9P6tbsUq5AFCEu9fGolLxTkikMW-zGMhRMsZb6ofrdCD9uni2PoEuW_1zfJPtZawNSjiy4vIg5o2TJeGiwmKjqM0y3bi4w".Replace('+', ' '))
                    },
                    Xml = WaSignIn_Valid
                };
            }
        }

        #endregion

        #region Token

        public static string Token_Saml2_Valid
        {
            get
            {
                return @"<Assertion ID=""_edc15efd-1117-4bf9-89da-28b1663fb890"" IssueInstant=""2017-04-23T16:16:17.348Z"" Version=""2.0"" xmlns=""urn:oasis:names:tc:SAML:2.0:assertion""><Issuer>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</Issuer><Signature xmlns=""http://www.w3.org/2000/09/xmldsig#""><SignedInfo><CanonicalizationMethod Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" /><SignatureMethod Algorithm=""http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"" /><Reference URI=""#_edc15efd-1117-4bf9-89da-28b1663fb890""><Transforms><Transform Algorithm=""http://www.w3.org/2000/09/xmldsig#enveloped-signature"" /><Transform Algorithm=""http://www.w3.org/2001/10/xml-exc-c14n#"" /></Transforms><DigestMethod Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256"" /><DigestValue>DO8QQoO629ApWPV3LiY2epQSv+I82iChybeRrXbhgtw=</DigestValue></Reference></SignedInfo><SignatureValue>O8JNyVKm9I7kMqlsaBgLCNwHA0qdXv34YHBVfg217lgeKkMC5taLU/EH7UeeMtapU6zMafcYoCH+Bp9zoqDpflgs78Hkjgn/dEUtjPFn7211VXClcTNqk+yhqXWtu6SKrabeIhKCKtoMA9lUAB4D6ABesb6MpwbM/ULq7T16tycZ3X//iXHeOiMwNiUAePYF22fmgrqRSDRHyLPtiLskP4UMksWJBrXUV96e9EU9aEciCvYpzMDv/VFUOCLiEkBqCdAtPVwVun+5eRk9zEh6qscWi0kAgFl3W3JhugcTTuGQYHXYVIHxbd5O33MwFIMUOmGrI1EXuk+cHIq2KUtSLg==</SignatureValue><KeyInfo><X509Data><X509Certificate>MIIDBTCCAe2gAwIBAgIQY4RNIR0dX6dBZggnkhCRoDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE3MDIxMzAwMDAwMFoXDTE5MDIxNDAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMBEizU1OJms31S/ry7iav/IICYVtQ2MRPhHhYknHImtU03sgVk1Xxub4GD7R15i9UWIGbzYSGKaUtGU9lP55wrfLpDjQjEgaXi4fE6mcZBwa9qc22is23B6R67KMcVyxyDWei+IP3sKmCcMX7Ibsg+ubZUpvKGxXZ27YgqFTPqCT2znD7K81YKfy+SVg3uW6epW114yZzClTQlarptYuE2mujxjZtx7ZUlwc9AhVi8CeiLwGO1wzTmpd/uctpner6oc335rvdJikNmc1cFKCK+2irew1bgUJHuN+LJA0y5iVXKvojiKZ2Ii7QKXn19Ssg1FoJ3x2NWA06wc0CnruLsCAwEAAaMhMB8wHQYDVR0OBBYEFDAr/HCMaGqmcDJa5oualVdWAEBEMA0GCSqGSIb3DQEBCwUAA4IBAQAiUke5mA86R/X4visjceUlv5jVzCn/SIq6Gm9/wCqtSxYvifRXxwNpQTOyvHhrY/IJLRUp2g9/fDELYd65t9Dp+N8SznhfB6/Cl7P7FRo99rIlj/q7JXa8UB/vLJPDlr+NREvAkMwUs1sDhL3kSuNBoxrbLC5Jo4es+juQLXd9HcRraE4U3UZVhUS2xqjFOfaGsCbJEqqkjihssruofaxdKT1CPzPMANfREFJznNzkpJt4H0aMDgVzq69NxZ7t1JiIuc43xRjeiixQMRGMi1mAB75fTyfFJ/rWQ5J/9kh0HMZVtHsqICBF1tHMTMIK5rwoweY0cuCIpN7A/zMOQtoD</X509Certificate></X509Data></KeyInfo></Signature><Subject><NameID Format=""urn:oasis:names:tc:SAML:2.0:nameid-format:persistent"">RrX3SPSxDw6z4KHaKB2V_mnv0G-LbRZdYvo1RQa1L7s</NameID><SubjectConfirmation Method=""urn:oasis:names:tc:SAML:2.0:cm:bearer"" /></Subject><Conditions NotBefore=""2017-04-23T16:11:17.348Z"" NotOnOrAfter=""2017-04-23T17:11:17.348Z""><AudienceRestriction><Audience>spn:fe78e0b4-6fe7-47e6-812c-fb75cee266a4</Audience></AudienceRestriction></Conditions><AttributeStatement><Attribute Name=""http://schemas.microsoft.com/identity/claims/tenantid""><AttributeValue>add29489-7269-41f4-8841-b63c95564420</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/identity/claims/objectidentifier""><AttributeValue>d1ad9ce7-b322-4221-ab74-1e1011e1bbcb</AttributeValue></Attribute><Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name""><AttributeValue>User1@Cyrano.onmicrosoft.com</AttributeValue></Attribute><Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname""><AttributeValue>1</AttributeValue></Attribute><Attribute Name=""http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname""><AttributeValue>User</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/identity/claims/displayname""><AttributeValue>User1</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/identity/claims/identityprovider""><AttributeValue>https://sts.windows.net/add29489-7269-41f4-8841-b63c95564420/</AttributeValue></Attribute><Attribute Name=""http://schemas.microsoft.com/claims/authnmethodsreferences""><AttributeValue>http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/password</AttributeValue></Attribute></AttributeStatement><AuthnStatement AuthnInstant=""2017-04-23T16:16:17.270Z""><AuthnContext><AuthnContextClassRef>urn:oasis:names:tc:SAML:2.0:ac:classes:Password</AuthnContextClassRef></AuthnContext></AuthnStatement></Assertion>";
            }
        }

        #endregion

    }
}
