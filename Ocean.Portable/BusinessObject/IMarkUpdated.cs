namespace Ocean.Portable.BusinessObject {
    /// <summary>
    /// Contact to mark objects as updated. Typically invoked by the generic layers after a successful update or insert.
    /// </summary>
    public interface IMarkUpdated {
        /// <summary>
        /// Called by the generic layers after a successful update or insert.
        /// </summary>
        void Updated();
    }
}