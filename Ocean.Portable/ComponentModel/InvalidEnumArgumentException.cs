namespace Ocean.Portable.ComponentModel {
    using System;

    public class InvalidEnumArgumentException : ArgumentException {

        public String ArgumentName { get; }

        public Type EnumClass { get; }

        public Int32 InvalidValue { get; }

        public InvalidEnumArgumentException(String argumentName, Int32 invalidValue, Type enumClass)
            : base("Invalid enum argument", argumentName) {
            this.ArgumentName = argumentName;
            this.InvalidValue = invalidValue;
            this.EnumClass = enumClass;
        }

        public override string ToString() {
            return $"{base.Message}, Argument Name {this.ArgumentName}, Enum Class {this.EnumClass.Name}, Invalid Value {this.InvalidValue}, Stack Trace {base.StackTrace}";
        }

    }
}
