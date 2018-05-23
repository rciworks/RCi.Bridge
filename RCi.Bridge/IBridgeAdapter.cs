namespace RCi.Bridge
{
    /// <summary>
    /// Adapter providing value for <see cref="Bridge{TValue}"/>.
    /// </summary>
    public interface IBridgeAdapter
    {
        /// <summary>
        /// Links adapter value to <see cref="Bridge{TValue}"/>.
        /// </summary>
        void LinkToBridge();
    }

    /// <inheritdoc />
    public interface IBridgeAdapter<out TValue> :
        IBridgeAdapter
    {
        /// <summary>
        /// Getter for instance which will be linked to <see cref="Bridge{TValue}"/>.
        /// </summary>
        TValue BridgeValue { get; }
    }
}
