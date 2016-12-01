namespace Ocean.Portable.ComponentModel {
    using System;

    public interface IDataErrorInfo {

        String Error { get; }

        String this[String propertyName] { get; }

    }
}
