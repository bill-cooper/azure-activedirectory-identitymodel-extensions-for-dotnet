﻿//------------------------------------------------------------------------------
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
using System.IO;
using System.Xml;
using Microsoft.IdentityModel.Tests;
using Xunit;

namespace Microsoft.IdentityModel.Tokens.Saml.Tests
{
    public class SamlSerializerTests
    {
#pragma warning disable CS3016 // Arrays as attribute arguments is not CLS-compliant
        [Theory, MemberData("ActionReadFromTheoryData")]
#pragma warning restore CS3016 // Arrays as attribute arguments is not CLS-compliant
        public void ReadAction(SamlTheoryData theoryData)
        {
            TestUtilities.WriteHeader($"{this}.ReadAction", theoryData);
            var context = new CompareContext($"{this}.QueryStringTest, {theoryData.TestId}");
            try
            {
                var sr = new StringReader(theoryData.ActionTestSet.Xml);
                var reader = XmlDictionaryReader.CreateDictionaryReader(XmlReader.Create(sr));
                var action = (theoryData.SamlSerializer as SamlSerializerPublic).ReadActionPublic(reader);
                theoryData.ExpectedException.ProcessNoException();

                IdentityComparer.AreEqual(action, theoryData.ActionTestSet.Action, context);
            }
            catch (Exception ex)
            {
                theoryData.ExpectedException.ProcessException(ex);
            }

            TestUtilities.AssertFailIfErrors(context);
        }

        public static TheoryData<SamlTheoryData> ActionReadFromTheoryData
        {
            get
            {
                return new TheoryData<SamlTheoryData>
                {
                    new SamlTheoryData
                    {
                        ExpectedException = new ExpectedException(typeof(ArgumentNullException), "IDX10000:"),
                        First = true,
                        SamlSerializer = new SamlSerializerPublic(),
                        ActionTestSet = ReferenceXml.SamlActionMissValue,
                        TestId = nameof(ReferenceXml.SamlActionMissValue)
                    },
                    new SamlTheoryData
                    {
                        ExpectedException = ExpectedException.NoExceptionExpected,
                        SamlSerializer = new SamlSerializerPublic(),
                        ActionTestSet = ReferenceXml.SamlActionNoNamespace,
                        TestId = nameof(ReferenceXml.SamlActionNoNamespace)
                    },
                    new SamlTheoryData
                    {
                        ExpectedException = new ExpectedException(typeof(SamlSecurityTokenException), "IDX11502:"),
                        SamlSerializer = new SamlSerializerPublic(),
                        ActionTestSet = ReferenceXml.SamlActionInvalidNamespace,
                        TestId = nameof(ReferenceXml.SamlActionInvalidNamespace)
                    },
                    new SamlTheoryData
                    {
                        ExpectedException = ExpectedException.NoExceptionExpected,
                        SamlSerializer = new SamlSerializerPublic(),
                        ActionTestSet = ReferenceXml.SamlActionValid,
                        TestId = nameof(ReferenceXml.SamlActionValid)
                    }
                };
            }
        }

        private class SamlSerializerPublic : SamlSerializer
        {
            public SamlAction ReadActionPublic(XmlDictionaryReader reader)
            {
                return base.ReadAction(reader);
            }
        }

    }
}
