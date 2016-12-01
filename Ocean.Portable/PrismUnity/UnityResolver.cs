namespace Ocean.Portable.PrismUnity {
    using System;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// Wrapper for the Unity container instance, typically passed in a constructor argument to denote intent that the Resolved class will be used in the type.
    /// </summary>
    /// <typeparam name="T">The type the Resolve method will return</typeparam>
    public class UnityResolver<T> : IResolver<T> {

        readonly IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityResolver{T}"/> class.
        /// </summary>
        /// <param name="container">The Unity container.</param>
        public UnityResolver(IUnityContainer container) {
            if (container == null) {
                throw new ArgumentNullException(nameof(container));
            }
            _container = container;
        }

        /// <summary>
        /// Using the container, resolves the requested instance.
        /// </summary>
        /// <returns>An instance of T</returns>
        public T Resolve() {
            return _container.Resolve<T>();
        }
    }
}
