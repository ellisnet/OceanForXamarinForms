namespace Ocean.Portable.BusinessObject {
    /// <summary>
    /// Provides contract to short circuit rule processing and change notification events while an object is being loaded.
    /// </summary>
    public interface ILoadable {
        /// <summary>
        /// Called when the business Object is being loaded from a database.  This saves time and processing; by passing property setter logic during loading.  After the business Object has been loaded the EndLoading MUST be called.
        /// </summary>
        void BeginLoading();

        /// <summary>
        /// After a business Object has been loaded and the BeginLoading method was called, developers must call this method, EndLoading.  This method marks the entity IsDirty = False, HasBeenValidated = False and raises these property changed events.
        /// </summary>
        void EndLoading();
    }
}