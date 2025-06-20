using System;

namespace GenerateEverything.Attributes
{
    [AttributeUsage(AttributeTargets.Field
        | AttributeTargets.Property
        | AttributeTargets.Method
        | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class GetEnumInfo : Attribute
    {
        public Type Type { get; }

        public GetEnumInfo(Type type) => Type = type;
    }
}
