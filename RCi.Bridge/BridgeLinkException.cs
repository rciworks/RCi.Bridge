using System;
using System.Runtime.Serialization;

namespace RCi.Bridge
{
    /// <inheritdoc />
    public class BridgeLinkException :
        Exception
    {
        /// <inheritdoc />
        public BridgeLinkException()
        {
        }

        /// <inheritdoc />
        public BridgeLinkException(string message) :
            base(message)
        {
        }

        /// <inheritdoc />
        public BridgeLinkException(string message, Exception innerException) :
            base(message, innerException)
        {
        }

        /// <inheritdoc />
        public BridgeLinkException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
    }
}
