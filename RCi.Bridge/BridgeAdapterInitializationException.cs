using System;
using System.Runtime.Serialization;

namespace RCi.Bridge
{
    /// <inheritdoc />
    public class BridgeAdapterInitializationException :
        Exception
    {
        /// <inheritdoc />
        public BridgeAdapterInitializationException()
        {
        }

        /// <inheritdoc />
        public BridgeAdapterInitializationException(string message) :
            base(message)
        {
        }

        /// <inheritdoc />
        public BridgeAdapterInitializationException(string message, Exception innerException) :
            base(message, innerException)
        {
        }

        /// <inheritdoc />
        public BridgeAdapterInitializationException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }
    }
}
