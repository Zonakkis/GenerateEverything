using System;

namespace GenerateEverything.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class GetFieldInfo : Attribute
    {
        public Type Type { get; }


        public GetFieldInfo(Type type)
        {
            Type = type;
        }
    }
}
