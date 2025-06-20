using System;

namespace GenerateEverything.Attributes
{
    [AttributeUsage(AttributeTargets.Field 
        | AttributeTargets.Property
        | AttributeTargets.Method
        | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class GetClassInfo : Attribute
    {
        public Type Type { get; }

        public GetClassInfo(Type type) => Type = type;
    }
}
