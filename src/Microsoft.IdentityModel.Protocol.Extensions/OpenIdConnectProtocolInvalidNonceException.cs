// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Runtime.Serialization;

namespace Microsoft.IdentityModel.Protocols
{
    /// <summary>
    /// This exception is thrown when an OpenIdConnect protocol handler encounters an invalid nonce.
    /// </summary>
    [Serializable]
    public class OpenIdConnectProtocolInvalidNonceException : OpenIdConnectProtocolException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenIdConnectProtocolInvalidNonceException"/> class.
        /// </summary>
        public OpenIdConnectProtocolInvalidNonceException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenIdConnectProtocolInvalidNonceException"/> class.
        /// </summary>
        /// <param name="message">Addtional information to be included in the exception and displayed to user.</param>
        public OpenIdConnectProtocolInvalidNonceException(String message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenIdConnectProtocolInvalidNonceException"/> class.
        /// </summary>
        /// <param name="message">Addtional information to be included in the exception and displayed to user.</param>
        /// <param name="innerException">A <see cref="Exception"/> that represents the root cause of the exception.</param>
        public OpenIdConnectProtocolInvalidNonceException(String message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenIdConnectProtocolInvalidNonceException"/> class.
        /// </summary>
        /// <param name="info">the <see cref="SerializationInfo"/> that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        protected OpenIdConnectProtocolInvalidNonceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}