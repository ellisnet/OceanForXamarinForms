namespace Ocean.Portable.PrismUnity {
    /// <summary>
    /// Defines a Resolve method for implementing classes
    /// </summary>
    /// <typeparam name="T">The type the Resolve method will return</typeparam>
    public interface IResolver<out T> {
        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <returns>An instance of T</returns>
        T Resolve();
    }
}
