namespace RCi.Bridge
{
    /// <inheritdoc />
    public abstract class BridgeAdapter :
        IBridgeAdapter
    {
        /// <inheritdoc />
        public abstract void LinkToBridge();
    }

    /// <inheritdoc cref="BridgeAdapter" />
    public abstract class BridgeAdapter<TValue> :
        BridgeAdapter,
        IBridgeAdapter<TValue>
    {
        /// <inheritdoc />
        public abstract TValue BridgeValue { get; }

        /// <inheritdoc />
        public sealed override void LinkToBridge()
        {
            Bridge<TValue>.Link(BridgeValue);
        }
    }
}
