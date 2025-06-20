using System;

namespace GenerateEverything.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class GetPropertyInfo : Attribute
    {
        public Type Type { get; }

        //public string? FieldName { get; set; }

        public GetPropertyInfo(Type type) => Type = type;
        //public GetFieldInfo(string fieldName) => FieldName = fieldName;
    }
}
