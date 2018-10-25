namespace RCi.Bridge
{
    /// <summary>
    /// Global storage for any type of value.
    /// </summary>
    public static class Bridge<TValue>
    {
        /// <summary>
        /// Static constructor. This will be called with each combination of generic types.
        /// </summary>
        static Bridge()
        {
            BridgeHook.EnsureHook();
        }

        /// <summary>
        /// Flag for marking that value is already stored.
        /// </summary>
        /// <remarks>
        /// The static flag will be allocated in each class compiled with different type of <typeparamref name="TValue"/>.
        /// </remarks>
        // ReSharper disable once StaticMemberInGenericType
        public static bool Initialized { get; private set; }

        /// <summary>
        /// Storage.
        /// </summary>
        public static TValue Value { get; private set; }

        /// <summary>
        /// Link value.
        /// </summary>
        public static void Link(TValue value)
        {
            // ensure it's not set yet
            if (Initialized)
            {
                throw new BridgeLinkException(string.Format(
                    "Cannot link value of type '{0}'. Link is already established.", typeof(TValue)));
            }

            // create link (store value)
            Value = value;

            // mark that it is initialized
            Initialized = true;
        }
    }
}
