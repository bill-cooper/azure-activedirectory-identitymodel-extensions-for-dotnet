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
using System.Collections.Specialized;
using Microsoft.IdentityModel.Tests;
using Xunit;

namespace Microsoft.IdentityModel.Protocols.WsFederation.Tests
{
    /// <summary>
    /// 
    /// </summary>
    public class WsFederationMessageTests
    {
#pragma warning disable CS3016 // Arrays as attribute arguments is not CLS-compliant
        [Theory, MemberData("MessageTheoryData")]
#pragma warning restore CS3016 // Arrays as attribute arguments is not CLS-compliant
        public void ConstructorTest(WsFederationMessageTheoryData theoryData)
        {
            TestUtilities.WriteHeader($"{this}.ConstructorTest", theoryData);
            try
            {
                // check default constructor
                WsFederationMessage wsFederationMessage1 = new WsFederationMessage
                {
                    IssuerAddress = theoryData.IssuerAddress,
                    Wreply = theoryData.Wreply,
                    Wct = theoryData.Wct
                };

                Assert.Equal(theoryData.IssuerAddress, wsFederationMessage1.IssuerAddress);
                Assert.Equal(theoryData.Wreply, wsFederationMessage1.Wreply);
                Assert.Equal(theoryData.Wct, wsFederationMessage1.Wct);

                // check copy constructor
                WsFederationMessage wsFederationMessage2 = new WsFederationMessage(wsFederationMessage1);

                Assert.Equal(theoryData.IssuerAddress, wsFederationMessage2.IssuerAddress);
                Assert.Equal(theoryData.Wreply, wsFederationMessage2.Wreply);
                Assert.Equal(theoryData.Wct, wsFederationMessage2.Wct);

                theoryData.ExpectedException.ProcessNoException();
            }
            catch (Exception ex)
            {
                theoryData.ExpectedException.ProcessException(ex);
            }
        }

#pragma warning disable CS3016 // Arrays as attribute arguments is not CLS-compliant
        [Theory, MemberData("MessageTheoryData")]
#pragma warning restore CS3016 // Arrays as attribute arguments is not CLS-compliant
        public void ParametersTest(WsFederationMessageTheoryData theoryData)
        {
            TestUtilities.WriteHeader($"{this}.ParametersTest", theoryData);
            try
            {
                WsFederationMessage wsFederationMessage = new WsFederationMessage
                {
                    IssuerAddress = theoryData.IssuerAddress,
                    Wreply = theoryData.Wreply,
                    Wct = theoryData.Wct
                };

                Assert.Equal(theoryData.IssuerAddress, wsFederationMessage.IssuerAddress);
                Assert.Equal(theoryData.Wreply, wsFederationMessage.Wreply);
                Assert.Equal(theoryData.Wct, wsFederationMessage.Wct);

                // add parameter
                wsFederationMessage.SetParameter(theoryData.Parameter1.Key, theoryData.Parameter1.Value);

                // add parameters
                var nameValueCollection = new NameValueCollection();
                nameValueCollection.Add(theoryData.Parameter2.Key, theoryData.Parameter2.Value);
                nameValueCollection.Add(theoryData.Parameter3.Key, theoryData.Parameter3.Value);
                wsFederationMessage.SetParameters(nameValueCollection);

                // validate the parameters are added
                Assert.Equal(theoryData.Parameter1.Value, wsFederationMessage.Parameters[theoryData.Parameter1.Key]);
                Assert.Equal(theoryData.Parameter2.Value, wsFederationMessage.Parameters[theoryData.Parameter2.Key]);
                Assert.Equal(theoryData.Parameter3.Value, wsFederationMessage.Parameters[theoryData.Parameter3.Key]);

                // remove parameter
                wsFederationMessage.SetParameter(theoryData.Parameter1.Key, null);

                // validate the parameter is removed
                Assert.Equal(false, wsFederationMessage.Parameters.ContainsKey(theoryData.Parameter1.Key));

                // create redirectUri
                var uriString = wsFederationMessage.BuildRedirectUrl();
                Uri uri = new Uri(uriString);

                // convert query back to WsFederationMessage
                WsFederationMessage wsFederationMessageReturned = WsFederationMessage.FromQueryString(uri.Query);

                // validate the parameters in the returned wsFederationMessage
                Assert.Equal(theoryData.Parameter2.Value, wsFederationMessageReturned.Parameters[theoryData.Parameter2.Key]);
                Assert.Equal(theoryData.Parameter3.Value, wsFederationMessageReturned.Parameters[theoryData.Parameter3.Key]);

                theoryData.ExpectedException.ProcessNoException();
            }
            catch (Exception ex)
            {
                theoryData.ExpectedException.ProcessException(ex);
            }
        }

        public static TheoryData<WsFederationMessageTheoryData> MessageTheoryData
        {
            get
            {
                var theoryData = new TheoryData<WsFederationMessageTheoryData>();

                theoryData.Add(
                    new WsFederationMessageTheoryData
                    {
                        First = true,
                        IssuerAddress = @"http://www.gotjwt.com",
                        Parameter1 = new KeyValuePair<string, string>("bob", "123"),
                        Parameter2 = new KeyValuePair<string, string>("tom", "456"),
                        Parameter3 = new KeyValuePair<string, string>("jerry", "789"),
                        Wct = Guid.NewGuid().ToString(),
                        Wreply = @"http://www.relyingparty.com", 
                        TestId = "WsFederationMessage test"
                    });

                return theoryData;
            }
        }

        public class WsFederationMessageTheoryData : TheoryDataBase
        {
            public string IssuerAddress { get; set; }

            public KeyValuePair<string, string> Parameter1 { get; set; }

            public KeyValuePair<string, string> Parameter2 { get; set; }

            public KeyValuePair<string, string> Parameter3 { get; set; }

            public string Wa { get; set; }

            public string Wauth { get; set; }

            public string Wct { get; set; }

            public string Wctx { get; set; }

            public string Wencoding { get; set; }

            public string Wfed { get; set; }

            public string Wfresh { get; set; }

            public string Whr { get; set; }

            public string Wp { get; set; }

            public string Wpseudo { get; set; }

            public string Wpseudoptr { get; set; }

            public string Wreply { get; set; }

            public string Wreq { get; set; }

            public string Wreqptr { get; set; }

            public string Wres { get; set; }

            public string Wresult { get; set; }

            public string Wresultptr { get; set; }

            public string Wtrealm { get; set; }
        }
    }
}
